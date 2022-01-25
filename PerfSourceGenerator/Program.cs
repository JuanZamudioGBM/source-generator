// ***********************************************************************
// <copyright file="Program.cs" company="MY">
//     My company
// </copyright>
// ***********************************************************************

namespace PerfSourceGenerator
{
    using BenchmarkDotNet.Running;

    /// <summary>
    /// The entry point for the project.
    /// </summary>
    public static class Program
    {
        /// <summary>Entry point of the program.</summary>
        /// <returns>The status of the program.</returns>
        public static int Main()
        {
            //// _ = BenchmarkRunner.Run<FromJsonBenchmark>();
            //// _ = BenchmarkRunner.Run<ToJsonBenchmark>();
            _ = BenchmarkRunner.Run(typeof(FromJsonBenchmark).Assembly);

            return 0;
        }
    }
}
