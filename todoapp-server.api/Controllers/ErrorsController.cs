using Microsoft.AspNetCore.Mvc;

namespace ToDoAppServer.API.Controllers;


public class ErrorsController : ApiController
{
	[Route("/error")]
	public IActionResult Error()
	{
		return Problem();
	}
}
