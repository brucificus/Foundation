using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace FGS.Extensions.Configuration.AWS.ElasticBeanstalk.IisEnv
{
    /// <summary>
    /// An implementation of <see cref="FileConfigurationProvider"/> that extracts "IIS Environment" configuration data and uses that as the actual configuration data being provided by this provider.
    /// </summary>
    public class AwsElasticBeanstalkIisEnvConfigurationProvider : JsonConfigurationProvider
    {
        /// <summary>
        /// Initializes a new instance with the specified source.
        /// </summary>
        /// <param name="source">The source settings.</param>
        public AwsElasticBeanstalkIisEnvConfigurationProvider(AwsElasticBeanstalkContainerConfigurationConfigurationSource source) : base(source) { }

        /// <summary>
        /// Loads the JSON data from a stream, but then extracts from it the "IIS Environment" configuration as the actual configuration data being provided by this provider.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        public override void Load(Stream stream)
        {
            base.Load(stream);

            Data = ExtractIisEnvConfiguration(Data);
        }

        /// <remarks>Roughly based on code seen at https://stackoverflow.com/questions/40127703/aws-elastic-beanstalk-environment-variables-in-asp-net-core-1-0 </remarks>
        private static IDictionary<string, string> ExtractIisEnvConfiguration(IDictionary<string, string> awsElasticBeanstalkContainerConfiguration)
        {
            var results = new Dictionary<string, string>();

            var applicableContainerConfigKeys = awsElasticBeanstalkContainerConfiguration.Keys.Where(k => k.StartsWith("iis:env:")).ToArray();
            foreach(var containerConfigKey in applicableContainerConfigKeys)
            {
                var actualKeyValuePair = awsElasticBeanstalkContainerConfiguration[containerConfigKey];

                var indexOfSeparator = actualKeyValuePair.IndexOf('=');
                var key = actualKeyValuePair.Substring(0, indexOfSeparator);
                var value = actualKeyValuePair.Substring(indexOfSeparator + 1);

                results.Add(key, value);
            }

            return results;
        }
    }
}