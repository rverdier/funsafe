using Funsafe.Buffers;
using Funsafe.Runner.Model;

namespace Funsafe.Runner.Serializers
{
    public unsafe class UnsafeBufferWrapperDeserializer5
    {
        public static void Deserialize(Message instance, UnsafeBufferWrapper wrapper)
        {
            instance.Header = *(Header*)wrapper.Cursor;
            wrapper.Cursor += sizeof(Header);
            instance.Id = wrapper.ReadInt32();
            instance.PartCount = wrapper.ReadInt32();

            for (var i = 0; i < instance.PartCount; i++)
            {
                instance.Parts[i] = *(MessagePart*)wrapper.Cursor;
                wrapper.Cursor += sizeof(MessagePart);
            }
        }
    }
}