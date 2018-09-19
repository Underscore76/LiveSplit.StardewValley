using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;
using System.Reflection;

namespace LiveSplit.StardewValley
{
    public class Factory : IComponentFactory
    {
        public string ComponentName => Component.Name;
        public string Description => "Auto Splitter for Stardew Valley";
        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        public ComponentCategory Category => ComponentCategory.Control;

        public string UpdateName => Component.Name;
        public string UpdateURL => "https://raw.githubusercontent.com/bluecheetah001/LiveSplit.StardewValley/master/Components/";
        public string XMLURL => UpdateURL + "Updates.xml";

        public IComponent Create(LiveSplitState state)
        {
            return new Component(state);
        }
    }
}
