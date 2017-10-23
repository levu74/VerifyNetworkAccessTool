using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace VerifyNetworkAccessTool
{
	public class WcfFSSession : IDisposable
	{
		
		private readonly string _netBiosName = Environment.MachineName;
		WindowsImpersonationContext impersonateContext = null;
		public WcfFSSession(IntPtr token, string volumename, string username, string domain)
		{
			Volume = volumename;
			Username = username;
			Domain = domain;
			Token = token;
			impersonateContext = WindowsIdentity.Impersonate(token);
		}

		public IntPtr Token { get; set; } = IntPtr.Zero;
		public string Volume { get; set; }
		public string Username { get; set; }
		public string Domain { get; set; }
		public void Dispose()
		{
			try
			{
				// Release Access
				if (Token != IntPtr.Zero)
				{
					impersonateContext.Undo();
					Kernel32.CloseHandle(Token);
				}
			}
			catch (Exception ex)
			{
				var exception = new Exception("Cannot close handle");
				exception.Data.Add("InnerEx", ex);
				throw exception;
			}
			
		}
	}
}
