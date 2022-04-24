using Exiled.API.Interfaces;
using System.ComponentModel;

namespace AfkReplace
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        [Description("Enable to help find bug sources (may spam console).")]
        public bool DebugMode { get; set; } = false;
    }
}
