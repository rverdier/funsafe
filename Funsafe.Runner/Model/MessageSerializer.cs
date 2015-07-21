using Funsafe.Buffers;

namespace Funsafe.Runner.Model
{
    public class UnsafeMessageSerializer
    {
        public static void Serialize(Message instance, UnsafeBufferWrapper wrapper)
        {
            wrapper.Write(ref instance.Header);
            wrapper.Write(instance.Id);
            wrapper.Write(instance.PartCount);

            for (var i = 0; i < instance.PartCount; i++)
            {
                wrapper.Write(ref instance.Parts[i]);
            }
        }

        public static void Deserialize(Message instance, UnsafeBufferWrapper wrapper)
        {
            instance.Header = wrapper.Read<Header>();
            instance.Id = wrapper.ReadInt32();
            instance.PartCount = wrapper.ReadInt32();

            for (var i = 0; i < instance.PartCount; i++)
            {
                instance.Parts[i] = wrapper.Read<MessagePart>();
            }
        }
    }
}