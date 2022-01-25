// ***********************************************************************
// <copyright file="ToJsonBenchmark.cs" company="MY">
//     My company
// </copyright>
// ***********************************************************************

namespace PerfSourceGenerator
{
    using BenchmarkDotNet.Attributes;
    using PerfSourceGenerator.Dto;
    using PerfSourceGenerator.Extensions;

    /// <summary>
    /// Benchmarks for<see cref="JsonExtensions.ToJson{T}(T)"/>.
    /// </summary>
    /// <threadsafety>
    /// Any public static (Shared in Visual Basic) members of this type are thread safe.
    /// Any instance members are not guaranteed to be thread safe.
    /// </threadsafety>
    [MemoryDiagnoser]
    public class ToJsonBenchmark
    {
        private HealthDetailDto healthDetail = new();
        private CommonJsonSerializerContext? commonJsonSerializerContext;

        /// <summary>Benchmarks the method <see cref="JsonExtensions.ToJson{T}(T)" />.</summary>
        /// <returns>A new instance of <see cref="HealthDetailDto"/>.</returns>
        [Benchmark(Baseline = true)]
        public string ToJson()
        {
            return this.healthDetail.ToJson();
        }

        /// <summary>Benchmarks the method <see cref="JsonExtensions.ToJson{T}(T)" />.</summary>
        /// <returns>A new instance of <see cref="HealthDetailDto"/>.</returns>
        [Benchmark]
        public string ToJsonContext()
        {
            return this.healthDetail.ToJson(this.commonJsonSerializerContext!);
        }

        /// <summary>Benchmarks the method <see cref="JsonExtensions.ToJson{T}(T)" />.</summary>
        /// <returns>A new instance of <see cref="HealthDetailDto"/>.</returns>
        [Benchmark]
        public string ToJsonContextInstance()
        {
            return this.healthDetail.ToJson(this.commonJsonSerializerContext!.HealthDetailDto);
        }

        /// <summary>Initialize the benchmark data.</summary>
        [GlobalSetup]
        public void GlobalSetup()
        {
            this.commonJsonSerializerContext = new CommonJsonSerializerContext(JsonExtensions.DefaultJsonSerializerOptions);
            this.healthDetail = "{\"dependencies\":[{\"actual_response_time\":0,\"expected_response_time\":0,\"host\":\"localhost:62662\",\"path\":\"GET|Uri\",\"port\":6500,\"scheme\":\"net.pipe\",\"status\":\"warning\"}],\"expected_response_time\":200,\"health_status\":\"ok\",\"service_id\":\"lorem-api\"}".FromJson<HealthDetailDto>()!;
        }
    }
}