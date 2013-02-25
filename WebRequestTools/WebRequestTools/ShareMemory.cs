using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace WebRequestTools
{
    public class ShareMemory
    {
        static string memName = "memShare";
        static long memSize = 1024;// * 5;
        int id = 0;

        static ShareMemory()
        {
            init(memName, memSize);
        }

        int Memoryoffset
        {
            get
            {
                return id * 1024;
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateFileMapping(int hFile, IntPtr lpAttributes, uint flProtect, uint dwMaxSizeHi, uint dwMaxSizeLow, string lpName);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr OpenFileMapping(int dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, string lpName);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr MapViewOfFile(IntPtr hFileMapping, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool UnmapViewOfFile(IntPtr pvBaseAddress);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32", EntryPoint = "GetLastError")]
        public static extern int GetLastError();

        const int ERROR_ALREADY_EXISTS = 183;

        const int FILE_MAP_COPY = 0x0001;
        const int FILE_MAP_WRITE = 0x0002;
        const int FILE_MAP_READ = 0x0004;
        const int FILE_MAP_ALL_ACCESS = 0x0002 | 0x0004;

        const int PAGE_READONLY = 0x02;
        const int PAGE_READWRITE = 0x04;
        const int PAGE_WRITECOPY = 0x08;
        const int PAGE_EXECUTE = 0x10;
        const int PAGE_EXECUTE_READ = 0x20;
        const int PAGE_EXECUTE_READWRITE = 0x40;

        const int SEC_COMMIT = 0x8000000;
        const int SEC_IMAGE = 0x1000000;
        const int SEC_NOCACHE = 0x10000000;
        const int SEC_RESERVE = 0x4000000;

        const int INVALID_HANDLE_VALUE = -1;

        static bool initSuccess = false;

        static IntPtr m_hSharedMemoryFile = IntPtr.Zero;
        static IntPtr MemoryPointer = IntPtr.Zero;

        static Semaphore semWrite;

        /// 
        /// 初始化共享内存
        /// 
        /// 共享内存名称
        /// 共享内存大小
        /// 
        public static void init(string strName, long lngSize)
        {

            m_hSharedMemoryFile = CreateFileMapping(INVALID_HANDLE_VALUE, IntPtr.Zero, (uint)PAGE_READWRITE, 0, (uint)lngSize, strName);

            if (GetLastError() == ERROR_ALREADY_EXISTS) //已经创建
            {
                m_hSharedMemoryFile = OpenFileMapping(FILE_MAP_ALL_ACCESS, false, strName);
            }

            if (m_hSharedMemoryFile == IntPtr.Zero)
            {
                return;
            }


            //---------------------------------------
            //创建内存映射
            MemoryPointer = MapViewOfFile(m_hSharedMemoryFile, FILE_MAP_ALL_ACCESS, 0, 0, (uint)lngSize);

            if (MemoryPointer == IntPtr.Zero)
            {
                CloseHandle(m_hSharedMemoryFile);
                return;
            }
            //---------------------------------------


            SetSemaphore();

            initSuccess = true;
        }

        /// 
        /// 关闭共享内存
        /// 
        public static void Close()
        {
            if (initSuccess)
            {
                UnmapViewOfFile(MemoryPointer);
                CloseHandle(m_hSharedMemoryFile);
            }
        }

        public static bool SetSemaphore()
        {
            try
            {
                semWrite = Semaphore.OpenExisting("WriteShareMemory");
            }
            catch (Exception)
            {
                semWrite = new Semaphore(1, 1, "WriteShareMemory");
            }
            return true;
        }

        public static Int32 getdata_Int32()
        {
            if (initSuccess == false)
            {
                return -1;
            }

            Byte[] bytData = new Byte[sizeof(Int32)];

            Marshal.Copy(MemoryPointer, bytData, 0, bytData.Length);

            return BitConverter.ToInt32(bytData, 0);
        }

        public static void setdata_bool(bool data)
        {
            if (initSuccess == false)
            {
                return;
            }

            Byte[] bytData = BitConverter.GetBytes(data);

            Marshal.Copy(bytData, 0, MemoryPointer, bytData.Length);
        }

        public static bool getdata_bool()
        {
            if (initSuccess == false)
            {
                return false;
            }

            Byte[] bytData = new Byte[sizeof(bool)];

            Marshal.Copy(MemoryPointer, bytData, 0, bytData.Length);

            return BitConverter.ToBoolean(bytData, 0);
        }

        public static void setdata_Int32(int data)
        {
            if (initSuccess == false)
            {
                return;
            }

            Byte[] bytData = BitConverter.GetBytes(data);

            Marshal.Copy(bytData, 0, MemoryPointer, bytData.Length);
        }

        public static double getdata_double()
        {
            if (initSuccess == false)
            {
                return -1;
            }

            Byte[] bytData = new Byte[sizeof(double)];

            Marshal.Copy(MemoryPointer, bytData, 0, bytData.Length);

            return BitConverter.ToDouble(bytData, 0);
        }


        public static void setdata_double(double data)
        {
            if (initSuccess == false)
            {
                return;
            }

            Byte[] bytData = BitConverter.GetBytes(data);

            semWrite.WaitOne(1000);
            Marshal.Copy(bytData, 0, MemoryPointer, bytData.Length);
            semWrite.Release();
        }

        public static string getdata_string(Encoding encode)
        {
            if (initSuccess == false)
            {
                return "";
            }
            if (encode == null)
            { encode = Encoding.Default; }

            Byte[] bytData = new Byte[1024];

            Marshal.Copy(MemoryPointer, bytData, 0, bytData.Length);

            return encode.GetString(bytData).Trim('\0');
            //return BitConverter.ToString(bytData, 0);
        }

        public static void setdata_string(string data, Encoding encode)
        {
            if (initSuccess == false)
            {
                return;
            }

            if (encode == null)
            { encode = Encoding.Default; }

            Byte[] bytData = new Byte[1024];
            Marshal.Copy(bytData, 0, MemoryPointer, 1024);

            bytData = encode.GetBytes(data);

            semWrite.WaitOne(1000);
            Marshal.Copy(bytData, 0, MemoryPointer, bytData.Length);
            semWrite.Release();
        }

        //字符串
        //String str = System.Text.Encoding.Unicode.GetString(bytData).Trim('\0');
        //Byte[] bytData = System.Text.Encoding.Unicode.GetBytes(strLengthAndCount);

    }
}
