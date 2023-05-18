namespace ToDoAppServer.Library.Models;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

public class User
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public byte[] PasswordHash { get; set; }
	public byte[] PasswordSalt { get; set; }
}
