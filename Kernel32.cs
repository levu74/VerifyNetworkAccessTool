using System;
using System.Runtime.InteropServices;

namespace VerifyNetworkAccessTool
{
	public class Kernel32
	{
		public const int INVALID_HANDLE_VALUE = unchecked((int)0xffffffff);
		public const int READ_CONTROL = 0x00020000;
		public const int SYNCHRONIZE = 0x00100000;

		public const int STANDARD_RIGHTS_REQUIRED = 0x000f0000;
		public const int STANDARD_RIGHTS_READ = READ_CONTROL;
		public const int STANDARD_RIGHTS_WRITE = READ_CONTROL;
		public const int STANDARD_RIGHTS_EXECUTE = READ_CONTROL;

		public const int FILE_ADD_FILE = 0x0002;
		public const int FILE_ADD_SUBDIRECTORY = 0x0004;
		public const int FILE_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED | SYNCHRONIZE | 0x1ff;
		public const int FILE_APPEND_DATA = 0x0004;
		public const int FILE_CREATE_PIPE_INSTANCE = 0x0004;
		public const int FILE_DELETE_CHILD = 0x0040;
		public const int FILE_EXECUTE = 0x0020;
		public const int FILE_LIST_DIRECTORY = 0x0001;
		public const int FILE_READ_ATTRIBUTES = 0x0080;
		public const int FILE_READ_DATA = 0x0001;
		public const int FILE_READ_EA = 0x0008;
		public const int FILE_TRAVERSE = 0x0020;
		public const int FILE_WRITE_ATTRIBUTES = 0x0100;
		public const int FILE_WRITE_DATA = 0x0002;
		public const int FILE_WRITE_EA = 0x0010;

		public const int FILE_GENERIC_READ = STANDARD_RIGHTS_READ |
																FILE_READ_DATA |
																FILE_READ_ATTRIBUTES |
																FILE_READ_EA |
																SYNCHRONIZE;

		public const int FILE_GENERIC_WRITE = STANDARD_RIGHTS_WRITE |
																FILE_WRITE_DATA |
																FILE_WRITE_ATTRIBUTES |
																FILE_WRITE_EA |
																FILE_APPEND_DATA |
																SYNCHRONIZE;

		public const int GENERIC_READ = unchecked((int)0x80000000);
		public const int GENERIC_WRITE = 0x40000000;
		public const int GENERIC_EXECUTE = 0x20000000;
		public const int GENERIC_ALL = 0x10000000;


		public const int FILE_FLAG_WRITE_THROUGH = unchecked((int)0x80000000);
		public const int FILE_FLAG_OVERLAPPED = 0x40000000;
		public const int FILE_FLAG_NO_BUFFERING = 0x20000000;
		public const int FILE_FLAG_RANDOM_ACCESS = 0x10000000;
		public const int FILE_FLAG_SEQUENTIAL_SCAN = 0x08000000;
		public const int FILE_FLAG_DELETE_ON_CLOSE = 0x04000000;
		public const int FILE_FLAG_BACKUP_SEMANTICS = 0x02000000;
		public const int FILE_FLAG_POSIX_SEMANTICS = 0x01000000;
		public const int FILE_FLAG_OPEN_REPARSE_POINT = 0x00200000;
		public const int FILE_FLAG_OPEN_NO_RECALL = 0x00100000;
		public const int FILE_FLAG_FIRST_PIPE_INSTANCE = 0x00080000;

		public const int CREATE_NEW = 1;
		public const int CREATE_ALWAYS = 2;
		public const int OPEN_ExISTING = 3;
		public const int OPEN_ALWAYS = 4;
		public const int TRUNCATE_EXISTING = 5;


