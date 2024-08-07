using Microsoft.AspNetCore.Mvc;
using Auth.API.Repository;
using Auth.API.DTO;
using Auth.API.Services;

namespace Auth.API.Controllers;

[ApiController]
[Route("user")]

public class UserController : Controller
{
  private readonly IUserRepository _repository;
  public UserController(IUserRepository repository)
  {
    _repository = repository;
  }

  [HttpPost]
  public IActionResult Create([FromBody] UserInsertDTO user)
  {
    try
    {
      var userCreated = _repository.Create(user);
      return Created("", new { token = AuthService.GenerateToken(userCreated) });
    }
    catch (Exception ex)
    {
      return BadRequest(new { message = ex.Message.ToString() });
    }

  }
}
