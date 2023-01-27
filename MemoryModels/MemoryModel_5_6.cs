using System;
using System.Diagnostics;

namespace LiveSplit.StardewValley.MemoryModels
{
    public class MemoryModel_5_6 : MemoryModel
    {
        private static readonly string CodeSignature = RemoveWhitespace( // Game1::setGameMode
                    "48 B8 vvvvvvvvvvvvvvvv", // _gameMode = mode
                    "40 88 30",
                    // if (temporaryContent != null) {
                    "E8 ????????", // Game1::get_temporaryContent()
                    "48 85 C0", // (temporaryContent == null)
                    "74 12", // if above check true, jump
                             // unload
                    "E8 ????????", // Game1::get_temporaryContent()
                    "48 8B C8",
                    "48 8B 00",
                    "48 8B 40 48",
                    "FF 50 20",// temporaryContent.Unload()

                    // } ENDIF
                    // switch(mode)
                    //  case 0 : jump to label 0x6D 
                    "85 DB",
                    "74 11",
                    // case 3: jump to label 0x128 (not here)
                    "83 FB 03",
                    "0F84 ????????",
                    //// otherwise return
                    "48 83 C4 20",
                    "5B",
                    "5E",
                    "5F",
                    "C3",
                    // label 0x6D
                    "33 F6", // flag = false
                    "48 B9 pppppppppppppppp", // the pointer to Game1._activeClickableMenu
                    "48 83 39 00" // check it's not null
                    );
        public MemoryModel_5_6(Process process) : base(process, CodeSignature) { }

        // netWorldState.(value as NetWorldState).isPaused.value]
        private readonly int[] PausedOffsets = { 208, 64, 136, 69 };
        public override bool IsPaused => ReadValue<bool>(PausedOffsets);

        // game1._isSaving
        private readonly int[] SavingOffsets = { 184, 182 };
        public override bool IsSaving => ReadValue<bool>(SavingOffsets);

        // graphics.inDeviceTransition
        public override bool IsConstructingGraphics => false; // Failed to find in MonoGame framework

        // Game1._newDayTask
        private readonly int[] NewDayTaskOffsets = { 264 };
        public override bool NewDayTaskExists => ReadValue<IntPtr>(NewDayTaskOffsets) != IntPtr.Zero;

        // (Game1.activeClickableMenu as TitleMenu).StartupMessageColor
        private readonly int[] TitleMenu_StartupMessageColorOffsets = { 0, 456 };
        public uint TitleColor => ReadValue<uint>(TitleMenu_StartupMessageColorOffsets);
        public override bool IsTitleMenu => (ReadValue<uint>(TitleMenu_StartupMessageColorOffsets) == TitleMenu_DeepSkyBlue);

        // Game1.options.musicVolumeLevel
        private readonly int[] MusicVolumeOffsets = { 184, 80, 240 };
        public float MusicVolume => ReadValue<float>(MusicVolumeOffsets);
        public override void SetMusicVolume(int level) { WriteValue<float>(MusicVolumeOffsets, level / 100f); }

        // Game1.options.soundVolumeLevel
        private readonly int[] SoundVolumeOffsets = { 184, 80, 244 };
        public override void SetSoundVolume(int level) { WriteValue<float>(SoundVolumeOffsets, level / 100f); }

        // Game1.options.footstepVolumeLevel
        private readonly int[] FootstepVolumeOffsets = { 184, 80, 248 };
        public override void SetFootstepVolume(int level) { WriteValue<float>(FootstepVolumeOffsets, level / 100f); }

        // Game1.options.ambientVolumeLevel
        private readonly int[] AmbientVolumeOffsets = { 184, 80, 252 };
        public override void SetAmbientVolume(int level) { WriteValue<float>(AmbientVolumeOffsets, level / 100f); }

        // Game1.options.emoteButton
        private readonly int[] EmoteButtonOffsets = { 184, 80, 216, 8 };
        public string EmoteAddress => ReadValue<IntPtr>(new int[] { 184, 80, 216 }).ToString("X");
        public override void UnbindEmoteButton() { WriteValue<int>(EmoteButtonOffsets, AttentionKey); }

        // Game1.options.chatButton
        // the SECOND offset would be at 10, I'm not exactly sure I understand why
        private readonly int[] ChatButtonOffsets = { 184, 80, 88, 8 };
        public string ChatAddress => ReadValue<IntPtr>(new int[] { 184, 80, 88 }).ToString("X");
        public override void UnbindChatButton() { WriteValue<int>(ChatButtonOffsets, AttentionKey); }

