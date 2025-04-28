using LiveSplit.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using LiveSplit.Options;

namespace LiveSplit.StardewValley
{
    public enum SplitTrigger
    {
        Manual,
        DayStart,
        DayEnd,
        MinesFloor,
        Marriage,
        Crafts,
        Pantry,
        FishTank,
        BoilerRoom,
        BulletinBoard,
        Vault,
        CC,
        Joja,
        HatMouse,
        FourCandles,
    }
    public class SplitHook
    {
        public SplitTrigger Trigger = SplitTrigger.Manual;
        public int Value = 0;
    }

    public class SplitState
    {
        public LiveSplitState State;
        public Dictionary<string, SplitHook> Hooks;
        public List<string> HookNames;
        public HashSet<string> AlreadyRunSplits;

        public int currDay;
        public int lastDay;
        public int currFloor;
        public int lastFloor;
        public int CC_restoreAreaTimer;
        public int CC_restoreAreaIndex;
        public int CC_restoreAreaPhase;
        public bool CC_isWatchingJunimoGoodbye;
        public bool WeddingHearts;
        public bool JojaVendingMachine;
        public bool HatMouse;
        public string LastSplit;
        public string CurrentLocationName;
        public int GrandpaScore;

        public SplitState(LiveSplitState state)
        {
            State = state;
            Hooks = new Dictionary<string, SplitHook>();
            HookNames = new List<string>();
            AlreadyRunSplits = new HashSet<string>();
            Reconstruct();
        }

        public void Reconstruct()
        {
            if (IsValid()) return;
            List<string> currSplits = new List<string>(CurrentSplits);

            //Log.Info("CurrentSplits: " + string.Join(",", currSplits));
            // add any new hook
            foreach (string split in currSplits)
            {
                if (Hooks.ContainsKey(split))
                    continue;
                Hooks.Add(split, new SplitHook());
            }
            // remove any unused hooks
            foreach (string split in HookNames)
            {
                if (currSplits.Contains(split))
                    continue;
                if (Hooks.ContainsKey(split))
                    Hooks.Remove(split);
            }
            HookNames = currSplits;
        }
        public void Reset()
        {
            currDay = 0;
            lastDay = 0;
            currFloor = 0;
            lastFloor = 0;
            CC_restoreAreaTimer = -1;
            CC_restoreAreaIndex = -1;
            CC_restoreAreaPhase = -1;
            CC_isWatchingJunimoGoodbye = false;
            GrandpaScore = 0;
            WeddingHearts = false;
            JojaVendingMachine = false;
            HatMouse = false;
            LastSplit = "";
            AlreadyRunSplits.Clear();
        }

        public bool IsValid()
        {
            return HookNames != null && HookNames.SequenceEqual(CurrentSplits);
        }
        public IEnumerable<string> CurrentSplits
        {
            get
            {
                return State.Run.Select(seg => seg.Name);
            }
        }

        public void UpdateSplitHook(string split, SplitHook hook)
        {
            if (Hooks.ContainsKey(split))
            {
                Hooks[split] = hook;
            }
        }

