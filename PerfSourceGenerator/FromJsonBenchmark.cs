// ***********************************************************************
// <copyright file="FromJsonBenchmark.cs" company="MY">
//     My company
// </copyright>
// ***********************************************************************

namespace PerfSourceGenerator
{
    using BenchmarkDotNet.Attributes;
    using PerfSourceGenerator.Dto;
    using PerfSourceGenerator.Extensions;

    /// <summary>
    /// Benchmarks for<see cref="JsonExtensions.FromJson(string)"/>.
    /// </summary>
    /// <threadsafety>
    /// Any public static (Shared in Visual Basic) members of this type are thread safe.
    /// Any instance members are not guaranteed to be thread safe.
    /// </threadsafety>
    [MemoryDiagnoser]
    public class FromJsonBenchmark
    {
        private string json = string.Empty;
        private CommonJsonSerializerContext? commonJsonSerializerContext;

        /// <summary>Benchmarks the method <see cref="JsonExtensions.FromJson(string)" />.</summary>
        /// <returns>A new instance of <see cref="HealthDetailDto"/>.</returns>
        [Benchmark(Baseline = true)]
        public HealthDetailDto? FromJson()
        {
            return this.json.FromJson<HealthDetailDto>();
        }

        /// <summary>Benchmarks the method <see cref="JsonExtensions.FromJson(string)" />.</summary>
        /// <returns>A new instance of <see cref="HealthDetailDto"/>.</returns>
        [Benchmark]
        public HealthDetailDto? FromJsonContext()
        {
            return this.json.FromJson<HealthDetailDto>(this.commonJsonSerializerContext!);
        }

        /// <summary>Benchmarks the method <see cref="JsonExtensions.FromJson(string)" />.</summary>
        /// <returns>A new instance of <see cref="HealthDetailDto"/>.</returns>
        [Benchmark]
        public HealthDetailDto? FromJsonContextInstance()
        {
            return this.json.FromJson<HealthDetailDto>(this.commonJsonSerializerContext!.HealthDetailDto);
        }

        /// <summary>Initialize the benchmark data.</summary>
        [GlobalSetup]
        public void GlobalSetup()
        {
            this.commonJsonSerializerContext = new CommonJsonSerializerContext(JsonExtensions.DefaultJsonSerializerOptions);
            this.json = "{\"dependencies\":[{\"actual_response_time\":0,\"expected_response_time\":0,\"host\":\"localhost:62662\",\"path\":\"GET|Uri\",\"port\":6500,\"scheme\":\"net.pipe\",\"status\":\"warning\"}],\"expected_response_time\":200,\"health_status\":\"ok\",\"service_id\":\"lorem-api\"}";
        }
    }
}