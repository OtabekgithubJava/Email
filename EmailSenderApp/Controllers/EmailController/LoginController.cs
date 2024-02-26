using EmailSenderApp.Application.Services.Email;
using EmailSenderApp.Domain.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmailSenderApp;

[ApiController]
[Route("api/[controller]")]
public class LoginController : Controller
{

    private readonly IEmailService _emailService;

    public LoginController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        bool isValidCredentials = await _emailService.VerifyCredentials(user);
    
        if (isValidCredentials)
        {
            return Ok("Login successful");
        }
        else
        {
            return BadRequest("Invalid credentials");
        }
    }
}