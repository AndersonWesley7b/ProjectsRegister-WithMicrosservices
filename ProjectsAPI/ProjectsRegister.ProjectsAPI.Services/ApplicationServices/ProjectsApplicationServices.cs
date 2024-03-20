using Microsoft.EntityFrameworkCore;
using ProjectsRegister.ProjectsAPI.Crosscutting.DTOS;
using ProjectsRegister.ProjectsAPI.Crosscutting.DTOS.Interfaces;
using ProjectsRegister.ProjectsAPI.Domain.Entities;
using ProjectsRegister.ProjectsAPI.Infrastructure.UnitOfWork.IUnitOfWork;
using ProjectsRegister.ProjectsAPI.Services.ApplicationServices.IApplicationServices;
using ProjectsRegister.ProjectsAPI.Services.ConnectedServices.IConnectedServices;

namespace ProjectsRegister.ProjectsAPI.Services.ApplicationServices;
public sealed class ProjectsApplicationServices : BaseApplicationServices, IProjectsApplicationServices
{
    private readonly IProjectsUnitOfWork _uow;
    private readonly IUsersConnectedServices _usersApplicationServices;

    public ProjectsApplicationServices(IProjectsUnitOfWork uow, IUsersConnectedServices usersApplicationServices)
    {
        _uow = uow;
        _usersApplicationServices = usersApplicationServices;
    }

    #region Queries

    private IQueryable<ResumedProjectDTO> GetAllProjectsQuery()
    {
        IQueryable<Project?> queryProjects = _uow.ProjectRepository.GetAllProjectsReadOnly();

        IQueryable<ResumedProjectDTO> query = from Project in queryProjects
                                              select new ResumedProjectDTO
                                              {
                                                  ProjectId = Project.ProjectId,
                                                  Name = Project.Name,
                                                  UserId = Project.UserId,
                                                  Description = Project.Description
                                              };

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
            throw new ArgumentNullException(nameof(_UpdateProject), "O objeto de projeto para atualização não pode ser nulo.");

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
            await _uow.SaveChangesAsync();
    }

    public async Task DeleteProjectsByUserId(Guid _UserId)
    {
        try
        {
            if (!ValidateGuid(_UserId))
                throw new Exception("Informe um usuário válido!");

            List<Project> Projects = await _uow.ProjectRepository.GetProjectsByUserId(_UserId).ToListAsync();

            if(Projects.Count > 0)
                _uow.ProjectRepository.DeleteProjects(Projects);

            await _uow.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteProject(Guid _ProjectId, bool _Commit)
    {
        if (!ValidateGuid(_ProjectId))
            throw new Exception("O Id do projeto informado é inválido!");

        Project projectToDelete = await _uow.ProjectRepository.GetProjectById(_ProjectId) ?? throw new NullReferenceException("Selecione um projeto válido para excluir!");

        _uow.ProjectRepository.DeleteProject(projectToDelete);

        if (_Commit)
            await _uow.SaveChangesAsync();
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
