using System;
using System.Collections.Generic;

namespace MemoryHelper
{
    public class MemoryModel
    {
        public readonly string GameVersion;
        public readonly string CodeSignature;
        public readonly string SignatureValue;
        public readonly string SignaturePointer;
        public readonly Dictionary<string, MemoryFinder> Fields;

        public MemoryModel(string gameVersion, string signatureValue, string signaturePointer, string codeSignature)
        {
            GameVersion = gameVersion;
            CodeSignature = codeSignature;
            SignatureValue = signatureValue;
            SignaturePointer = signaturePointer;
            Fields = new Dictionary<string, MemoryFinder>();
        }

        protected static string RemoveWhiteSpace(params string[] str)
        {
            return string.Concat(str).Replace(" ", "");
        }

        public static MemoryModel V2(string gameVersion)
        {
            MemoryModel model = new MemoryModel(
                gameVersion: gameVersion,
                signatureValue: "_gameMode",
                signaturePointer: "_activeClickableMenu",
                codeSignature: RemoveWhiteSpace(
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
                    "83 3D pppppppp 00") // _activeClickableMenu
                );
            model.Fields["IsSaving"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("_isSaving")
                .GetValue<bool>();
            model.Fields["IsConstructingGraphics"] = MemoryFinder
                .GetStaticField("graphics")
                .GetField("inDeviceTransition")
                .GetValue<bool>();
            model.Fields["NewDayTask"] = MemoryFinder
                .GetStaticField("_newDayTask")
                .GetValue<IntPtr>();
            model.Fields["TitleMenu_StartupMessageColor"] = MemoryFinder
                .GetStaticField("_activeClickableMenu")
                .AsType("StardewValley.Menus.TitleMenu")
                .GetField("startupMessageColor")
                .GetValue<int>();
            model.Fields["Options.musicVolumeLevel"] = MemoryFinder
                .GetStaticField("options")
                .GetField("musicVolumeLevel")
                .GetValue<int>();
            model.Fields["Options.soundVolumeLevel"] = MemoryFinder
                .GetStaticField("options")
                .GetField("soundVolumeLevel")
                .GetValue<int>();
            model.Fields["Options.ambientVolumeLevel"] = MemoryFinder
                .GetStaticField("options")
                .GetField("ambientVolumeLevel")
                .GetValue<int>();
            model.Fields["Options.footstepVolumeLevel"] = MemoryFinder
                .GetStaticField("options")
                .GetField("footstepVolumeLevel")
                .GetValue<int>();
            model.Fields["Options.enableZoom"] = MemoryFinder
                .GetStaticField("options")
                .GetField("zoomButtons")
                .GetValue<bool>();
            model.Fields["Options.ToolHit"] = MemoryFinder
                .GetStaticField("options")
                .GetField("alwaysShowToolHitLocation")
                .GetValue<bool>();
            return model;
        }

        public static MemoryModel V3(string gameVersion)
        {
            MemoryModel model = new MemoryModel(
                gameVersion: gameVersion,
                signatureValue: "_gameMode",
                signaturePointer: "_activeClickableMenu",
                codeSignature: RemoveWhiteSpace(
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
            "83 3D pppppppp 00") // _activeClickableMenu
                );
            model.Fields["IsPaused"] = MemoryFinder
                .GetStaticField("netWorldState")
                .GetField("value")
                .AsType("StardewValley.Network.NetWorldState")
                .GetField("isPaused")
                .GetField("value")
                .GetValue<bool>();
            model.Fields["IsSaving"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("_isSaving")
                .GetValue<bool>();
            model.Fields["IsConstructingGraphics"] = MemoryFinder
                .GetStaticField("graphics")
                .GetField("inDeviceTransition")
                .GetValue<bool>();
            model.Fields["NewDayTask"] = MemoryFinder
                .GetStaticField("_newDayTask")
                .GetValue<IntPtr>();
            model.Fields["TitleMenu_StartupMessageColor"] = MemoryFinder
                .GetStaticField("_activeClickableMenu")
                .AsType("StardewValley.Menus.TitleMenu")
                .GetField("startupMessageColor")
                .GetValue<int>();
            model.Fields["Options.musicVolumeLevel"] = MemoryFinder
                .GetStaticField("options")
                .GetField("musicVolumeLevel")
                .GetValue<int>();
            model.Fields["Options.soundVolumeLevel"] = MemoryFinder
                .GetStaticField("options")
                .GetField("soundVolumeLevel")
                .GetValue<int>();
            model.Fields["Options.ambientVolumeLevel"] = MemoryFinder
                .GetStaticField("options")
                .GetField("ambientVolumeLevel")
                .GetValue<int>();
            model.Fields["Options.footstepVolumeLevel"] = MemoryFinder
                .GetStaticField("options")
                .GetField("footstepVolumeLevel")
                .GetValue<int>();
            model.Fields["Options.enableZoom"] = MemoryFinder
                .GetStaticField("options")
                .GetField("zoomButtons")
                .GetValue<bool>();
            model.Fields["Options.ToolHit"] = MemoryFinder
                .GetStaticField("options")
                .GetField("alwaysShowToolHitLocation")
                .GetValue<bool>();
            model.Fields["Optiions.ChatButtons"] = MemoryFinder
                .GetStaticField("options")
                .GetField("chatButton")
                .GetValue<IntPtr>();

            model.Fields["Options.AdvancedCraftnig"] = MemoryFinder
                .GetStaticField("options")
                .GetField("showAdvancedCraftingInformation")
                .GetValue<bool>();
            model.Fields["Options.emoteButton"] = MemoryFinder
                .GetStaticField("options")
                .GetField("emoteButton")
                .GetValue<IntPtr>();
            return model;
        }

        public static MemoryModel V5(string gameVersion)
        {
            MemoryModel model = new MemoryModel(
                gameVersion: gameVersion,
                signatureValue: "_gameMode",
                signaturePointer: "_activeClickableMenu",
                codeSignature: RemoveWhiteSpace(
                     "8B C6",
                     "A2 vvvvvvvv",
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
                     "83 3D pppppppp 00",
                     "74 3C")
                );
            model.Fields["IsPaused"] = MemoryFinder
                .GetStaticField("netWorldState")
                .GetField("value")
                .AsType("StardewValley.Network.NetWorldState")
                .GetField("isPaused")
                .GetField("value")
                .GetValue<bool>();
            model.Fields["IsSaving"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("_isSaving")
                .GetValue<bool>();
            model.Fields["IsConstructingGraphics"] = MemoryFinder
                .GetStaticField("graphics")
                .GetField("inDeviceTransition")
                .GetValue<bool>();
            model.Fields["NewDayTask"] = MemoryFinder
                .GetStaticField("_newDayTask")
                .GetValue<IntPtr>();
            model.Fields["ActiveClickableMenu"] = MemoryFinder
                .GetStaticField("_activeClickableMenu")
                .GetValue<IntPtr>();
            model.Fields["TitleMenu_StartupMessageColor"] = MemoryFinder
                .GetStaticField("_activeClickableMenu")
                .AsType("StardewValley.Menus.TitleMenu")
                .GetField("startupMessageColor")
                .GetValue<int>();
            model.Fields["Options.MusicVolume"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("musicVolumeLevel")
                .GetValue<float>();
            model.Fields["Options.SoundVolume"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("soundVolumeLevel")
                .GetValue<float>();
            model.Fields["Options.ambientVolumeLevel"] = MemoryFinder
                .GetStaticField("options")
                .GetField("ambientVolumeLevel")
                .GetValue<int>();
            model.Fields["Options.footstepVolumeLevel"] = MemoryFinder
                .GetStaticField("options")
                .GetField("footstepVolumeLevel")
                .GetValue<int>();
            model.Fields["Options.emoteButton"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("emoteButton")
                .GetValue<IntPtr>();
            model.Fields["Optiions.ChatButtons"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("chatButton")
                .GetValue<IntPtr>();
            model.Fields["Optiions.ChatButtons"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("chatButton")
                .GetValue<IntPtr>();
            model.Fields["Options.EnableZoom"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("zoomButtons")
                .GetValue<bool>();
            model.Fields["Options.ToolHit"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("alwaysShowToolHitLocation")
                .GetValue<bool>();
            model.Fields["Options.AdvancedCraftnig"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("showAdvancedCraftingInformation")
                .GetValue<bool>();
            model.Fields["Options.LegacySlingshot"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("useLegacySlingshotFiring")
                .GetValue<bool>();
            return model;
        }

        public static MemoryModel V5_5(string gameVersion)
        {
            MemoryModel model = new MemoryModel(
                gameVersion: gameVersion,
                signatureValue: "_gameMode",
                signaturePointer: "_activeClickableMenu",
                codeSignature: RemoveWhiteSpace( // Game1::setGameMode
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
                    )
                );
            model.Fields["IsPaused"] = MemoryFinder
                .GetStaticField("netWorldState")
                .GetField("value")
                .AsType("StardewValley.Network.NetWorldState")
                .GetField("isPaused")
                .GetField("value")
                .GetValue<bool>();
            model.Fields["IsSaving"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("_isSaving")
                .GetValue<bool>();
            model.Fields["IsConstructingGraphics"] = MemoryFinder
                .GetStaticField("graphics")
                .GetField("inDeviceTransition")
                .GetValue<bool>();
            model.Fields["NewDayTask"] = MemoryFinder
                .GetStaticField("_newDayTask")
                .GetValue<IntPtr>();
            model.Fields["ActiveClickableMenu"] = MemoryFinder
                .GetStaticField("_activeClickableMenu")
                .GetValue<IntPtr>();
            model.Fields["TitleMenu_StartupMessageColor"] = MemoryFinder
                .GetStaticField("_activeClickableMenu")
                .AsType("StardewValley.Menus.TitleMenu")
                .GetField("startupMessageColor")
                .GetValue<int>();
            model.Fields["Options.MusicVolume"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("musicVolumeLevel")
                .GetValue<float>();
            model.Fields["Options.SoundVolume"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("soundVolumeLevel")
                .GetValue<float>();
            model.Fields["Options.ambientVolumeLevel"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("ambientVolumeLevel")
                .GetValue<float>();
            model.Fields["Options.footstepVolumeLevel"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("footstepVolumeLevel")
                .GetValue<float>();
            model.Fields["Options.emoteButton"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("emoteButton")
                .GetValue<IntPtr>();
            model.Fields["Optiions.ChatButtons"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("chatButton")
                .GetValue<IntPtr>();
            model.Fields["Optiions.ChatButtons"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("chatButton")
                .GetValue<IntPtr>();
            model.Fields["Options.EnableZoom"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("zoomButtons")
                .GetValue<bool>();
            model.Fields["Options.ToolHit"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("alwaysShowToolHitLocation")
                .GetValue<bool>();
            model.Fields["Options.AdvancedCraftnig"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("showAdvancedCraftingInformation")
                .GetValue<bool>();
            model.Fields["Options.LegacySlingshot"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("useLegacySlingshotFiring")
                .GetValue<bool>();
            return model;
        }
    }
}
