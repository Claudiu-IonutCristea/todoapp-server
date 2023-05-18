using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;

namespace ToDoAppServer.Library.ServiceErrors;

public static partial class Errors
{
	public static class Login
	{
		public static Error EmailNotFound => Error.Validation(
			code: "auth.email.notFound");
		public static Error PasswordIncorect => Error.Validation(
			code: "auth.password.incorrect");
	}

	public static class Register
	{
		public static Error EmailAlreadyInUse => Error.Validation(
			code: "auth.email.alreadyExists",
			description: "An account with this email already exists!");
	}
}
