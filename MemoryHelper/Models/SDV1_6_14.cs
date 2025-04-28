using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryHelper.Models
{
    public static class SDV1_6_14
    {
        public static string SemVer = "1.6.14.24317";
        public static string GameVersion = "1.6.14-steam";
        public static string CodeSignature()
        {
            return MemoryModel.RemoveWhitespace(
                "85 DB",
                "0F 85 9C000000",
                "33 F6",
                "48 B9 pppppppppppppppp",
                "48 83 39 00",
                "74 3E",
                "48 B9 vvvvvvvvvvvvvvvv",
                "48 8B 09",
                "48 85 C9",
                "74 2C",
                "48 8B 49 10"
                );
        }

        public static MemoryModel Get()
        {
            MemoryModel model = new MemoryModel(
                gameVersion: GameVersion,
                signatureValue: "currentGameTime",
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

            model.Fields["currentLocation.Name"] = MemoryFinder
                .GetStaticField("game1")
                .GetField("instanceGameLocation")
                .GetField("name")
                .AsType("Netcode.NetString")
                .GetField("value")
                .GetValue<string>();
            model.Fields["DaysPlayed"] = MemoryFinder
                .GetStaticField("netWorldState")
                .GetField("value")
                .AsType("StardewValley.Network.NetWorldState")
                .GetField("daysPlayed")
                .AsType("Netcode.NetInt")
                .GetField("value")
                .GetValue<int>();
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
public static void setGameMode(byte mode)
{
    Game1.log.Verbose("setGameMode( '" + Game1.GameModeToString(mode) + "' )");
    Game1._gameMode = mode;
    Game1.temporaryContent?.Unload();
    if (mode != 0)
    {
        return;
    }
    bool skip = false;
    if (Game1.activeClickableMenu != null)
    {
        GameTime gameTime = Game1.currentGameTime;
        if (gameTime != null && gameTime.TotalGameTime.TotalSeconds > 10.0)
        {
            skip = true;
        }
    }
    if (Game1.game1.instanceIndex <= 0)
    {
        TitleMenu titleMenu = (TitleMenu)(Game1.activeClickableMenu = new TitleMenu());
        if (skip)
        {
            titleMenu.skipToTitleButtons();
        }
    }
}
*/

/* Op Code
StardewValley.Game1::setGameMode - 57                    - push rdi
StardewValley.Game1::setGameMode+1- 56                    - push rsi
StardewValley.Game1::setGameMode+2- 53                    - push rbx
StardewValley.Game1::setGameMode+3- 48 83 EC 20           - sub rsp,20 { 32 }
StardewValley.Game1::setGameMode+7- C5F877                - vzeroupper 
StardewValley.Game1::setGameMode+A- 48 B8 385BEDF7CF010000 - mov rax,000001CFF7ED5B38 { (1CFDFEF7498) }
StardewValley.Game1::setGameMode+14- 48 8B 30              - mov rsi,[rax]
StardewValley.Game1::setGameMode+17- 48 B8 F815EFF7CF010000 - mov rax,000001CFF7EF15F8 { (1CFE16D6750) }
StardewValley.Game1::setGameMode+21- 48 8B 38              - mov rdi,[rax]
StardewValley.Game1::setGameMode+24- 0FB6 D9               - movzx ebx,cl
StardewValley.Game1::setGameMode+27- 8B CB                 - mov ecx,ebx
StardewValley.Game1::setGameMode+29- E8 02E361FF           - call 7FFC5FA388F0
StardewValley.Game1::setGameMode+2E- 48 8B D0              - mov rdx,rax
StardewValley.Game1::setGameMode+31- 48 8B CF              - mov rcx,rdi
StardewValley.Game1::setGameMode+34- 49 B8 0016EFF7CF010000 - mov r8,000001CFF7EF1600 { (1CFE16D6788) }
StardewValley.Game1::setGameMode+3E- 4D 8B 00              - mov r8,[r8]
StardewValley.Game1::setGameMode+41- E8 42E360FF           - call 7FFC5FA28948
StardewValley.Game1::setGameMode+46- 48 8B D0              - mov rdx,rax
StardewValley.Game1::setGameMode+49- 48 8B CE              - mov rcx,rsi
StardewValley.Game1::setGameMode+4C- 49 BB A80A915FFC7F0000 - mov r11,00007FFC5F910AA8 { (7FFC5F91FD80) }
StardewValley.Game1::setGameMode+56- FF 15 8C644FFF        - call qword ptr [7FFC5F910AA8] { ->7FFC5F91FD80 }
StardewValley.Game1::setGameMode+5C- 88 1D E5076EFF        - mov [7FFC5FAFAE07],bl { (0) }
StardewValley.Game1::setGameMode+62- E8 D9E261FF           - call 7FFC5FA38900
StardewValley.Game1::setGameMode+67- 48 8B C8              - mov rcx,rax
StardewValley.Game1::setGameMode+6A- 48 85 C9              - test rcx,rcx
StardewValley.Game1::setGameMode+6D- 74 0A                 - je StardewValley.Game1::setGameMode+79
StardewValley.Game1::setGameMode+6F- 48 8B 01              - mov rax,[rcx]
StardewValley.Game1::setGameMode+72- 48 8B 40 48           - mov rax,[rax+48]
StardewValley.Game1::setGameMode+76- FF 50 20              - call qword ptr [rax+20]
StardewValley.Game1::setGameMode+79- 85 DB                 - test ebx,ebx
StardewValley.Game1::setGameMode+7B- 0F85 9C000000         - jne StardewValley.Game1::setGameMode+11D
StardewValley.Game1::setGameMode+81- 33 F6                 - xor esi,esi
StardewValley.Game1::setGameMode+83- 48 B9 705BEDF7CF010000 - mov rcx,000001CFF7ED5B70 { (1CFE16D6D50) }
StardewValley.Game1::setGameMode+8D- 48 83 39 00           - cmp qword ptr [rcx],00 { 0 }
StardewValley.Game1::setGameMode+91- 74 3E                 - je StardewValley.Game1::setGameMode+D1
StardewValley.Game1::setGameMode+93- 48 B9 085CEDF7CF010000 - mov rcx,000001CFF7ED5C08 { (1CFDFEDCF88) }
StardewValley.Game1::setGameMode+9D- 48 8B 09              - mov rcx,[rcx]
StardewValley.Game1::setGameMode+A0- 48 85 C9              - test rcx,rcx
StardewValley.Game1::setGameMode+A3- 74 2C                 - je StardewValley.Game1::setGameMode+D1
StardewValley.Game1::setGameMode+A5- 48 8B 49 10           - mov rcx,[rcx+10]
StardewValley.Game1::setGameMode+A9- C5F857C0              - vxorps xmm0,xmm0,xmm0
StardewValley.Game1::setGameMode+AD- C4E1FB2AC1            - vcvtsi2sd xmm0,rax,rcx
StardewValley.Game1::setGameMode+B2- C5FB5E05 76 000000    - divsd xmm0,xmm0,[7FFC6041A6F0] { (10000000.00) }
StardewValley.Game1::setGameMode+BA- C5F92E05 76 000000    - vucomisd xmm0,[7FFC6041A6F8] { (10.00) }
StardewValley.Game1::setGameMode+C2- 0F97 C1               - seta cl
StardewValley.Game1::setGameMode+C5- 0FB6 C9               - movzx ecx,cl
StardewValley.Game1::setGameMode+C8- 85 C9                 - test ecx,ecx
StardewValley.Game1::setGameMode+CA- 74 05                 - je StardewValley.Game1::setGameMode+D1
StardewValley.Game1::setGameMode+CC- BE 01000000           - mov esi,00000001 { 1 }
StardewValley.Game1::setGameMode+D1- 48 B9 285CEDF7CF010000 - mov rcx,000001CFF7ED5C28 { (1CFE0FE7168) }
StardewValley.Game1::setGameMode+DB- 48 8B 09              - mov rcx,[rcx]
StardewValley.Game1::setGameMode+DE- 83 B9 94000000 00     - cmp dword ptr [rcx+00000094],00 { 0 }
StardewValley.Game1::setGameMode+E5- 7E 08                 - jle StardewValley.Game1::setGameMode+EF
StardewValley.Game1::setGameMode+E7- 48 83 C4 20           - add rsp,20 { 32 }
StardewValley.Game1::setGameMode+EB- 5B                    - pop rbx
StardewValley.Game1::setGameMode+EC- 5E                    - pop rsi
StardewValley.Game1::setGameMode+ED- 5F                    - pop rdi
StardewValley.Game1::setGameMode+EE- C3                    - ret 
StardewValley.Game1::setGameMode+EF- 48 B9 60ABFD5FFC7F0000 - mov rcx,00007FFC5FFDAB60 { (17826304) }
StardewValley.Game1::setGameMode+F9- E8 42F4085F           - call coreclr.dll+29B00
StardewValley.Game1::setGameMode+FE- 48 8B F8              - mov rdi,rax
StardewValley.Game1::setGameMode+101- 48 8B CF              - mov rcx,rdi
StardewValley.Game1::setGameMode+104- E8 6FECB0FF           - call 7FFC5FF29338
StardewValley.Game1::setGameMode+109- 48 8B CF              - mov rcx,rdi
StardewValley.Game1::setGameMode+10C- E8 77E461FF           - call 7FFC5FA38B48
StardewValley.Game1::setGameMode+111- 85 F6                 - test esi,esi
StardewValley.Game1::setGameMode+113- 74 08                 - je StardewValley.Game1::setGameMode+11D
StardewValley.Game1::setGameMode+115- 48 8B CF              - mov rcx,rdi
StardewValley.Game1::setGameMode+118- E8 7BECB0FF           - call 7FFC5FF29358
StardewValley.Game1::setGameMode+11D- 90                    - nop 
StardewValley.Game1::setGameMode+11E- 48 83 C4 20           - add rsp,20 { 32 }
StardewValley.Game1::setGameMode+122- 5B                    - pop rbx
StardewValley.Game1::setGameMode+123- 5E                    - pop rsi
StardewValley.Game1::setGameMode+124- 5F                    - pop rdi
StardewValley.Game1::setGameMode+125- C3                    - ret 
StardewValley.Game1::setGameMode+126- CC                    - int 3 

*/