using LiveSplit.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplit.StardewValley.MemoryModels
{
    public class MemoryModel_6_8_x86 : MemoryModel
    {
        private static readonly string CodeSignature = RemoveWhitespace( // Game1::setGameMode
                    "8B D0",
                    "8B CB",
                    "FF 15 ????????",
                    "8B C6",
                    "A2 vvvvvvvv",
                    "FF 15 ????????",
                    "85 C0",
                    "74 0A",
                    "8B C8",
                    "8B 01",
                    "8B 40 28",
                    "FF 50 18",
                    "85 F6",
                    "74 11",
                    "83 FE 03",
                    "0F84 A2000000",
                    "8D 65 F4",
                    "5B",
                    "5E",
                    "5F",
                    "5D",
                    "C3",
                    "33 FF",
                    "83 3D pppppppp 00"
                    );
        public MemoryModel_6_8_x86(Process process) : base(process, CodeSignature) {
        }
        // netWorldState.(value as NetWorldState).isPaused.value]
        private readonly int[] PausedOffsets = { 108, 40, 68, 45 };
        public override bool IsPaused => ReadValue<bool>(PausedOffsets);

        // game1._isSaving
        private readonly int[] SavingOffsets = { 92, 105 };
        public override bool IsSaving => ReadValue<bool>(SavingOffsets);

        // graphics.inDeviceTransition
        public override bool IsConstructingGraphics => false; // Failed to find in MonoGame framework

        // Game1._newDayTask
        private readonly int[] NewDayTaskOffsets = { 136 };
        public override bool NewDayTaskExists => ReadValue<IntPtr>(NewDayTaskOffsets) != IntPtr.Zero;

        // (Game1.activeClickableMenu as TitleMenu).StartupMessageColor
        private readonly int[] TitleMenu_StartupMessageColorOffsets = { 0, 328 };
        public uint TitleColor => ReadValue<uint>(TitleMenu_StartupMessageColorOffsets);
        public override bool IsTitleMenu => (ReadValue<uint>(TitleMenu_StartupMessageColorOffsets) == TitleMenu_DeepSkyBlue);

        // Game1.options.musicVolumeLevel
        private readonly int[] MusicVolumeOffsets = { 92, 40, 124 };
        public float MusicVolume => ReadValue<float>(MusicVolumeOffsets);
        public override void SetMusicVolume(int level) { WriteValue<float>(MusicVolumeOffsets, level / 100f); }

        // Game1.options.soundVolumeLevel
        private readonly int[] SoundVolumeOffsets = { 92, 40, 128 };
        public override void SetSoundVolume(int level) { WriteValue<float>(SoundVolumeOffsets, level / 100f); }

        // Game1.options.footstepVolumeLevel
        private readonly int[] FootstepVolumeOffsets = { 92, 40, 132 };
        public override void SetFootstepVolume(int level) { WriteValue<float>(FootstepVolumeOffsets, level / 100f); }

        // Game1.options.ambientVolumeLevel
        private readonly int[] AmbientVolumeOffsets = { 92, 40, 136 };
        public override void SetAmbientVolume(int level) { WriteValue<float>(AmbientVolumeOffsets, level / 100f); }

        // Game1.options.emoteButton
        private readonly int[] EmoteButtonOffsets = { 92, 40, 108, 8 };
        public string EmoteAddress => ReadValue<IntPtr>(new int[] { 92, 40, 108 }).ToString("X");
        public override void UnbindEmoteButton() {}

        // Game1.options.chatButton
        // the SECOND offset would be at 10, I'm not exactly sure I understand why
        private readonly int[] ChatButtonOffsets = { 92, 40, 44, 8 };
        public string ChatAddress => ReadValue<IntPtr>(new int[] { 92, 40, 44 }).ToString("X");
        public override void UnbindChatButton() {}

        // Game1.options.enableZoom
        private readonly int[] EnableZoomOffsets = { 92, 40, 202 };
        public override void EnableZoomButton() { WriteValue<bool>(EnableZoomOffsets, true); }

        // Game1.options.showAdvancedCraftingInformation
        private readonly int[] AdvancedCraftingOffsets = { 92, 40, 207 };
        public override void AdvancedCrafting() { WriteValue<bool>(AdvancedCraftingOffsets, true); }

        // Game1.options.alwaysShowToolHitLocation
        private readonly int[] ToolHitOffsets = { 92, 40, 194 };
        public override void ToolHitIndicator() { WriteValue<bool>(ToolHitOffsets, true); }

        // Game1.options.useLegacySlingshotFiring
        private readonly int[] SlingshotModeOffset = { 92, 40, 220 };
        public override void SlingshotMode(bool legacy) { WriteValue<bool>(SlingshotModeOffset, legacy); }


        private readonly int[] CurrentLocationNameOffsets = { 92, 16, 180, 40 };
        public override string CurrentLocationName => ReadString(CurrentLocationNameOffsets, "", 4);

        private readonly int[] DaysPlayedOffsets = { 108, 40, 60, 44 };
        public override int DaysPlayed => ReadValue<int>(DaysPlayedOffsets, -1);

        private readonly int[] Event_IsWeddingOffsets = { 92, 16, 240, 173 };
        private readonly int[] Event_CurrentCommandOffsets = { 92, 16, 240, 132 };
        private readonly int[] Event_EventIdOffsets = { 92, 16, 240, 8 };
        public override bool Event_IsWedding => ReadValue<bool>(Event_IsWeddingOffsets, false);
        public override int Event_CurrentCommand => ReadValue<int>(Event_CurrentCommandOffsets, -1);
        public override string Event_EventId => ReadString(Event_EventIdOffsets, "-1", 4);

        private readonly int[] CommunityCenter_restoreAreaTimerOffsets = { 92, 16, 464 };
        public override int CC_restoreAreaTimer => ReadValue<int>(CommunityCenter_restoreAreaTimerOffsets);

        private readonly int[] CommunityCenter_restoreAreaPhaseOffsets = { 92, 16, 468 };
        public override int CC_restoreAreaPhase => ReadValue<int>(CommunityCenter_restoreAreaPhaseOffsets);

        private readonly int[] CommunityCenter_restoreAreaIndexOffsets = { 92, 16, 472 };
        public override int CC_restoreAreaIndex => ReadValue<int>(CommunityCenter_restoreAreaIndexOffsets);

        private readonly int[] CommunityCenter_isWatchingJunimoGoodbyeOffsets = { 92, 16, 476 };
        public override bool CC_isWatchingJunimoGoodbye => ReadValue<bool>(CommunityCenter_isWatchingJunimoGoodbyeOffsets);

        private readonly int[] ShopMenu_PPDOffsets = { 0, 148 };
        public override string ShopMenu_PersonPortraitDialogue => ReadString(ShopMenu_PPDOffsets, "", 4);

        private readonly int[] Farm_grandpaScoreOffsets = { 92, 16, 400, 44 };
        public override int Farm_grandpaScore => ReadValue<int>(Farm_grandpaScoreOffsets, 0);
    }
}

