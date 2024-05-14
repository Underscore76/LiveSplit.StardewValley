using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveSplit.StardewValley.MemoryModels
{
    public class MemoryModel_6_3 : MemoryModel
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
                    "0FB6 C9",
                    "85 C9",
                    "74 05",
                    "BE 01000000"
                    );
        public MemoryModel_6_3(Process process) : base(process, CodeSignature) { 
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
        private readonly int[] EnableZoomOffsets = { 184, 80, 314 };
        public override void EnableZoomButton() { WriteValue<bool>(EnableZoomOffsets, true); }

        // Game1.options.showAdvancedCraftingInformation
        private readonly int[] AdvancedCraftingOffsets = { 184, 80, 319 };
        public override void AdvancedCrafting() { WriteValue<bool>(AdvancedCraftingOffsets, true); }

        // Game1.options.alwaysShowToolHitLocation
        private readonly int[] ToolHitOffsets = { 184, 80, 306 };
        public override void ToolHitIndicator() { WriteValue<bool>(ToolHitOffsets, true); }

        // Game1.options.useLegacySlingshotFiring
        private readonly int[] SlingshotModeOffset = { 184, 80, 330 };
        public override void SlingshotMode(bool legacy) { WriteValue<bool>(SlingshotModeOffset, legacy); }
    }
}
