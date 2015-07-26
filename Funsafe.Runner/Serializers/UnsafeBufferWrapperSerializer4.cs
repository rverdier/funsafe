using Funsafe.Buffers;
using Funsafe.Runner.Model;

namespace Funsafe.Runner.Serializers
{
    public unsafe class UnsafeBufferWrapperSerializer4
    {
        public static void Serialize(Message instance, UnsafeBufferWrapper wrapper)
        {
            *(Header*)wrapper.Cursor = instance.Header;
            wrapper.Cursor += sizeof(Header);
            wrapper.Write(instance.Id);
            wrapper.Write(instance.PartCount);

            for (var i = 0; i < instance.PartCount; i++)
            {
                *(MessagePart*)wrapper.Cursor = instance.Parts[i];
                wrapper.Cursor += sizeof(MessagePart);
            }
        }
    }
}