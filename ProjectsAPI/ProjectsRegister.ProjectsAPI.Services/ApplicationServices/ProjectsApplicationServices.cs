using Microsoft.EntityFrameworkCore;
using ProjectsRegister.ProjectsAPI.Crosscutting.DTOS;
using ProjectsRegister.ProjectsAPI.Crosscutting.DTOS.Interfaces;
using ProjectsRegister.ProjectsAPI.Domain.Entities;
using ProjectsRegister.ProjectsAPI.Infrastructure.UnitOfWork.IUnitOfWork;
using ProjectsRegister.ProjectsAPI.Services.ApplicationServices.IApplicationServices;

namespace ProjectsRegister.ProjectsAPI.Services.ApplicationServices;
public sealed class ProjectsApplicationServices : BaseApplicationServices, IProjectsApplicationServices 
{
    private readonly IProjectsUnitOfWork _uow;
    private readonly IUsersApplicationServices _usersApplicationServices;

    public ProjectsApplicationServices(IProjectsUnitOfWork uow, IUsersApplicationServices usersApplicationServices)
    {
        _uow = uow;
        _usersApplicationServices = usersApplicationServices;
    }

    #region Queries

    private IQueryable<ResumedProjectDTO> GetAllProjectsQuery()
    {
        IQueryable<Project?> queryProjects = _uow.ProjectRepository.GetAllProjectsReadOnly();

        IQueryable<ResumedProjectDTO> query = (from Project in queryProjects
                                               select new ResumedProjectDTO
                                               {
                                                   ProjectId = Project.ProjectId,
                                                   Name = Project.Name,
                                                   UserId = Project.UserId,
                                                   Description = Project.Description
                                               });

        return query;

    }

    #endregion

    #region DatabaseOperation

    public async Task<List<ResumedProjectDTO>> GetAllProjects()
    {
        var projects = await GetAllProjectsQuery().ToListAsync();
        return projects;
    }

    public async Task CreateNewProject(CreateProjectDTO _NewProject, bool _Commit = false)
    {
        await ProjectValidate(_NewProject);

        Project Project = new()
        {
            Name = _NewProject.Name,
            CreatedOn = DateTime.UtcNow,
            Description = _NewProject.Description,
            ProjectLink = _NewProject.ProjectLink,
            RepositoryLink = _NewProject.RepositoryLink,
            UserId = _NewProject.UserId,
        };

        ValidateModel(Project);

        await _uow.ProjectRepository.AddProject(Project);

        if (_Commit)
            await _uow.SaveChangesAsync();
    }

    public async Task UpdateProject(FullProjectDTO _UpdateProject, bool _Commit = false)
    {
        if (_UpdateProject == null)
            throw new ArgumentNullException(nameof(_UpdateProject), "O objeto de usuário para atualização não pode ser nulo.");

        Project projectToUpdate = await _uow.ProjectRepository.GetProjectById(_UpdateProject.ProjectId) ?? throw new NullReferenceException("Selecione um projeto válido para realizar o update!");

        await ProjectValidate(_UpdateProject);

        projectToUpdate.Name = _UpdateProject.Name;
        projectToUpdate.Description = _UpdateProject.Description;
        projectToUpdate.ProjectLink = _UpdateProject.ProjectLink;
        projectToUpdate.RepositoryLink = _UpdateProject.RepositoryLink;
        projectToUpdate.UserId = _UpdateProject.UserId;
        projectToUpdate.LastUpdatedOn = DateTime.UtcNow;
        ValidateModel(projectToUpdate);

        if (_Commit)
            await _uow.ProjectRepository.CommitChanges();
    }

    #endregion

    #region Validations
    private async Task ProjectValidate(IProjectDTO Project)
    {
        await _usersApplicationServices.CheckUserExists(Project.UserId);

        if (Equals(Project, null))
            throw new Exception("Informe os dados do seu projeto!");
    }

    #endregion

}
