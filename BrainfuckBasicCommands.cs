using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			vm.RegisterCommand('.', b => 
			{
				write(Convert.ToChar(b.Memory[b.MemoryPointer]));
			});
			vm.RegisterCommand('+', b => 
			{ 
				if (b.Memory[b.MemoryPointer] == 255) b.Memory[b.MemoryPointer] = 0; 
				else b.Memory[b.MemoryPointer]++; 
			});
			vm.RegisterCommand('-', b => 
			{
                if (b.Memory[b.MemoryPointer] == 0) b.Memory[b.MemoryPointer] = 255;
                else b.Memory[b.MemoryPointer]--;
            });
			vm.RegisterCommand(',', b =>
			{
				b.Memory[b.MemoryPointer] = Convert.ToByte(read());
			});
            vm.RegisterCommand('>', b =>
            {
				if (b.MemoryPointer+1 == b.Memory.Length)
					b.MemoryPointer = 0;
				else
					b.MemoryPointer++;
            });
            vm.RegisterCommand('<', b =>
            {
                if (b.MemoryPointer == 0)
                    b.MemoryPointer = b.Memory.Length-1;
                else
                    b.MemoryPointer--;
            });
			CommandsForSymbols(vm);
        }

        public static void CommandsForSymbols(IVirtualMachine vm)
        {
			var symbols = new char[] { 'a','z','A','Z','0','9' };
			for (int i = 0; i < symbols.Length; i = i+2)
			{
				for (int j = symbols[i]; j <= symbols[i+1] ; j++)
				{
					int t = j;
					vm.RegisterCommand(Convert.ToChar(t), b =>
					{
						b.Memory[b.MemoryPointer] = (byte)t;
					});
				}
			}
        }
    }
}