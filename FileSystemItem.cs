using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerifyNetworkAccessTool
{
	public class FileSystemItem
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		public string FullPath { get; set; }
		public long Size { get; set; }

	}
}
