﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VirusTotalScanner.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("VirusTotalScanner.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Virus Total API Key missing.
        /// </summary>
        internal static string FormMain_FormMain_Virus_Total_API_Key_missing {
            get {
                return ResourceManager.GetString("FormMain_FormMain_Virus_Total_API_Key_missing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No Virus Total API Key found. Please go to the settings and add your API key.
        /// </summary>
        internal static string FormMain_FormMain_Virus_Total_API_Key_Not_found {
            get {
                return ResourceManager.GetString("FormMain_FormMain_Virus_Total_API_Key_Not_found", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You need to restart the program before the changed settings take effect..
        /// </summary>
        internal static string FormMain_settingsToolStripMenuItem_Click_Settings_Changed_Restart_Message {
            get {
                return ResourceManager.GetString("FormMain_settingsToolStripMenuItem_Click_Settings_Changed_Restart_Message", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Connection Problem | Check Internet Connection.
        /// </summary>
        internal static string FormMain_VirusTotalQueue_StateChanged_Connection_Problem {
            get {
                return ResourceManager.GetString("FormMain_VirusTotalQueue_StateChanged_Connection_Problem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Idle.
        /// </summary>
        internal static string FormMain_VirusTotalQueue_StateChanged_Idle {
            get {
                return ResourceManager.GetString("FormMain_VirusTotalQueue_StateChanged_Idle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Scanning.
        /// </summary>
        internal static string FormMain_VirusTotalQueue_StateChanged_Scanning {
            get {
                return ResourceManager.GetString("FormMain_VirusTotalQueue_StateChanged_Scanning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap VirusTotal_Icon {
            get {
                object obj = ResourceManager.GetObject("VirusTotal_Icon", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
    }
}
