using Extensions.Configuration.Object;
using System;

// Note that this namespace is Microsoft.Extensions.Configuration for usability reasons.
namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// Extension methods for adding <see cref="ObjectConfigurationProvider"/>.
    /// </summary>
    public static class ObjectConfigurationExtensions
    {
        /// <summary>
        /// Adds a <see cref="ObjectConfigurationProvider"/> to with a given configuration object.
        /// </summary>
        /// <param name="builder"><see cref="IConfigurationBuilder"/> to add provider to.</param>
        /// <param name="configurationObject">Configuration object with configuration values.</param>
        /// <returns>Same <see cref="IConfigurationBuilder"/> instance to continue configuration.</returns>
        public static IConfigurationBuilder AddObject(this IConfigurationBuilder builder, object configurationObject)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configurationObject is null)
            {
                throw new ArgumentNullException(nameof(configurationObject));
            }

            return builder.Add((ObjectConfigurationSource source) => source.ConfigurationObject = configurationObject);
        }
    }
}
