using System;
using System.Diagnostics;

namespace LiveSplit.StardewValley.MemoryModels
{
    public class MemoryModel_5_6_x86 : MemoryModel
    {
        private static readonly string CodeSignature = RemoveWhitespace( // Game1::setGameMode
                     "55",        //- push ebp
                    "8B EC",        //- mov ebp,esp
                    "57",        //- push edi
                    "56",        //- push esi
                    "83 EC 08",        //- sub esp,08
                    "8B 3D ????????",        //- mov edi,[055017EC]
                    "0FB6 F1",        //- movzx esi,cl
                    "8B CE",        //- mov ecx,esi
                    "FF 15 ????????",        //- call dword ptr [06BD373C]
                    "8B D0",        //- mov edx,eax
                    "8B CF",        //- mov ecx,edi
                    "E8 ????????",        //- call System.Console::WriteLine
                    "8B C6",        //- mov eax,esi
                    "A2 vvvvvvvv",        //- mov [02F47055],al
                    "FF 15 ????????",        //- call dword ptr [06BD3754]
                    "85 C0",        //- test eax,eax
                    "74 10",        //- je StardewValley.Game1::setGameMode+43
                    "FF 15 ????????",        //- call dword ptr [06BD3754]
                    "8B C8",        //- mov ecx,eax
                    "8B 01",        //- mov eax,[ecx]
                    "8B 40 28",        //- mov eax,[eax+28]
                    "FF 50 18",        //- call dword ptr [eax+18]
                                       // case 0: jump to label case0
                    "85 F6",         //- test esi,esi
                    "74 10",         //- je StardewValley.Game1::setGameMode+57
                    "83 FE 03",         //- cmp esi,03
                    "0F84 A1000000",         //- je StardewValley.Game1::setGameMode+F1
                    "8D 65 F8",         //- lea esp,[ebp-08]
                    "5E",         //- pop esi
                    "5F",         //- pop edi
                    "5D",         //- pop ebp
                    "C3",         //- ret 
                                  // label: case0
                    "33 FF",     // bool skip = false
                    "83 3D pppppppp 00",     // activeClickableMenu == null
                    "74 3C",     //- je StardewValley.Game1::setGameMode+9E
                    "A1 ????????",     //- mov eax,[054F38BC]
                    "85 C0",     //- test eax,eax
                    "74 33",     //- je StardewValley.Game1::setGameMode+9E
                    "3A 40 08",     //- cmp al,[eax+08]
                    "8D 48 08",     //- lea ecx,[eax+08]
                    "8B 01",     //- mov eax,[ecx]
                    "8B 51 04"     //- mov edx,[ecx+04]
                    );
        public MemoryModel_5_6_x86(Process process) : base(process, CodeSignature) { }

        // netWorldState.(value as NetWorldState).isPaused.value]
        private readonly int[] PausedOffsets = { 104, 36, 68, 41 };
        public override bool IsPaused => ReadValue<bool>(PausedOffsets);

        // game1._isSaving
        private readonly int[] SavingOffsets = { 92, 110 };
        public override bool IsSaving => ReadValue<bool>(SavingOffsets);

        // graphics.inDeviceTransition
        public override bool IsConstructingGraphics => false; // Failed to find in MonoGame framework

        // Game1._newDayTask
        private readonly int[] NewDayTaskOffsets = { 132 };
        public override bool NewDayTaskExists => ReadValue<IntPtr>(NewDayTaskOffsets) != IntPtr.Zero;

        // (Game1.activeClickableMenu as TitleMenu).StartupMessageColor
        private readonly int[] TitleMenu_StartupMessageColorOffsets = { 0, 332 };
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
        public override void UnbindEmoteButton() { WriteValue<int>(EmoteButtonOffsets, AttentionKey); }

        // Game1.options.chatButton
        // the SECOND offset would be at 10, I'm not exactly sure I understand why
        private readonly int[] ChatButtonOffsets = { 92, 40, 44, 8 };
        public string ChatAddress => ReadValue<IntPtr>(new int[] { 92, 40, 44 }).ToString("X");
        public override void UnbindChatButton() { WriteValue<int>(ChatButtonOffsets, AttentionKey); }

        // Game1.options.enableZoom
        private readonly int[] EnableZoomOffsets = { 92, 40, 203 };
        public override void EnableZoomButton() { WriteValue<bool>(EnableZoomOffsets, true); }

        // Game1.options.showAdvancedCraftingInformation
        private readonly int[] AdvancedCraftingOffsets = { 92, 40, 208 };
        public override void AdvancedCrafting() { WriteValue<bool>(AdvancedCraftingOffsets, true); }

        // Game1.options.alwaysShowToolHitLocation
        private readonly int[] ToolHitOffsets = { 92, 40, 194 };
        public override void ToolHitIndicator() { WriteValue<bool>(ToolHitOffsets, true); }

        // Game1.options.useLegacySlingshotFiring
        private readonly int[] SlingshotModeOffset = { 92, 40, 218 };
        public override void SlingshotMode(bool legacy) { WriteValue<bool>(SlingshotModeOffset, legacy); }

        private readonly int[] CurrentLocationNameOffsets = { 92, 16, 136, 36 };
        public override string CurrentLocationName => ReadString(CurrentLocationNameOffsets, "");

        private readonly int[] DaysPlayedOffsets = { -464, 304, 172 };
        public override int DaysPlayed => (int)ReadValue<UInt32>(DaysPlayedOffsets);

        private readonly int[] Event_IsWeddingOffsets = { 92, 16, 200, 184 };
        private readonly int[] Event_CurrentCommandOffsets = { 92, 16, 200, 116 };
        private readonly int[] Event_EventIdOffsets = { 92, 16, 200, 148 };
        public override bool Event_IsWedding => ReadValue<bool>(Event_IsWeddingOffsets, false);
        public override int Event_CurrentCommand => ReadValue<int>(Event_CurrentCommandOffsets, -1);
        public override int Event_EventId => ReadValue<int>(Event_EventIdOffsets, -1);

        private readonly int[] CommunityCenter_restoreAreaTimerOffsets = { 92, 16, 372 };
        public override int CC_restoreAreaTimer => ReadValue<int>(CommunityCenter_restoreAreaTimerOffsets);

        private readonly int[] CommunityCenter_restoreAreaPhaseOffsets = { 92, 16, 376 };
        public override int CC_restoreAreaPhase => ReadValue<int>(CommunityCenter_restoreAreaPhaseOffsets);

        private readonly int[] CommunityCenter_restoreAreaIndexOffsets = { 92, 16, 380 };
        public override int CC_restoreAreaIndex => ReadValue<int>(CommunityCenter_restoreAreaIndexOffsets);

        private readonly int[] CommunityCenter_isWatchingJunimoGoodbyeOffsets = { 92, 16, 384 };
        public override bool CC_isWatchingJunimoGoodbye => ReadValue<bool>(CommunityCenter_isWatchingJunimoGoodbyeOffsets);

        private readonly int[] ShopMenu_PPDOffsets = { 0, 144 };
        public override string ShopMenu_PersonPortraitDialogue => ReadString(ShopMenu_PPDOffsets, "");
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
