using LiveSplit.ComponentUtil;
using LiveSplit.Model;
using LiveSplit.Options;
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

        public State(LiveSplitState state)
        {
            Settings = new Settings();
            Stopwatch = Stopwatch.StartNew();
            Process = null;
            Memory = null;
            Timer = new TimerModel() { CurrentState = state };
            state.OnStart += OnStart;
        }

        // always use a game timer
        private void OnStart(object sender, EventArgs e)
        {
            Timer.InitializeGameTime();
            Log.Info("[SDV] timer started");
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
                            Memory = new MemoryModel_2(Process);
                            break;
                        case "1.3.6794.32951":
                            Log.Info("[SDV] Attached to version 1.3.28-steam");
                            Memory = new MemoryModel_3(Process);
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
            if (Settings.RemovePause && Memory.IsPaused) return true;
            if (Settings.RemoveSave && Memory.IsSaving) return true;
            if (Settings.RemoveRebuildGraphics && Memory.IsConstructingGraphics) return true;

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
    }
}
