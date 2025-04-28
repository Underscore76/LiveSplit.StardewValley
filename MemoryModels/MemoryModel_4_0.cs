using System;
using System.Diagnostics;

namespace LiveSplit.StardewValley.MemoryModels
{
    public class MemoryModel_4_0 : MemoryModel
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

        public MemoryModel_4_0(Process process) : base(process, CodeSignature) { }

        // Game1.game1._isSaving
        private readonly int[] SavingOffsets = { 104, 176 };
        public override bool IsSaving => ReadValue<bool>(SavingOffsets);

        // Game.graphics.inDeviceTransition
        private readonly int[] ConstructingGraphicsOffsets = { -476, 74 };
        public override bool IsConstructingGraphics => ReadValue<bool>(ConstructingGraphicsOffsets);

        // Game1._newDayTask
        private readonly int[] NewDayTaskOffsets = { 144 };
        public override bool NewDayTaskExists => ReadValue<IntPtr>(NewDayTaskOffsets) != IntPtr.Zero;

        // (Game1.activeClickableMenu as TitleMenu).startupMessageColorOffsets
        private readonly int[] TitleMenu_StartupMessageColorOffsets = { 0, 236 };
        public override bool IsTitleMenu => (ReadValue<uint>(TitleMenu_StartupMessageColorOffsets) == TitleMenu_DeepSkyBlue);

        // Game1.options.zoomButttons
        private readonly int[] EnableZoomOffsets = { 100, 185 };
        public override void EnableZoomButton() { WriteValue<bool>(EnableZoomOffsets, true); }

        // Game1.options.ambientVolumeLevel
        private readonly int[] AmbientVolumeOffsets = { 100, 136 };
        public override void SetAmbientVolume(int level) { WriteValue<float>(AmbientVolumeOffsets, level / 100f); }

        // Game1.options.footstepVolumeLevel
        private readonly int[] FootstepVolumeOffsets = { 100, 132 };
        public override void SetFootstepVolume(int level) { WriteValue<float>(FootstepVolumeOffsets, level / 100f); }

        // Game1.options.musicVolumeLevel
        private readonly int[] MusicVolumeOffsets = { 100, 120 };
        public override void SetMusicVolume(int level) { WriteValue<float>(MusicVolumeOffsets, level / 100f); }

        // Game1.options.soundVolumeLevel
        private readonly int[] SoundVolumeOffsets = { 100, 124 };
        public override void SetSoundVolume(int level) { WriteValue<float>(SoundVolumeOffsets, level / 100f); }

        // Game1.options.alwaysShowToolHitLocation
        private readonly int[] ToolHitOffsets = { 100, 176 };
        public override void ToolHitIndicator() { WriteValue<bool>(ToolHitOffsets, true); }

        // (Game1.netWorldState.value as NetWorldState).isPaused.value
        private readonly int[] PausedOffsets = { 116, 36, 48, 41 };
        public override bool IsPaused => ReadValue<bool>(PausedOffsets);

        // Game1.options.chatButtons
        // the SECOND offset would be at 10, I'm not exactly sure I understand why
        private readonly int[] ChatButtonOffsets = { 100, 48, 8 };
        public override void UnbindChatButton() { WriteValue<int>(ChatButtonOffsets, AttentionKey); }

        // Game1.options.emoteButton
        private readonly int[] EmoteButtonOffsets = { 100, 112, 8 };
        public override void UnbindEmoteButton() { WriteValue<int>(EmoteButtonOffsets, AttentionKey); }

        // Game1.options.showAdvancedCraftingInformation
        private readonly int[] AdvancedCraftingOffsets = { 100, 194 };
        public override void AdvancedCrafting() { WriteValue<bool>(AdvancedCraftingOffsets, true); }

        // not avalable in v1.4
        public override void SlingshotMode(bool legacy) { }


        private readonly int[] CurrentLocationNameOffsets = { -440, 124, 36 };
        public override string CurrentLocationName => ReadString(CurrentLocationNameOffsets, "", 4);

        private readonly int[] DaysPlayedOffsets = { -464, 280, 172 };
        public override int DaysPlayed => (int)ReadValue<UInt32>(DaysPlayedOffsets);

        private readonly int[] Event_IsWeddingOffsets = { -440, 184, 191 };
        private readonly int[] Event_CurrentCommandOffsets = { -440, 184, 120 };
        private readonly int[] Event_EventIdOffsets = { -440, 184, 152 };
        public override bool Event_IsWedding => ReadValue<bool>(Event_IsWeddingOffsets, false);
        public override int Event_CurrentCommand => ReadValue<int>(Event_CurrentCommandOffsets, -1);
        public override string Event_EventId => ReadValue<int>(Event_EventIdOffsets, -1).ToString();

        private readonly int[] CommunityCenter_restoreAreaTimerOffsets = { -440, 324 };
        public override int CC_restoreAreaTimer => ReadValue<int>(CommunityCenter_restoreAreaTimerOffsets);

        private readonly int[] CommunityCenter_restoreAreaPhaseOffsets = { -440, 328 };
        public override int CC_restoreAreaPhase => ReadValue<int>(CommunityCenter_restoreAreaPhaseOffsets);

        private readonly int[] CommunityCenter_restoreAreaIndexOffsets = { -440, 332 };
        public override int CC_restoreAreaIndex => ReadValue<int>(CommunityCenter_restoreAreaIndexOffsets);

        private readonly int[] CommunityCenter_isWatchingJunimoGoodbyeOffsets = { -440, 336 };
        public override bool CC_isWatchingJunimoGoodbye => ReadValue<bool>(CommunityCenter_isWatchingJunimoGoodbyeOffsets);

        private readonly int[] ShopMenu_PPDOffsets = { 0, 132 };
        public override string ShopMenu_PersonPortraitDialogue => ReadString(ShopMenu_PPDOffsets, "", 4);
    }
}
