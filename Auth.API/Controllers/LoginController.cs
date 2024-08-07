using Microsoft.AspNetCore.Mvc;
using Auth.API.Repository;
using Auth.API.DTO;
using Auth.API.Services;
using Auth.API.Models;
using System.Net;

namespace Auth.API.Controllers;

[ApiController]
[Route("login")]

public class LoginController : Controller
{
  private readonly IUserRepository _repository;

  public LoginController(IUserRepository repository)
  {
    _repository = repository;
  }

  [HttpPost]
  public IActionResult Login([FromBody] LoginDTO user)
  {
    try
    {
      var userLogged = _repository.Login(user);
      return Ok(new { token = AuthService.GenerateToken(userLogged) });
    }
    catch (Exception ex)
    {
      return BadRequest(new { message = ex.Message.ToString() });
    }

  }
}
