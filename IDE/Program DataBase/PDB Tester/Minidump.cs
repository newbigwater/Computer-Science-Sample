using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace PDB_Tester
{
    public class Minidump
    {
        [Flags]
        public enum _MINIDUMP_TYPE
        {
            MiniDumpNormal = 0x00000000,
            MiniDumpWithDataSegs = 0x00000001,
            MiniDumpWithFullMemory = 0x00000002,
            MiniDumpWithHandleData = 0x00000004,
            MiniDumpFilterMemory = 0x00000008,
            MiniDumpScanMemory = 0x00000010,
            MiniDumpWithUnloadedModules = 0x00000020,
            MiniDumpWithIndirectlyReferencedMemory = 0x00000040,
            MiniDumpFilterModulePaths = 0x00000080,
            MiniDumpWithProcessThreadData = 0x00000100,
            MiniDumpWithPrivateReadWriteMemory = 0x00000200,
            MiniDumpWithoutOptionalData = 0x00000400,
            MiniDumpWithFullMemoryInfo = 0x00000800,
            MiniDumpWithThreadInfo = 0x00001000,
            MiniDumpWithCodeSegs = 0x00002000,
            MiniDumpWithoutAuxiliaryState = 0x00004000,
            MiniDumpWithFullAuxiliaryState = 0x00008000,
            MiniDumpWithPrivateWriteCopyMemory = 0x00010000,
            MiniDumpIgnoreInaccessibleMemory = 0x00020000,
            MiniDumpWithTokenInformation = 0x00040000,
            MiniDumpValidTypeFlags = 0x0007ffff,
        };

        // Pack=4 is important! So it works also for x64!
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        struct MiniDumpExceptionInformation
        {
            public uint ThreadId;
            public IntPtr ExceptioonPointers;
            [MarshalAs(UnmanagedType.Bool)]
            public bool ClientPointers;
        }

        [DllImport("dbghelp.dll",
          EntryPoint = "MiniDumpWriteDump",
          CallingConvention = CallingConvention.StdCall,
          CharSet = CharSet.Unicode,
          ExactSpelling = true,
          SetLastError = true)]
        static extern bool MiniDumpWriteDump(
          IntPtr hProcess,
          uint processId,
          IntPtr hFile,
          _MINIDUMP_TYPE dumpType,
          ref MiniDumpExceptionInformation expParam,
          IntPtr userStreamParam,
          IntPtr callbackParam);

        [DllImport("kernel32.dll", EntryPoint = "GetCurrentThreadId", ExactSpelling = true)]
        static extern uint GetCurrentThreadId();

        [DllImport("kernel32.dll", EntryPoint = "GetCurrentProcess", ExactSpelling = true)]
        static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll", EntryPoint = "GetCurrentProcessId", ExactSpelling = true)]
        static extern uint GetCurrentProcessId();

        [DllImport("kernel32.dll", EntryPoint = "TerminateProcess", ExactSpelling = true)]
        static extern Int32 TerminateProcess(IntPtr hprocess, Int32 ExitCode);

        public static void install_self_mini_dump()
        {
            MiniDumpExceptionInformation exp;

            exp.ThreadId = GetCurrentThreadId();
            exp.ClientPointers = false;
            exp.ExceptioonPointers = System.Runtime.InteropServices.Marshal.GetExceptionPointers();

            //덤프 파일 이름
            string dt = DateTime.Now.ToString("yyyy.MM.dd.hh.mm.ss");
            string fileName = dt + ".dmp";

            //덤프 파일 만들기
            using (var fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None))
            {
                bool bRet = MiniDumpWriteDump(
                  GetCurrentProcess(),
                  GetCurrentProcessId(),
                  fs.SafeFileHandle.DangerousGetHandle(),
                  //덤프 파일에 포함할 정보 설정
                  _MINIDUMP_TYPE.MiniDumpNormal,
                  ref exp,
                  IntPtr.Zero,
                  IntPtr.Zero);
            }

            //프로그램 강제 종료
            TerminateProcess(GetCurrentProcess(), 0);
        }
    }
}
