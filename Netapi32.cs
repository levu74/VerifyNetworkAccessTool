using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VerifyNetworkAccessTool
{
	public class Netapi32
	{
		#region NetShareEnum
		[DllImport("Netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int NetShareEnum(string servername, int level, ref IntPtr bufptr, uint prefmaxlen, ref int entriesread, ref int totalentries, ref int resume_handle);
		#endregion

		#region NetApiBufferFree
		[DllImport("Netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int NetApiBufferFree(IntPtr Buffer);
		#endregion
	}
}
