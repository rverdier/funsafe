using Funsafe.Buffers;
using Funsafe.Runner.Model;

namespace Funsafe.Runner.Serializers
{
    public class UnsafeBufferWrapperSerializer2
    {
        public static void Serialize(Message instance, UnsafeBufferWrapper wrapper)
        {
            wrapper.Write2(ref instance.Header);
            wrapper.Write(instance.Id);
            wrapper.Write(instance.PartCount);

            for (var i = 0; i < instance.PartCount; i++)
            {
                wrapper.Write2(ref instance.Parts[i]);
            }
        }
    }
}