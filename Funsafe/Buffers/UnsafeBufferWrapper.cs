using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Funsafe.Buffers
{
    // ridiculously unsafe; no bound check at all
    public sealed unsafe partial class UnsafeBufferWrapper : IDisposable
    {
        private GCHandle _pinnedGCHandle;

        private byte* _pBuffer;
        private byte* _cursor;
        private bool _disposed;
        private byte[] _buffer;

        public byte* Cursor
        {
            get { return _cursor; }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value > _pBuffer + _buffer.Length)
                    throw new ArgumentOutOfRangeException();

                if (value < _pBuffer)
                    throw new ArgumentOutOfRangeException();

                _cursor = value;
            }
        }

        public int Position { get { return (int)(_cursor - _pBuffer); } }

        public UnsafeBufferWrapper(byte[] buffer)
        {
            SetBuffer(buffer);
        }

        public void SetBuffer(byte[] buffer)
        {
            if (_pinnedGCHandle.IsAllocated)
                _pinnedGCHandle.Free();

            _buffer = buffer;
            _pinnedGCHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            _pBuffer = (byte*)_pinnedGCHandle.AddrOfPinnedObject().ToPointer();
            _cursor = _pBuffer;
        }

        public void ResetCursor()
        {
            _cursor = _pBuffer;
        }

        public void Dispose()
        {
            DisposeGCHandle();

            GC.SuppressFinalize(this);
        }

        private void DisposeGCHandle()
        {
            if (_disposed)
                return;

            if (_pinnedGCHandle.IsAllocated)
                _pinnedGCHandle.Free();

            _disposed = true;
        }

        ~UnsafeBufferWrapper()
        {
            DisposeGCHandle();
        }
    }
}
