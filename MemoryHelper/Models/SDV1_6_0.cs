using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryHelper.Models
{
    public static class SDV1_6_0
    {
        public static string SemVer = "1.6.0.24079";
        public static string GameVersion = "1.6.0-steam";
        public static string CodeSignature()
        {
            return MemoryModel.RemoveWhitespace(
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
*/

/* Op Code
 * StardewValley.Game1::setGameMode - 57                    - push rdi
StardewValley.Game1::setGameMode+1- 56                    - push rsi
StardewValley.Game1::setGameMode+2- 53                    - push rbx
StardewValley.Game1::setGameMode+3- 48 83 EC 20           - sub rsp,20
StardewValley.Game1::setGameMode+7- C5F877                - vzeroupper 
StardewValley.Game1::setGameMode+A- 48 B8 F05A524461020000 - mov rax,0000026144525AF0
StardewValley.Game1::setGameMode+14- 48 8B 30              - mov rsi,[rax]
StardewValley.Game1::setGameMode+17- 48 B8 3816544461020000 - mov rax,0000026144541638
StardewValley.Game1::setGameMode+21- 48 8B 38              - mov rdi,[rax]
StardewValley.Game1::setGameMode+24- 0FB6 D9               - movzx ebx,cl
StardewValley.Game1::setGameMode+27- 8B CB                 - mov ecx,ebx
StardewValley.Game1::setGameMode+29- E8 CAA362FF           - call 7FF7DB6787D8
StardewValley.Game1::setGameMode+2E- 48 8B D0              - mov rdx,rax
StardewValley.Game1::setGameMode+31- 48 8B CF              - mov rcx,rdi
StardewValley.Game1::setGameMode+34- 49 B8 4016544461020000 - mov r8,0000026144541640
StardewValley.Game1::setGameMode+3E- 4D 8B 00              - mov r8,[r8]
StardewValley.Game1::setGameMode+41- E8 22A561FF           - call 7FF7DB668948
StardewValley.Game1::setGameMode+46- 48 8B D0              - mov rdx,rax
StardewValley.Game1::setGameMode+49- 48 8B CE              - mov rcx,rsi
StardewValley.Game1::setGameMode+4C- 49 BB 780A55DBF77F0000 - mov r11,00007FF7DB550A78
StardewValley.Game1::setGameMode+56- FF 15 3C2650FF        - call qword ptr [7FF7DB550A78]
StardewValley.Game1::setGameMode+5C- 88 1D 84C46EFF        - mov [7FF7DB73A8C6],bl
StardewValley.Game1::setGameMode+62- E8 A1A362FF           - call 7FF7DB6787E8
StardewValley.Game1::setGameMode+67- 48 8B C8              - mov rcx,rax
StardewValley.Game1::setGameMode+6A- 48 85 C9              - test rcx,rcx
StardewValley.Game1::setGameMode+6D- 74 0A                 - je StardewValley.Game1::setGameMode+79
StardewValley.Game1::setGameMode+6F- 48 8B 01              - mov rax,[rcx]
StardewValley.Game1::setGameMode+72- 48 8B 40 48           - mov rax,[rax+48]
StardewValley.Game1::setGameMode+76- FF 50 20              - call qword ptr [rax+20]
StardewValley.Game1::setGameMode+79- 85 DB                 - test ebx,ebx
StardewValley.Game1::setGameMode+7B- 74 11                 - je StardewValley.Game1::setGameMode+8E
StardewValley.Game1::setGameMode+7D- 83 FB 03              - cmp ebx,03
StardewValley.Game1::setGameMode+80- 0F84 AD000000         - je StardewValley.Game1::setGameMode+133
StardewValley.Game1::setGameMode+86- 48 83 C4 20           - add rsp,20
StardewValley.Game1::setGameMode+8A- 5B                    - pop rbx
StardewValley.Game1::setGameMode+8B- 5E                    - pop rsi
StardewValley.Game1::setGameMode+8C- 5F                    - pop rdi
StardewValley.Game1::setGameMode+8D- C3                    - ret 
StardewValley.Game1::setGameMode+8E- 33 F6                 - xor esi,esi
StardewValley.Game1::setGameMode+90- 48 B9 285B524461020000 - mov rcx,0000026144525B28
StardewValley.Game1::setGameMode+9A- 48 83 39 00           - cmp qword ptr [rcx],00
StardewValley.Game1::setGameMode+9E- 74 3E                 - je StardewValley.Game1::setGameMode+DE
StardewValley.Game1::setGameMode+A0- 48 B9 C05B524461020000 - mov rcx,0000026144525BC0
StardewValley.Game1::setGameMode+AA- 48 8B 09              - mov rcx,[rcx]
StardewValley.Game1::setGameMode+AD- 48 85 C9              - test rcx,rcx
StardewValley.Game1::setGameMode+B0- 74 2C                 - je StardewValley.Game1::setGameMode+DE
StardewValley.Game1::setGameMode+B2- 48 8B 49 10           - mov rcx,[rcx+10]
StardewValley.Game1::setGameMode+B6- C5F857C0              - vxorps xmm0,xmm0,xmm0
StardewValley.Game1::setGameMode+BA- C4E1FB2AC1            - vcvtsi2sd xmm0,rax,rcx
StardewValley.Game1::setGameMode+BF- C5FB5E05 89 000000    - divsd xmm0,xmm0,[7FF7DC04E530]
StardewValley.Game1::setGameMode+C7- C5F92E05 89 000000    - vucomisd xmm0,[7FF7DC04E538]
StardewValley.Game1::setGameMode+CF- 0F97 C1               - seta cl
StardewValley.Game1::setGameMode+D2- 0FB6 C9               - movzx ecx,cl
StardewValley.Game1::setGameMode+D5- 85 C9                 - test ecx,ecx
StardewValley.Game1::setGameMode+D7- 74 05                 - je StardewValley.Game1::setGameMode+DE
StardewValley.Game1::setGameMode+D9- BE 01000000           - mov esi,00000001
StardewValley.Game1::setGameMode+DE- 48 B9 E05B524461020000 - mov rcx,0000026144525BE0
StardewValley.Game1::setGameMode+E8- 48 8B 09              - mov rcx,[rcx]
StardewValley.Game1::setGameMode+EB- 83 B9 94000000 00     - cmp dword ptr [rcx+00000094],00
StardewValley.Game1::setGameMode+F2- 7E 08                 - jle StardewValley.Game1::setGameMode+FC
StardewValley.Game1::setGameMode+F4- 48 83 C4 20           - add rsp,20
StardewValley.Game1::setGameMode+F8- 5B                    - pop rbx
StardewValley.Game1::setGameMode+F9- 5E                    - pop rsi
StardewValley.Game1::setGameMode+FA- 5F                    - pop rdi
StardewValley.Game1::setGameMode+FB- C3                    - ret 
StardewValley.Game1::setGameMode+FC- 48 B9 F88AC4DBF77F0000 - mov rcx,00007FF7DBC48AF8
StardewValley.Game1::setGameMode+106- E8 05B6065F           - call coreclr.dll+29AF0
StardewValley.Game1::setGameMode+10B- 48 8B F8              - mov rdi,rax
StardewValley.Game1::setGameMode+10E- 48 8B CF              - mov rcx,rdi
StardewValley.Game1::setGameMode+111- E8 0AFFB1FF           - call 7FF7DBB6E400
StardewValley.Game1::setGameMode+116- 48 8B CF              - mov rcx,rdi
StardewValley.Game1::setGameMode+119- E8 2AA562FF           - call 7FF7DB678A28
StardewValley.Game1::setGameMode+11E- 85 F6                 - test esi,esi
StardewValley.Game1::setGameMode+120- 74 1F                 - je StardewValley.Game1::setGameMode+141
StardewValley.Game1::setGameMode+122- 48 8B CF              - mov rcx,rdi
StardewValley.Game1::setGameMode+125- E8 16FFB1FF           - call 7FF7DBB6E420
StardewValley.Game1::setGameMode+12A- 90                    - nop 
StardewValley.Game1::setGameMode+12B- 48 83 C4 20           - add rsp,20
StardewValley.Game1::setGameMode+12F- 5B                    - pop rbx
StardewValley.Game1::setGameMode+130- 5E                    - pop rsi
StardewValley.Game1::setGameMode+131- 5F                    - pop rdi
StardewValley.Game1::setGameMode+132- C3                    - ret 
StardewValley.Game1::setGameMode+133- C6 05 A0C36EFF 01     - mov byte ptr [7FF7DB73A8BA],01
StardewValley.Game1::setGameMode+13A- C6 05 9AC36EFF 00     - mov byte ptr [7FF7DB73A8BB],00
StardewValley.Game1::setGameMode+141- 48 83 C4 20           - add rsp,20
StardewValley.Game1::setGameMode+145- 5B                    - pop rbx
StardewValley.Game1::setGameMode+146- 5E                    - pop rsi
StardewValley.Game1::setGameMode+147- 5F                    - pop rdi
StardewValley.Game1::setGameMode+148- C3                    - ret 
*/