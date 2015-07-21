using System;
using System.Runtime.InteropServices;
using Funsafe.Buffers;
using NUnit.Framework;

namespace Funsafe.Tests.Buffers
{
    [TestFixture]
    public unsafe class UnsafeBufferWrapperTests
    {
        [Test]
        public void Should_read_and_write_bools()
        {
            var buffer = new byte[1024];
            using (var wrapper = new UnsafeBufferWrapper(buffer))
            {
                const bool value = true;
                wrapper.Write(value);
                Assert.AreEqual(wrapper.Position, sizeof (bool));
                wrapper.ResetCursor();
                var readValue = wrapper.ReadBoolean();
                Assert.AreEqual(value, readValue);
            }
        }

        [Test]
        public void Should_read_and_write_bytes()
        {
            var buffer = new byte[1024];
            using (var wrapper = new UnsafeBufferWrapper(buffer))
            {
                const byte value = (byte) 42;
                wrapper.Write(value);
                Assert.AreEqual(wrapper.Position, sizeof (byte));
                wrapper.ResetCursor();
                var readValue = wrapper.ReadByte();
                Assert.AreEqual(value, readValue);
            }
        }

        [Test]
        public void Should_read_and_write_chars()
        {
            var buffer = new byte[1024];
            using (var wrapper = new UnsafeBufferWrapper(buffer))
            {
                const char value = 'x';
                wrapper.Write(value);
                Assert.AreEqual(wrapper.Position, sizeof (char));
                wrapper.ResetCursor();
                var readValue = wrapper.ReadChar();
                Assert.AreEqual(value, readValue);
            }
        }

        [Test]
        public void Should_read_and_write_decimals()
        {
            var buffer = new byte[1024];
            using (var wrapper = new UnsafeBufferWrapper(buffer))
            {
                const decimal value = 42.42m;
                wrapper.Write(value);
                Assert.AreEqual(wrapper.Position, sizeof (decimal));
                wrapper.ResetCursor();
                var readValue = wrapper.ReadDecimal();
                Assert.AreEqual(value, readValue);
            }
        }

        [Test]
        public void Should_read_and_write_doubles()
        {
            var buffer = new byte[1024];
            using (var wrapper = new UnsafeBufferWrapper(buffer))
            {
                const double value = 42.42;
                wrapper.Write(value);
                Assert.AreEqual(wrapper.Position, sizeof (double));
                wrapper.ResetCursor();
                var readValue = wrapper.ReadDouble();
                Assert.AreEqual(value, readValue);
            }
        }

        [Test]
        public void Should_read_and_write_enums()
        {
            var buffer = new byte[1024];
            using (var wrapper = new UnsafeBufferWrapper(buffer))
            {
                var value = ConsoleColor.Cyan;
                wrapper.Write(ref value);
                Assert.AreEqual(wrapper.Position, sizeof (ConsoleColor));
                wrapper.ResetCursor();
                var readValue = wrapper.Read<ConsoleColor>();
                Assert.AreEqual(value, readValue);
            }
        }

        [Test]
        public void Should_read_and_write_floats()
        {
            var buffer = new byte[1024];
            using (var wrapper = new UnsafeBufferWrapper(buffer))
            {
                const float value = 42.42f;
                wrapper.Write(value);
                Assert.AreEqual(wrapper.Position, sizeof (float));
                wrapper.ResetCursor();
                var readValue = wrapper.ReadSingle();
                Assert.AreEqual(value, readValue);
            }
        }

        [Test]
        public void Should_read_and_write_ints()
        {
            var buffer = new byte[1024];
            using (var wrapper = new UnsafeBufferWrapper(buffer))
            {
                const int value = 42;
                wrapper.Write(value);
                Assert.AreEqual(wrapper.Position, sizeof (int));
                wrapper.ResetCursor();
                var readValue = wrapper.ReadInt32();
                Assert.AreEqual(value, readValue);
            }
        }

