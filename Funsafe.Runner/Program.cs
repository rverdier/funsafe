using System;
using System.Diagnostics;
using System.IO;
using Funsafe.Buffers;
using Funsafe.Runner.Model;

namespace Funsafe.Runner
{
    internal class Program
    {
        private static void Main()
        {
            const int warmupBatchCount = 100;
            const int warmupBatchSize = 10;
            
            BenchBinarySerialization(warmupBatchCount, warmupBatchSize);
            BenchUnsafeSerialization(warmupBatchCount, warmupBatchSize);

            const int batchCount = 100*1000;
            const int batchSize = 100;

            Measure("Binary serialization", () => BenchBinarySerialization(batchCount, batchSize), batchCount*batchSize);
            Measure("Unsafe binary serialization", () => BenchUnsafeSerialization(batchCount, batchSize), batchCount*batchSize);
        }

        private static void Measure(string actionName, Action action, int operationCount)
        {
            Console.WriteLine("{0} - Starting measure...", actionName);
            GC.Collect();
            var gcCount = GC.CollectionCount(0);
            var stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            gcCount = GC.CollectionCount(0) - gcCount;
            Console.WriteLine("Completed in {0:N0}ms ({1:N0} ops/s) - GC count: {2}", stopwatch.ElapsedMilliseconds, operationCount/stopwatch.Elapsed.TotalSeconds, gcCount);
        }

        private static void BenchUnsafeSerialization(int batchCount, int batchSize)
        {
            using (var wrapper = new UnsafeBufferWrapper(new byte[1024*100]))
            {
                var message = CreateMessage();

                for (var i = 0; i < batchCount; i++)
                {
                    wrapper.ResetCursor();

                    for (var j = 0; j < batchSize; j++)
                    {
                        UnsafeMessageSerializer.Serialize(message, wrapper);
                    }

                    wrapper.ResetCursor();

                    for (var j = 0; j < batchSize; j++)
                    {
                        UnsafeMessageSerializer.Deserialize(message, wrapper);
                    }
                }
            }
        }

        private static void BenchBinarySerialization(int batchCount, int batchSize)
        {
            using (var memoryStream = new MemoryStream(new byte[1024*100]))
            using (var binaryWriter = new BinaryWriter(memoryStream))
            using (var binaryReader = new BinaryReader(memoryStream))
            {
                var message = CreateMessage();

                for (var i = 0; i < batchCount; i++)
                {
                    memoryStream.Position = 0;

                    for (var j = 0; j < batchSize; j++)
                    {
                        BinaryMessageSerializer.Serialize(message, binaryWriter);
                    }

                    memoryStream.Position = 0;

                    for (var j = 0; j < batchSize; j++)
                    {
                        BinaryMessageSerializer.Deserialize(message, binaryReader);
                    }
                }
            }
        }

        private static Message CreateMessage()
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
                PartCount = 12
            };

            for (var i = 0; i < message.PartCount; i++)
            {
                message.Parts[i] = new MessagePart
                {
                    Field1 = i,
                    Field2 = new DateTime(i),
                    Field3 = i%2 == 0,
                    Field4 = i*2.0,
                    Field5 = i/2.0m
                };
            }

            return message;
        }
    }
}