using System;
using System.Diagnostics;

namespace LiveSplit.StardewValley.MemoryModels
{
    public class MemoryModel_2_33 : MemoryModel
    {
        private static readonly string CodeSignature = RemoveWhitespace(
            "8B C6",
            "25 FF000000",
            "A2 vvvvvvvv", // _gameMode
            "FF 15 ????????",
            "85 C0",
            "74 10",
            "FF 15 ????????",
            "8B C8",
            "8B 01",
            "8B 40 28",
            "FF 50 18",
            "81 E6 FF000000",
            "75 78",
            "83 3D pppppppp 00"); // _activeClickableMenu

        public MemoryModel_2_33(Process process) : base(process, CodeSignature) { }

        // Game1.game1._isSaving
        private readonly int[] SavingOffsets = { 100, 160 };
        public override bool IsSaving => ReadValue<bool>(SavingOffsets);

        // Game.graphics.inDeviceTransition
        private readonly int[] ConstructingGraphicsOffsets = { -384, 74 };
        public override bool IsConstructingGraphics => ReadValue<bool>(ConstructingGraphicsOffsets);

        // Game1._newDayTask
        private readonly int[] NewDayTaskOffsets = { 112 };
        public override bool NewDayTaskExists => ReadValue<IntPtr>(NewDayTaskOffsets) != IntPtr.Zero;

        // (Game1.activeClickableMenu as TitleMenu).startupMessageColorOffsets
        private readonly int[] TitleMenu_StartupMessageColorOffsets = { 0, 208 };
        public override bool IsTitleMenu => (ReadValue<uint>(TitleMenu_StartupMessageColorOffsets) == TitleMenu_DeepSkyBlue);

        // Game1.options.zoomButttons
        private readonly int[] EnableZoomOffsets = { 96, 169 };
        public override void EnableZoomButton() { WriteValue<bool>(EnableZoomOffsets, true); }

        // Game1.options.ambientVolumeLevel
        private readonly int[] AmbientVolumeOffsets = { 96, 124 };
        public override void SetAmbientVolume(int level) { WriteValue<float>(AmbientVolumeOffsets, level / 100f); }

        // Game1.options.footstepVolumeLevel
        private readonly int[] FootstepVolumeOffsets = { 96, 120 };
        public override void SetFootstepVolume(int level) { WriteValue<float>(FootstepVolumeOffsets, level / 100f); }

        // Game1.options.musicVolumeLevel
        private readonly int[] MusicVolumeOffsets = { 96, 108 };
        public override void SetMusicVolume(int level) { WriteValue<float>(MusicVolumeOffsets, level / 100f); }

        // Game1.options.soundVolumeLevel
        private readonly int[] SoundVolumeOffsets = { 96, 112 };
        public override void SetSoundVolume(int level) { WriteValue<float>(SoundVolumeOffsets, level / 100f); }

        // Game1.options.alwaysShowToolHitLocation
        private readonly int[] ToolHitOffsets = { 96, 160 };
        public override void ToolHitIndicator() { WriteValue<bool>(ToolHitOffsets, true); }

        // not avalable in v1.2
        public override bool IsPaused => false;
        public override void SlingshotMode(bool legacy) { }
        public override void AdvancedCrafting() { }
        public override void UnbindChatButton() { }
        public override void UnbindEmoteButton() { }
    }
}
