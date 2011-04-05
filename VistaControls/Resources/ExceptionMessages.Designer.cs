﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VistaControls.Resources {
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
    internal class ExceptionMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ExceptionMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("VistaControls.Resources.ExceptionMessages", typeof(ExceptionMessages).Assembly);
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
        ///   Looks up a localized string similar to Common Controls library version 6.0 not loaded. Must run on Vista and must provide a manifest..
        /// </summary>
        internal static string CommonControlEntryPointNotFound {
            get {
                return ResourceManager.GetString("CommonControlEntryPointNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Desktop composition is not enabled..
        /// </summary>
        internal static string DwmNotEnabled {
            get {
                return ResourceManager.GetString("DwmNotEnabled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Desktop composition is not supported by operating system..
        /// </summary>
        internal static string DwmOsNotSupported {
            get {
                return ResourceManager.GetString("DwmOsNotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to get thumbnail&apos;s original size..
        /// </summary>
        internal static string DwmThumbnailQueryFailure {
            get {
                return ResourceManager.GetString("DwmThumbnailQueryFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to update thumbnail properties..
        /// </summary>
        internal static string DwmThumbnailUpdateFailure {
            get {
                return ResourceManager.GetString("DwmThumbnailUpdateFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Source and target windows cannot be the same..
        /// </summary>
        internal static string DwmWindowMatch {
            get {
                return ResourceManager.GetString("DwmWindowMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Native call to {0} failed..
        /// </summary>
        internal static string NativeCallFailure {
            get {
                return ResourceManager.GetString("NativeCallFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to create TaskDialog..
        /// </summary>
        internal static string TaskDialogFailure {
            get {
                return ResourceManager.GetString("TaskDialogFailure", resourceCulture);
            }
        }
    }
}
