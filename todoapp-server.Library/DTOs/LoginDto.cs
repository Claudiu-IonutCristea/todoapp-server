using System.ComponentModel.DataAnnotations;

namespace ToDoAppServer.Library.DTOs;

public class LoginDto
{
	[Required]
	[MaxLength(100)]
	[EmailAddress]
	public string Email { get; set; } = string.Empty;

	[Required]
	[MaxLength(100)]
	public string Password { get; set; } = string.Empty;
}
