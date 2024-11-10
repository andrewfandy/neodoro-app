using API.Models.DTOs;
using API.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController  : ControllerBase
{
    private readonly UserService _service;

    /* 
     * TODO:
     * - CREATE SERVICE LAYER FOR AUTHENTICATION
     */
    public UserController(UserService service)
    {
        _service = service;
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var result = await _service.GetUserByIdAsync(id);
        var response = new
        {
            data = result.Data,
            message = result.Message,
            status = result.IsSuccess
        };
        if (!result.IsSuccess) return BadRequest(response);
        
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await _service.GetAllUsers();
        var response = new
        {
            data = result.Data,
            message = result.Message,
            status = result.IsSuccess
        };

        if (!result.IsSuccess) return BadRequest(response);
        return Ok(response);
    }

    // [HttpDelete()]
    // public async Task<IActionResult> DeleteUser(UserDetailDto model)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }
    //     var result = await _service.DeleteUser(model);
    //     var response = new
    //     {
    //         status = result.IsSuccess,
    //         message = result.Message
    //     };
    //     if (!result.IsSuccess) return BadRequest(response);
    //
    //     return Ok(response);
    // }

    [HttpPut]
    public async Task<IActionResult> Edit(UserDetailDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _service.EditUserAsync(model);
        if (!result.IsSuccess) return BadRequest(new { Message = result.Message });
        
        
        return Ok(new { data = result.Data, message = result.Message });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _service.LoginUserAsync(model);
        var response = new
        {
            data = result.Data,
            message = result.Message
        };
        if (!result.IsSuccess) return BadRequest(response);

        return Ok(response);
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _service.RegisterUserAsync(model);
        var response = new
        {
            data = result.Data,
            message = result.Message
        };
        
        if (!result.IsSuccess) return BadRequest(response);

        return CreatedAtAction(nameof(GetUserById), new { Id = result.Data?.Id }, response);

    }
    
}