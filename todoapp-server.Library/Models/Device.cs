using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ToDoAppServer.Library.Models;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

public class Device
{
	[Key]
	public long Id { get; set; }

	[MaxLength(16)] //16 bytes for IPv6 (future proofing here oK?), 4 bytes for IPv4
	public byte[]? IPAddress { get; set; }

	[Required]
	[MaxLength(100)]
	public string UserAgentFamily { get; set; }

	[MaxLength(50)]
	public string? OSFamily { get; set; }

	[MaxLength(50)]
	public string? DeviceFamily { get; set; }

	[MaxLength(50)]
	public string? DeviceModel { get; set; }


	[Required]
	public int UserId { get; set; }

	public RefreshToken RefreshToken { get; set; }
}