        public void Update(MemoryModel memory)
        {
            CurrentLocationName = memory.CurrentLocationName;
            // handle day changes
            lastDay = currDay;
            currDay = Math.Max(currDay, memory.DaysPlayed);
            //Log.Info($"[SDV] {CurrentLocationName} {currDay}");
            // handle mines floor changes
            lastFloor = currFloor;
            if (CurrentLocationName.StartsWith("UndergroundMine"))
            {
                if (Int32.TryParse(CurrentLocationName.Substring("UndergroundMine".Length), out int floor))
                {
                    currFloor = Math.Max(currFloor, floor);
                }
            }

            // town events
            if (CurrentLocationName == "Town")
            {
                if (!WeddingHearts)
                {
                    WeddingHearts = memory.IsWeddingHearts;
                }
                if (!JojaVendingMachine)
                {
                    JojaVendingMachine = memory.JojaVendingMachine;
                }
            }

            if (CurrentLocationName == "Farm")
            {
                if (GrandpaScore < 4)
                    GrandpaScore = memory.Farm_grandpaScore;
            }

            // Hiyo, poke!
            if (CurrentLocationName == "Forest" && !HatMouse)
            {
                //Log.Info($"[SDV] {memory.ShopMenu_PersonPortraitDialogue}");
                HatMouse = memory.ShopMenu_PersonPortraitDialogue.Contains("Hiyo, poke.");
            }

            // CC bundles
            if (CurrentLocationName == "CommunityCenter")
            {
                CC_restoreAreaTimer = memory.CC_restoreAreaTimer;
                CC_restoreAreaIndex = memory.CC_restoreAreaIndex;
                CC_restoreAreaPhase = memory.CC_restoreAreaPhase;
                CC_isWatchingJunimoGoodbye = memory.CC_isWatchingJunimoGoodbye;
            }
            else
            {
                CC_restoreAreaTimer = -1;
                CC_restoreAreaIndex = -1;
                CC_restoreAreaPhase = -1;
                CC_isWatchingJunimoGoodbye = false;
            }

            //Log.Info($"[SDV] {CurrentLocationName} {CC_restoreAreaIndex},{CC_restoreAreaPhase},{CC_restoreAreaTimer},{CC_isWatchingJunimoGoodbye}");
            if (LastSplit != State.CurrentSplit.Name && !AlreadyRunSplits.Contains(State.CurrentSplit.Name))
            {
                // implies that the CurrentSplit has changed and it's not already been checked
                // must have been a forward split, mark the prior split as invalid
                AlreadyRunSplits.Add(LastSplit);
            }
            LastSplit = State.CurrentSplit.Name;
        }

        public bool ShouldSplit(MemoryModel memory)
        {
            Update(memory);
            SplitHook hook;
            if (AlreadyRunSplits.Contains(State.CurrentSplit.Name) || !Hooks.TryGetValue(State.CurrentSplit.Name, out hook))
            {
                return false;
            }
            switch (hook.Trigger)
            {
                case SplitTrigger.Manual:
                    return false;
                case SplitTrigger.DayStart:
                    return hook.Value == currDay;
                case SplitTrigger.DayEnd:
                    return hook.Value + 1 == currDay;
                case SplitTrigger.MinesFloor:
                    return hook.Value == currFloor;
                case SplitTrigger.Marriage:
                    return WeddingHearts;
                case SplitTrigger.Pantry:
                    return CC_restoreAreaIndex == 0 && CC_restoreAreaPhase == 3 && CC_restoreAreaTimer > 0;
                case SplitTrigger.Crafts:
                    return CC_restoreAreaIndex == 1 && CC_restoreAreaPhase == 3 && CC_restoreAreaTimer > 0;
                case SplitTrigger.FishTank:
                    return CC_restoreAreaIndex == 2 && CC_restoreAreaPhase == 3 && CC_restoreAreaTimer > 0;
                case SplitTrigger.BoilerRoom:
                    return CC_restoreAreaIndex == 3 && CC_restoreAreaPhase == 3 && CC_restoreAreaTimer > 0;
                case SplitTrigger.Vault:
                    return CC_restoreAreaIndex == 4 && CC_restoreAreaPhase == 3 && CC_restoreAreaTimer > 0;
                case SplitTrigger.BulletinBoard:
                    return CC_restoreAreaIndex == 5 && CC_restoreAreaPhase == 3 && CC_restoreAreaTimer > 0;
                case SplitTrigger.CC:
                    return CC_restoreAreaPhase == 3 && CC_isWatchingJunimoGoodbye && CC_restoreAreaTimer > 0;
                case SplitTrigger.Joja:
                    return JojaVendingMachine;
                case SplitTrigger.HatMouse:
                    return HatMouse;
                case SplitTrigger.FourCandles:
                    return GrandpaScore == 4;
                default:
                    return false;
            }
        }
    }
}