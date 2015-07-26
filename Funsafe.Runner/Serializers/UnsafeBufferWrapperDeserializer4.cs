using Funsafe.Buffers;
using Funsafe.Runner.Model;

namespace Funsafe.Runner.Serializers
{
    public class UnsafeBufferWrapperDeserializer4
    {
        public static void Deserialize(Message instance, UnsafeBufferWrapper wrapper)
        {
            instance.Header = wrapper.Read4<Header>();
            instance.Id = wrapper.ReadInt32();
            instance.PartCount = wrapper.ReadInt32();

            for (var i = 0; i < instance.PartCount; i++)
            {
                instance.Parts[i] = wrapper.Read4<MessagePart>();
            }
        }
    }
}