using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.App.Entities.Common
{

    //Hàm được sử dụng để chạy xác thực AD trước khi lưu ảnh trong module Quan hệ
    public sealed class SafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
    {

        private SafeTokenHandle()
                : base(true)
        {
        }

        [DllImport("kernel32.dll")]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr handle);

        protected override bool ReleaseHandle()
        {
            return CloseHandle(handle);
        }


    }
}
