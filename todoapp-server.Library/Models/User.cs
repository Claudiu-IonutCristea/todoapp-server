using System.ComponentModel.DataAnnotations;

namespace ToDoAppServer.Library.Models;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

public class User
{
	[Key]
	public int Id { get; set; }

	[MaxLength(50)]
	public string Name { get; set; } = string.Empty;

	[Required]
	[MaxLength(100)]
	[EmailAddress]
	public string Email { get; set; } = string.Empty;

	[Required]
	[MaxLength(64)]
	public byte[] PasswordHash { get; set; }

	[Required]
	[MaxLength(128)]
	public byte[] PasswordSalt { get; set; }

	public List<Device> Devices { get; set; } = new();
}
