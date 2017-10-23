using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Configuration;

namespace VerifyNetworkAccessTool
{
	public class NetworkUtil
	{


		private readonly string _netBiosName = Environment.MachineName;


		public WcfFSSession RemoteLogin(string username, string plainPassword, string domain, string volumeName)
		{

			string message = null;
			bool isSuccess = false;
			IntPtr token = IntPtr.Zero;
			WindowsImpersonationContext impersonateContext = null;

			int logonType = Advapi32.LOGON32_LOGON_NEW_CREDENTIALS;
			if (String.Compare(volumeName.Substring(2), _netBiosName, StringComparison.OrdinalIgnoreCase) == 0)
				logonType = Advapi32.LOGON32_LOGON_NETWORK;

			bool logonUser = Advapi32.LogonUser(username, domain, plainPassword, logonType, Advapi32.LOGON32_PROVIDER_WINNT50, ref token);

			if (logonUser)
			{
				try
				{
					if (token != IntPtr.Zero)
						impersonateContext = WindowsIdentity.Impersonate(token);

					// Define retry counter in case not access temporary because network problem
					int retryCounter = 0;
					bool needRetry = false;
					do
					{
						int entriesRead = 0;
						int totalEntries = 0;
						int hResume = 0;
						IntPtr pBuffer = IntPtr.Zero;

						// Set needRetry to false at begin
						needRetry = false;

						// Verify accessibility
						int returnCode = Netapi32.NetShareEnum(volumeName, 1, ref pBuffer, 0xFFFFFFFF, ref entriesRead, ref totalEntries, ref hResume);

						var returnMessage = new System.ComponentModel.Win32Exception(returnCode).Message;

						switch (returnCode)
						{
							case 0:
								message = $"LogonUser is successful";
								isSuccess = true;
								break;
							case 53:
								message = $"The volume '{volumeName}' is not available.";
								throw new ApplicationException(message);
							case 5:
								message = $"LogonUser is successful, but Login Failed and return code is 5";
								break;
							default:
								message = $"LogonUser is successful. Return Code is {returnCode}";
								// Increase retry counter
								needRetry = retryCounter++ < MaximumAccessRetry;
								break;
						}

						// free the allocated memory
						Netapi32.NetApiBufferFree(pBuffer);
					}
					while (needRetry);
				}
				catch (Exception ex)
				{
					throw;
				}
				finally
				{
					// Release session after verify accessibility
					impersonateContext?.Undo();
				}
			}
			else
			{
				int lastError = Marshal.GetLastWin32Error();
				message = "LogonUser and Login Failed and return code is " + lastError;
			}

			// Create session when login successfully
			if (isSuccess)
			{

				return new WcfFSSession(token, volumeName, username, domain);
			}
			else
			{
				throw new Exception(message);
			}
		}


		public int MaximumAccessRetry
		{
			get
			{
				int maximunAccessRetry = 5;
				try
				{
					maximunAccessRetry = System.Convert.ToInt32(ConfigurationManager.AppSettings["MaximumAccessRetry"]);
				}
				catch (Exception)
				{
				}
				return maximunAccessRetry;
			}
		}
	}
}
