﻿using Extensions.Configuration.Object.Internal;
using Microsoft.Extensions.Configuration;
using System;

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
        /// Creates an instance of <see cref="ObjectConfigurationProvider"/> using the provided configuration object.
        /// </summary>
        /// <param name="configurationObject">Object used as a source for configuration.</param>
        public ObjectConfigurationProvider(object configurationObject)
        {
            ConfigurationObject = configurationObject ?? throw new ArgumentNullException(nameof(configurationObject));
        }

        /// <summary>
        /// Recursively loads values from <see cref="ConfigurationObject"/> to this provider.
        /// </summary>
        public override void Load()
        {
            LoadRecursively(null, ConfigurationObject);
        }

        private void LoadRecursively(string currentKey, object section)
        {
            if (section is null)
            {
                return;
            }

            if (section is string || section.GetType().IsPrimitive)
            {
                base.Set(currentKey, section.ToString());
            }
            else if (section.GetType().TryGetFields(out var fields))
            {
                foreach (var field in fields)
                {
                    var name = field.GetName();
                    var value = field.GetValue(section);

                    var newKey = string.IsNullOrWhiteSpace(currentKey)
                        ? name
                        : $"{currentKey}:{name}";

                    LoadRecursively(newKey, value);
                }
            }
        }
    }
}