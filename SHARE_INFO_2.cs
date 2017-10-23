using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VerifyNetworkAccessTool
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct SHARE_INFO_2
	{
		[MarshalAs(UnmanagedType.LPWStr)]
		public string shi2_netname;
		public int shi2_type;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string shi2_remark;
		public int shi2_permissions;
		public int shi2_maxUsers;
		public int shi2_current_users;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string shi2_path;
		[MarshalAs(UnmanagedType.LPWStr)]
		public string shi2_password;
	}
}
