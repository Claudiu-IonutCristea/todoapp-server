using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoAppServer.Library.DTOs;
public class RefreshTokenDto
{
	[Required]
	[MaxLength(88)]
	public string Token { get; set; } = string.Empty;

	[Required]
	public DateTime Created { get; set; }

	[Required]
	public DateTime Expires { get; set; }
}
