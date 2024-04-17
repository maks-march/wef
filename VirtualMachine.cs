using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }

		public Dictionary<char, Action<IVirtualMachine>> Commands;

		public VirtualMachine(string program, int memorySize)
		{
			Instructions = program;
			Memory = new byte[memorySize];
			MemoryPointer = 0;
			InstructionPointer = 0;
			Commands = new Dictionary<char, Action<IVirtualMachine>>();
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			Commands.Add(symbol, execute);
		}

		public void Run()
		{
            for (; InstructionPointer < Instructions.Length; InstructionPointer++)
			{
				if (Commands.ContainsKey(Instructions[InstructionPointer]))
					Commands[Instructions[InstructionPointer]](this);
            }
        }
	}
}