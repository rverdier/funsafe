using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Funsafe.Buffers
{
    public unsafe partial class UnsafeBufferWrapper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Write<T>(ref T blittableStruct) where T : struct
        {
            _cursor = AccessorRegistry<T>.Write(_cursor, ref blittableStruct);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Read<T>() where T : struct
        {
            T item;
            _cursor = AccessorRegistry<T>.Read(_cursor, out item);
            return item;
        }

        public static void PrepareAccessorFor<T>()
            where T : struct
        {
            AccessorRegistry<T>.GenerateAccessors();
        }

        private static class AccessorRegistry<T>
        {
            public static readonly WriteAccessor Write;
            public static readonly ReadAccessor Read;

            static AccessorRegistry()
            {
                Write = GenerateWriteAccessor();
                Read = GenerateReadAccessor();
            }

            private static ReadAccessor GenerateReadAccessor()
            {
                var parameterTypes = new[] {typeof (byte*), typeof (T).MakeByRefType()};
                var ownerType = typeof (AccessorRegistry<T>);
                var dynamicMethod = new DynamicMethod("Read" + typeof (T).Name, MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard, typeof (byte*), parameterTypes, ownerType, true);

                var ilGenerator = dynamicMethod.GetILGenerator();

                ilGenerator.Emit(OpCodes.Ldarg_1);
                ilGenerator.Emit(OpCodes.Ldarg_0);

                ilGenerator.Emit(OpCodes.Ldobj, typeof (T));
                ilGenerator.Emit(OpCodes.Stobj, typeof (T));

                ilGenerator.Emit(OpCodes.Ldarg_0);
                ilGenerator.Emit(OpCodes.Sizeof, typeof (T));
                ilGenerator.Emit(OpCodes.Add);
                ilGenerator.Emit(OpCodes.Ret);

                return (ReadAccessor) dynamicMethod.CreateDelegate(typeof (ReadAccessor));
            }

            private static WriteAccessor GenerateWriteAccessor()
            {
                var parameterTypes = new[] {typeof (byte*), typeof (T).MakeByRefType()};
                var ownerType = typeof (AccessorRegistry<T>);
                var dynamicMethod = new DynamicMethod("Write" + typeof (T).Name, MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard, typeof (byte*), parameterTypes, ownerType, true);

                var ilGenerator = dynamicMethod.GetILGenerator();

                ilGenerator.Emit(OpCodes.Ldarg_0);
                ilGenerator.Emit(OpCodes.Ldarg_1);
                ilGenerator.Emit(OpCodes.Ldobj, typeof (T));
                ilGenerator.Emit(OpCodes.Stobj, typeof (T));

                ilGenerator.Emit(OpCodes.Ldarg_0);
                ilGenerator.Emit(OpCodes.Sizeof, typeof (T));
                ilGenerator.Emit(OpCodes.Add);
                ilGenerator.Emit(OpCodes.Ret);

                return (WriteAccessor) dynamicMethod.CreateDelegate(typeof (WriteAccessor));
            }

            public static void GenerateAccessors()
            {
            }

            internal delegate byte* WriteAccessor(byte* cursor, ref T item);

            internal delegate byte* ReadAccessor(byte* cursor, out T item);
        }
    }
}