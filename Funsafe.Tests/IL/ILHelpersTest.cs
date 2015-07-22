using System;
using NUnit.Framework;

namespace Funsafe.Tests.IL
{
    [TestFixture]
    public unsafe class ILHelpersTest
    {
        [Test]
        public void Should_move_cursor_forward_and_return_read_value()
        {
            var guid = Guid.NewGuid();
            byte* bufferPtr = stackalloc byte[1024];

            var cursor = bufferPtr;
            *(Guid*) cursor = guid;

            var value = ILHelpers.Read3<Guid>(ref cursor);

            Assert.AreEqual(value, guid);
            Assert.AreEqual((int) cursor, (int) bufferPtr + sizeof (Guid));
        }

        [Test]
        public void Should_read_and_move_cursor_forward()
        {
            var guid = Guid.NewGuid();
            byte* bufferPtr = stackalloc byte[1024];

            var cursor = bufferPtr;
            *(Guid*) cursor = guid;

            Guid value;
            ILHelpers.Read(ref cursor, out value);

            Assert.AreEqual(value, guid);
            Assert.AreEqual((int) cursor, (int) bufferPtr + sizeof (Guid));
        }

        [Test]
        public void Should_read_and_return_new_cursor_value()
        {
            var guid = Guid.NewGuid();
            byte* bufferPtr = stackalloc byte[1024];

            var cursor = bufferPtr;
            *(Guid*) cursor = guid;

            Guid value;
            cursor = ILHelpers.Read2(cursor, out value);

            Assert.AreEqual(value, guid);
            Assert.AreEqual((int) cursor, (int) bufferPtr + sizeof (Guid));
        }

        [Test]
        public void Should_write_and_move_cursor_forward()
        {
            var guid = Guid.NewGuid();
            byte* bufferPtr = stackalloc byte[1024];
            var cursor = bufferPtr;

            ILHelpers.Write(ref cursor, ref guid);

            var value = *(Guid*) bufferPtr;

            Assert.AreEqual(value, guid);
            Assert.AreEqual((int) cursor, (int) bufferPtr + sizeof (Guid));
        }

        [Test]
        public void Should_write_and_return_new_cursor_value()
        {
            var guid = Guid.NewGuid();
            byte* bufferPtr = stackalloc byte[1024];
            var cursor = bufferPtr;

            cursor = ILHelpers.Write2(cursor, ref guid);

            var value = *(Guid*) bufferPtr;

            Assert.AreEqual(value, guid);
            Assert.AreEqual((int) cursor, (int) bufferPtr + sizeof (Guid));
        }
    }
}