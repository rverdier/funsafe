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

            BenchBinarySerialization(warmupBatchCount, warmupBatchSize, 1);
            BenchBinaryDeserialization(warmupBatchCount, warmupBatchSize, 1);

            BenchUnsafeSerialization(warmupBatchCount, warmupBatchSize, 1);
            BenchUnsafeDeserialization(warmupBatchCount, warmupBatchSize, 1);

            const int batchCount = 100*1000;
            const int batchSize = 100;

            for (var i = 0; i <= 10; i += 2)
            {
                var messagePartCount = i;
                var operationCount = batchCount*batchSize;

                Console.WriteLine("# {0} message parts", messagePartCount);
                Console.WriteLine();
                Measure("Binary serialization", () => BenchBinarySerialization(batchCount, batchSize, messagePartCount), operationCount);
                Measure("Binary deserialization", () => BenchBinaryDeserialization(batchCount, batchSize, messagePartCount), operationCount);
                Console.WriteLine();
                Measure("Unsafe serialization", () => BenchUnsafeSerialization(batchCount, batchSize, messagePartCount), operationCount);
                Measure("Unsafe deserialization", () => BenchUnsafeDeserialization(batchCount, batchSize, messagePartCount), operationCount);
                Console.WriteLine();
            }
        }

        private static void Measure(string actionName, Action action, int operationCount)
        {
            GC.Collect();
            var gcCount = GC.CollectionCount(0);
            var stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            gcCount = GC.CollectionCount(0) - gcCount;
            Console.WriteLine("{0}\t{1,10:N0}ms\t{2,12:N0} ops/s\t{3}GC", actionName, stopwatch.ElapsedMilliseconds, operationCount/stopwatch.Elapsed.TotalSeconds, gcCount);
        }

        private static void BenchUnsafeSerialization(int batchCount, int batchSize, int messagePartCount)
        {
            using (var wrapper = new UnsafeBufferWrapper(new byte[1024*100]))
            {
                var message = CreateMessage(messagePartCount);

                for (var i = 0; i < batchCount; i++)
                {
                    wrapper.ResetCursor();

                    for (var j = 0; j < batchSize; j++)
                    {
                        UnsafeMessageSerializer.Serialize(message, wrapper);
                    }
                }
            }
        }

        private static void BenchUnsafeDeserialization(int batchCount, int batchSize, int messagePartCount)
        {
            using (var wrapper = new UnsafeBufferWrapper(new byte[1024*100]))
            {
                var message = CreateMessage(messagePartCount);

                for (var j = 0; j < batchSize; j++)
                {
                    UnsafeMessageSerializer.Serialize(message, wrapper);
                }

                for (var i = 0; i < batchCount; i++)
                {
                    wrapper.ResetCursor();

                    for (var j = 0; j < batchSize; j++)
                    {
                        UnsafeMessageSerializer.Deserialize(message, wrapper);
                    }
                }
            }
        }

        private static void BenchBinarySerialization(int batchCount, int batchSize, int messagePartCount)
        {
            using (var memoryStream = new MemoryStream(new byte[1024*100]))
            using (var binaryWriter = new BinaryWriter(memoryStream))
            {
                var message = CreateMessage(messagePartCount);

                for (var i = 0; i < batchCount; i++)
                {
                    memoryStream.Position = 0;

                    for (var j = 0; j < batchSize; j++)
                    {
                        BinaryMessageSerializer.Serialize(message, binaryWriter);
                    }
                }
            }
        }

        private static void BenchBinaryDeserialization(int batchCount, int batchSize, int messagePartCount)
        {
            using (var memoryStream = new MemoryStream(new byte[1024*100]))
            using (var binaryWriter = new BinaryWriter(memoryStream))
            using (var binaryReader = new BinaryReader(memoryStream))
            {
                var message = CreateMessage(messagePartCount);

                for (var j = 0; j < batchSize; j++)
                {
                    BinaryMessageSerializer.Serialize(message, binaryWriter);
                }

                for (var i = 0; i < batchCount; i++)
                {
                    memoryStream.Position = 0;

                    for (var j = 0; j < batchSize; j++)
                    {
                        BinaryMessageSerializer.Deserialize(message, binaryReader);
                    }
                }
            }
        }

        private static Message CreateMessage(int messagePartCount)
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
                    Field3 = i%2 == 0,
                    Field4 = i*2.0,
                    Field5 = i/2.0m
                };
            }

            return message;
        }
    }
}