using System;
using System.IO;
using Funsafe.Runner.Model;

namespace Funsafe.Runner.Serializers
{
    public class BinaryReaderDeserializer
    {
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