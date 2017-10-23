using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VerifyNetworkAccessTool
{
	public class Mpr
	{
		#region NetShareEnum
		[DllImport("Mpr.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int WNetGetConnection(string lpLocalName, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpRemoteName, ref int lpnLength);
		#endregion
	}
}
