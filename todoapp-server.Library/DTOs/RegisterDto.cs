using System.ComponentModel.DataAnnotations;

namespace ToDoAppServer.Library.DTOs;

public class RegisterDto
{
	[Required]
	[EmailAddress]
	[MaxLength(100)]
	public string Email { get; set; } = string.Empty;

	[MaxLength(50)]
	public string Name { get; set; } = string.Empty;

	[Required]
	[MaxLength(100)]
	public string Password { get; set; } = string.Empty;
}