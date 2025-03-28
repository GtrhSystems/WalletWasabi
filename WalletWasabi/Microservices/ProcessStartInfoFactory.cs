using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WalletWasabi.Microservices;

/// <summary>
/// Factory for <see cref="ProcessStartInfo"/> with pre-defined properties as needed in Wasabi Wallet.
/// </summary>
public class ProcessStartInfoFactory
{
	/// <summary>
	/// Creates new <see cref="ProcessStartInfo"/> instance.
	/// </summary>
	/// <param name="processPath">Path to process.</param>
	/// <param name="arguments">Process arguments.</param>
	/// <param name="openConsole">Open console window. Only for Windows platform.</param>
	/// <returns><see cref="ProcessStartInfo"/> instance.</returns>
	public static ProcessStartInfo Make(string processPath, string arguments, bool openConsole = false)
	{
		if (openConsole && !RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
		{
			throw new PlatformNotSupportedException($"{RuntimeInformation.OSDescription} is not supported.");
		}

		var p = new ProcessStartInfo(fileName: processPath, arguments)
		{
			FileName = processPath,
			Arguments = arguments,
			RedirectStandardOutput = !openConsole,
			RedirectStandardError = !openConsole,
			UseShellExecute = openConsole,
			CreateNoWindow = !openConsole,
			WindowStyle = ProcessWindowStyle.Normal
		};

		return p;
	}
}
