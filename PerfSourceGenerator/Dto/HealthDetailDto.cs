// ***********************************************************************
// <copyright file="HealthDetailDto.cs" company="MY">
//     My company
// </copyright>
// ***********************************************************************

namespace PerfSourceGenerator.Dto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Health status of a service.
    /// </summary>
    /// <threadsafety>
    /// Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to
    /// be thread safe.
    /// </threadsafety>
    public class HealthDetailDto
    {
        /// <summary>
        /// Gets or sets an enumeration of the dependencies of the service.
        /// </summary>
        /// <value>The internal dependencies of the service if exist.</value>
        [JsonPropertyName(PropertyNames.Dependencies)]
        [Display(Name = PropertyNames.Dependencies)]
        public IEnumerable<ServiceDependencyDto>? Dependencies { get; set; }

        /// <summary>
        /// Gets or sets the expected response time of the service.
        /// </summary>
        /// <value>The expected response time defined for the service, it should be a value expressed in milliseconds.</value>
        [JsonPropertyName(PropertyNames.ExpectedResponseTime)]
        [Display(Name = PropertyNames.ExpectedResponseTime)]
        public int ExpectedResponseTime { get; set; }

        /// <summary>
        /// Gets or sets a value of <see cref="HealthStatus"/> that represents the status of the service.
        /// </summary>
        /// <value>One of the <see cref="HealthStatus"/> values. The default is <see cref="HealthStatus.Ok"/>.</value>
        [JsonPropertyName(PropertyNames.HealthStatus)]
        [Display(Name = PropertyNames.HealthStatus)]
        public HealthStatus HealthStatus { get; set; }

        /// <summary>
        /// Gets or sets the service identifier.
        /// </summary>
        /// <value>The identifier of the service as exists in the application service.</value>
        [JsonPropertyName(PropertyNames.ServiceId)]
        [Display(Name = PropertyNames.ServiceId)]
        public string ServiceId { get; set; } = string.Empty;
    }
}