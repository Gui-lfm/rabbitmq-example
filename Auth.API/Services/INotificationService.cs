namespace Auth.API.Services;

using Auth.API.Models;

public interface INotificationService
{
    void Send(Message notification);
}