.assembly extern mscorlib
{
	.publickeytoken = (B7 7A 5C 56 19 34 E0 89 )
	.ver 4:0:0:0
}

.assembly Funsafe.IL
{
	.ver 1:0:0:0
}

.namespace Funsafe
{
    .class public auto ansi sealed beforefieldinit ILHelpers
        extends [mscorlib]System.Object
    {
        
     	.method public hidebysig static void Read<T> (uint8*& cursor, [out] !!T& item) cil managed aggressiveinlining 
		{
			.maxstack 3

			ldarg.1
			ldarg.0
			ldind.i
			ldobj !!T
			stobj !!T

			ldarg.0
			dup
			ldind.i
			sizeof !!T
			add
			stind.i
			ret
		}
		
		.method public hidebysig static uint8* Read2<T> (uint8* cursor, [out] !!T& item) cil managed aggressiveinlining 
		{
			.maxstack 3

			ldarg.1
			ldarg.0
			ldobj !!T
			stobj !!T

			ldarg.0
			sizeof !!T
			add
			ret
		}
		
		.method public hidebysig static !!T Read3<T> (uint8*& cursor) cil managed aggressiveinlining 
		{
			.maxstack 3
			.locals init (
				[0] !!T returnValue
			)

			ldarg.0
			ldind.i
			ldobj !!T
			stloc.0
			
			ldarg.0
			dup
			ldind.i
			sizeof !!T
			add
			stind.i

			ldloc.0
			ret
		}

		.method public hidebysig static void Write<T> (uint8*& cursor, !!T& item) cil managed aggressiveinlining
		{
			.maxstack 3

			ldarg.0
			ldind.i
			ldarg.1
			ldobj !!T
			stobj !!T

			ldarg.0
			dup
			ldind.i
			sizeof !!T
			add
			stind.i
			ret
		}
		
		.method public hidebysig static uint8* Write2<T> (uint8* cursor, !!T& item) cil managed aggressiveinlining
		{
			.maxstack 3

			ldarg.0
			ldarg.1
			ldobj !!T
			stobj !!T

			ldarg.0
			sizeof !!T
			add
			ret
		}
    }
}
