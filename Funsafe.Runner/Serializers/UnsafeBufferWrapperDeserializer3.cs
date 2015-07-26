using Funsafe.Buffers;
using Funsafe.Runner.Model;

namespace Funsafe.Runner.Serializers
{
    public class UnsafeBufferWrapperDeserializer3
    {
        public static void Deserialize(Message instance, UnsafeBufferWrapper wrapper)
        {
            instance.Header = wrapper.Read3<Header>();
            instance.Id = wrapper.ReadInt32();
            instance.PartCount = wrapper.ReadInt32();

            for (var i = 0; i < instance.PartCount; i++)
            {
                instance.Parts[i] = wrapper.Read3<MessagePart>();
            }
        }
    }
}