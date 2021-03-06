﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.261
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Ext.Direct.Mvc.Resources {
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [DebuggerNonUserCode()]
    [CompilerGenerated()]
    internal class DirectResources {
        
        private static ResourceManager resourceMan;
        
        private static CultureInfo resourceCulture;
        
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DirectResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager {
            get {
                if (ReferenceEquals(resourceMan, null)) {
                    ResourceManager temp = new ResourceManager("Ext.Direct.Mvc.Resources.DirectResources", typeof(DirectResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DirectRequest is null. Check that POST parameters were passed..
        /// </summary>
        internal static string Common_DirectRequestIsNull {
            get {
                return ResourceManager.GetString("Common_DirectRequestIsNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value cannot be null or empty..
        /// </summary>
        internal static string Common_NullOrEmpty {
            get {
                return ResourceManager.GetString("Common_NullOrEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Method {0} has already been configured for action {1}..
        /// </summary>
        internal static string DirectAction_MethodExists {
            get {
                return ResourceManager.GetString("DirectAction_MethodExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Action {0} has already been configured..
        /// </summary>
        internal static string DirectProvider_ActionExists {
            get {
                return ResourceManager.GetString("DirectProvider_ActionExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to find action {0}..
        /// </summary>
        internal static string DirectProvider_ActionNotFound {
            get {
                return ResourceManager.GetString("DirectProvider_ActionNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unable to find method {0} in action {1}..
        /// </summary>
        internal static string DirectProvider_MethodNotFound {
            get {
                return ResourceManager.GetString("DirectProvider_MethodNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Number of arguments does not match the definition of method {0} in action {1}..
        /// </summary>
        internal static string DirectProvider_WrongNumberOfArguments {
            get {
                return ResourceManager.GetString("DirectProvider_WrongNumberOfArguments", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet..
        /// </summary>
        internal static string JsonRequest_GetNotAllowed {
            get {
                return ResourceManager.GetString("JsonRequest_GetNotAllowed", resourceCulture);
            }
        }
    }
}
