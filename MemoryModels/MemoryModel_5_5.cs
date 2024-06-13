using System;
using System.Diagnostics;

namespace LiveSplit.StardewValley.MemoryModels
{
    public class MemoryModel_5_5 : MemoryModel
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
        public MemoryModel_5_5(Process process) : base(process, CodeSignature) { }

        // netWorldState.(value as NetWorldState).isPaused.value]
        private readonly int[] PausedOffsets = { 208, 64, 136, 69 };
        public override bool IsPaused => ReadValue<bool>(PausedOffsets);

        // game1._isSaving
        private readonly int[] SavingOffsets = { 184, 178 };
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
        private readonly int[] MusicVolumeOffsets = { 184, 80, 248 };
        public float MusicVolume => ReadValue<float>(MusicVolumeOffsets);
        public override void SetMusicVolume(int level) { WriteValue<float>(MusicVolumeOffsets, level / 100f); }

        // Game1.options.soundVolumeLevel
        private readonly int[] SoundVolumeOffsets = { 184, 80, 252 };
        public override void SetSoundVolume(int level) { WriteValue<float>(SoundVolumeOffsets, level / 100f); }

        // Game1.options.footstepVolumeLevel
        private readonly int[] FootstepVolumeOffsets = { 184, 80, 256 };
        public override void SetFootstepVolume(int level) { WriteValue<float>(FootstepVolumeOffsets, level / 100f); }

        // Game1.options.ambientVolumeLevel
        private readonly int[] AmbientVolumeOffsets = { 184, 80, 260 };
        public override void SetAmbientVolume(int level) { WriteValue<float>(AmbientVolumeOffsets, level / 100f); }

        // Game1.options.emoteButton
        private readonly int[] EmoteButtonOffsets = { 184, 80, 224, 8 };
        public string EmoteAddress => ReadValue<IntPtr>(new int[] { 184, 80, 224 }).ToString("X");
        public override void UnbindEmoteButton() {}

        // Game1.options.chatButton
        // the SECOND offset would be at 10, I'm not exactly sure I understand why
        private readonly int[] ChatButtonOffsets = { 184, 80, 96, 8 };
        public string ChatAddress => ReadValue<IntPtr>(new int[] { 184, 80, 96 }).ToString("X");
        public override void UnbindChatButton() {}

        // Game1.options.enableZoom
        private readonly int[] EnableZoomOffsets = { 184, 80, 327 };
        public override void EnableZoomButton() { WriteValue<bool>(EnableZoomOffsets, true); }

        // Game1.options.showAdvancedCraftingInformation
        private readonly int[] AdvancedCraftingOffsets = { 184, 80, 318 };
        public override void AdvancedCrafting() { WriteValue<bool>(AdvancedCraftingOffsets, true); }

        // Game1.options.alwaysShowToolHitLocation
        private readonly int[] ToolHitOffsets = { 184, 80, 332 };
        public override void ToolHitIndicator() { WriteValue<bool>(ToolHitOffsets, true); }

        // Game1.options.useLegacySlingshotFiring
        private readonly int[] SlingshotModeOffset = { 184, 80, 342 };
        public override void SlingshotMode(bool legacy) { WriteValue<bool>(SlingshotModeOffset, legacy); }

        public override int DaysPlayed => 0;
        public override string CurrentLocationName => "";
    }
}

//}

/*
IsPaused : ReadValue<Boolean>(  )
IsSaving : ReadValue<Boolean>(  )
IsConstructingGraphics : Failed to find field inDeviceTransition in type Microsoft.Xna.Framework.GraphicsDeviceManager
NewDayTask : ReadPointer( 264 )
ActiveClickableMenu : ReadPointer( 0 )
TitleMenu_StartupMessageColor : ReadValue<Int32>(  )
Options.MusicVolume : ReadValue<Single>( 184, 76, 244 )
Options.SoundVolume : ReadValue<Single>(  )
Options.emoteButton : ReadPointer( 184, 76, 220 )
Optiions.ChatButtons : ReadPointer( 184, 76, 92 )
Options.EnableZoom : ReadValue<Boolean>( 184, 76, 323 )
Options.ToolHit : ReadValue<Boolean>( 184, 76, 314 )
Options.AdvancedCraftnig : ReadValue<Boolean>( 184, 76, 328 )
Options.LegacySlingshot : ReadValue<Boolean>( 184, 76, 338 )
*/
