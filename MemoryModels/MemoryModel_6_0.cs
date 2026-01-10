using LiveSplit.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplit.StardewValley.MemoryModels
{
    public class MemoryModel_6_0 : MemoryModel
    {
        private static readonly string CodeSignature = RemoveWhitespace( // Game1::setGameMode
                    "85 DB", // switch mode
                    "74 11",
                    "83 FB 03",
                    "0F84 AD000000",
                    "48 83 C4 20",
                    "5B",
                    "5E",
                    "5F",
                    "C3",
                    "33 F6",
                    "48 B9 pppppppppppppppp", // the pointer Game1.activeClickableMenu
                    "48 83 39 00", // the check that the value inside the ptr is not null
                    "74 3E", // jump when null
                    "48 B9 vvvvvvvvvvvvvvvv", // Game1.currentGameTime
                    "48 8B 09",
                    "48 85 C9",
                    "74 2C",
                    "48 8B 49 10",
                    "C5F857C0",
                    "C4E1FB2AC1",
                    "C5FB5E05 89 000000",
                    "C5F92E05 89 000000",
                    "0F97 C1",
                    "0FB6 C9"
                    );
        public MemoryModel_6_0(Process process) : base(process, CodeSignature)
        {
        }

        // netWorldState.(value as NetWorldState).isPaused.value]
        private readonly int[] PausedOffsets = { 216, 72, 136, 77 };
        public override bool IsPaused => ReadValue<bool>(PausedOffsets);

        // game1._isSaving
        private readonly int[] SavingOffsets = { 184, 169 };
        public override bool IsSaving => ReadValue<bool>(SavingOffsets);

        // graphics.inDeviceTransition
        public override bool IsConstructingGraphics => false; // Failed to find in MonoGame framework

        // Game1._newDayTask
        private readonly int[] NewDayTaskOffsets = { 272 };
        public override bool NewDayTaskExists => ReadValue<IntPtr>(NewDayTaskOffsets) != IntPtr.Zero;

        // (Game1.activeClickableMenu as TitleMenu).StartupMessageColor
        private readonly int[] TitleMenu_StartupMessageColorOffsets = { 0, 456 };
        public uint TitleColor => ReadValue<uint>(TitleMenu_StartupMessageColorOffsets);
        public override bool IsTitleMenu => (ReadValue<uint>(TitleMenu_StartupMessageColorOffsets) == TitleMenu_DeepSkyBlue);

        private readonly int[] CurrentLocationNameOffsets = { 184, 32, 352, 72 };
        public override string CurrentLocationName => ReadString(CurrentLocationNameOffsets, "", 8);

        private readonly int[] DaysPlayedOffsets = { 216, 72, 120, 76 };
        public override int DaysPlayed => ReadValue<int>(DaysPlayedOffsets, -1);

        private readonly int[] Event_IsWeddingOffsets = { 184, 32, 472, 297 };
        private readonly int[] Event_CurrentCommandOffsets = { 184, 32, 472, 256 };
        private readonly int[] Event_EventIdOffsets = { 184, 32, 472, 16 };
        public override bool Event_IsWedding => ReadValue<bool>(Event_IsWeddingOffsets, false);
        public override int Event_CurrentCommand => ReadValue<int>(Event_CurrentCommandOffsets, -1);
        public override string Event_EventId => ReadString(Event_EventIdOffsets, "-1", 8);

        private readonly int[] CommunityCenter_restoreAreaTimerOffsets = { 184, 32, 836 };
        public override int CC_restoreAreaTimer => ReadValue<int>(CommunityCenter_restoreAreaTimerOffsets);

        private readonly int[] CommunityCenter_restoreAreaPhaseOffsets = { 184, 32, 840 };
        public override int CC_restoreAreaPhase => ReadValue<int>(CommunityCenter_restoreAreaPhaseOffsets);

        private readonly int[] CommunityCenter_restoreAreaIndexOffsets = { 184, 32, 844 };
        public override int CC_restoreAreaIndex => ReadValue<int>(CommunityCenter_restoreAreaIndexOffsets);

        private readonly int[] CommunityCenter_isWatchingJunimoGoodbyeOffsets = { 184, 32, 848 };
        public override bool CC_isWatchingJunimoGoodbye => ReadValue<bool>(CommunityCenter_isWatchingJunimoGoodbyeOffsets);

        private readonly int[] ShopMenu_PPDOffsets = { 0, 272 };
        public override string ShopMenu_PersonPortraitDialogue => ReadString(ShopMenu_PPDOffsets, "", 8);

        private readonly int[] Farm_grandpaScoreOffsets = { 184, 32, 720, 76 };
        public override int Farm_grandpaScore => ReadValue<int>(Farm_grandpaScoreOffsets, 0);
    }
}

/*
Scanning for Stardew Valley...
FileVersion 1.6.0.24079
Attaching to version 1.6.0-steam ( 1.6.0.24079 )...
26144525b28 26144525bc0
Computing field offsets...
IsPaused : ReadValue<Boolean>( 216, 72, 136, 77 )
IsSaving : ReadValue<Boolean>( 184, 169 )
IsConstructingGraphics : Failed to find field inDeviceTransition in type Microsoft.Xna.Framework.GraphicsDeviceManager
NewDayTask : ReadPointer( 272 )
ActiveClickableMenu : ReadPointer( 0 )
TitleMenu_StartupMessageColor : ReadValue<Int32>( 0, 456 )
currentLocation.Name : ReadString( 184, 32, 352, 72, 0 )
DaysPlayed : ReadValue<Int32>( 216, 72, 120, 76 )
CurrentEvent.IsWedding : ReadValue<Boolean>( 184, 32, 472, 297 )
CurrentEvent.currentCommand : ReadValue<Int32>( 184, 32, 472, 256 )
CurrentEvent.id : ReadValue<Int32>( 184, 32, 472, 16 )
CommunityCenter.restoreAreaIndex : ReadValue<Int32>( 184, 32, 844 )
CommunityCenter.restoreAreaPhase : ReadValue<Int32>( 184, 32, 840 )
CommunityCenter.restoreAreaTimer : ReadValue<Int32>( 184, 32, 836 )
CommunityCenter._isWatchingJunimoGoodbye : ReadValue<Boolean>( 184, 32, 848 )
ShopMenu.potraitPersonDialogue : ReadString( 0, 272, 0 )
Farm.grandpaScore.Value : ReadValue<Int32>( 184, 32, 720, 76 )
 */