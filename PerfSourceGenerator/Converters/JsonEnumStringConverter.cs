// ***********************************************************************
// <copyright file="JsonEnumStringConverter.cs" company="MY">
//     My company
// </copyright>
// ***********************************************************************

namespace PerfSourceGenerator.Converters
{
    using System;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A factory to create converters of <see cref="Enum"/> to <see cref="string"/> using <see cref="EnumMemberAttribute"/> if exist.
    /// </summary>
    /// <threadsafety>
    /// Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed
    /// to be thread safe.
    /// </threadsafety>
    public class JsonEnumStringConverter : JsonConverterFactory
    {
        /// <summary>
        /// When overridden in a derived class, determines whether the converter instance can convert the specified object type.
        /// </summary>
        /// <param name="typeToConvert">The type of the object to check whether it can be converted by this converter instance.</param>
        /// <returns><see langword="true"/> if the instance can convert the specified object type; otherwise, <see langword="false"/>.</returns>
        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert != null)
            {
                return typeToConvert.IsEnum;
            }

            return false;
        }

        /// <summary>
        /// Creates a converter for a specified type.
        /// </summary>
        /// <param name="typeToConvert">The type handled by the converter.</param>
        /// <param name="options">The serialization options to use.</param>
        /// <returns>A converter which is compatible with <paramref name="typeToConvert"/>.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="typeToConvert"/> is <see langword="null"/>.</exception>
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            JsonConverter converter = (JsonConverter)Activator.CreateInstance(
                typeof(JsonEnumConverter<>).MakeGenericType(typeToConvert),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                null,
                culture: null)!;

            return converter;
        }
    }
}