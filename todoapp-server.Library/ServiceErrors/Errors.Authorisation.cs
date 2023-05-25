using ErrorOr;

namespace ToDoAppServer.Library.ServiceErrors;
public static partial class Errors
{
	public static class Authorisation
	{
		public static Error TokenExpired => Error.Validation(
			code: "auth.acctoken.expired",
			description: "The provided access token is expired!");

		public static Error NoAuthHeader => Error.NotFound(
			code: "auth.acctoken.noauthheader",
			description: "The request has no authorisation header!");

		public static Error Unexpected => Error.Unexpected(
			code: "auth.acctoken.unexpected");
	}
}
