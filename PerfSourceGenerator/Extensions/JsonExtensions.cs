// ***********************************************************************
// <copyright file="JsonExtensions.cs" company="MY">
//     My company
// </copyright>
// ***********************************************************************

namespace PerfSourceGenerator.Extensions
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Text.Json.Serialization.Metadata;
    using PerfSourceGenerator.Converters;

    /// <summary>
    /// Extensions to handle conversions to Json objects.
    /// </summary>
    /// <threadsafety>
    /// Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed
    /// to be thread safe.
    /// </threadsafety>
    public static class JsonExtensions
    {
        /// <summary>
        /// The default Json options to use to serialize.
        /// </summary>
        public static readonly JsonSerializerOptions DefaultJsonSerializerOptions = Create();

        /// <summary>
        /// Parses the text representing a single JSON value into an instance of the type specified by a generic type parameter.
        /// </summary>
        /// <typeparam name="T">The target type of the JSON value.</typeparam>
        /// <param name="json">The JSON text to parse.</param>
        /// <returns>A <typeparamref name="T"/> representation of the JSON value.</returns>
        public static T? FromJson<T>(this string json)
            where T : class
        {
            return FromJson<T>(json, JsonExtensions.DefaultJsonSerializerOptions);
        }

        /// <summary>
        /// Parses the text representing a single JSON value into an instance of the type specified by a generic type parameter.
        /// </summary>
        /// <typeparam name="T">The target type of the JSON value.</typeparam>
        /// <param name="json">The JSON text to parse.</param>
        /// <param name="options">Options to control the behavior during parsing.</param>
        /// <returns>A <typeparamref name="T"/> representation of the JSON value.</returns>
        public static T? FromJson<T>(this string json, JsonSerializerOptions options)
            where T : class
        {
            return JsonSerializer.Deserialize<T>(json, options);
        }

        /// <summary>
        /// Parses the text representing a single JSON value into an instance of the type specified by a generic type parameter.
        /// </summary>
        /// <typeparam name="T">The target type of the JSON value.</typeparam>
        /// <param name="json">The JSON text to parse.</param>
        /// <param name="jsonTypeInfo">Metadata about the type to convert.</param>
        /// <returns>A <typeparamref name="T"/> representation of the JSON value.</returns>
        public static T? FromJson<T>(this string json, JsonTypeInfo<T> jsonTypeInfo)
            where T : class
        {
            return JsonSerializer.Deserialize<T>(json, jsonTypeInfo);
        }

        /// <summary>
        /// Parses the text representing a single JSON value into an instance of the type specified by a generic type parameter.
        /// </summary>
        /// <typeparam name="T">The target type of the JSON value.</typeparam>
        /// <param name="json">The JSON text to parse.</param>
        /// <param name="context"> A metadata provider for serializable types.</param>
        /// <returns>A <typeparamref name="T"/> representation of the JSON value.</returns>
        public static T? FromJson<T>(this string json, JsonSerializerContext context)
            where T : class
        {
            return (T?)JsonSerializer.Deserialize(json, typeof(T), context);
        }

        /// <summary>
        /// Converts the value of a type specified by a generic type parameter into a JSON string.
        /// </summary>
        /// <typeparam name="T">The underlying type of <paramref name="origin"/>.</typeparam>
        /// <param name="origin">The object to convert to JSon.</param>
        /// <returns>A JSON string representation of <paramref name="origin"/>.</returns>
        public static string ToJson<T>(this T origin)
            where T : class
        {
            return ToJson(origin, DefaultJsonSerializerOptions);
        }

        /// <summary>
        /// Converts the value of a type specified by a generic type parameter into a JSON string.
        /// </summary>
        /// <typeparam name="T">The underlying type of <paramref name="origin"/>.</typeparam>
        /// <param name="origin">The object to convert to JSon.</param>
        /// <param name="options">Options to control serialization behavior.</param>
        /// <returns>A JSON string representation of <paramref name="origin"/>.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="options"/> is <see langword="null"/>.</exception>
        public static string ToJson<T>(this T origin, JsonSerializerOptions options)
            where T : class
        {
            return JsonSerializer.Serialize(origin, options);
        }

        /// <summary>
        /// Converts the value of a type specified by a generic type parameter into a JSON string.
        /// </summary>
        /// <typeparam name="T">The underlying type of <paramref name="origin"/>.</typeparam>
        /// <param name="origin">The object to convert to JSon.</param>
        /// <param name="jsonTypeInfo">Metadata about the type to convert.</param>
        /// <returns>A JSON string representation of <paramref name="origin"/>.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="jsonTypeInfo"/> is <see langword="null"/>.</exception>
        public static string ToJson<T>(this T origin, JsonTypeInfo<T> jsonTypeInfo)
            where T : class
        {
            return JsonSerializer.Serialize(origin, jsonTypeInfo);
        }

        /// <summary>
        /// Converts the value of a type specified by a generic type parameter into a JSON string.
        /// </summary>
        /// <typeparam name="T">The underlying type of <paramref name="origin"/>.</typeparam>
        /// <param name="origin">The object to convert to JSon.</param>
        /// <param name="context"> A metadata provider for serializable types.</param>
        /// <returns>A JSON string representation of <paramref name="origin"/>.</returns>
        /// <exception cref="ArgumentNullException">When <paramref name="context"/> is <see langword="null"/>.</exception>
        public static string ToJson<T>(this T? origin, JsonSerializerContext context)
            where T : class
        {
            return JsonSerializer.Serialize(origin, typeof(T), context);
        }

        private static JsonSerializerOptions Create()
        {
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = false,
            };

            options.Converters.Add(new JsonEnumStringConverter());
            return options;
        }
    }
}