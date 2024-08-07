using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;

using Notification.Service.Models;
using Notification.Service.Service;

namespace Notification.Service
{
  public class Program
  {
    private static AutoResetEvent waitHandle = new AutoResetEvent(false);

    public static void Main(string[] args)
    {
    //ponteiro para caso haja algum problema de conexão com o RabbitMQ, o 
    // programa não fechará e tentará se reconectar novamente e seguir com as suas responsabilidades.
      Inicio:
      try
      {
        // conexão é criada
        var MessageBrokerHost =  Environment.GetEnvironmentVariable("MESSAGE_BROKER_HOST");
        var factory = new ConnectionFactory { HostName = MessageBrokerHost };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        {
          // verifica se a fila existe, e caso não, cria uma nova com o determinado nome
          channel.QueueDeclare(queue: "notification",
                  durable: false,
                  exclusive: false,
                  autoDelete: false,
                  arguments: null);

          Console.WriteLine("Waiting for new notifications...");

          // criando o consumer da aplicação
          var consumer = new EventingBasicConsumer(channel);

          // processos a serem realizados com uma mensagem que chega da fila
          consumer.Received += (model, ea) =>
          {
            var body = ea.Body.ToArray();
            Message message = JsonConvert.DeserializeObject<Message>(Encoding.UTF8.GetString(body));
            try
            {
              EmailService.Send(message!);
              Console.WriteLine("Mail sent");
            }
            catch (Exception ex)
            {
              Console.WriteLine("Mail failed");
            }
          };

          //  método que irá de fato tirar as mensagens da fila notification, usando o método 'BasicConsume do rabbitMQ'
          channel.BasicConsume(queue: "notification",
              autoAck: true,
              consumer: consumer);

          Console.CancelKeyPress += (o, e) =>
          {
            Console.WriteLine("Exit...");
            waitHandle.Set();
          };

          waitHandle.WaitOne();
        }

      } // caso tenha algum erro, instrução tenta reconectar apontando para o início do código.
      catch (Exception connectionException)
      {
        Console.WriteLine("Error on connect to rabbitmq");
        Console.WriteLine("Trying reconnect");
        System.Threading.Thread.Sleep(5000);
        goto Inicio;
      }
    }
  }
}
