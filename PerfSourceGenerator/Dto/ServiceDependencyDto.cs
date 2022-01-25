// ***********************************************************************
// <copyright file="ServiceDependencyDto.cs" company="MY">
//     My company
// </copyright>
// ***********************************************************************

namespace PerfSourceGenerator.Dto
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Health status of the service and its dependencies.
    /// </summary>
    /// <threadsafety>
    /// Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to
    /// be thread safe.
    /// </threadsafety>
    public class ServiceDependencyDto
    {
        /// <summary>
        /// Gets or sets the actual response time of the dependency.
        /// </summary>
        /// <value>The actual response time defined for the dependency, it should be a value expressed in milliseconds.</value>
        [JsonPropertyName(PropertyNames.ActualResponseTime)]
        [Display(Name = PropertyNames.ActualResponseTime)]
        public long ActualResponseTime { get; set; }

        /// <summary>
        /// Gets or sets the expected response time of the dependency.
        /// </summary>
        /// <value>The expected response time defined for the dependency, it should be a value expressed in milliseconds.</value>
        [JsonPropertyName(PropertyNames.ExpectedResponseTime)]
        [Display(Name = PropertyNames.ExpectedResponseTime)]
        public long ExpectedResponseTime { get; set; }

        /// <summary>
        /// Gets or sets the hosts where the dependency lives.
        /// </summary>
        /// <value>The host where the dependency lives, the host can be a WCF service, SQL Server or any other computer.</value>
        [JsonPropertyName(PropertyNames.Host)]
        [Display(Name = PropertyNames.Host)]
        public string Host { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the URL to connect to the dependency.
        /// </summary>
        /// <value>The path to connect to the dependency from the server.</value>
        [JsonPropertyName(PropertyNames.Path)]
        [Display(Name = PropertyNames.Path)]
        public string? Path { get; set; }

        /// <summary>
        /// Gets or sets the port to connect to the dependency, usually this is a <see cref="uint"/>.
        /// </summary>
        /// <value>The port to connect to the dependency from the service.</value>
        [JsonPropertyName(PropertyNames.Port)]
        [Display(Name = PropertyNames.Port)]
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets a string that represents the scheme to connect to the dependency.
        /// </summary>
        /// <value>The scheme to connect to the dependency, valid values can be http, https, named-pipes, net.tcp, solace.tcp.</value>
        [JsonPropertyName(PropertyNames.Scheme)]
        [Display(Name = PropertyNames.Scheme)]
        public string? Scheme { get; set; }

        /// <summary>
        /// Gets or sets a value of <see cref="HealthStatus"/> that represents the status of the dependency.
        /// </summary>
        /// <value>One of the <see cref="HealthStatus"/> values. The default is <see cref="HealthStatus.Ok"/>.</value>
        [JsonPropertyName(PropertyNames.Status)]
        [Display(Name = PropertyNames.Status)]
        public HealthStatus Status { get; set; }
    }
}