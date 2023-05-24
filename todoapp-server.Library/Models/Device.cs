using System.ComponentModel.DataAnnotations;

namespace ToDoAppServer.Library.Models;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

public class Device
{
	[Key]
	public long Id { get; set; }



	[Required]
	public int UserId { get; set; }

	[Required]
	public long RefreshTokenId { get; set; }
}
