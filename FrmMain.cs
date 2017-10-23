using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VerifyNetworkAccessTool
{
	public partial class FrmMain : Form
	{
		public FrmMain()
		{
			InitializeComponent();

			var windowsIdentity = WindowsIdentity.GetCurrent();
			if (windowsIdentity != null)
			{
				string sFullName = windowsIdentity.Name;
				DefaultDomain = sFullName.Substring(0, sFullName.IndexOf(@"\", StringComparison.Ordinal));
			}
		}

		private void btnTestCredential_Click(object sender, EventArgs e)
		{
			var networkUtil = new NetworkUtil();
			try
			{
				using (var session = networkUtil.RemoteLogin(Username, Password, Domain, Volume))
				{
					if (session != null)
					{
						MessageBox.Show("Test credential successfully");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Test credential failed.\n {ex.Message}");
			}

		}

		public string Volume
		{
			get
			{
				var volume = string.Empty;
				var path = string.Empty;
				if (!SplitFullName(txtPath.Text, out volume, out path))
				{
					MessageBox.Show("Invalid path");
				}

				return volume;
			}
		}

		public string FullPath
		{
			get
			{
				var volume = string.Empty;
				var path = string.Empty;
				if (!SplitFullName(txtPath.Text, out volume, out path))
				{
					//MessageBox.Show("Invalid path");
				}

				return path;
			}
		}

		public string Domain
		{
			get
			{
				if (string.IsNullOrEmpty(txtDomain.Text))
				{
					return DefaultDomain;
				}
				else
				{
					return txtDomain.Text;
				}
			}
		}
		public string Username => txtUsername.Text;
		public string Password => txtPassword.Text;

		public string DefaultDomain { get; set; }

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			var networkUtil = new NetworkUtil();
			try
			{
				using (var session = networkUtil.RemoteLogin(Username, Password, Domain, Volume))
				{
					if (session != null)
					{
						NetShares netShares = new NetShares();
						var items = new System.Collections.Concurrent.ConcurrentBag<FileSystemItem>();
						if (FullPath == string.Empty || FullPath == @"\")
						{
							var shares = netShares.EnumNetShares(Volume);

							if (shares != null && shares.Any())
							{

								foreach (var share in shares)
								{
									try
									{
										var fullPath = $@"{Volume}\{share.shi1_netname}\";
										var di = new DirectoryInfo(fullPath);
										var dmsShareFolder = new FileSystemItem
										{
											Name = share.shi1_netname,
											Type = "Directory",
											CreationDate = di.CreationTime,
											ModifiedDate = di.LastWriteTime,
											FullPath = di.FullName

										};
										items.Add(dmsShareFolder);
									}
									catch (Exception ex)
									{
									}
									finally
									{
									}
								}
							}

							var dialog = new FrmListItem(items.ToList());

							dialog.Show();
						}
						else if(File.Exists(txtPath.Text) == false)
						{
							var directories = Directory.GetDirectories(txtPath.Text);
							foreach (var directory in directories)
							{
								try
								{
									var fullPath = directory;
									var di = new DirectoryInfo(fullPath);
									var dmsShareFolder = new FileSystemItem
									{
										Name = di.Name,
										Type = "Directory",
										CreationDate = di.CreationTime,
										ModifiedDate = di.LastWriteTime,
										FullPath = di.FullName
									};
									items.Add(dmsShareFolder);
								}
								catch (Exception ex)
								{
								}
								finally
								{
								}
							}

							var files = Directory.GetFiles(txtPath.Text);
							foreach (var file in files)
							{
								try
								{
									var fullPath = file;
									var di = new FileInfo(fullPath);
									var dmsShareFolder = new FileSystemItem
									{
										Name = di.Name,
										Type = "File",
										CreationDate = di.CreationTime,
										ModifiedDate = di.LastWriteTime,
										Size = di.Length,
										FullPath = di.FullName

									};
									items.Add(dmsShareFolder);
								}
								catch (Exception ex)
								{
								}
								finally
								{
								}
							}

							var dialog = new FrmListItem(items.ToList());

							dialog.Show();
						}
						else
						{
							var fileName = txtPath.Text;
							var fileInfo = new FileInfo(fileName);
							string message = $"File Name: {fileInfo.Name}\nSize: {fileInfo.Length} bytes\nCreated date: {fileInfo.CreationTime}\nModified date: {fileInfo.LastWriteTime}";

							MessageBox.Show($"File info:\n{message}");
						}
						
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Unable to access the volume with provided credential.\n{ex.Message}");
			}
		}

		private bool SplitFullName(string fullName, out string volume, out string path)
		{
			string pattern = @"^(?<Volume>\\\\[^\\]+)(?<Path>(\\.*)*)$";
			volume = null;
			path = null;

			if (string.IsNullOrEmpty(fullName))
			{
				return false;
			}

			fullName = fullName.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

			Match match = Regex.Match(fullName, pattern);
			if (match.Success == false)
			{
				return false;
			}

			Group groupVolume = match.Groups["Volume"];
			if (groupVolume.Success)
				volume = groupVolume.Value;

			Group groupPath = match.Groups["Path"];
			if (groupPath.Success)
				path = groupPath.Value;

			return true;
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void FrmMain_Load(object sender, EventArgs e)
		{
			var windowsIdentity = WindowsIdentity.GetCurrent();
			var principal = new WindowsPrincipal(windowsIdentity);
			if (!principal.IsInRole(WindowsBuiltInRole.Administrator))
			{
				MessageBox.Show("Please run application as Administrator");
				this.Close();
				return;
			}

			if (windowsIdentity != null)
			{
				tsbLabel.Text =$"The application is running under user: {windowsIdentity.Name}";
			
			}
		}

		public static bool IsAdministrator()
		{
			var identity = WindowsIdentity.GetCurrent();
			var principal = new WindowsPrincipal(identity);
			return principal.IsInRole(WindowsBuiltInRole.Administrator);
		}
	}
}
