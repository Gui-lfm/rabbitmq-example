namespace Auth.API.Services;

using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using Auth.API.Models;

public class NotificationService : INotificationService
{
  public void Send(Message message)
  {
    // criando conexão
    var messageBrokerHost = Environment.GetEnvironmentVariable("MESSAGE_BROKER_HOST");
    var _factory = new ConnectionFactory { HostName = messageBrokerHost };
    using var connection = _factory.CreateConnection();
    using var channel = connection.CreateModel();
    {
      // verifica se a fila existe, e caso não, cria uma nova com o determinado nome
      channel.QueueDeclare(queue: "notification",
               durable: false,
               exclusive: false,
               autoDelete: false,
               arguments: null);

      // montando uma mensagem para enviar para a fila
      string messageSerialize = JsonConvert.SerializeObject(message);
      var body = Encoding.UTF8.GetBytes(messageSerialize);

      // mensagem é enviada para a fila usnado o método 'BasicPublish' da biblioteca do RabbitMQ.
      channel.BasicPublish(exchange: string.Empty,
          routingKey: "notification",
          basicProperties: null,
          body: body);
    }
  }
}