		public const int FILE_SHARE_READ = 0x00000001;
		public const int FILE_SHARE_WRITE = 0x00000002;
		public const int FILE_SHARE_DELETE = 0x00000004;
		public const int FILE_ATTRIBUTE_READONLY = 0x00000001;
		public const int FILE_ATTRIBUTE_HIDDEN = 0x00000002;
		public const int FILE_ATTRIBUTE_SYSTEM = 0x00000004;
		public const int FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
		public const int FILE_ATTRIBUTE_ARCHIVE = 0x00000020;
		public const int FILE_ATTRIBUTE_DEVICE = 0x00000040;
		public const int FILE_ATTRIBUTE_NORMAL = 0x00000080;
		public const int FILE_ATTRIBUTE_TEMPORARY = 0x00000100;
		public const int FILE_ATTRIBUTE_SPARSE_FILE = 0x00000200;
		public const int FILE_ATTRIBUTE_REPARSE_POINT = 0x00000400;
		public const int FILE_ATTRIBUTE_COMPRESSED = 0x00000800;
		public const int FILE_ATTRIBUTE_OFFLINE = 0x00001000;
		public const int FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000;
		public const int FILE_ATTRIBUTE_ENCRYPTED = 0x00004000;
		public const int FILE_NOTIFY_CHANGE_FILE_NAME = 0x00000001;
		public const int FILE_NOTIFY_CHANGE_DIR_NAME = 0x00000002;
		public const int FILE_NOTIFY_CHANGE_ATTRIBUTES = 0x00000004;
		public const int FILE_NOTIFY_CHANGE_SIZE = 0x00000008;
		public const int FILE_NOTIFY_CHANGE_LAST_WRITE = 0x00000010;
		public const int FILE_NOTIFY_CHANGE_LAST_ACCESS = 0x00000020;
		public const int FILE_NOTIFY_CHANGE_CREATION = 0x00000040;
		public const int FILE_NOTIFY_CHANGE_SECURITY = 0x00000100;
		public const int FILE_ACTION_ADDED = 0x00000001;
		public const int FILE_ACTION_REMOVED = 0x00000002;
		public const int FILE_ACTION_MODIFIED = 0x00000003;
		public const int FILE_ACTION_RENAMED_OLD_NAME = 0x00000004;
		public const int FILE_ACTION_RENAMED_NEW_NAME = 0x00000005;
		public const int MAILSLOT_NO_MESSAGE = -1;
		public const int MAILSLOT_WAIT_FOREVER = -1;
		public const int FILE_CASE_SENSITIVE_SEARCH = 0x00000001;
		public const int FILE_CASE_PRESERVED_NAMES = 0x00000002;
		public const int FILE_UNICODE_ON_DISK = 0x00000004;
		public const int FILE_PERSISTENT_ACLS = 0x00000008;
		public const int FILE_FILE_COMPRESSION = 0x00000010;
		public const int FILE_VOLUME_QUOTAS = 0x00000020;
		public const int FILE_SUPPORTS_SPARSE_FILES = 0x00000040;
		public const int FILE_SUPPORTS_REPARSE_POINTS = 0x00000080;
		public const int FILE_SUPPORTS_REMOTE_STORAGE = 0x00000100;
		public const int FILE_VOLUME_IS_COMPRESSED = 0x00008000;
		public const int FILE_SUPPORTS_OBJECT_IDS = 0x00010000;
		public const int FILE_SUPPORTS_ENCRYPTION = 0x00020000;
		public const int FILE_NAMED_STREAMS = 0x00040000;
		public const int FILE_READ_ONLY_VOLUME = 0x00080000;

		//public const int SECURITY_ANONYMOUS = (int)SecurityImpersonationLevel.SecurityAnonymous << 16;
		//public const int SECURITY_IDENTIFICATION = (int)SecurityImpersonationLevel.SecurityIdentification << 16;
		//public const int SECURITY_IMPERSONATION = (int)SecurityImpersonationLevel.SecurityImpersonation << 16;
		//public const int SECURITY_DELEGATION = (int)SecurityImpersonationLevel.SecurityDelegation << 16;

		public const int SECURITY_CONTEXT_TRACKING = 0x00040000;
		public const int SECURITY_EFFECTIVE_ONLY = 0x00080000;

		public const int SECURITY_SQOS_PRESENT = 0x00100000;
		public const int SECURITY_VALID_SQOS_FLAGS = 0x001F0000;

		public const int DRIVE_UNKNOWN = 0;
		public const int DRIVE_NO_ROOT_DIR = 1;
		public const int DRIVE_REMOVABLE = 2;
		public const int DRIVE_FIXED = 3;
		public const int DRIVE_REMOTE = 4;
		public const int DRIVE_CDROM = 5;
		public const int DRIVE_RAMDISK = 6;

		#region CloseHandle
		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool CloseHandle(IntPtr hObject);
		#endregion

		#region CopyFile
		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool CopyFile(string lpExistingFileName, string lpNewFileName, bool bFailIfExists);
		#endregion

		#region CreateDirectory
		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool CreateDirectory(string lpPathName, IntPtr lpSecurityAttributes);
		#endregion

		#region CreateFile
		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr CreateFile(string lpFileName, int dwDesiredAccess, int dwSharedMode, IntPtr lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);
		#endregion

		#region DeleteFile
		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool DeleteFile(string lpFileName);
		#endregion

		#region GetDriveType
		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, SetLastError = false)]
		public static extern int GetDriveType(string lpRootPathName);
		#endregion

		#region ReadFile
		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool ReadFile(IntPtr hFile, IntPtr lpBuffer, int nNumberOfBytesToRead, out int lpNumberOfBytesRead, IntPtr lpOverlapped);
		#endregion

		#region WriteFile
		[DllImport("Kernel32.Dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool WriteFile(IntPtr hFile, IntPtr lpBuffer, int nNumberOfBytesToWrite, out int lpNumberOfBytesWritten, IntPtr lpOverlapped);
		#endregion

	}
}
