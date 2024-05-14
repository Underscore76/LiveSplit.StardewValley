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

        protected static string RemoveWhitespace(params string[] str)
        {
            return string.Concat(str).Replace(" ", "");
        }

        public static MemoryModel V2(string gameVersion)
        {
            MemoryModel model = new MemoryModel(
                gameVersion: gameVersion,
                signatureValue: "_gameMode",
                signaturePointer: "_activeClickableMenu",
                codeSignature: RemoveWhitespace(
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
                codeSignature: RemoveWhitespace(
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

            model.Fields["Options.AdvancedCrafting"] = MemoryFinder
                .GetStaticField("options")
                .GetField("showAdvancedCraftingInformation")
                .GetValue<bool>();
            model.Fields["Options.emoteButton"] = MemoryFinder
                .GetStaticField("options")
                .GetField("emoteButton")
                .GetValue<IntPtr>();
            model.Fields["currentLocation"] = MemoryFinder
                .GetStaticField("currentLocation")
                .GetField("name")
                .AsType("Netcode.NetString")
                .GetField("value")
                .GetValue<string>();
            model.Fields["DaysPlayed"] = MemoryFinder
                .GetStaticField("_player")
                .GetField("stats")
                .GetField("daysPlayed")
                .GetValue<uint>();
            model.Fields["CurrentEvent.IsWedding"] = MemoryFinder
                .GetStaticField("currentLocation")
                .GetField("currentEvent")
                .GetField("isWedding")
                .GetValue<bool>();
            model.Fields["CurrentEvent.currentCommand"] = MemoryFinder
                .GetStaticField("currentLocation")
                .GetField("currentEvent")
                .GetField("currentCommand")
                .GetValue<int>();
            model.Fields["CurrentEvent.id"] = MemoryFinder
                .GetStaticField("currentLocation")
                .GetField("currentEvent")
                .GetField("id")
                .GetValue<int>();
            model.Fields["CommunityCenter.restoreAreaIndex"] = MemoryFinder
                .GetStaticField("currentLocation")
                .AsType("StardewValley.Locations.CommunityCenter")
                .GetField("restoreAreaIndex")
                .GetValue<int>();
            model.Fields["CommunityCenter.restoreAreaPhase"] = MemoryFinder
                .GetStaticField("currentLocation")
                .AsType("StardewValley.Locations.CommunityCenter")
                .GetField("restoreAreaPhase")
                .GetValue<int>();
            model.Fields["CommunityCenter.restoreAreaTimer"] = MemoryFinder
                .GetStaticField("currentLocation")
                .AsType("StardewValley.Locations.CommunityCenter")
                .GetField("restoreAreaTimer")
                .GetValue<int>();
            model.Fields["CommunityCenter._isWatchingJunimoGoodbye"] = MemoryFinder
                .GetStaticField("currentLocation")
                .AsType("StardewValley.Locations.CommunityCenter")
                .GetField("_isWatchingJunimoGoodbye")
                .GetValue<bool>();
            model.Fields["ShopMenu.potraitPersonDialogue"] = MemoryFinder
                .GetStaticField("_activeClickableMenu")
                .AsType("StardewValley.Menus.ShopMenu")
                .GetField("potraitPersonDialogue")
                .GetValue<string>();
            return model;
        }

        public static MemoryModel V5(string gameVersion)
        {
            MemoryModel model = new MemoryModel(
                gameVersion: gameVersion,
                signatureValue: "_gameMode",
                signaturePointer: "_activeClickableMenu",
                codeSignature: RemoveWhitespace(
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
            model.Fields["Options.AdvancedCrafting"] = MemoryFinder
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
                codeSignature: RemoveWhitespace( // Game1::setGameMode
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
            model.Fields["Options.AdvancedCrafting"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("showAdvancedCraftingInformation")
                .GetValue<bool>();
            model.Fields["Options.LegacySlingshot"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("useLegacySlingshotFiring")
                .GetValue<bool>();
            model.Fields["currentLocation"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceGameLocation")
                .GetField("name")
                .GetField("value")
                .GetValue<string>();
            return model;
        }
        public static MemoryModel V5_6(string gameVersion)
        {
            MemoryModel model = new MemoryModel(
                gameVersion: gameVersion,
                signatureValue: "_gameMode",
                signaturePointer: "_activeClickableMenu",
                codeSignature: RemoveWhitespace( // Game1::setGameMode
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
            model.Fields["Options.AdvancedCrafting"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("showAdvancedCraftingInformation")
                .GetValue<bool>();
            model.Fields["Options.LegacySlingshot"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("useLegacySlingshotFiring")
                .GetValue<bool>();
            model.Fields["currentLocation"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceGameLocation")
                .GetField("name")
                .AsType("Netcode.NetString")
                .GetField("value")
                .GetValue<string>();
            model.Fields["DaysPlayed"] = MemoryFinder
                .GetStaticField("_player")
                .GetField("stats")
                .GetField("daysPlayed")
                .GetValue<uint>();
            model.Fields["CurrentEvent.IsWedding"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceGameLocation")
                .GetField("currentEvent")
                .GetField("isWedding")
                .GetValue<bool>();
            model.Fields["CurrentEvent.currentCommand"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceGameLocation")
                .GetField("currentEvent")
                .GetField("currentCommand")
                .GetValue<int>();
            model.Fields["CurrentEvent.id"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceGameLocation")
                .GetField("currentEvent")
                .GetField("id")
                .GetValue<int>();
            model.Fields["CommunityCenter.restoreAreaIndex"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceGameLocation")
                .AsType("StardewValley.Locations.CommunityCenter")
                .GetField("restoreAreaIndex")
                .GetValue<int>();
            model.Fields["CommunityCenter.restoreAreaPhase"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceGameLocation")
                .AsType("StardewValley.Locations.CommunityCenter")
                .GetField("restoreAreaPhase")
                .GetValue<int>();
            model.Fields["CommunityCenter.restoreAreaTimer"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceGameLocation")
                .AsType("StardewValley.Locations.CommunityCenter")
                .GetField("restoreAreaTimer")
                .GetValue<int>();
            model.Fields["CommunityCenter._isWatchingJunimoGoodbye"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceGameLocation")
                .AsType("StardewValley.Locations.CommunityCenter")
                .GetField("_isWatchingJunimoGoodbye")
                .GetValue<bool>();
            model.Fields["ShopMenu.potraitPersonDialogue"] = MemoryFinder
                .GetStaticField("_activeClickableMenu")
                .AsType("StardewValley.Menus.ShopMenu")
                .GetField("potraitPersonDialogue")
                .GetValue<string>();
            return model;
        }
        public static MemoryModel V5_6_x86(string gameVersion)
        {
            MemoryModel model = new MemoryModel(
                gameVersion: gameVersion,
                signatureValue: "_gameMode",
                signaturePointer: "_activeClickableMenu",
                codeSignature: RemoveWhiteSpace( // Game1::setGameMode
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
                    "A2 vvvvvvvv   ",        //- mov [02F47055],al
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
            model.Fields["Options.AdvancedCrafting"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("showAdvancedCraftingInformation")
                .GetValue<bool>();
            model.Fields["Options.LegacySlingshot"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceOptions")
                .GetField("useLegacySlingshotFiring")
                .GetValue<bool>();
            model.Fields["currentLocation.Name"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceGameLocation")
                .GetField("name")
                .AsType("Netcode.NetString")
                .GetField("value")
                .GetValue<string>();
            model.Fields["DaysPlayed"] = MemoryFinder
                .GetStaticField("_player")
                .GetField("stats")
                .GetField("daysPlayed")
                .GetValue<uint>();
            model.Fields["CurrentEvent.IsWedding"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceGameLocation")
                .GetField("currentEvent")
                .GetField("isWedding")
                .GetValue<bool>();
            model.Fields["CurrentEvent.currentCommand"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceGameLocation")
                .GetField("currentEvent")
                .GetField("currentCommand")
                .GetValue<int>();
            model.Fields["CurrentEvent.id"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceGameLocation")
                .GetField("currentEvent")
                .GetField("id")
                .GetValue<int>();
            model.Fields["CommunityCenter.restoreAreaIndex"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceGameLocation")
                .AsType("StardewValley.Locations.CommunityCenter")
                .GetField("restoreAreaIndex")
                .GetValue<int>();
            model.Fields["CommunityCenter.restoreAreaPhase"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceGameLocation")
                .AsType("StardewValley.Locations.CommunityCenter")
                .GetField("restoreAreaPhase")
                .GetValue<int>();
            model.Fields["CommunityCenter.restoreAreaTimer"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceGameLocation")
                .AsType("StardewValley.Locations.CommunityCenter")
                .GetField("restoreAreaTimer")
                .GetValue<int>();
            model.Fields["CommunityCenter._isWatchingJunimoGoodbye"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceGameLocation")
                .AsType("StardewValley.Locations.CommunityCenter")
                .GetField("_isWatchingJunimoGoodbye")
                .GetValue<bool>();
            model.Fields["ShopMenu.potraitPersonDialogue"] = MemoryFinder
                .GetStaticField("_activeClickableMenu")
                .AsType("StardewValley.Menus.ShopMenu")
                .GetField("potraitPersonDialogue")
                .GetValue<string>();
            return model;
        }

        public static MemoryModel V6_3(string gameVersion)
        {
            MemoryModel model = new MemoryModel(
                gameVersion: gameVersion,
                signatureValue: "currentGameTime",
                signaturePointer: "_activeClickableMenu",
                /*
                public static void setGameMode(byte mode)
                {
                    log.Verbose("setGameMode( '" + GameModeToString(mode) + "' )");
                    _gameMode = mode;
                    temporaryContent?.Unload();
                    switch (mode)
                    {
                    case 0:
                    {
                        bool skip = false;
                        if (activeClickableMenu != null)
                        {
                            GameTime gameTime = currentGameTime;
                            if (gameTime != null && gameTime.TotalGameTime.TotalSeconds > 10.0)
                            {
                                skip = true;
                            }
                        }
                        if (game1.instanceIndex <= 0)
                        {
                            TitleMenu titleMenu = (TitleMenu)(activeClickableMenu = new TitleMenu());
                            if (skip)
                            {
                                titleMenu.skipToTitleButtons();
                            }
                        }
                        break;
                    }
                    case 3:
                        hasApplied1_3_UpdateChanges = true;
                        hasApplied1_4_UpdateChanges = false;
                        break;
                    }
                }
// save registers onto the stack
StardewValley.Game1::setGameMode - 57                    - push rdi
StardewValley.Game1::setGameMode+1- 56                    - push rsi
StardewValley.Game1::setGameMode+2- 53                    - push rbx
// https://learn.microsoft.com/en-us/windows-hardware/drivers/debugger/x64-architecture
// on windows: rcx is the first integer argument (so first byte is cl)
// stack allocate 20 bytes
StardewValley.Game1::setGameMode+3- 48 83 EC 20           - sub rsp,20
StardewValley.Game1::setGameMode+7- C5F877                - vzeroupper


// log.Verbose calls
// pointer to the logger
StardewValley.Game1::setGameMode+A- 48 B8 F05AC9F254020000 - mov rax,00000254F2C95AF0
StardewValley.Game1::setGameMode+14- 48 8B 30              - mov rsi,[rax]
// "setGameMode( '" string address
StardewValley.Game1::setGameMode+17- 48 B8 8015CBF254020000 - mov rax,00000254F2CB1580
StardewValley.Game1::setGameMode+21- 48 8B 38              - mov rdi,[rax]
// lift the byte up to int32? importantly bl = cl now (so contains mode)
StardewValley.Game1::setGameMode+24- 0FB6 D9               - movzx ebx,cl
StardewValley.Game1::setGameMode+27- 8B CB                 - mov ecx,ebx
StardewValley.Game1::setGameMode+29- E8 4AC363FF           - call 7FFAD0D887D8   // StardewValley.Game1.GameModeToString
StardewValley.Game1::setGameMode+2E- 48 8B D0              - mov rdx,rax
StardewValley.Game1::setGameMode+31- 48 8B CF              - mov rcx,rdi
// "' )" string address
StardewValley.Game1::setGameMode+34- 49 B8 8815CBF254020000 - mov r8,00000254F2CB1588
StardewValley.Game1::setGameMode+3E- 4D 8B 00              - mov r8,[r8]
StardewValley.Game1::setGameMode+41- E8 A2C462FF           - call 7FFAD0D78948 // System.String.Concat
// move result rax into rdx
StardewValley.Game1::setGameMode+46- 48 8B D0              - mov rdx,rax
// move logger into rcx
StardewValley.Game1::setGameMode+49- 48 8B CE              - mov rcx,rsi
StardewValley.Game1::setGameMode+4C- 49 BB 580AC6D0FA7F0000 - mov r11,00007FFAD0C60A58
StardewValley.Game1::setGameMode+56- FF 15 9C4551FF        - call qword ptr [7FFAD0C60A58]


// _gameMode = mode
StardewValley.Game1::setGameMode+5C- 88 1D E4E36FFF        - mov [7FFAD0E4A8A6],bl
                7FFAD0E4A8A6
                    E4E36FFF

// Dispose temp content
// StardewValley.Game1.get_TemporaryContent
StardewValley.Game1::setGameMode+62- E8 21C363FF           - call 7FFAD0D887E8 
// move result into rcx
StardewValley.Game1::setGameMode+67- 48 8B C8              - mov rcx,rax
// null check
StardewValley.Game1::setGameMode+6A- 48 85 C9              - test rcx,rcx
StardewValley.Game1::setGameMode+6D- 74 0A                 - je StardewValley.Game1::setGameMode+79
// not null, call dispose
StardewValley.Game1::setGameMode+6F- 48 8B 01              - mov rax,[rcx]
StardewValley.Game1::setGameMode+72- 48 8B 40 48           - mov rax,[rax+48]
StardewValley.Game1::setGameMode+76- FF 50 20              - call qword ptr [rax+20]


// switch(mode)
StardewValley.Game1::setGameMode+79- 85 DB                 - test ebx,ebx
// jump to 8E if mode == 0
StardewValley.Game1::setGameMode+7B- 74 11                 - je StardewValley.Game1::setGameMode+8E
// jump to 133 if mode == 3
StardewValley.Game1::setGameMode+7D- 83 FB 03              - cmp ebx,03
StardewValley.Game1::setGameMode+80- 0F84 AD000000         - je StardewValley.Game1::setGameMode+133
// unhandled case, exit
StardewValley.Game1::setGameMode+86- 48 83 C4 20           - add rsp,20
StardewValley.Game1::setGameMode+8A- 5B                    - pop rbx
StardewValley.Game1::setGameMode+8B- 5E                    - pop rsi
StardewValley.Game1::setGameMode+8C- 5F                    - pop rdi
StardewValley.Game1::setGameMode+8D- C3                    - ret 


// case 0:
// bool flag = false
StardewValley.Game1::setGameMode+8E- 33 F6                 - xor esi,esi
// Game1.activeClickableMenu => 00000254F2C95B28 (value inside is _activeClickableMenu property get?)
StardewValley.Game1::setGameMode+90- 48 B9 285BC9F254020000 - mov rcx,00000254F2C95B28
// if menu ptr is null jump to LABEL:0xDE
StardewValley.Game1::setGameMode+9A- 48 83 39 00           - cmp qword ptr [rcx],00
StardewValley.Game1::setGameMode+9E- 74 3E                 - je StardewValley.Game1::setGameMode+DE
// Microsoft.Xna.Framework.GameTime
StardewValley.Game1::setGameMode+A0- 48 B9 C05BC9F254020000 - mov rcx,00000254F2C95BC0
StardewValley.Game1::setGameMode+AA- 48 8B 09              - mov rcx,[rcx]
StardewValley.Game1::setGameMode+AD- 48 85 C9              - test rcx,rcx
StardewValley.Game1::setGameMode+B0- 74 2C                 - je StardewValley.Game1::setGameMode+DE
StardewValley.Game1::setGameMode+B2- 48 8B 49 10           - mov rcx,[rcx+10]
StardewValley.Game1::setGameMode+B6- C5F857C0              - vxorps xmm0,xmm0,xmm0
StardewValley.Game1::setGameMode+BA- C4E1FB2AC1            - vcvtsi2sd xmm0,rax,rcx
StardewValley.Game1::setGameMode+BF- C5FB5E05 89 000000    - divsd xmm0,xmm0,[7FFAD174C5B0]
StardewValley.Game1::setGameMode+C7- C5F92E05 89 000000    - vucomisd xmm0,[7FFAD174C5B8]
StardewValley.Game1::setGameMode+CF- 0F97 C1               - seta cl
StardewValley.Game1::setGameMode+D2- 0FB6 C9               - movzx ecx,cl
StardewValley.Game1::setGameMode+D5- 85 C9                 - test ecx,ecx
StardewValley.Game1::setGameMode+D7- 74 05                 - je StardewValley.Game1::setGameMode+DE
StardewValley.Game1::setGameMode+D9- BE 01000000           - mov esi,00000001

// LABEL: 0xDE
// Game1.game1 address
StardewValley.Game1::setGameMode+DE- 48 B9 E05BC9F254020000 - mov rcx,00000254F2C95BE0
StardewValley.Game1::setGameMode+E8- 48 8B 09              - mov rcx,[rcx]
// instanceIndex <= 0
StardewValley.Game1::setGameMode+EB- 83 B9 94000000 00     - cmp dword ptr [rcx+00000094],00
StardewValley.Game1::setGameMode+F2- 7E 08                 - jle StardewValley.Game1::setGameMode+FC
StardewValley.Game1::setGameMode+F4- 48 83 C4 20           - add rsp,20
StardewValley.Game1::setGameMode+F8- 5B                    - pop rbx
StardewValley.Game1::setGameMode+F9- 5E                    - pop rsi
StardewValley.Game1::setGameMode+FA- 5F                    - pop rdi
StardewValley.Game1::setGameMode+FB- C3                    - ret 
StardewValley.Game1::setGameMode+FC- 48 B9 F88A35D1FA7F0000 - mov rcx,00007FFAD1358AF8
StardewValley.Game1::setGameMode+106- E8 85D50A5F           - call coreclr.dll+29AF0
StardewValley.Game1::setGameMode+10B- 48 8B F8              - mov rdi,rax
StardewValley.Game1::setGameMode+10E- 48 8B CF              - mov rcx,rdi
StardewValley.Game1::setGameMode+111- E8 8A1EB3FF           - call 7FFAD127E400
StardewValley.Game1::setGameMode+116- 48 8B CF              - mov rcx,rdi
StardewValley.Game1::setGameMode+119- E8 AAC463FF           - call 7FFAD0D88A28
StardewValley.Game1::setGameMode+11E- 85 F6                 - test esi,esi
StardewValley.Game1::setGameMode+120- 74 1F                 - je StardewValley.Game1::setGameMode+141
StardewValley.Game1::setGameMode+122- 48 8B CF              - mov rcx,rdi
StardewValley.Game1::setGameMode+125- E8 961EB3FF           - call 7FFAD127E420
StardewValley.Game1::setGameMode+12A- 90                    - nop 
StardewValley.Game1::setGameMode+12B- 48 83 C4 20           - add rsp,20
StardewValley.Game1::setGameMode+12F- 5B                    - pop rbx
StardewValley.Game1::setGameMode+130- 5E                    - pop rsi
StardewValley.Game1::setGameMode+131- 5F                    - pop rdi
StardewValley.Game1::setGameMode+132- C3                    - ret
// case 3:
StardewValley.Game1::setGameMode+133- C6 05 00E36FFF 01     - mov byte ptr [7FFAD0E4A89A],01
StardewValley.Game1::setGameMode+13A- C6 05 FAE26FFF 00     - mov byte ptr [7FFAD0E4A89B],00
StardewValley.Game1::setGameMode+141- 48 83 C4 20           - add rsp,20
StardewValley.Game1::setGameMode+145- 5B                    - pop rbx
StardewValley.Game1::setGameMode+146- 5E                    - pop rsi
StardewValley.Game1::setGameMode+147- 5F                    - pop rdi
StardewValley.Game1::setGameMode+148- C3                    - ret 
StardewValley.Game1::setGameMode+149- CC                    - int 3 


                */
                codeSignature: RemoveWhitespace( // Game1::setGameMode
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
                    "74 05",
                    "BE 01000000"
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

/*
 * 
 * 


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
 * 
 * 
 */