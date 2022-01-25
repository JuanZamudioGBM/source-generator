// ***********************************************************************
// <copyright file="JsonEnumConverter.cs" company="MY">
//     My company
// </copyright>
// ***********************************************************************

namespace PerfSourceGenerator.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Custom converter to write a <see cref="Enum"/> as a string using <see cref="EnumMemberAttribute"/> if exist.
    /// </summary>
    /// <typeparam name="T">The type of object or value handled by the converter.</typeparam>
    /// <seealso cref="JsonConverter{T}"/>
    /// <threadsafety>
    /// Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed
    /// to be thread safe.
    /// </threadsafety>
    public class JsonEnumConverter<T> : JsonConverter<T>
          where T : struct, Enum
    {
        /// <summary>
        /// A cache to store properties that have already been parsed.
        /// </summary>
        private static readonly Dictionary<Tuple<string, T>, string> Cache = new();

        /// <summary>
        /// Determines whether this instance can convert the specified type.
        /// </summary>
        /// <param name="typeToConvert">The type.</param>
        /// <returns><see langword="true"/> if this instance can convert the specified type; otherwise, <see langword="false"/>.</returns>
        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert != null)
            {
                return typeToConvert.IsEnum;
            }

            return false;
        }

        /// <summary>
        /// Reads and converts the JSON to type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="typeToConvert">The type to convert.</param>
        /// <param name="options">An object that specifies serialization options to use.</param>
        /// <returns>The converted value.</returns>
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            JsonTokenType token = reader.TokenType;

            if (token == JsonTokenType.Number && reader.TryGetInt32(out int int32))
            {
                return Unsafe.As<int, T>(ref int32);
            }

            if (token == JsonTokenType.String)
            {
                string enumString = reader.GetString()!.Replace("_", string.Empty, StringComparison.InvariantCultureIgnoreCase);

                if (!Enum.TryParse(enumString, ignoreCase: true, out T value))
                {
                    throw new JsonException($"Value [{enumString}]  was not found in enumeration [{typeof(T).FullName!}].");
                }

                return value;
            }

            throw new JsonException($"Cant deserialize JsonTokenType [{token}] in enumeration [{typeof(T).FullName!}].");
        }

        /// <summary>
        /// Writes a specified value as JSON.
        /// </summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="value">The value to convert to JSON.</param>
        /// <param name="options">An object that specifies serialization options to use.</param>
        /// <exception cref="ArgumentNullException">When <paramref name="writer"/> is <see langword="null"/>.</exception>
        public override void Write([NotNull] Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            string currentValue = GetEnumMemberValue(value);
            writer.WriteStringValue(currentValue);
        }

        private static string GetEnumMemberValue(T value)
        {
            var enumType = typeof(T);
            var tuple = new Tuple<string, T>(enumType.FullName!, value);

            if (!Cache.TryGetValue(tuple, out string? resultCached))
            {
                lock (TypeLock.SyncLock)
                {
                    if (Cache.TryGetValue(tuple, out string? resultLocked))
                    {
                        return resultLocked;
                    }

                    string currentValue = value.ToString();

                    var memberInfos = enumType.GetMember(currentValue);
                    var enumValueMemberInfo = memberInfos[0];
                    var valueAttributes = enumValueMemberInfo.GetCustomAttributes(typeof(EnumMemberAttribute), false);
                    if (valueAttributes.Length > 0)
                    {
                        var attribute = (EnumMemberAttribute)valueAttributes[0];
                        currentValue = attribute.Value ?? string.Empty;
                    }

                    Cache.Add(tuple, currentValue);
                    return currentValue;
                }
            }

            return resultCached;
        }

        private static class TypeLock
        {
            public static readonly object SyncLock = new();
        }
    }
}