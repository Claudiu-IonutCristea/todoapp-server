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
		public static Error TokenInvalid => Error.Failure(
			code: "rfrshtoken.invalid",
			description: "Refresh token is invalid!");

		public static Error TokenExpired => Error.Validation(
			code: "rfrshtoken.expired",
			description: "Token is valid, but expired!");

		public static Error TokenNotValidForUser => Error.Validation(
			code: "rfrshtoken.user.notvalid",
			description: "Token is not valid for this user!");

		public static Error TokenRevoked => Error.Validation(
			code: "rfrshtoken.revoked",
			description: "Tokenn has been revoked!");
	}
}
