using System.ComponentModel.DataAnnotations;
using ToDoAppServer.Library.Models;

namespace ToDoAppServer.Library.DTOs;
public class UserDto
{
	[Required]
	public int Id { get; set; }

	[MaxLength(50)]
	public string? Name { get; set; }

	[Required]
	[MaxLength(100)]
	[EmailAddress]
	public string Email { get; set; } = string.Empty;

	[MaxLength(64)]
	public byte[]? PasswordHash { get; set; }

	[MaxLength(128)]
	public byte[]? PasswordSalt { get; set; }

	public List<Device>? Devices { get; set; }
}
