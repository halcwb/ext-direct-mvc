using System.Configuration;

namespace Ext.Direct.Mvc {

    public class ProviderConfiguration : ConfigurationSection {

        private static ProviderConfiguration _configuration;

        public static ProviderConfiguration GetConfiguration() {
            if (_configuration == null) {
                _configuration = ConfigurationManager.GetSection("ext.direct") as ProviderConfiguration;
            }
            return _configuration ?? (_configuration = new ProviderConfiguration());
        }

        [ConfigurationProperty("name", IsRequired = false, DefaultValue = "Ext.app.REMOTING_API")]
        public string Name {
            get { return (string)this["name"]; }
        }

        // Optional namespace for generated proxy methods.
        [ConfigurationProperty("namespace", IsRequired = false)]
        public string Namespace {
            get { return (string)this["namespace"]; }
        }

        // One or more names of assemblies to generate proxy for, separated by a comma.
        [ConfigurationProperty("assembly", IsRequired = false)]
        public string Assembly {
            get { return (string)this["assembly"]; }
        }

        // Number that specifies the amount of time in milliseconds to wait before sending a batched request.
        // If not specified then the default value, configured by Ext JS will be used, which is 10
        [ConfigurationProperty("buffer", IsRequired = false, DefaultValue = null)]
        public int? Buffer {
            get { return (int?)this["buffer"]; }
        }

        // Number of times to re-attempt delivery on failure of a call.
        // If not specified then the default value, configured by Ext JS will be used, which is 1
        [ConfigurationProperty("maxRetries", IsRequired = false, DefaultValue = null)]
        public int? MaxRetries {
            get { return (int?)this["maxRetries"]; }
        }

        // The timeout to use for each request.
        // If not specified then the default value, configured by Ext JS will be used, which is I don't remember
        [ConfigurationProperty("timeout", IsRequired = false, DefaultValue = null)]
        public int? Timeout {
            get { return (int?)this["timeout"]; }
        }

        // The format in which DateTime objects should be returned. Valid values are "Iso", "JS" or "JavaScript". All case insensitive.
        [ConfigurationProperty("dateFormat", IsRequired = false, DefaultValue = "Iso")]
        public string DateFormat {
            get { return (string)this["dateFormat"]; }
        }

        // Turns debug mode on if set to true. For development only! Set it to false on production environment.
        [ConfigurationProperty("debug", IsRequired = false, DefaultValue = false)]
        public bool Debug {
            get { return (bool)this["debug"]; }
        }
    }
}
