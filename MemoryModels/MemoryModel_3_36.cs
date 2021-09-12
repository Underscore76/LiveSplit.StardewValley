using System;
using System.Diagnostics;

namespace LiveSplit.StardewValley.MemoryModels
{
    public class MemoryModel_3_36 : MemoryModel
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
            "0F 84 8F000000",
            "8D 65 F8",
            "5E",
            "5F",
            "5D",
            "C3",
            "33 FF",
            "83 3D pppppppp 00"); // _activeClickableMenu

        public MemoryModel_3_36(Process process) : base(process, CodeSignature) { }

        // Game1.game1._isSaving
        private readonly int[] SavingOffsets = { 104, 172 };
        public override bool IsSaving => ReadValue<bool>(SavingOffsets);

        // Game.graphics.inDeviceTransition
        private readonly int[] ConstructingGraphicsOffsets = { -400, 74 };
        public override bool IsConstructingGraphics => ReadValue<bool>(ConstructingGraphicsOffsets);

        // Game1._newDayTask
        private readonly int[] NewDayTaskOffsets = { 128 };
        public override bool NewDayTaskExists => ReadValue<IntPtr>(NewDayTaskOffsets) != IntPtr.Zero;

        // (Game1.activeClickableMenu as TitleMenu).startupMessageColorOffsets
        private readonly int[] TitleMenu_StartupMessageColorOffsets = { 0, 208 };
        public override bool IsTitleMenu => (ReadValue<uint>(TitleMenu_StartupMessageColorOffsets) == TitleMenu_DeepSkyBlue);

        // Game1.options.zoomButttons
        private readonly int[] EnableZoomOffsets = { 100, 173 };
        public override void EnableZoomButton() { WriteValue<bool>(EnableZoomOffsets, true); }

        // Game1.options.ambientVolumeLevel
        private readonly int[] AmbientVolumeOffsets = { 100, 124 };
        public override void SetAmbientVolume(int level) { WriteValue<float>(AmbientVolumeOffsets, level / 100f); }

        // Game1.options.footstepVolumeLevel
        private readonly int[] FootstepVolumeOffsets = { 100, 120 };
        public override void SetFootstepVolume(int level) { WriteValue<float>(FootstepVolumeOffsets, level / 100f); }

        // Game1.options.musicVolumeLevel
        private readonly int[] MusicVolumeOffsets = { 100, 108 };
        public override void SetMusicVolume(int level) { WriteValue<float>(MusicVolumeOffsets, level / 100f); }

        // Game1.options.soundVolumeLevel
        private readonly int[] SoundVolumeOffsets = { 100, 112 };
        public override void SetSoundVolume(int level) { WriteValue<float>(SoundVolumeOffsets, level / 100f); }

        // Game1.options.alwaysShowToolHitLocation
        private readonly int[] ToolHitOffsets = { 100, 164 };
        public override void ToolHitIndicator() { WriteValue<bool>(ToolHitOffsets, true); }

        // (Game1.netWorldState.value as NetWorldState).isPaused.value
        private readonly int[] PausedOffsets = { 112, 36, 48, 41 };
        public override bool IsPaused => ReadValue<bool>(PausedOffsets);

        // Game1.options.chatButtons
        // the SECOND offset would be at 10, I'm not exactly sure I understand why
        private readonly int[] ChatButtonOffsets = { 100, 48, 8 };
        public override void UnbindChatButton() { WriteValue<int>(ChatButtonOffsets, AttentionKey); }


        // not avalable in v1.3
        public override void SlingshotMode(bool legacy) { }
        public override void AdvancedCrafting() { }
        public override void UnbindEmoteButton() { }
    }
}
