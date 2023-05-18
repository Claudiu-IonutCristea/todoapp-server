namespace ToDoAppServer.Library.DTOs;

public class AuthTokentsDto
{
	public string RefreshToken { get; set; } = string.Empty;
	public string AccessToken { get; set; } = string.Empty;
}
