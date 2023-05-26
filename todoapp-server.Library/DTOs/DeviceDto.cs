using System.ComponentModel.DataAnnotations;

namespace ToDoAppServer.Library.DTOs;

public class DeviceDto
{
	[MaxLength(16)] //16 bytes for IPv6 (future proofing here oK?), 4 bytes for IPv4
	public byte[]? IPAddress { get; set; }

	[Required]
	[MaxLength(100)]
	public string UserAgentFamily { get; set; } = string.Empty;

	[MaxLength(50)]
	public string OSFamily { get; set; } = string.Empty;

	[MaxLength(50)]
	public string DeviceFamily { get; set; } = string.Empty;
}
