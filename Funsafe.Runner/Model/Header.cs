using System;

namespace Funsafe.Runner.Model
{
    public struct Header
    {
        public int Flags;
        public uint Length;
        public DateTime Timestamp;
        public uint Version;
    }
}