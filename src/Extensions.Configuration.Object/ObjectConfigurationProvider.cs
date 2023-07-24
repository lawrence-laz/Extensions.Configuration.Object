using Extensions.Configuration.Object.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;

namespace Extensions.Configuration.Object
{
    /// <summary>
    /// Loads configuration key/values from an object into a provider.
    /// </summary>
    public class ObjectConfigurationProvider : ConfigurationProvider
    {
        /// <summary>
        /// Object used as a source for configuration.
        /// </summary>
        public object ConfigurationObject { get; set; }

        /// <summary>
        /// Gets or sets the configuration root key.
        /// </summary>
        /// <value>
        /// The configuration root key.
        /// </value>
        public string ConfigurationRootKey { get; set; }

        /// <summary>
        /// Creates an instance of <see cref="ObjectConfigurationProvider" /> using the provided configuration object.
        /// </summary>
        /// <param name="configurationObject">Object used as a source for configuration.</param>
        /// <param name="configurationRootKey">The configuration root key.</param>
        /// <exception cref="System.ArgumentNullException">configurationObject</exception>
        public ObjectConfigurationProvider(object configurationObject, string configurationRootKey) : this(configurationObject) => ConfigurationRootKey = configurationRootKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectConfigurationProvider"/> class.
        /// </summary>
        /// <param name="configurationObject">The configuration object.</param>
        public ObjectConfigurationProvider(object configurationObject) => ConfigurationObject = configurationObject ?? throw new ArgumentNullException(nameof(configurationObject));

        /// <summary>
        /// Recursively loads values from <see cref="ConfigurationObject"/> to this provider.
        /// </summary>
        public override void Load()
        {
            LoadRecursively(ConfigurationRootKey, ConfigurationObject);
        }

        private void LoadRecursively(string currentKey, object section)
        {
            if (section is null)
            {
                return;
            }

            bool simpleValue = section is string || section.GetType().IsPrimitive 
                || section is Uri || section is Guid || section is TimeSpan;
            if (simpleValue)
            {
                base.Set(currentKey, section.ToString());
            }
            else if (section is Enum) // Enum.
            {
                base.Set(currentKey, Enum.GetName(section.GetType(), section));
            }
            else if (section is IDictionary dictionarySection) // Dictionary.
            {
                foreach (DictionaryEntry item in dictionarySection)
                {
                    var name = item.Key.ToString();
                    var value = item.Value;
                    var newKey = string.IsNullOrWhiteSpace(currentKey)
                        ? name
                        : $"{currentKey}:{name}";

                    LoadRecursively(newKey, value);
                }
            }
            else if (section is IEnumerable enumerableSection) // Enumerable.
            {
                var index = 0;
                foreach (var item in enumerableSection)
                {
                    var name = index.ToString();
                    var value = item;
                    var newKey = string.IsNullOrWhiteSpace(currentKey)
                        ? name
                        : $"{currentKey}:{name}";

                    LoadRecursively(newKey, value);

                    ++index;
                }
            }
            else // Complex object.
            {
                foreach (var field in section.GetType().GetConfigurationFields())
                {
                    var name = field.GetName();
                    var value = field.GetValue(section);

                    var newKey = string.IsNullOrWhiteSpace(currentKey)
                        ? name
                        : $"{currentKey}:{name}";

                    LoadRecursively(newKey, value);
                }

                foreach (var property in section.GetType().GetConfigurationProperties())
                {
                    var name = property.Name;
                    var value = property.GetValue(section);

                    var newKey = string.IsNullOrWhiteSpace(currentKey)
                        ? name
                        : $"{currentKey}:{name}";

                    LoadRecursively(newKey, value);
                }
            }
        }
    }
}
