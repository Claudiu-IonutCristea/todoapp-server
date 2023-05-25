using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAppServer.Library.ServiceErrors;
public static partial class Errors
{
	public static class AccessToken
	{
		public static Error TokenKeyInvalid => Error.Unexpected(
			code: "acctoken.key.invalid",
			description: "Access token key is invalid!");

		public static Error TokenSignatureInvalid => Error.Validation(
			code: "acctoken.signature.invalid",
			description: "Access token signature is invalid!");
	}

	public static class RefreshToken
	{

	}
}
