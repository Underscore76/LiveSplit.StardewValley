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

        //protected T ReadPointer<T>(IntPtr address, T defaultValue = default(T))
        //    where T : struct
        //{

        //}

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
        // used in determining runtime
        public abstract bool IsPaused { get; }
        public abstract bool IsSaving { get; }
        public abstract bool IsConstructingGraphics { get; }
        public abstract bool NewDayTaskExists { get; }
        public abstract bool IsTitleMenu { get; }
        // Settings
        public abstract void SetMusicVolume(int level);
        public abstract void SetSoundVolume(int level);
        public abstract void SetFootstepVolume(int level);
        public abstract void SetAmbientVolume(int level);
        public readonly int AttentionKey = 246;
        public abstract void UnbindEmoteButton();
        public abstract void UnbindChatButton();
        public abstract void EnableZoomButton();
        public abstract void AdvancedCrafting();
        public abstract void ToolHitIndicator();
        public abstract void SlingshotMode(bool legacy);
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

        public override bool NewDayTaskExists => throw new NotImplementedException();

        public override bool IsTitleMenu => throw new NotImplementedException();

        public override void SetMusicVolume(int level)
        {
            throw new NotImplementedException();
        }

        public override void SetSoundVolume(int level)
        {
            throw new NotImplementedException();
        }

        public override void SetFootstepVolume(int level)
        {
            throw new NotImplementedException();
        }

        public override void SetAmbientVolume(int level)
        {
            throw new NotImplementedException();
        }

        public override void UnbindEmoteButton()
        {
            throw new NotImplementedException();
        }

        public override void UnbindChatButton()
        {
            throw new NotImplementedException();
        }

        public override void EnableZoomButton()
        {
            throw new NotImplementedException();
        }

        public override void AdvancedCrafting()
        {
            throw new NotImplementedException();
        }

        public override void ToolHitIndicator()
        {
            throw new NotImplementedException();
        }

        public override void SlingshotMode(bool legacy)
        {
            throw new NotImplementedException();
        }
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

        public override bool NewDayTaskExists => throw new NotImplementedException();

        public override bool IsTitleMenu => throw new NotImplementedException();
        public override void SetMusicVolume(int level)
        {
            throw new NotImplementedException();
        }

        public override void SetSoundVolume(int level)
        {
            throw new NotImplementedException();
        }

        public override void SetFootstepVolume(int level)
        {
            throw new NotImplementedException();
        }

        public override void SetAmbientVolume(int level)
        {
            throw new NotImplementedException();
        }

        public override void UnbindEmoteButton()
        {
            throw new NotImplementedException();
        }

        public override void UnbindChatButton()
        {
            throw new NotImplementedException();
        }

        public override void EnableZoomButton()
        {
            throw new NotImplementedException();
        }

        public override void AdvancedCrafting()
        {
            throw new NotImplementedException();
        }

        public override void ToolHitIndicator()
        {
            throw new NotImplementedException();
        }

        public override void SlingshotMode(bool legacy)
        {
            throw new NotImplementedException();
        }
    }

    public class MemoryModel_5 : MemoryModel
    {
        private static readonly string CodeSignature = RemoveWhitespace(
            "8B C6",
            "A2 vvvvvvvv", // _gameMode
            "FF 15 ????????",
            "85 C0",
            "74 10",
            "FF 15 ????????",
            "8B C8",
            "8B 01",
            "8B 40 28",
            "FF 50 18",
            "85 F6",
            "74 10",
            "83 FE 03",
            "0F 84 ????????",
            "8D 65 F8",
            "5E",
            "5F",
            "5D",
            "C3",
            "33 FF",
            "83 3D pppppppp 00", // _activeClickableMenu
            "74 3C");
        public MemoryModel_5(Process process) : base(process, CodeSignature) { }

        // netWorldState.(value as NetWorldState).isPaused.value]
        private readonly int[] PausedOffsets = { 108, 36, 60, 41 };
        public override bool IsPaused => ReadValue<bool>(PausedOffsets);

        // game1._isSaving
        private readonly int[] SavingOffsets = { 96, 106 };
        public override bool IsSaving => ReadValue<bool>(SavingOffsets);

        // graphics.inDeviceTransition
        private readonly int[] ConstructingGraphicsOffsets = { -476, 74 };
        public override bool IsConstructingGraphics => ReadValue<bool>(ConstructingGraphicsOffsets);

        // Game1._newDayTask
        private readonly int[] NewDayTaskOffsets = { 136 };
        public override bool NewDayTaskExists => ReadValue<IntPtr>(NewDayTaskOffsets) != IntPtr.Zero;

        // (Game1.activeClickableMenu as TitleMenu).StartupMessageColor
        private readonly int[] TitleMenu_StartupMessageColorOffsets = { 0, 332 };
        private readonly uint DeepSkyBlue = 4294950656;
        public override bool IsTitleMenu => (ReadValue<uint>(TitleMenu_StartupMessageColorOffsets) == DeepSkyBlue);

        // Game1.options.musicVolumeLevel
        private readonly int[] MusicVolumeOffsets = { 96, 40, 124 };
        public override void SetMusicVolume(int level)
        {
            WriteValue<float>(MusicVolumeOffsets, level / 100f);
        }

        // Game1.options.soundVolumeLevel
        private readonly int[] SoundVolumeOffsets = { 96, 40, 128 };
        public override void SetSoundVolume(int level)
        {
            WriteValue<float>(SoundVolumeOffsets, level / 100f);
        }

        // Game1.options.footstepVolumeLevel
        private readonly int[] FootstepVolumeOffsets = { 96, 40, 132 };
        public override void SetFootstepVolume(int level)
        {
            WriteValue<float>(FootstepVolumeOffsets, level / 100f);
        }

        // Game1.options.ambientVolumeLevel
        private readonly int[] AmbientVolumeOffsets = { 96, 40, 136 };
        public override void SetAmbientVolume(int level)
        {
            WriteValue<float>(AmbientVolumeOffsets, level / 100f);
        }

        // Game1.options.emoteButton
        private readonly int[] EmoteButtonOffsets = { 96, 40, 112, 8 };
        public override void UnbindEmoteButton()
        {
            WriteValue<int>(EmoteButtonOffsets, AttentionKey);
        }

        // Game1.options.chatButton
        // the SECOND offset would be at 10, I'm not exactly sure I understand why
        private readonly int[] ChatButtonOffsets = { 96, 40, 48, 8 };
        public override void UnbindChatButton()
        {
            WriteValue<int>(ChatButtonOffsets, AttentionKey);
        }

        // Game1.options.enableZoom
        private readonly int[] EnableZoomOffsets = { 96, 40, 205 };
        public override void EnableZoomButton()
        {
            WriteValue<bool>(EnableZoomOffsets, true);
        }

        private readonly int[] AdvancedCraftingOffsets = { 96, 40, 213 };
        public override void AdvancedCrafting()
        {
            WriteValue<bool>(AdvancedCraftingOffsets, true);
        }

        private readonly int[] ToolHitOffsets = { 96, 40, 196 };
        public override void ToolHitIndicator()
        {
            WriteValue<bool>(ToolHitOffsets, true);
        }

        private readonly int[] SlingshotModeOffset = { 96, 40, 218 };
        public override void SlingshotMode(bool legacy)
        {
            WriteValue<bool>(SlingshotModeOffset, legacy);
        }
    }
}
