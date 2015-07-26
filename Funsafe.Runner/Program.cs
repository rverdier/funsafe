using System;
using System.Collections.Generic;
using System.Linq;
using Funsafe.Runner.Benches;

namespace Funsafe.Runner
{
    internal class Program
    {
        private static void Main()
        {
            var benches = LoadBenches();

            const int warmupBatchCount = 100;
            const int warmupBatchSize = 10;
            const int warmupMessagePartCount = 2;

            foreach (var bench in benches)
            {
                bench.Run(warmupBatchCount, warmupBatchSize, warmupMessagePartCount);
            }

            Console.WriteLine("Serialization");
            Console.WriteLine("-------------");
            Console.WriteLine();

            RunBenches(benches, BenchCategory.Serialization);

            Console.WriteLine();
            Console.WriteLine("Deserialization");
            Console.WriteLine("---------------");
            Console.WriteLine();

            RunBenches(benches, BenchCategory.Deserialization);
        }

        private static void RunBenches(IList<Bench> benches, BenchCategory benchCategory)
        {
            const int batchCount = 100 * 1000;
            const int batchSize = 100;
            for (var i = 0; i <= 10; i += 2)
            {
                var messagePartCount = i;

                Console.WriteLine();
                Console.WriteLine("# {0} message parts", messagePartCount);
                Console.WriteLine();

                var results = new List<BenchResult>();

                foreach (var bench in benches.Where(x => x.Category == benchCategory))
                {
                    results.Add(bench.Run(batchCount, batchSize, messagePartCount));
                }

                foreach (var result in results.OrderByDescending(x => x.OperationPerSecond))
                {
                    Console.WriteLine(result);
                }
            }
        }

        private static IList<Bench> LoadBenches()
        {
            return (from type in typeof(Program).Assembly.GetTypes()
                    where typeof(Bench).IsAssignableFrom(type) && !type.IsAbstract
                    orderby type.Name
                    let instance = (Bench)Activator.CreateInstance(type)
                    where instance.Category != BenchCategory.Ignored
                    select instance).ToList();
        }
    }
}
