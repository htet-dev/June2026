using June2026.Database.AppDbContextModels;
using June2026.Domain.Features.User;
using June2026.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace June2026.WebApi.Controllers;

// api/user
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersAsync()
    {
        var model = await _userService.GetUsersAsync(new UserListRequestModel());

        if(model.IsSuccess)
        {
            return Ok(model);
        }
        else
        {
            return BadRequest(model);
        }
    }

    // api/user/edit/1
    // api/user/1
    [HttpGet("Edit/{id}")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserAsync(int id)
    {
        var model = await _userService.GetUserAsync(new UserEditRequestModel { UserId = id });

        if(model.IsSuccess)
        {
            return Ok(model);
        }
        else
        {
            return BadRequest(model);
        }        
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserCreateRequestModel requestModel)
    {
        var model = await _userService.CreateUserAsync(requestModel);

        if (model.IsSuccess)
        {
            return Ok(model);
        }
        else
        {
            return BadRequest(model);
        }
    }    

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchUserAsync(int id, UserPatchRequestModel requestModel)
    {
        var model = await _userService.PatchUserAsync(new UserPatchRequestModel  
        { 
           UserId = id,
           Username = requestModel.Username,
           Password = requestModel.Password           
        });

        if (model.IsSuccess)
        {
            return Ok(model);
        }
        else
        {
            return BadRequest(model);
        }
    }

    [HttpDelete("{UserId}")]
    public async Task<IActionResult> DeleteUserAsync(UserDeleteRequestModel requestModel)
    {
        var model = await _userService.DeleteUserAsync(requestModel);

        if (model.IsSuccess)
        {
            return Ok(model);
        }
        else
        {
            return BadRequest(model);
        }
    }  
}