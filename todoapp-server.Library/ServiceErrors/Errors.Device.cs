using ErrorOr;

namespace ToDoAppServer.Library.ServiceErrors;

public static partial class Errors
{
	public static class Device
	{
		public static Error NoMatchUserAgentFamily => Error.Validation(
			code: "device.uafamily.nomatch");

		public static Error NoMatchDeviceFamily => Error.Validation(
			code: "device.devicefamily.nomatch");

		public static Error NoMatchOSFamily => Error.Validation(
			code: "device.osfamily.nomatch");

		public static Error NoMatchIpAddress => Error.Validation(
			code: "device.ipaddress.nomatch");

		public static Error Success => Error.Validation(
			code: "device.validation.success");
	}
}
