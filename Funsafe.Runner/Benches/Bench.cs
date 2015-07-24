using System;
using System.Diagnostics;
using Funsafe.Runner.Model;

namespace Funsafe.Runner.Benches
{
    public abstract class Bench
    {
        public virtual string Name { get { return GetType().Name; } }

        public BenchResult Run(int batchCount, int batchSize, int messagePartCount)
        {
            GC.Collect();
            var gcCount = GC.CollectionCount(0);
            var stopwatch = Stopwatch.StartNew();
            DoRun(batchCount, batchSize, messagePartCount);
            stopwatch.Stop();
            gcCount = GC.CollectionCount(0) - gcCount;
            var operationCount = batchCount * batchSize;
            return new BenchResult(Name, stopwatch.Elapsed, gcCount, operationCount / stopwatch.Elapsed.TotalSeconds);
        }

        protected abstract void DoRun(int batchCount, int batchSize, int messagePartCount);

        protected static Message CreateMessage(int messagePartCount)
        {
            var message = new Message
            {
                Header =
                {
                    Version = 1,
                    Flags = 42,
                    Length = 248,
                    Timestamp = DateTime.UtcNow
                },
                Id = 123,
                PartCount = messagePartCount
            };

            for (var i = 0; i < message.PartCount; i++)
            {
                message.Parts[i] = new MessagePart
                {
                    Field1 = i,
                    Field2 = new DateTime(i),
                    Field3 = i % 2 == 0,
                    Field4 = i * 2.0,
                    Field5 = i / 2.0m
                };
            }

            return message;
        }
    }
}
