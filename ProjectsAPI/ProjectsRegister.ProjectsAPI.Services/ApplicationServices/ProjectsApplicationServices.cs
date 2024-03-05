using Microsoft.EntityFrameworkCore;
using ProjectsRegister.ProjectsAPI.Crosscutting.DTOS;
using ProjectsRegister.ProjectsAPI.Domain.Entities;
using ProjectsRegister.ProjectsAPI.Infrastructure.Repositories.IRepositories;
using ProjectsRegister.ProjectsAPI.Services.ApplicationServices.IApplicationServices;

namespace ProjectsRegister.ProjectsAPI.Services.ApplicationServices;
public sealed class ProjectsApplicationServices : BaseApplicationServices, IProjectsApplicationServices 
{
    private readonly IProjectsRepository _projectRepository;
    private readonly IUsersApplicationServices _usersApplicationServices;

    public ProjectsApplicationServices(IProjectsRepository projectRepository, IUsersApplicationServices usersApplicationServices)
    {
        _projectRepository = projectRepository;
        _usersApplicationServices = usersApplicationServices;
    }

    #region Queries

    private IQueryable<ResumedProjectDTO> GetAllProjectsQuery()
    {
        IQueryable<Project?> queryProjects = _projectRepository.GetAllProjectsReadOnly();

        IQueryable<ResumedProjectDTO> query = (from Project in queryProjects
                                               select new ResumedProjectDTO
                                               {
                                                   ProjectId = Project.ProjectId,
                                                   Name = Project.Name,
                                                   UserId = Project.UserId,
                                               });

        return query;

    }

    #endregion

    #region DatabaseOperation

    public async Task<List<ResumedProjectDTO>> GetAllProjects()
    {
        List<ResumedProjectDTO> projects = await GetAllProjectsQuery().ToListAsync();
        return projects;
    }

    public async Task CreateNewProject(CreateProjectDTO _NewProject, bool _Commit = false)
    {
        NewProjectValidate(_NewProject);
        _usersApplicationServices.CheckUserExists(_NewProject.UserId);

        Project project = new()
        { 
            Name = _NewProject.Name,
            UserId = _NewProject.UserId,
            CreatedOn = DateTime.UtcNow,
            Description = _NewProject.Description,
            ProjectLink = _NewProject.ProjectLink,
            RepositoryLink = _NewProject.RepositoryLink,
        };

        ValidateModel(project);

        await _projectRepository.AddProject(project);

        if (_Commit)
            await _projectRepository.CommitChanges();
    }

    #endregion

    #region Validations
    private static void NewProjectValidate(CreateProjectDTO _NewProject)
    {
        if (Equals(_NewProject, null))
            throw new Exception("Preencha os dados do projeto corretamente, para realizar um novo cadastro");
    }

    #endregion

}