        // Game1.options.enableZoom
        private readonly int[] EnableZoomOffsets = { 184, 80, 319 };
        public override void EnableZoomButton() { WriteValue<bool>(EnableZoomOffsets, true); }

        // Game1.options.showAdvancedCraftingInformation
        private readonly int[] AdvancedCraftingOffsets = { 184, 80, 324 };
        public override void AdvancedCrafting() { WriteValue<bool>(AdvancedCraftingOffsets, true); }

        // Game1.options.alwaysShowToolHitLocation
        private readonly int[] ToolHitOffsets = { 184, 80, 310 };
        public override void ToolHitIndicator() { WriteValue<bool>(ToolHitOffsets, true); }

        // Game1.options.useLegacySlingshotFiring
        private readonly int[] SlingshotModeOffset = { 184, 80, 334 };
        public override void SlingshotMode(bool legacy) { WriteValue<bool>(SlingshotModeOffset, legacy); }

        private readonly int[] CurrentLocationNameOffsets = { 184, 32, 272, 64 };
        public override string CurrentLocationName => ReadString(CurrentLocationNameOffsets, "", 8);

        private readonly int[] DaysPlayedOffsets = { -928, 496, 184 };
        public override int DaysPlayed => (int)ReadValue<UInt32>(DaysPlayedOffsets);

        private readonly int[] Event_IsWeddingOffsets = { 184, 32, 400, 300 };
        private readonly int[] Event_CurrentCommandOffsets = { 184, 32, 400, 232 };
        private readonly int[] Event_EventIdOffsets = { 184, 32, 400, 264 };
        public override bool Event_IsWedding => ReadValue<bool>(Event_IsWeddingOffsets, false);
        public override int Event_CurrentCommand => ReadValue<int>(Event_CurrentCommandOffsets, -1);
        public override int Event_EventId => ReadValue<int>(Event_EventIdOffsets, -1);

        private readonly int[] CommunityCenter_restoreAreaTimerOffsets = { 184, 32, 680 };
        public override int CC_restoreAreaTimer => ReadValue<int>(CommunityCenter_restoreAreaTimerOffsets);

        private readonly int[] CommunityCenter_restoreAreaPhaseOffsets = { 184, 32, 684 };
        public override int CC_restoreAreaPhase => ReadValue<int>(CommunityCenter_restoreAreaPhaseOffsets);

        private readonly int[] CommunityCenter_restoreAreaIndexOffsets = { 184, 32, 688 };
        public override int CC_restoreAreaIndex => ReadValue<int>(CommunityCenter_restoreAreaIndexOffsets);

        private readonly int[] CommunityCenter_isWatchingJunimoGoodbyeOffsets = { 184, 32, 692 };
        public override bool CC_isWatchingJunimoGoodbye => ReadValue<bool>(CommunityCenter_isWatchingJunimoGoodbyeOffsets);

        private readonly int[] ShopMenu_PPDOffsets = { 0, 264 };
        public override string ShopMenu_PersonPortraitDialogue => ReadString(ShopMenu_PPDOffsets, "", 8);
    }
}

//}

/*
Scanning for Stardew Valley...
Attaching to version 1.5.6-steam ( 1.5.6.21356 )...
Computing field offsets...
IsPaused : ReadValue<Boolean>( 208, 64, 136, 69 )
IsSaving : ReadValue<Boolean>( 184, 182 )
IsConstructingGraphics : Failed to find field inDeviceTransition in type Microsoft.Xna.Framework.GraphicsDeviceManager
NewDayTask : ReadPointer( 264 )
ActiveClickableMenu : ReadPointer( 0 )
TitleMenu_StartupMessageColor : ReadValue<Int32>( 0, 456 )
Options.MusicVolume : ReadValue<Single>( 184, 80, 240 )
Options.SoundVolume : ReadValue<Single>( 184, 80, 244 )
Options.ambientVolumeLevel : ReadValue<Single>( 184, 80, 252 )
Options.footstepVolumeLevel : ReadValue<Single>( 184, 80, 248 )
Options.emoteButton : ReadPointer( 184, 80, 216 )
Optiions.ChatButtons : ReadPointer( 184, 80, 88 )
Options.EnableZoom : ReadValue<Boolean>( 184, 80, 319 )
Options.ToolHit : ReadValue<Boolean>( 184, 80, 310 )
Options.AdvancedCraftnig : ReadValue<Boolean>( 184, 80, 324 )
Options.LegacySlingshot : ReadValue<Boolean>( 184, 80, 334 )
*/
