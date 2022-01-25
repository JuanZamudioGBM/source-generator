// ***********************************************************************
// <copyright file="HealthStatus.cs" company="MY">
//     My company
// </copyright>
// ***********************************************************************

namespace PerfSourceGenerator.Dto
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Possible values of a status of a service or a dependency.
    /// </summary>
    /// <threadsafety>
    /// Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to
    /// be thread safe.
    /// </threadsafety>
    public enum HealthStatus
    {
        /// <summary>Use when the service/dependency is working correctly.</summary>
        [EnumMember(Value = PropertyNames.Ok)]
        Ok,

        /// <summary>Use when the service/dependency is running but the health its not perfect.</summary>
        [EnumMember(Value = PropertyNames.Warning)]
        Warning,

        /// <summary>Use when the service/dependency is failing.</summary>
        [EnumMember(Value = PropertyNames.Critical)]
        Critical,

        /// <summary>Use when one or more of the dependencies of the services are failing.</summary>
        [EnumMember(Value = PropertyNames.DependencyError)]
        DependencyError,

        /// <summary>Use when the service/dependency is running but its slower than expected.</summary>
        [EnumMember(Value = PropertyNames.Belated)]
        Belated,
    }
}