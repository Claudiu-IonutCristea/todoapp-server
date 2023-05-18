using Microsoft.AspNetCore.Mvc;

namespace todoapp_server.api.Controllers;


public class ErrorsController : ControllerBase
{
	[Route("/error")]
	public IActionResult Error()
	{
		return Problem();
	}
}
