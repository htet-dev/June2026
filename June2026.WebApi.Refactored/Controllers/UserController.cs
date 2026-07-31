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
    public UserController()
    {
        _userService = new UserService();
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        var model = _userService.GetUsers(new UserListRequestModel());

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
    public IActionResult GetUser(int id)
    {
        var model = _userService.GetUser(new UserEditRequestModel { UserId = id });

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
    public IActionResult CreateUser([FromBody] UserCreateRequestModel requestModel)
    {
        var model = _userService.CreateUser(requestModel);

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
    public IActionResult PatchUser(int id, UserPatchRequestModel requestModel)
    {
        var model = _userService.PatchUser(new UserPatchRequestModel  
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
    public IActionResult DeleteUser(UserDeleteRequestModel requestModel)
    {
        var model = _userService.DeleteUser(requestModel);

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