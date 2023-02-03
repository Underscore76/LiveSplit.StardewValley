using LiveSplit.ComponentUtil;
using System;
using System.Diagnostics;
using System.Text;
using LiveSplit.Options;

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

        protected void WriteValue<T>(int[] offsets, T value)
            where T : struct
        {
            int[] ptrOffsets = new int[offsets.Length - 1];
            Array.Copy(offsets, ptrOffsets, ptrOffsets.Length);
            int last = offsets[ptrOffsets.Length];
            IntPtr ptr = ReadPointer(ptrOffsets);
            if (ptr != IntPtr.Zero)
            {
                Process.WriteValue(ptr + last, value);
            }
        }

        protected IntPtr ReadPointer(int[] offsets, IntPtr defaultValue = default(IntPtr))
        {
            if (TryReadOffsets(false, offsets, out var address))
            {
                return Process.ReadPointer(address, defaultValue);
            }
            return defaultValue;
        }

        public string ReadString(int[] offsets, string defaultValue = null, int ptrWidth = 4)
        {
            try
            {
                if (TryReadOffsets(false, offsets, out var address) &&
                    Process.ReadPointer(address, out address) &&
                    Process.ReadValue<int>(address + ptrWidth, out var len) &&
                    Process.ReadBytes(address + ptrWidth + 4, len * 2, out var bytes))
                {
                    return Encoding.Unicode.GetString(bytes);
                }
            }
            catch { }
            return defaultValue;
        }

        private bool TryReadOffsets(bool isValue, int[] offsets, out IntPtr address)
        {

            if (isValue && offsets.Length == 1)
            {
                //Log.Info(string.Format("[SDV] address: 0x{0}", ReferenceValueAddress.ToString("X")));
                address = ReferenceValueAddress + offsets[0];
                //Log.Info(string.Format("[SDV]       address: 0x{0}", address.ToString("X")));
                return true;
            }
            //Log.Info(string.Format("[SDV] address: 0x{0}", ReferencePointerAddress.ToString("X")));
            address = ReferencePointerAddress + offsets[0];
            for (int i = 1; i < offsets.Length; i++)
            {
                //Log.Info(string.Format("[SDV]       address: 0x{0}", address.ToString("X")));
                if (!Process.ReadPointer(address, out address)) return false;
                address += offsets[i];
            }
            //Log.Info(string.Format("[SDV]       address: 0x{0}", address.ToString("X")));
            return true;
        }
        // used in determining runtime
        public virtual bool IsPaused => false;
        public virtual bool IsSaving => false;
        public virtual bool IsConstructingGraphics => false;
        public virtual bool NewDayTaskExists => false;
        public virtual bool IsTitleMenu => false;
        protected readonly uint TitleMenu_DeepSkyBlue = 4294950656;

        // split hook data
        // Game1.stats.DaysPlayed
        public virtual string CurrentLocationName => "";
        public virtual string ShopMenu_PersonPortraitDialogue => "";
        public virtual int DaysPlayed => -1;
        public bool IsWeddingHearts => Event_IsWedding && Event_CurrentCommand > 22;
        public bool JojaVendingMachine => Event_EventId == 502261 && Event_CurrentCommand > 21;
        public virtual bool Event_IsWedding => false;
        public virtual int Event_EventId => -1;
        public virtual int Event_CurrentCommand => -1;
        public virtual bool CC_isWatchingJunimoGoodbye => false;
        public virtual int CC_restoreAreaIndex => -1;
        public virtual int CC_restoreAreaTimer => -1;
        public virtual int CC_restoreAreaPhase => -1;

        // Settings
        public virtual void SetMusicVolume(int level) { }
        public virtual void SetSoundVolume(int level) { }
        public virtual void SetFootstepVolume(int level) { }
        public virtual void SetAmbientVolume(int level) { }
        public readonly int AttentionKey = 246;
        public virtual void UnbindEmoteButton() { }
        public virtual void UnbindChatButton() { }
        public virtual void EnableZoomButton() { }
        public virtual void AdvancedCrafting() { }
        public virtual void ToolHitIndicator() { }
        public virtual void SlingshotMode(bool legacy) { }
    }
}
