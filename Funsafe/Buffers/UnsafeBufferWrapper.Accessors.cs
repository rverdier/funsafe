using System.Runtime.CompilerServices;

namespace Funsafe.Buffers
{
    public unsafe partial class UnsafeBufferWrapper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadBoolean()
        {
            var value = *(bool*)_cursor;
            _cursor += sizeof(bool);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte ReadByte()
        {
            var value = *_cursor;
            _cursor += sizeof(byte);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public char ReadChar()
        {
            var value = *(char*)_cursor;
            _cursor += sizeof(char);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public decimal ReadDecimal()
        {
            var value = *(decimal*)_cursor;
            _cursor += sizeof(decimal);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ReadDouble()
        {
            var value = *(double*)_cursor;
            _cursor += sizeof(double);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public short ReadInt16()
        {
            var value = *(short*)_cursor;
            _cursor += sizeof(short);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int ReadInt32()
        {
            var value = *(int*)_cursor;
            _cursor += sizeof(int);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long ReadInt64()
        {
            var value = *(long*)_cursor;
            _cursor += sizeof(long);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public sbyte ReadSByte()
        {
            var value = *(sbyte*)_cursor;
            _cursor += sizeof(sbyte);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float ReadSingle()
        {
            var value = *(float*)_cursor;
            _cursor += sizeof(float);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort ReadUInt16()
        {
            var value = *(ushort*)_cursor;
            _cursor += sizeof(ushort);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint ReadUInt32()
        {
            var value = *(uint*)_cursor;
            _cursor += sizeof(uint);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong ReadUInt64()
        {
            var value = *(ulong*)_cursor;
            _cursor += sizeof(ulong);
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(bool value)
        {
            *(bool*)_cursor = value;
            _cursor += sizeof(bool);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(byte value)
        {
            *_cursor = value;
            _cursor += sizeof(byte);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(sbyte value)
        {
            *(sbyte*)_cursor = value;
            _cursor += sizeof(sbyte);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(char value)
        {
            *(char*)_cursor = value;
            _cursor += sizeof(char);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(decimal value)
        {
            *(decimal*)_cursor = value;
            _cursor += sizeof(decimal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(double value)
        {
            *(double*)_cursor = value;
            _cursor += sizeof(double);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(float value)
        {
            *(float*)_cursor = value;
            _cursor += sizeof(float);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(int value)
        {
            *(int*)_cursor = value;
            _cursor += sizeof(int);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(uint value)
        {
            *(uint*)_cursor = value;
            _cursor += sizeof(uint);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(long value)
        {
            *(long*)_cursor = value;
            _cursor += sizeof(long);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(ulong value)
        {
            *(ulong*)_cursor = value;
            _cursor += sizeof(ulong);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(short value)
        {
            *(short*)_cursor = value;
            _cursor += sizeof(short);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write(ushort value)
        {
            *(ushort*)_cursor = value;
            _cursor += sizeof(ushort);
        }
    }
}
