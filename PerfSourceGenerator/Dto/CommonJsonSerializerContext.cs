// ***********************************************************************
// <copyright file="CommonJsonSerializerContext.cs" company="MY">
//     My company
// </copyright>
// ***********************************************************************

namespace PerfSourceGenerator.Dto
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Serialization Context.
    /// </summary>
    /// <threadsafety>
    /// Any public static (Shared in Visual Basic) members of this type are thread safe.
    /// Any instance members are not guaranteed to be thread safe.
    /// </threadsafety>
    [JsonSerializable(typeof(HealthDetailDto))]
    [JsonSerializable(typeof(ServiceDependencyDto))]
    [JsonSourceGenerationOptions(WriteIndented = false, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
    internal partial class CommonJsonSerializerContext : JsonSerializerContext
    {
    }
}