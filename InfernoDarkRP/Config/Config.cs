using Exiled.API.Interfaces;
using System.ComponentModel;

namespace InfernoDarkRP
{
    public class Config : IConfig
    {
        [Description("Whether or not this plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;
        
        [Description("Whether or not to display debug messages in the server console.")]
        public bool Debug { get; set; } = true;
    }
}