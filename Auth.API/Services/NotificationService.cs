namespace Auth.API.Services;

using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using Auth.API.Models;

public class NotificationService : INotificationService
{
    public void Send(Message message)
    {
      var _factory = new ConnectionFactory { HostName = "localhost"};
      using var connection = _factory.CreateConnection();
      using var channel = connection.CreateModel();
    }
}
