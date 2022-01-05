using ReactiveUI;
using Stardrop.Models;
using Stardrop.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace Stardrop.ViewModels
{
    public class SettingsWindowViewModel : ViewModelBase
    {
        // Setting bindings
        public string SMAPIPath { get { return Program.settings.SMAPIFolderPath; } set { Program.settings.SMAPIFolderPath = value; Pathing.SetSmapiPath(Program.settings.SMAPIFolderPath, String.IsNullOrEmpty(Program.settings.ModFolderPath)); } }
        public string ModFolderPath { get { return Program.settings.ModFolderPath; } set { Program.settings.ModFolderPath = value; Pathing.SetModPath(Program.settings.ModFolderPath); } }
        public bool IgnoreHiddenFolders { get { return Program.settings.IgnoreHiddenFolders; } set { Program.settings.IgnoreHiddenFolders = value; } }
        public bool EnableProfileSpecificModConfigs { get { return Program.settings.EnableProfileSpecificModConfigs; } set { Program.settings.EnableProfileSpecificModConfigs = value; } }

        // Tooltips
        public string ToolTip_SMAPI { get; set; }
        public string ToolTip_ModFolder { get; set; }
        public string ToolTip_Theme { get; set; }
        public string ToolTip_IgnoreHiddenFolders { get; set; }
        public string ToolTip_EnableProfileSpecificModConfigs { get; set; }
        public string ToolTip_Save { get; set; }
        public string ToolTip_Cancel { get; set; }

        // Other UI controls
        public bool ShowMainMenu { get; set; }

        public SettingsWindowViewModel()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                ToolTip_SMAPI = "The file path of StardewModdingAPI";
                ToolTip_ModFolder = "The folder path of the mod folder";
                ToolTip_Theme = "The current theme of Stardrop";
                ToolTip_IgnoreHiddenFolders = "If checked, Stardrop will ignore any mods which have a parent folder that start with \".\"";
                ToolTip_EnableProfileSpecificModConfigs = "If checked, Stardrop will save and restore config.json files from mods when swapping profiles";
                ToolTip_Save = "Save Changes";
                ToolTip_Cancel = "Cancel";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // TEMPORARY FIX: Due to bug with Avalonia on Linux platforms, tooltips currently cause crashes when they disappear
                // To work around this, tooltips are purposely not displayed
            }

            ShowMainMenu = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }
    }
}
