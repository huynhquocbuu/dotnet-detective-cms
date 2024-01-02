namespace Cms.Application.Admin.Models.User;

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string FullName { get; set; }
    public string Roles { get; set; }

    public string Password { get; set; }
}