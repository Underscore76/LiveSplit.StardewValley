using LiveSplit.ComponentUtil;
using System;
using System.Diagnostics;
using System.Text;

namespace LiveSplit.StardewValley
{
    public abstract class MemoryModel
    {
        private readonly Process Process;
        private readonly string CodeSignature;

        private IntPtr ReferencePointerAddress = IntPtr.Zero;
        private IntPtr ReferenceValueAddress = IntPtr.Zero;

        public bool FullyAttached => ReferencePointerAddress != IntPtr.Zero;

        public MemoryModel(Process process, string codeSignature)
        {
            Process = process;
            CodeSignature = codeSignature;

            ScanAgain();
        }

        public void ScanAgain()
        {
            int pointerIndex = CodeSignature.IndexOf('p') / 2;
            int valueIndex = CodeSignature.IndexOf('v') / 2;

            IntPtr codeLocation = ScanFor(CodeSignature);
            if (codeLocation != IntPtr.Zero)
            {
                ReferencePointerAddress = Process.ReadPointer(codeLocation + pointerIndex);
                ReferenceValueAddress = Process.ReadPointer(codeLocation + valueIndex);
            }
        }

        public static string RemoveWhitespace(params string[] str)
        {
            return string.Concat(str).Replace(" ", "");
        }

        private IntPtr ScanFor(string signature)
        {
            var target = new SigScanTarget(0, signature);
            foreach (var page in Process.MemoryPages())
            {
                if (page.State == MemPageState.MEM_COMMIT && page.Type == MemPageType.MEM_PRIVATE)
                {
                    var scanner = new SignatureScanner(Process, page.BaseAddress, (int)page.RegionSize);
                    var location = scanner.Scan(target);
                    if (location != IntPtr.Zero)
                    {
                        return location;
                    }
                }
            }
            return IntPtr.Zero;
        }

        protected T ReadValue<T>(int[] offsets, T defaultValue = default(T))
            where T : struct
        {
            if (TryReadOffsets(true, offsets, out var address))
            {
                return Process.ReadValue(address, defaultValue);
            }
            return defaultValue;
        }

        protected IntPtr ReadPointer(int[] offsets, IntPtr defaultValue = default(IntPtr))
        {
            if (TryReadOffsets(false, offsets, out var address))
            {
                return Process.ReadPointer(address, defaultValue);
            }
            return defaultValue;
        }

        public string ReadString(int[] offsets, string defaultValue = null)
        {
            if (TryReadOffsets(false, offsets, out var address) &&
                Process.ReadPointer(address, out address) &&
                Process.ReadValue<int>(address + 4, out var len) &&
                Process.ReadBytes(address + 8, len * 2, out var bytes))
            {
                return Encoding.Unicode.GetString(bytes);
            }
            return defaultValue;
        }

        private bool TryReadOffsets(bool isValue, int[] offsets, out IntPtr address)
        {
            if (isValue && offsets.Length == 1)
            {
                address = ReferenceValueAddress + offsets[0];
                return true;
            }

            address = ReferencePointerAddress + offsets[0];
            for (int i = 1; i < offsets.Length; i++)
            {
                if (!Process.ReadPointer(address, out address)) return false;
                address += offsets[i];
            }
            return true;
        }

        public abstract bool IsPaused { get; }
        public abstract bool IsSaving { get; }
        public abstract bool IsConstructingGraphics { get; }
    }

    public class MemoryModel_2 : MemoryModel
    {
        private static readonly string CodeSignature = RemoveWhitespace(
            "0fb6 15 vvvvvvvv", // _gameMode
            "85d2",
            "0f85 ????????",
            "83 3d pppppppp 00"); // _activeClickableMenu
        public MemoryModel_2(Process process) : base(process, CodeSignature) { }

        // does not exist
        public override bool IsPaused => false;

        // game1._isSaving
        private readonly int[] SavingOffsets = { 100, 160 };
        public override bool IsSaving => ReadValue<bool>(SavingOffsets);

        // graphics.inDeviceTransition
        private readonly int[] ConstructingGraphicsOffsets = { -384, 74 };
        public override bool IsConstructingGraphics => ReadValue<bool>(ConstructingGraphicsOffsets);
    }

    public class MemoryModel_3 : MemoryModel
    {
        private static readonly string CodeSignature = RemoveWhitespace(
            "0fb6 15 vvvvvvvv", // _gameMode
            "85d2",
            "0f85 ????????",
            "83 3d pppppppp 00"); // _activeClickableMenu
        public MemoryModel_3(Process process) : base(process, CodeSignature) { }

        // netWorldState.(value as NetWorldState).isPaused.value]
        private readonly int[] PausedOffsets = { 108, 36, 48, 41 };
        public override bool IsPaused => ReadValue<bool>(PausedOffsets);

        // game1._isSaving
        private readonly int[] SavingOffsets = { 100, 164 };
        public override bool IsSaving => ReadValue<bool>(SavingOffsets);

        // graphics.inDeviceTransition
        private readonly int[] ConstructingGraphicsOffsets = { -400, 74 };
        public override bool IsConstructingGraphics => ReadValue<bool>(ConstructingGraphicsOffsets);
    }
}
