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
