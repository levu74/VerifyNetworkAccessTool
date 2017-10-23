using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VerifyNetworkAccessTool
{
	public class FileSystemUtil
	{
	
		public string ConvertToNetworkPath(string resourcePath)
		{
			if (resourcePath.StartsWith(@"\\") == false)
			{
				int entriesRead = 0;
				int totalEntries = 0;
				int hResume = 0;
				IntPtr pBuffer = IntPtr.Zero;

				string networkPath = null;

				Netapi32.NetShareEnum(System.Net.Dns.GetHostName(), 2, ref pBuffer, 0xFFFFFFFF, ref entriesRead, ref totalEntries, ref hResume);

				try
				{
					Type type = typeof(SHARE_INFO_2);
					int offset = Marshal.SizeOf(type);

					for (Int64 i = 0, lpItem = pBuffer.ToInt64(); i < entriesRead; i++, lpItem += offset)
					{
						IntPtr pItem = new IntPtr(lpItem);
						SHARE_INFO_2 shi2 = (SHARE_INFO_2)Marshal.PtrToStructure(pItem, type);

						if (shi2.shi2_type != 0) continue;

						if ((resourcePath + @"\").ToLower().IndexOf(shi2.shi2_path.ToLower() + @"\") != -1)
						{
							networkPath = @"\\" + System.Net.Dns.GetHostName() + @"\" + shi2.shi2_netname + resourcePath.Substring(shi2.shi2_path.Length);
							break;
						}
					}

					if (networkPath == null)
					{
						string rootPath = resourcePath.Substring(0, 2);
						StringBuilder sb = new StringBuilder(260);
						int stringSize = sb.Capacity;
						int driveType = Kernel32.GetDriveType(rootPath);
						int wnetError = Mpr.WNetGetConnection(rootPath, sb, ref stringSize);

						if (Kernel32.GetDriveType(rootPath) == Kernel32.DRIVE_REMOTE && Mpr.WNetGetConnection(rootPath, sb, ref stringSize) == 0)
						{
							networkPath = resourcePath.Replace(rootPath, sb.ToString());
						}
						else
						{
							
						}
					}

					if (networkPath == null)
					{
						networkPath = @"\\" + System.Net.Dns.GetHostName() + @"\" + resourcePath.Replace(":", "$");
					}

				}
				catch (Exception ex)
				{
				}
				finally
				{
					Netapi32.NetApiBufferFree(pBuffer);
				}

				return networkPath;
			}
			else
				return resourcePath;
		}

		public string ConvertToLocalPath(string networkPath)
		{
			string localPath = networkPath;

			if (networkPath.StartsWith(@"\\"))
			{
				string[] folders = localPath.Substring(2).Split('\\');

				if (folders.Length >= 2 && string.Compare(folders[0], System.Net.Dns.GetHostName(), true) == 0)
				{
					if (folders[1].Length == 2 && folders[1][1] == '$')
					{
						int index = networkPath.IndexOf(folders[1]);
						localPath = folders[1].Replace('$', ':') + networkPath.Substring(index + 2);
					}
				}
			}

			return localPath;
		}
	}
}
