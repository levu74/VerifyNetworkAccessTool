using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VerifyNetworkAccessTool
{
	public class Advapi32
	{
		#region LogonUser
		public const int LOGON32_LOGON_BATCH = 4;
		public const int LOGON32_LOGON_INTERACTIVE = 2;
		public const int LOGON32_LOGON_NETWORK = 3;
		public const int LOGON32_LOGON_NETWORK_CLEARTEXT = 8;
		public const int LOGON32_LOGON_NEW_CREDENTIALS = 9;
		public const int LOGON32_LOGON_SERVICE = 5;
		public const int LOGON32_LOGON_UNLOCK = 7;

		public const int LOGON32_PROVIDER_DEFAULT = 0;
		public const int LOGON32_PROVIDER_WINNT50 = 3;
		public const int LOGON32_PROVIDER_WINNT40 = 2;
		public const int LOGON32_PROVIDER_WINNT35 = 1;

		[DllImport("Advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
		#endregion

		#region DuplicateToken

		public const int SecurityAnonymous = 0;
		public const int SecurityIdentification = 1;
		public const int SecurityImpersonation = 2;
		public const int SecurityDelegation = 3;

		[DllImport("Advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool DuplicateToken(IntPtr ExistingTokenHandle, int ImpersonationLevel, ref IntPtr DuplicateTokenHandle);
		#endregion
	}
}
