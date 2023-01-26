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
        //Marriage,
        //Crafts,
        //Pantry,
        //FishTank,
        //BoilerRoom,
        //BulletinBoard,
        //Vault,
        //CC,
        //Joja,
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

        public int currDay;
        public int lastDay;
        public int currFloor;
        public int lastFloor;
        public bool WeddingHearts;

        public SplitState(LiveSplitState state)
        {
            State = state;
            Hooks = new Dictionary<string, SplitHook>();
            HookNames = new List<string>();
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
            WeddingHearts = false;
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
            lastFloor = currFloor;
            lastDay = currDay;
            if (!WeddingHearts) WeddingHearts = memory.IsWeddingHearts;
            currFloor = Math.Max(currFloor, memory.CurrentMinesFloor);
            currDay = Math.Max(currDay, memory.DaysPlayed);
            //Log.Info(string.Format("DaysPlayed: {0}\tCurrMaxFloor: {1}", currDay, currFloor));
        }

        public bool ShouldSplit(MemoryModel memory)
        {
            Update(memory);
            SplitHook hook;
            if (!Hooks.TryGetValue(State.CurrentSplit.Name, out hook))
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
                //case SplitTrigger.Marriage:
                //    return WeddingHearts;
                default:
                    return false;
            }
        }
    }
}
