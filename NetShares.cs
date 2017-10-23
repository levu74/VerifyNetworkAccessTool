using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VerifyNetworkAccessTool
{
	public class NetShares
	{
		const uint MAX_PREFERRED_LENGTH = 0xFFFFFFFF;
		const int NERR_Success = 0;

		public IList<SHARE_INFO_1> EnumNetShares(string serverName)
		{
			List<SHARE_INFO_1> ShareInfos = new List<SHARE_INFO_1>();
			int entriesread = 0;
			int totalentries = 0;
			int resume_handle = 0;
			int nStructSize = Marshal.SizeOf(typeof(SHARE_INFO_1));
			IntPtr bufPtr = IntPtr.Zero;
			StringBuilder server = new StringBuilder(serverName);
			int ret = Netapi32.NetShareEnum(server.ToString(), 1, ref bufPtr, MAX_PREFERRED_LENGTH, ref entriesread, ref totalentries, ref resume_handle);
			if (ret == NERR_Success)
			{
				IntPtr currentPtr = bufPtr;
				for (int i = 0; i < entriesread; i++)
				{
					SHARE_INFO_1 shi1 = (SHARE_INFO_1)Marshal.PtrToStructure(currentPtr, typeof(SHARE_INFO_1));
					ShareInfos.Add(shi1);
					//Remember, 64-bit systems have 64-bit pointers. Using ToInt32 will cause an ArithmeticOverview exception.
					//Use ToInt64 if you need to.
					// IntPtr.Size = 8 => 64-bit process
					if (IntPtr.Size == 8)
					{
						currentPtr = new IntPtr(currentPtr.ToInt64() + nStructSize);
					}
					else
					{
						currentPtr = new IntPtr(currentPtr.ToInt32() + nStructSize);
					}
				}
				Netapi32.NetApiBufferFree(bufPtr);
				return ShareInfos;
			}
			else
			{
				ShareInfos.Add(new SHARE_INFO_1("ERROR=" + ret.ToString(), 10, string.Empty));
				return ShareInfos;
			}
		}
	}
}
