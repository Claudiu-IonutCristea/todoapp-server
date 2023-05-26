using UAParser;
using System.Net;
using ToDoAppServer.Library.DTOs;
using Device = ToDoAppServer.Library.Models.Device;

namespace ToDoAppServer.API.Services;

public interface IDeviceService
{
	DeviceDto GetDeviceDtoFromUserAgent(string userAgentString, IPAddress? ipAddress);
}

public class DeviceService : IDeviceService
{
	public DeviceDto GetDeviceDtoFromUserAgent(string userAgentString, IPAddress? ipAddress)
	{
		var uaParser = Parser.GetDefault();

		var c = uaParser.Parse(userAgentString);

		var deviceDto = new DeviceDto
		{
			IPAddress = ipAddress?.GetAddressBytes(),
			UserAgentFamily = c.UA.Family,
			OSFamily = c.OS.Family,
			DeviceFamily = c.Device.Family,
		};

		return deviceDto;
	}

	public ErrorOr<Success> ValidateDevice(Device deviceToValidate, DeviceDto expectedDeviceData)
	{
		var errors = new List<Error>();

		if(expectedDeviceData.UserAgentFamily != deviceToValidate.UserAgentFamily)
			errors.Add(Errors.Device.NoMatchUserAgentFamily);

		if(expectedDeviceData.DeviceFamily != deviceToValidate.DeviceFamily)
			errors.Add(Errors.Device.NoMatchDeviceFamily);

		if(expectedDeviceData.OSFamily != deviceToValidate.OSFamily)
			errors.Add(Errors.Device.NoMatchOSFamily);

		if(expectedDeviceData.IPAddress != null && deviceToValidate.IPAddress != null)
			if(expectedDeviceData.IPAddress.SequenceEqual(deviceToValidate.IPAddress))
				errors.Add(Errors.Device.NoMatchIpAddress);

		if(errors.Count > 0)
			return errors;

		return Result.Success;
	}
}
