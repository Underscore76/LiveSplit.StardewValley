using LiveSplit.Model;
using LiveSplit.UI.Components;
using System;
using System.Reflection;

namespace LiveSplit.StardewValley
{
    public class Factory : IComponentFactory
    {
        public string ComponentName => Component.Name;
        public string Description => "Load Remover for Stardew Valley";
        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        public ComponentCategory Category => ComponentCategory.Control;

        public string UpdateName => Component.Name;
        public string UpdateURL => "https://raw.githubusercontent.com/underscore76/LiveSplit.StardewValley/master/";
        public string XMLURL => UpdateURL + "Components/Updates.xml";

        public IComponent Create(LiveSplitState state)
        {
            return new Component(state);
        }
    }
}
