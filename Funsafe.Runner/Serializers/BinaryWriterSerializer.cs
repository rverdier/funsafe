using System.IO;
using Funsafe.Runner.Model;

namespace Funsafe.Runner.Serializers
{
    public class BinaryWriterSerializer
    {
        public static void Serialize(Message instance, BinaryWriter writer)
        {
            writer.Write(instance.Header.Length);
            writer.Write(instance.Header.Version);
            writer.Write(instance.Header.Flags);
            writer.Write(instance.Header.Timestamp.Ticks);
            writer.Write(instance.Id);
            writer.Write(instance.PartCount);

            for (var i = 0; i < instance.PartCount; i++)
            {
                var part = instance.Parts[i];
                writer.Write(part.Field1);
                writer.Write(part.Field2.Ticks);
                writer.Write(part.Field3);
                writer.Write(part.Field4);
            }
        }
    }
}
