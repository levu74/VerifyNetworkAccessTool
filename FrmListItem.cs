using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VerifyNetworkAccessTool
{
	public partial class FrmListItem : Form
	{
		List<FileSystemItem> _items;
		public FrmListItem(List<FileSystemItem> items)
		{
			InitializeComponent();
			_items = items;
		}

		private void FrmListItem_Load(object sender, EventArgs e)
		{
			fileSystemItemBindingSource.DataSource = _items;
		}
	}
}
