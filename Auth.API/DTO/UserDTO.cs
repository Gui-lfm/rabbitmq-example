namespace Auth.API.DTO;

public class UserInsertDTO
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class LoginDTO
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class LoginResponseDTO
{
    public string? Token { get; set; }
}


public class UserResponseDTO
{
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
}