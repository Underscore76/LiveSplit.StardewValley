using LiveSplit.Model;
using LiveSplit.Options;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.StardewValley
{
    class Component : LogicComponent
    {
        public const string Name = "Stardew Valley Auto Splitter";
        public override string ComponentName => Name;

        private State State;

        public Component(LiveSplitState state)
        {
            State = new State(state);
        }

        public override Control GetSettingsControl(LayoutMode mode)
        {
            return State.Settings;
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            State.Update();
        }

        public override XmlNode GetSettings(XmlDocument document)
        {
            bool log = document.OuterXml != "";
            XmlElement parent = document.CreateElement("Settings");
            State.Settings.WriteXml(parent);
            return parent;
        }

        public override void SetSettings(XmlNode parent)
        {
            State.Settings.ReadXml((XmlElement)parent);
        }

        public override void Dispose()
        {
            State.Dispose();
        }
    }
}

