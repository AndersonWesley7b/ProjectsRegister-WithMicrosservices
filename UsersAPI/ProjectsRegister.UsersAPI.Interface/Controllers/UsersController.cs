﻿using Microsoft.AspNetCore.Mvc;
using ProjectsRegister.UsersAPI.Crosscutting.DTOS;
using ProjectsRegister.UsersAPI.Services.ApplicationServices.IApplicationServices;

namespace UsersRegister.UsersAPI.Interface.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{

	private readonly IUsersApplicationServices _usersApplicationServices;

    public UsersController(IUsersApplicationServices usersApplicationServices)
    {
        _usersApplicationServices = usersApplicationServices;
    }

	[HttpGet("GetUsersForSelect")]
    public async Task<IActionResult> GetUsersForSelect()
    {
		try
		{
			List<SelectDTO> Users = await _usersApplicationServices.GetUsersForSelect();
			return Ok(Users);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
    }

	[HttpPost("CreateNewUser")]
	public async Task<IActionResult> CreateNewUser(CreateUserDTO NewUser)
	{
		try
		{
			await _usersApplicationServices.CreateNewUser(NewUser, true);
			return Ok();
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}

    [HttpGet("CheckUserExists/{UserId}")]
    public async Task<IActionResult> CheckUserExists(Guid UserId)
    {
        try
        {
            await _usersApplicationServices.CheckUserExists(UserId);
            return Ok();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}