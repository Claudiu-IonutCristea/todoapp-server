﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAppServer.Library.DTOs;
public class RegisterDto
{
	[Required]
	public string Email { get; set; } = string.Empty;

	[Required]
	public string Name { get; set; } = string.Empty;

	[Required]
	public string Password { get; set; } = string.Empty;
}
