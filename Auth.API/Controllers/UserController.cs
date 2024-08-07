using Microsoft.AspNetCore.Mvc;
using Auth.API.Repository;
using Auth.API.DTO;
using Auth.API.Services;
using Auth.API.Models;

namespace Auth.API.Controllers;

[ApiController]
[Route("user")]

public class UserController : Controller
{
  private readonly IUserRepository _repository;
  private readonly INotificationService _notificationService;
  public UserController(IUserRepository repository, INotificationService notificationService)
  {
    _repository = repository;
    _notificationService = notificationService;
  }

  [HttpPost]
  public IActionResult Create([FromBody] UserInsertDTO user)
  {
    try
    {
      var userCreated = _repository.Create(user);

      var messageText = "<h4>Cadastro realizado no ShopTry </h4>";
      messageText += "<p> Olá: " + user.Name;
      messageText += "<p> Boas vindas ao ShopTry</p>";

      Message message = new()
      {
        Title = "Shop Try - Cadastro realizado",
        Text = messageText,
        MailTo = user.Email
      };

      _notificationService.Send(message);


      return Created("", new { token = AuthService.GenerateToken(userCreated) });
    }
    catch (Exception ex)
    {
      return BadRequest(new { message = ex.Message.ToString() });
    }

  }
}
