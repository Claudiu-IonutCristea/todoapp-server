using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ToDoAppServer.API.Services;

namespace ToDoAppServer.Tests.Services.DeviceService;
public class GetDeviceDtoFromUserAgentTests
{
	private readonly IDeviceService _deviceService;

	public GetDeviceDtoFromUserAgentTests(IDeviceService deviceService)
	{
		_deviceService = deviceService;
	}

	[Theory]
	[ClassData(typeof(GetDeviceDtoFromUserAgentTestData))]
	public void UserAgentTests(string uaString, string expectedUA, string expectedOS, string expectedDevice)
	{
		var result = _deviceService.GetDeviceDtoFromUserAgent(uaString, null);

		Assert.Multiple(
			() => Assert.Equal(expectedOS, result.OSFamily),
			() => Assert.Equal(expectedUA, result.UserAgentFamily),
			() => Assert.Equal(expectedDevice, result.DeviceFamily)
		);
	}

	[Theory]
	[MemberData(nameof(IPAdresses_Data))]
	public void IPAdressesTests(IPAddress? ip)
	{
		var ipBytes = ip?.GetAddressBytes();

		var result = _deviceService.GetDeviceDtoFromUserAgent(string.Empty, ip);

		Assert.Equivalent(ipBytes, result.IPAddress);
	}

	public static IEnumerable<object?[]> IPAdresses_Data
		=> new List<object?[]>
		{
			new object?[] { IPAddress.Parse("54.11.12.34") },
			new object?[] { IPAddress.Parse("0.0.0.1") },
			new object?[] { IPAddress.Parse("24.56.12.245") },

			new object?[] { null },

			//Examples for IPv6 strings taken from here:
			//https://www.ibm.com/docs/en/ts4500-tape-library?topic=functionality-ipv4-ipv6-address-formats
			new object?[] { IPAddress.Parse("2001:db8:3333:4444:5555:6666:7777:8888") },
			new object?[] { IPAddress.Parse("2001:db8:3333:4444:CCCC:DDDD:EEEE:FFFF") },
			new object?[] { IPAddress.Parse("::") },
			new object?[] { IPAddress.Parse("2001:db8::") },
			new object?[] { IPAddress.Parse("::1234:5678") },
			new object?[] { IPAddress.Parse("2001:db8::1234:5678") },
			new object?[] { IPAddress.Parse("2001:0db8:0001:0000:0000:0ab9:C0A8:0102") },

			//IPv6 (dual)
			new object?[] { IPAddress.Parse("2001:db8:3333:4444:5555:6666:1.2.3.4") },
			new object?[] { IPAddress.Parse("::11.22.33.44") },
			new object?[] { IPAddress.Parse("2001:db8::123.123.123.123") },
			new object?[] { IPAddress.Parse("::1234:5678:91.123.4.56") },
			new object?[] { IPAddress.Parse("::1234:5678:1.2.3.4") },
			new object?[] { IPAddress.Parse("2001:db8::1234:5678:5.6.7.8") },
		};
}
