using Microsoft.AspNetCore.Mvc;

namespace todoapp_server.API.Controllers;


public class ErrorsController : ControllerBase
{
	[Route("/error")]
	public IActionResult Error()
	{
		return Problem();
	}
}
