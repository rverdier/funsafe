using System;
using System.IO;

namespace Funsafe.Runner.Model
{
    public class BinaryMessageSerializer
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

        public static void Deserialize(Message instance, BinaryReader reader)
        {
            instance.Header.Length = reader.ReadUInt32();
            instance.Header.Version = reader.ReadUInt32();
            instance.Header.Flags = reader.ReadInt32();
            instance.Header.Timestamp = new DateTime(reader.ReadInt64());
            instance.Id = reader.ReadInt32();
            instance.PartCount = reader.ReadInt32();

            for (var i = 0; i < instance.PartCount; i++)
            {
                instance.Parts[i] = new MessagePart
                {
                    Field1 = reader.ReadInt64(),
                    Field2 = new DateTime(reader.ReadInt64()),
                    Field3 = reader.ReadBoolean(),
                    Field4 = reader.ReadDouble()
                };
            }
        }
    }
}