using Microsoft.AspNetCore.Mvc;
using PlaceRentalApp.Application.Models;
using PlaceRentalApp.Application.Services;

namespace PlaceRentalApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService) 
        => _userService = userService;

    [HttpGet("id")]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetById(id);

        return Ok(user);
    }

    [HttpPost]
    public IActionResult Post(CreateUserInputModel model)
    {
        var id = _userService.Insert(model);

        return CreatedAtAction(nameof(GetById), new { id }, model);
    }
}
