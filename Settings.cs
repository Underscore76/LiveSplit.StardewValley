using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.StardewValley
{
    public partial class Settings : UserControl
    {
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
        #endregion

        public Settings()
        {
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
        #endregion


        public bool Override(MemoryModel memory)
        {
            if (memory.IsTitleMenu) return true;

            memory.SetMusicVolume(MusicVolumeLevel);
            memory.SetSoundVolume(SoundVolumeLevel);
            memory.SetFootstepVolume(FootstepVolumeLevel);
            memory.SetAmbientVolume(AmbientVolumeLevel);

            if (!(memory is MemoryModels.MemoryModel_5_5))
            {
                if (UnbindEmoteButton) memory.UnbindEmoteButton();
                if (UnbindChatButton) memory.UnbindChatButton();
            }
            if (EnableZoomButton) memory.EnableZoomButton();
            if (AdvancedCrafting) memory.AdvancedCrafting();
            if (ToolHitButton) memory.ToolHitIndicator();

            memory.SlingshotMode(SlingshotMode == LEGACY_MODE);

            return false;
        }
    }
}
