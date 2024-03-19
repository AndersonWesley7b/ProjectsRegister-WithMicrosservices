using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectsRegister.ProjectsAPI.Crosscutting.DTOS;
using ProjectsRegister.ProjectsAPI.Services.ApplicationServices.IApplicationServices;

namespace ProjectsRegister.ProjectsAPI.Interface.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{

	private readonly IProjectsApplicationServices _projectsApplicationServices;

    public ProjectsController(IProjectsApplicationServices projectsApplicationServices)
    {
        _projectsApplicationServices = projectsApplicationServices;
    }

	[HttpGet("GetAllProjects")]
    public async Task<IActionResult> GetAllProjets()
    {
		try
		{
			List<ResumedProjectDTO> Projects = await _projectsApplicationServices.GetAllProjects();
			return Ok(Projects);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
    }

	[HttpPost("CreateNewProject")]
	public async Task<IActionResult> CreateNewProject(CreateProjectDTO NewProject)
	{
		try
		{
			await _projectsApplicationServices.CreateNewProject(NewProject, true);
			return Ok();
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}
	
	[HttpPut("UpdateProject")]
	public async Task<IActionResult> UpdateProject(FullProjectDTO UpdateProject)
	{
		try
		{
			await _projectsApplicationServices.UpdateProject(UpdateProject, true);
			return Ok();
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}

}
