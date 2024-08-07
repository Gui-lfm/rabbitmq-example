using Auth.API.DTO;

namespace Auth.API.Repository;

public interface IUserRepository
{

  UserResponseDTO Create(UserInsertDTO user);
  UserResponseDTO Login(LoginDTO user);
}