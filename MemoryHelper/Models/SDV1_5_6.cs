using MemoryHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryHelper.Models
{
    public static class SDV1_5_6
    {
        public static string SemVer = "1.5.6.22018";
        public static string GameVersion = "1.5.6-steam";
        public static string CodeSignature()
        {
            return MemoryModel.RemoveWhitespace(
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
        }

        public static MemoryModel Get()
        {
            MemoryModel model = new MemoryModel(
                gameVersion: GameVersion,
                signatureValue: "_gameMode",
                signaturePointer: "_activeClickableMenu",
                codeSignature: CodeSignature()
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
            model.Fields["Farm.grandpaScore.Value"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceGameLocation")
                .AsType("StardewValley.Farm")
                .GetField("grandpaScore")
                .AsType("Netcode.NetInt")
                .GetField("value")
                .GetValue<int>();
            return model;
        }
    }
}

/*
Attaching to version 1.5.6-steam ( 1.5.6.22018 )...
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
Options.AdvancedCrafting : ReadValue<Boolean>( 184, 80, 324 )
Options.LegacySlingshot : ReadValue<Boolean>( 184, 80, 334 )
currentLocation : ReadString( 184, 32, 272, 64, 0 )
DaysPlayed : ReadValue<UInt32>( -928, 496, 184 )
CurrentEvent.IsWedding : ReadValue<Boolean>( 184, 32, 400, 300 )
CurrentEvent.currentCommand : ReadValue<Int32>( 184, 32, 400, 232 )
CurrentEvent.id : ReadValue<Int32>( 184, 32, 400, 264 )
CommunityCenter.restoreAreaIndex : ReadValue<Int32>( 184, 32, 688 )
CommunityCenter.restoreAreaPhase : ReadValue<Int32>( 184, 32, 684 )
CommunityCenter.restoreAreaTimer : ReadValue<Int32>( 184, 32, 680 )
CommunityCenter._isWatchingJunimoGoodbye : ReadValue<Boolean>( 184, 32, 692 )
ShopMenu.potraitPersonDialogue : ReadString( 0, 264, 0 )
Farm.grandpaScore.Value : ReadValue<Int32>( 184, 32, 592, 68 )
*/