using LiveSplit.ComponentUtil;
using LiveSplit.Model;
using LiveSplit.Options;
using LiveSplit.StardewValley.MemoryModels;
using System;
using System.Diagnostics;
using System.Linq;

namespace LiveSplit.StardewValley
{
    public class State
    {
        public Settings Settings;

        private const int ProcessScanInterval = 1000;
        private Stopwatch Stopwatch;
        private Process Process;
        private MemoryModel Memory;

        private TimerModel Timer;
        private bool StartupTitleMenu;
        private bool NeedsOverride;

        public State(LiveSplitState state)
        {
            Settings = new Settings();
            Stopwatch = Stopwatch.StartNew();
            Process = null;
            Memory = null;
            Timer = new TimerModel() { CurrentState = state };
            state.OnStart += OnStart;
        }

        private void OnStart(object sender, EventArgs e)
        {
            Timer.InitializeGameTime(); // trigger GameTime start
            Log.Info("[SDV] timer started");
            StartupTitleMenu = Memory.IsTitleMenu;
            NeedsOverride = Settings.EnableSettingsOverride;
        }

        public void Update()
        {
            ScanForProcess();

            if (Memory == null || !Memory.FullyAttached)
            {
                return;
            }
            switch (Timer.CurrentState.CurrentPhase)
            {
                case TimerPhase.NotRunning:
                    if (ShouldStart())
                    {
                        Timer.Start();
                    }
                    break;
                case TimerPhase.Running:
                    if (!StartupTitleMenu) { OverrideSettings(); }
                    if (ShouldReset())
                    {
                        Timer.Reset();
                    }
                    else if (ShouldSplit())
                    {
                        Timer.Split();
                    }
                    Timer.CurrentState.IsGameTimePaused = IsLoading();
                    break;
                case TimerPhase.Paused:
                    // nothing
                    break;
                case TimerPhase.Ended:
                    if (ShouldReset())
                    {
                        Timer.Reset();
                    }
                    break;
            }
        }

        private void ScanForProcess()
        {
            if (Process != null && Process.HasExited)
            {
                Log.Info("[SDV] Game closed");
                Process = null;
                Memory = null;
                Stopwatch.Restart();
            }
            else if (Process == null && Stopwatch.ElapsedMilliseconds > ProcessScanInterval)
            {
                Process = Process.GetProcesses().FirstOrDefault(p => p.ProcessName == "Stardew Valley" && !p.HasExited);
                if (Process != null)
                {
                    string version = Process.MainModuleWow64Safe().FileVersionInfo.FileVersion;
                    switch (version)
                    {
                        case "1.2.6400.27469":
                            Log.Info("[SDV] Attached to version 1.2.33-steam");
                            Memory = new MemoryModel_2_33(Process);
                            break;
                        case "1.3.7114.34001":
                            Log.Info("[SDV] Attached to version 1.3.36-steam");
                            Memory = new MemoryModel_3_36(Process);
                            break;
                        case "1.3.7269.37809":
                            Log.Info("[SDV] Attached to version 1.4.0-steam");
                            Memory = new MemoryModel_4_0(Process);
                            break;
                        case "1.3.7346.34283":
                            Log.Info("[SDV] Attached to version 1.4.5-steam");
                            Memory = new MemoryModel_4_5(Process);
                            break;
                        case "1.3.7853.31734":
                            Log.Info("[SDV] Attached to version 1.5.4-steam");
                            Memory = new MemoryModel_5_4(Process);
                            break;
                        case "1.3.37.0":
                            Log.Info("[SDV] Attached to version 1.5.5-steam");
                            Memory = new MemoryModel_5_5(Process);
                            break;
                        case "1.5.6.21356":
                        case "1.5.6.22018":
                            Log.Info("[SDV] Attached to version 1.5.6-steam");
                            Memory = new MemoryModel_5_6(Process);
                            break;
                        case "1.6.3.24087":
                            Log.Info("[SDV] Attached to version 1.6.3-steam");
                            Memory = new MemoryModel_6_3(Process);
                            break;
                    }
                    if (Memory == null)
                    {
                        Log.Error("[SDV] Unknown file version " + version);
                    }
                }
                Stopwatch.Restart();
            }
            else if (Memory != null && !Memory.FullyAttached && Stopwatch.ElapsedMilliseconds > ProcessScanInterval)
            {
                Memory.ScanAgain();
                Stopwatch.Restart();
            }
        }

        private bool IsLoading()
        {
            // if someone reloads the title menu, we don't want to pause the timer
            StartupTitleMenu &= Memory.IsTitleMenu;
            //Log.Info(string.Format("[SDV] IsTitleMenu: {0} {1}", Memory.IsTitleMenu, StartupTitleMenu));
            //Log.Info(string.Format("[SDV] NeweDayTaskExists: {0}", Memory.NewDayTaskExists));
            //Log.Info(string.Format("[SDV] IsSaving: {0}", Memory.IsSaving));
            if (StartupTitleMenu) return true;
            if (Memory.NewDayTaskExists) return true;
            if (Settings.RemoveRebuildGraphics && Memory.IsConstructingGraphics) return true;
            if (Settings.RemoveSave && Memory.IsSaving) return true;

            return false;
        }

        private bool ShouldStart()
        {
            // not sure yet how to detect being in the new game menu

            return false;
        }

        private bool ShouldSplit()
        {
            // nothing yet as i am not sure how to get/parse split data

            return false;
        }

        private bool ShouldReset()
        {
            // not sure yet how to detect being in the new game menu

            return false;
        }

        public void Dispose()
        {
            Settings.Dispose();
        }

        public void OverrideSettings()
        {
            if (!NeedsOverride) return;
            NeedsOverride = Settings.Override(Memory);
        }
    }
}
