using Microsoft.AspNetCore.Mvc;

namespace ToDoAppServer.API.Controllers;


public class ErrorsController : ControllerBase
{
	[Route("/error")]
	public IActionResult Error()
	{
		return Problem();
	}
}
