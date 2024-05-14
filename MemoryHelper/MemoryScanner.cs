using LiveSplit.ComponentUtil;
using Microsoft.Diagnostics.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MemoryHelper
{
    public class MemoryScanner
    {
        public static MemoryScanner TryAttach()
        {
            Process process = Process.GetProcesses().FirstOrDefault(p => p.ProcessName == "Stardew Valley" && !p.HasExited);
            if (process != null)
            {
                DataTarget dataTarget = DataTarget.AttachToProcess(process.Id, false);
                if (dataTarget == null || dataTarget.ClrVersions.Count() == 0)
                {
                    return null;
                }
                return new MemoryScanner(process, dataTarget);
            }
            return null;
        }

        private Process Process;
        private ClrRuntime Runtime;

        public MemoryScanner(Process process, DataTarget dataTarget)
        {
            Process = process;
            Runtime = dataTarget.ClrVersions[0].CreateRuntime();
        }

        public bool HasExited => Process.HasExited;

        public string FileVersion => Process.MainModule.FileVersionInfo.FileVersion;

        public static readonly int ObjectHeaderSize = IntPtr.Size;

        public ClrType GetTypeByName(string type)
        {
            return Runtime.Heap.GetTypeByName(type);
        }

        public IntPtr GetAddress(ClrStaticField field)
        {
            return new IntPtr((long)field.GetAddress(Runtime.AppDomains.First()));
        }

        public bool TryReadPointer(IntPtr address, out IntPtr value)
        {
            return Process.ReadPointer(address, false, out value);
        }

        public IntPtr ReadPointer(IntPtr address, IntPtr def = default(IntPtr))
        {
            return Process.ReadPointer(address, def);
        }

        public byte[] ReadBytes(IntPtr address, int count, byte[] def = null)
        {
            if (Process.ReadBytes(address, count, out var bytes))
            {
                return bytes;
            }
            return def;
        }

        public T ReadValue<T>(IntPtr address, T def = default(T))
            where T : struct
        {
            Debug.Assert(typeof(T) != typeof(IntPtr), "Use ReadPointer to read an IntPtr");
            return Process.ReadValue(address, def);
        }

        public bool TryReadString(IntPtr address, out string value)
        {
            int len = ReadValue<int>(address + ObjectHeaderSize, -1);
            if (len <= 0)
            {
                value = "";
                return len == 0;
            }
            else
            {
                byte[] bytes = ReadBytes(address + ObjectHeaderSize + sizeof(int), len * 2);
                if (bytes == null)
                {
                    value = "";
                    return false;
                }
                else
                {
                    value = Encoding.Unicode.GetString(bytes);
                    return true;
                }
            }
        }

        public string ReadString(IntPtr address, string def = "")
        {
            int len = ReadValue<int>(address + ObjectHeaderSize, -1);
            if (len < 0) return def;
            if (len == 0) return "";
            byte[] bytes = ReadBytes(address + ObjectHeaderSize + sizeof(int), len * 2);
            if (bytes == null) return def;
            return Encoding.Unicode.GetString(bytes);
        }

        public IEnumerable<IntPtr> ScanFor(string signature)
        {
            var target = new SigScanTarget(0, signature);
            foreach (var page in Process.MemoryPages())
            {
                if (page.State == MemPageState.MEM_COMMIT && page.Type == MemPageType.MEM_PRIVATE)
                {
                    var scanner = new SignatureScanner(Process, page.BaseAddress, (int)page.RegionSize);
                    foreach (IntPtr loc in scanner.ScanAll(target))
                    {
                        yield return loc;
                    }
                }
            }
        }

        public void Dispose()
        {
            Runtime.DataTarget.Dispose();
        }
    }
}
