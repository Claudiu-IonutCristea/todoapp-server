using System.Collections;

namespace ToDoAppServer.Tests.Services.DeviceService;
public class GetDeviceDtoFromUserAgentTestData : IEnumerable<object[]>
{
	public IEnumerator<object[]> GetEnumerator()
	{
		//Test string taken from here:
		//https://deviceatlas.com/blog/list-of-user-agent-strings

		#region Android Mobile User Agents
		yield return new object[]
		{
			"Mozilla/5.0 (Linux; Android 12; SM-S906N Build/QP1A.190711.020; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/80.0.3987.119 Mobile Safari/537.36",
			"Chrome Mobile WebView", "Android", "Samsung SM-S906N"
		};

		yield return new object[]
		{
			"Mozilla/5.0 (Linux; Android 12; Pixel 6 Build/SD1A.210817.023; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/94.0.4606.71 Mobile Safari/537.36",
			"Chrome Mobile WebView", "Android", "Pixel 6"
		};

		yield return new object[]
		{
			"Mozilla/5.0 (Linux; Android 6.0.1; Nexus 6P Build/MMB29P) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.83 Mobile Safari/537.36",
			"Chrome Mobile", "Android", "Huawei Nexus 6P"
		};

		yield return new object[]
		{
			"Mozilla/5.0 (Linux; Android 6.0; HTC One X10 Build/MRA58K; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/61.0.3163.98 Mobile Safari/537.36",
			"Chrome Mobile WebView", "Android", "HTC One X10"
		};

		yield return new object[]
		{
			"Mozilla/5.0 (Linux; Android 6.0.1; E6653 Build/32.2.A.0.253) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.98 Mobile Safari/537.36",
			"Chrome Mobile", "Android", "E6653"
		};
		#endregion

		#region Iphone Mobile User Agents
		yield return new object[]
		{
			"Mozilla/5.0 (iPhone14,6; U; CPU iPhone OS 15_4 like Mac OS X) AppleWebKit/602.1.50 (KHTML, like Gecko) Version/10.0 Mobile/19E241 Safari/602.1",
			"Mobile Safari", "iOS", "iPhone"
		};

		yield return new object[]
		{
			"Mozilla/5.0 (iPhone; CPU iPhone OS 12_0 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) CriOS/69.0.3497.105 Mobile/15E148 Safari/605.1",
			"Chrome Mobile iOS", "iOS", "iPhone"
		};
		#endregion

		#region MS Windows Phone User Agents
		yield return new object[]
		{
			"Mozilla/5.0 (Windows Phone 10.0; Android 6.0.1; Microsoft; RM-1152) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Mobile Safari/537.36 Edge/15.15254",
			"Edge Mobile", "Windows Phone", "Generic Smartphone"
		};
		#endregion

		#region Desktop User Agents
		yield return new object[]
		{
			"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.246",
			"Edge", "Windows", "Other"
		};

		yield return new object[]
		{
			"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_2) AppleWebKit/601.3.9 (KHTML, like Gecko) Version/9.0.2 Safari/601.3.9",
			"Safari", "Mac OS X", "Mac"
		};
		#endregion
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
