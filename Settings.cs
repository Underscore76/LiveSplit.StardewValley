using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.Options;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LiveSplit.StardewValley
{
    public partial class Settings : UserControl
    {
        public SplitState State;
        private const string RemoveSave_name = "RemoveSave";
        public bool RemoveSave
        {
            get { return RemoveSave_box.Checked; }
            set { RemoveSave_box.Checked = value; }
        }

        private const string RemoveRebuildGraphics_name = "RemoveRebuildGraphics";
        public bool RemoveRebuildGraphics
        {
            get { return RemoveRebuildGraphics_box.Checked; }
            set { RemoveRebuildGraphics_box.Checked = value; }
        }

        #region SettingsOverride

        private const string EnableSettingsOverride_name = "EnableSettingsOverride";
        public bool EnableSettingsOverride
        {
            get { return EnableSettingsOverride_box.Checked; }
            set { EnableSettingsOverride_box.Checked = value; }
        }

        private const string MusicVolume_name = "MusicVolume";
        public int MusicVolumeLevel
        {
            get { return MusicVolume.Value; }
            set { MusicVolume.Value = value; }
        }

        private const string SoundVolume_name = "SoundVolume";
        public int SoundVolumeLevel
        {
            get { return SoundVolume.Value; }
            set { SoundVolume.Value = value; }
        }

        private const string FootstepVolume_name = "FootstepVolume";
        public int FootstepVolumeLevel
        {
            get { return FootstepVolume.Value; }
            set { FootstepVolume.Value = value; }
        }

        private const string AmbientVolume_name = "AmbientVolume";
        public int AmbientVolumeLevel
        {
            get { return AmbientVolume.Value; }
            set { AmbientVolume.Value = value; }
        }

        private const string UnbindEmoteButton_name = "UnbindEmoteButton";
        public bool UnbindEmoteButton
        {
            get { return UnbindEmoteButton_box.Checked; }
            set { UnbindEmoteButton_box.Checked = value; }
        }

        private const string UnbindChatButton_name = "UnbindChatButton";
        public bool UnbindChatButton
        {
            get { return UnbindChatButton_box.Checked; }
            set { UnbindChatButton_box.Checked = value; }
        }

        private const string EnableZoomButton_name = "EnableZoomButton";
        public bool EnableZoomButton
        {
            get { return EnableZoomButtons_box.Checked; }
            set { EnableZoomButtons_box.Checked = value; }
        }

        private const string ToolHitButton_name = "ToolHitButton";
        public bool ToolHitButton
        {
            get { return ToolHitLocations_box.Checked; }
            set { ToolHitLocations_box.Checked = value; }
        }

        private const string AdvancedCrafting_name = "AdvancedCrafting";
        public bool AdvancedCrafting
        {
            get { return AdvancedCrafting_box.Checked; }
            set { AdvancedCrafting_box.Checked = value; }
        }

        private const string SlingshotMode_name = "SlingshotMode";
        private const int LEGACY_MODE = 1;
        public int SlingshotMode
        {
            get { return SlingshotMode_dropdown.SelectedIndex; }
            set { SlingshotMode_dropdown.SelectedIndex = value; }
        }

        private const string UseAutosplit_name = "UseAutosplit";
        public bool UseAutosplit
        {
            get { return UseAutosplit_box.Checked; }
            set { UseAutosplit_box.Checked = value; }
        }
        private const string StartOnOk_name = "StartOnOk";
        public bool StartOnOk
        {
            get { return StartOnOk_box.Checked; }
            set { StartOnOk_box.Checked = value; }
        }


        private const string SplitState_Name = "SplitState";
        #endregion

        public Settings(SplitState state)
        {
            //Log.Info("loaded the settings before calling set settings?");
            State = state;
            InitializeComponent();

            RemoveSave = true;
            RemoveRebuildGraphics = true;
            EnableSettingsOverride = true;

            MusicVolumeLevel = 0;
            SoundVolumeLevel = 0;
            AmbientVolumeLevel = 0;
            FootstepVolumeLevel = 0;

            UnbindEmoteButton = false;
            UnbindChatButton = false;
            EnableZoomButton = true;
            AdvancedCrafting = false;
            ToolHitButton = false;
            SlingshotMode = LEGACY_MODE;
            this.Load += Settings_Load;
            this.splitAssignView.CellValueChanged += SplitAssignView_CellValueChanged;

            this.Trigger.ValueMember = "Value";
            this.Trigger.DisplayMember = "Display";
            var enumValues = Enum.GetValues(typeof(SplitTrigger)).OfType<SplitTrigger>().ToList();
            this.Trigger.MaxDropDownItems = enumValues.Count;
            this.Trigger.DataSource = enumValues.Select(value => new { Display = value.ToString(), Value = value }).ToList();
            this.dayInput.ValueChanged += DayInput_ValueChanged;
            this.seasonInput.SelectedValueChanged += SeasonInput_SelectedValueChanged;
            this.seasonInput.SelectedIndex = 0;
            this.yearInput.ValueChanged += YearInput_ValueChanged;
        }

        private void Settings_Load(object sender, System.EventArgs e)
        {
            State.Reconstruct();
            if (splitAssignView != null)
            {
                splitAssignView.Rows.Clear();
                foreach (string split in State.HookNames)
                {
                    splitAssignView.Rows.Add(new object[] {
                            split,
                            null,
                            State.Hooks[split].Value,
                        }
                    );
                    splitAssignView.Rows[splitAssignView.Rows.Count - 1].Cells["Trigger"].Value = State.Hooks[split].Trigger;
                }
            }
        }

        public void WriteXml(XmlElement element)
        {
            WriteBool(element, RemoveSave_name, RemoveSave);
            WriteBool(element, RemoveRebuildGraphics_name, RemoveRebuildGraphics);
            WriteBool(element, EnableSettingsOverride_name, EnableSettingsOverride);

            WriteInt(element, MusicVolume_name, MusicVolumeLevel);
            WriteInt(element, SoundVolume_name, SoundVolumeLevel);
            WriteInt(element, AmbientVolume_name, AmbientVolumeLevel);
            WriteInt(element, FootstepVolume_name, FootstepVolumeLevel);

            WriteBool(element, UnbindEmoteButton_name, UnbindEmoteButton);
            WriteBool(element, UnbindChatButton_name, UnbindChatButton);
            WriteBool(element, EnableZoomButton_name, EnableZoomButton);
            WriteBool(element, AdvancedCrafting_name, AdvancedCrafting);
            WriteBool(element, ToolHitButton_name, ToolHitButton);
            WriteInt(element, SlingshotMode_name, SlingshotMode);

            WriteBool(element, UseAutosplit_name, UseAutosplit);
            WriteBool(element, StartOnOk_name, StartOnOk);
            WriteSplitState(element, SplitState_Name, State);
        }

        public void ReadXml(XmlElement element)
        {
            RemoveSave = ReadBool(element, RemoveSave_name, RemoveSave);
            RemoveRebuildGraphics = ReadBool(element, RemoveRebuildGraphics_name, RemoveRebuildGraphics);
            EnableSettingsOverride = ReadBool(element, EnableSettingsOverride_name, EnableSettingsOverride);

            MusicVolumeLevel = ReadInt(element, MusicVolume_name, MusicVolumeLevel);
            SoundVolumeLevel = ReadInt(element, SoundVolume_name, SoundVolumeLevel);
            AmbientVolumeLevel = ReadInt(element, AmbientVolume_name, AmbientVolumeLevel);
            FootstepVolumeLevel = ReadInt(element, FootstepVolume_name, FootstepVolumeLevel);

            UnbindEmoteButton = ReadBool(element, UnbindEmoteButton_name, UnbindEmoteButton);
            UnbindChatButton = ReadBool(element, UnbindChatButton_name, UnbindChatButton);
            EnableZoomButton = ReadBool(element, EnableZoomButton_name, EnableZoomButton);
            AdvancedCrafting = ReadBool(element, AdvancedCrafting_name, AdvancedCrafting);
            ToolHitButton = ReadBool(element, ToolHitButton_name, ToolHitButton);
            SlingshotMode = ReadInt(element, SlingshotMode_name, SlingshotMode);
            UseAutosplit = ReadBool(element, UseAutosplit_name, UseAutosplit);
            StartOnOk = ReadBool(element, StartOnOk_name, StartOnOk);
            ReadSplitState(element, SplitState_Name, State);
        }

        #region Write XML Methods

        private static void WriteBool(XmlElement parent, string name, bool value)
        {
            WriteString(parent, name, value.ToString());
        }

        private static void WriteInt(XmlElement parent, string name, int value)
        {
            WriteString(parent, name, value.ToString());
        }

        private static void WriteString(XmlElement parent, string name, string value)
        {
            XmlElement child = parent.OwnerDocument.CreateElement(name);
            child.InnerText = value;
            parent.AppendChild(child);
        }

        private static void WriteSplitState(XmlElement parent, string name, SplitState state)
        {
            XmlElement element = parent.OwnerDocument.CreateElement(name);
            foreach (var hookName in state.HookNames)
            {
                if (!state.Hooks.ContainsKey(hookName)) continue;

                XmlElement child = parent.OwnerDocument.CreateElement("Hook");
                WriteString(child, "HookName", hookName);
                WriteString(child, "Trigger", state.Hooks[hookName].Trigger.ToString());
                WriteInt(child, "Value", state.Hooks[hookName].Value);
                element.AppendChild(child);
            }
            parent.AppendChild(element);
        }

        #endregion
        #region Read XML Methods

        private static bool ReadBool(XmlElement parent, string name, bool default_)
        {
            string str = ReadString(parent, name, null);
            if (str != null && bool.TryParse(str, out bool value)) return value;
            return default_;
        }

        private static int ReadInt(XmlElement parent, string name, int default_)
        {
            string str = ReadString(parent, name, null);
            if (str != null && int.TryParse(str, out int value)) return value;
            return default_;
        }

        private static string ReadString(XmlElement parent, string name, string default_)
        {
            XmlElement child = parent[name];
            if (child != null) return child.InnerText;
            return default_;
        }

        private static void ReadSplitState(XmlElement parent, string name, SplitState state)
        {
            XmlElement element = parent[name];
            if (element == null) return;

            state.Hooks.Clear();
            state.HookNames.Clear();
            foreach (XmlElement child in element.ChildNodes)
            {
                string hookName = ReadString(child, "HookName", "");
                string triggerName = ReadString(child, "Trigger", "Manual");
                SplitTrigger trigger = SplitTrigger.Manual;
                if (!Enum.TryParse(triggerName, out trigger))
                {
                    trigger = SplitTrigger.Manual;
                }

                int value = ReadInt(child, "Value", 0);
                if (hookName != "")
                {
                    state.HookNames.Add(hookName);
                    state.Hooks.Add(hookName, new SplitHook { Value = value, Trigger = trigger });
                }
            }
        }
        #endregion


        public bool Override(MemoryModel memory)
        {
            if (memory.IsTitleMenu) return true;

            memory.SetMusicVolume(MusicVolumeLevel);
            memory.SetSoundVolume(SoundVolumeLevel);
            memory.SetFootstepVolume(FootstepVolumeLevel);
            memory.SetAmbientVolume(AmbientVolumeLevel);

            if (UnbindEmoteButton) memory.UnbindEmoteButton();
            if (UnbindChatButton) memory.UnbindChatButton();

            if (EnableZoomButton) memory.EnableZoomButton();
            if (AdvancedCrafting) memory.AdvancedCrafting();
            if (ToolHitButton) memory.ToolHitIndicator();

            memory.SlingshotMode(SlingshotMode == LEGACY_MODE);

            return false;
        }


        private void SplitAssignView_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            //Log.Info("SplitAssignView_CellValueChanged");
            switch (splitAssignView.Columns[e.ColumnIndex].Name)
            {
                case "Trigger":
                    State.Hooks[State.HookNames[e.RowIndex]].Trigger = (SplitTrigger)splitAssignView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    break;
                case "TriggerValue":
                    if (int.TryParse(Convert.ToString(splitAssignView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), out int val))
                    {
                        State.Hooks[State.HookNames[e.RowIndex]].Value = val;
                        //Log.Info(string.Format("Setting {0},{1} to {2}", e.RowIndex, e.ColumnIndex, i));
                    }
                    else
                    {
                        splitAssignView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = State.Hooks[State.HookNames[e.RowIndex]].Value;
                    }
                    break;
            }
        }

        private void SetActualDay()
        {
            int day = (int)this.dayInput.Value;
            day += 28 * this.seasonInput.SelectedIndex;
            day += 112 * ((int)this.yearInput.Value - 1);
            this.actualDay.Text = day.ToString();
        }

        private void YearInput_ValueChanged(object sender, EventArgs e)
        {
            SetActualDay();
        }

        private void SeasonInput_SelectedValueChanged(object sender, EventArgs e)
        {
            SetActualDay();
        }

        private void DayInput_ValueChanged(object sender, EventArgs e)
        {
            SetActualDay();
        }
    }
}
