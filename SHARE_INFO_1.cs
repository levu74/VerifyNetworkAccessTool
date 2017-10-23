using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VerifyNetworkAccessTool
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct SHARE_INFO_1
	{
		[MarshalAs(UnmanagedType.LPWStr)]
		public string shi1_netname;
		public uint shi1_type;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string shi1_remark;

		public SHARE_INFO_1(string sharename, uint sharetype, string remark)
		{
			this.shi1_netname = sharename;
			this.shi1_type = sharetype;
			this.shi1_remark = remark;
		}

		public override string ToString()
		{
			return shi1_netname;
		}
	}
}