        [Test]
        public void Should_read_and_write_longs()
        {
            var buffer = new byte[1024];
            using (var wrapper = new UnsafeBufferWrapper(buffer))
            {
                const long value = 42L;
                wrapper.Write(value);
                Assert.AreEqual(wrapper.Position, sizeof (long));
                wrapper.ResetCursor();
                var readValue = wrapper.ReadInt64();
                Assert.AreEqual(value, readValue);
            }
        }

        [Test]
        public void Should_read_and_write_sbytes()
        {
            var buffer = new byte[1024];
            using (var wrapper = new UnsafeBufferWrapper(buffer))
            {
                const sbyte value = (sbyte) 42;
                wrapper.Write(value);
                Assert.AreEqual(wrapper.Position, sizeof (sbyte));
                wrapper.ResetCursor();
                var readValue = wrapper.ReadSByte();
                Assert.AreEqual(value, readValue);
            }
        }

        [Test]
        public void Should_read_and_write_shorts()
        {
            var buffer = new byte[1024];
            using (var wrapper = new UnsafeBufferWrapper(buffer))
            {
                const short value = 42;
                wrapper.Write(value);
                Assert.AreEqual(wrapper.Position, sizeof (short));
                wrapper.ResetCursor();
                var readValue = wrapper.ReadInt16();
                Assert.AreEqual(value, readValue);
            }
        }

        [Test]
        public void Should_read_and_write_structs()
        {
            var buffer = new byte[1024];
            using (var wrapper = new UnsafeBufferWrapper(buffer))
            {
                var value = Guid.NewGuid();
                wrapper.Write(ref value);
                Assert.AreEqual(wrapper.Position, sizeof (Guid));
                wrapper.ResetCursor();
                var readValue = wrapper.Read<Guid>();
                Assert.AreEqual(value, readValue);
            }
        }

        [Test]
        public void Should_read_and_write_uints()
        {
            var buffer = new byte[1024];
            using (var wrapper = new UnsafeBufferWrapper(buffer))
            {
                const uint value = 42u;
                wrapper.Write(value);
                Assert.AreEqual(wrapper.Position, sizeof (uint));
                wrapper.ResetCursor();
                var readValue = wrapper.ReadUInt32();
                Assert.AreEqual(value, readValue);
            }
        }

        [Test]
        public void Should_read_and_write_ulongs()
        {
            var buffer = new byte[1024];
            using (var wrapper = new UnsafeBufferWrapper(buffer))
            {
                const ulong value = 42UL;
                wrapper.Write(value);
                Assert.AreEqual(wrapper.Position, sizeof (ulong));
                wrapper.ResetCursor();
                var readValue = wrapper.ReadUInt64();
                Assert.AreEqual(value, readValue);
            }
        }

        [Test]
        public void Should_read_and_write_ushorts()
        {
            var buffer = new byte[1024];
            using (var wrapper = new UnsafeBufferWrapper(buffer))
            {
                const ushort value = 42;
                wrapper.Write(value);
                Assert.AreEqual(wrapper.Position, sizeof (ushort));
                wrapper.ResetCursor();
                var readValue = wrapper.ReadUInt16();
                Assert.AreEqual(value, readValue);
            }
        }

        [Test]
        public void Should_read_and_write_custom_structs()
        {
            var buffer = new byte[1024];
            using (var wrapper = new UnsafeBufferWrapper(buffer))
            {
                var value = new CustomStruct {Field1 = 42, Field2 = TimeSpan.FromHours(42), Field3 = DateTime.UtcNow};
                wrapper.Write(ref value);
                Assert.AreEqual(wrapper.Position, sizeof(CustomStruct));
                wrapper.ResetCursor();
                var readValue = wrapper.Read<CustomStruct>();
                Assert.AreEqual(value, readValue);
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct CustomStruct
        {
            [FieldOffset(0)]
            public int Field1;

            [FieldOffset(4)]
            public TimeSpan Field2;

            [FieldOffset(12)]
            public DateTime Field3;
        }
    }
}