/*
Attaching to version 1.6.8-steam-compat ( 1.6.8.24119_x86 )...
Computing field offsets...
IsPaused : ReadValue<Boolean>( 108, 40, 68, 45 )
IsSaving : ReadValue<Boolean>( 92, 105 )
IsConstructingGraphics : ReadValue<Boolean>( -468, 74 )
NewDayTask : ReadPointer( 136 )
ActiveClickableMenu : ReadPointer( 0 )
TitleMenu_StartupMessageColor : ReadValue<Int32>( 0, 328 )
Options.MusicVolume : ReadValue<Single>( 92, 40, 124 )
Options.SoundVolume : ReadValue<Single>( 92, 40, 128 )
Options.ambientVolumeLevel : ReadValue<Single>( 92, 40, 136 )
Options.footstepVolumeLevel : ReadValue<Single>( 92, 40, 132 )
Options.emoteButton : ReadPointer( 92, 40, 108 )
Optiions.ChatButtons : ReadPointer( 92, 40, 44 )
Options.EnableZoom : ReadValue<Boolean>( 92, 40, 202 )
Options.ToolHit : ReadValue<Boolean>( 92, 40, 194 )
Options.AdvancedCraftnig : ReadValue<Boolean>( 92, 40, 207 )
Options.LegacySlingshot : ReadValue<Boolean>( 92, 40, 220 )
currentLocation.Name : ReadString( 92, 16, 180, 40, 0 )
DaysPlayed : ReadValue<Int32>( 108, 40, 60, 44 )
CurrentEvent.IsWedding : ReadValue<Boolean>( 92, 16, 240, 173 )
CurrentEvent.currentCommand : ReadValue<Int32>( 92, 16, 240, 132 )
CurrentEvent.id : ReadValue<Int32>( 92, 16, 240, 8 )
CommunityCenter.restoreAreaIndex : ReadValue<Int32>( 92, 16, 472 )
CommunityCenter.restoreAreaPhase : ReadValue<Int32>( 92, 16, 468 )
CommunityCenter.restoreAreaTimer : ReadValue<Int32>( 92, 16, 464 )
CommunityCenter._isWatchingJunimoGoodbye : ReadValue<Boolean>( 92, 16, 476 )
ShopMenu.potraitPersonDialogue : ReadString( 0, 148, 0 )
Farm.grandpaScore.Value : ReadValue<Int32>( 92, 16, 400, 44 )
 */