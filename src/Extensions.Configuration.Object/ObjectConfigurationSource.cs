using Microsoft.Extensions.Configuration;
using System;

namespace Extensions.Configuration.Object
{
    /// <summary>
    /// Represents an anonymous, class or struct object as an <see cref="IConfigurationSource"/>.
    /// </summary>
    public class ObjectConfigurationSource : IConfigurationSource
    {
        /// <summary>
        /// Object used as a source for configuration.
        /// </summary>
        public object ConfigurationObject { get; set; }

        /// <summary>
        /// Builds the <see cref="ObjectConfigurationProvider"/> for this source.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
        /// <returns>An <see cref="ObjectConfigurationProvider"/></returns>
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            if (ConfigurationObject is null)
            {
                throw new ArgumentNullException($"Property {ConfigurationObject} needs to be set before calling method {nameof(Build)}.");
            }

            return new ObjectConfigurationProvider(ConfigurationObject);
        }
    }
}
