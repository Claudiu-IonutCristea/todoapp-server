using System.ComponentModel.DataAnnotations;

namespace ToDoAppServer.Library.DTOs;

public partial class AuthentificationDTOs
{
	public class LoginDTO
	{
		[Required]
		[MaxLength(100)]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;

		[Required]
		public string Password { get; set; } = string.Empty;
	}
}
