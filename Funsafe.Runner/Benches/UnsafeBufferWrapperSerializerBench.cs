using Funsafe.Buffers;
using Funsafe.Runner.Serializers;

namespace Funsafe.Runner.Benches
{
    internal class UnsafeBufferWrapperSerializerBench : Bench
    {
        protected override void DoRun(int batchCount, int batchSize, int messagePartCount)
        {
            using (var wrapper = new UnsafeBufferWrapper(new byte[1024 * 100]))
            {
                var message = CreateMessage(messagePartCount);

                for (var i = 0; i < batchCount; i++)
                {
                    wrapper.ResetCursor();

                    for (var j = 0; j < batchSize; j++)
                    {
                        UnsafeBufferWrapperSerializer.Serialize(message, wrapper);
                    }
                }
            }
        }
    }
}
