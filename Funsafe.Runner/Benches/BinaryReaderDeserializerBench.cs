using System.IO;
using Funsafe.Runner.Serializers;

namespace Funsafe.Runner.Benches
{
    internal class BinaryReaderDeserializerBench : Bench
    {
        protected override void DoRun(int batchCount, int batchSize, int messagePartCount)
        {
            using (var memoryStream = new MemoryStream(new byte[1024 * 100]))
            using (var binaryWriter = new BinaryWriter(memoryStream))
            using (var binaryReader = new BinaryReader(memoryStream))
            {
                var message = CreateMessage(messagePartCount);

                for (var j = 0; j < batchSize; j++)
                {
                    BinaryWriterSerializer.Serialize(message, binaryWriter);
                }

                for (var i = 0; i < batchCount; i++)
                {
                    memoryStream.Position = 0;

                    for (var j = 0; j < batchSize; j++)
                    {
                        BinaryReaderDeserializer.Deserialize(message, binaryReader);
                    }
                }
            }
        }
    }
}