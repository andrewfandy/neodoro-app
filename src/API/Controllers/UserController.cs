using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/test")]
public class UserController  : ControllerBase
{
    private readonly UserRepository _user;
    public UserController(UserRepository user)
    {
        _user = user;
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _user.GetItemAsync(1);

        return user != null ? Ok(new {data = user}) : Ok(new {message = "No"});
    }
    
    // TODO: Authentication
}