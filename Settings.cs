using System.Windows.Forms;
using System.Xml;
using LiveSplit.ComponentUtil;
using LiveSplit.Options;

namespace LiveSplit.StardewValley
{
    public partial class Settings : UserControl
    {
        private const string RemovePause_name = "RemovePause";
        public bool RemovePause
        {
            get { return RemovePause_box.Checked; }
            set { RemovePause_box.Checked = value; }
        }

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

        public Settings()
        {
            InitializeComponent();
            
            RemovePause = true;
            RemoveSave = true;
            RemoveRebuildGraphics = true;
        }

        public void WriteXml(XmlElement element)
        {
            WriteBool(element, RemovePause_name, RemovePause);
            WriteBool(element, RemoveSave_name, RemoveSave);
            WriteBool(element, RemoveRebuildGraphics_name, RemoveRebuildGraphics);
        }

        public void ReadXml(XmlElement element)
        {
            RemovePause = ReadBool(element, RemovePause_name, RemovePause);
            RemoveSave = ReadBool(element, RemoveSave_name, RemoveSave);
            RemoveRebuildGraphics = ReadBool(element, RemoveRebuildGraphics_name, RemoveRebuildGraphics);
        }

        #region Write XML Methods

        private static void WriteBool(XmlElement parent, string name, bool value)
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

        private static string ReadString(XmlElement parent, string name, string default_)
        {
            XmlElement child = parent[name];
            if (child != null) return child.InnerText;
            return default_;
        }

        #endregion
    }
}
