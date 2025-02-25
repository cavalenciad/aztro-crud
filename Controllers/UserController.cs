using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.Data;
using Microsoft.AspNetCore.Mvc;
using AztroWebApplication.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(ApplicationDbContext context)
    {
        _userService = new UserService(context);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserById(id);

        if (user == null)
        {
            return NotFound(new ErrorResponse { Message = "User not found", StatusCode = 404 });
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        var createdUser = await _userService.CreateUser(user);

        if (createdUser == null)
        {
            return BadRequest(new ErrorResponse { Message = "User must be between 18 and 80 years old", StatusCode = 400 });
        }

        return Created(nameof(GetUserById), createdUser);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        var updatedUser = await _userService.UpdateUser(id, user);

        if (updatedUser == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var userRemoved = await _userService.DeleteUserById(id);

        if (userRemoved == null)
        {
            return NotFound(new ErrorResponse { Message = "User not found", StatusCode = 404 });
        }

        // Delete user
        return Ok(new { Message = "User deleted", User = userRemoved });
    }
}