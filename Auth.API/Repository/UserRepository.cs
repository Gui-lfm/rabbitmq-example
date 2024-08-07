namespace Auth.API.Repository;
using Microsoft.EntityFrameworkCore;
using Auth.API.Models;
using Auth.API.Context;
using Auth.API.DTO;

public class UserRepository : IUserRepository
{
    protected readonly IDatabaseContext _context;
    public UserRepository(IDatabaseContext context) 
    {
        _context = context;
    }
    public UserResponseDTO Create(UserInsertDTO user)
    {
        var existingUser = _context.Users.Where(u => u.Email == user.Email);
        if (existingUser.Any()) throw new Exception("E-mail already in use");

        var newUser = new User{ Name = user.Name, Email = user.Email, Password = user.Password};
        _context.Users.Add(newUser);
        _context.SaveChanges();
        return new UserResponseDTO {
            UserId = newUser.UserId,
            Name = newUser.Name,
            Email = newUser.Email,
        };

    }
    public UserResponseDTO Login(LoginDTO user)
    {
        var existingUser = _context.Users.Where(u => u.Email == user.Email && u.Password == user.Password);
        if (!existingUser.Any()) throw new Exception("E-mail or password wrong");
        var findedUser = existingUser.First();
        return new UserResponseDTO {
            UserId = findedUser.UserId,
            Name = findedUser.Name,
            Email = findedUser.Email,
        };
    }
}