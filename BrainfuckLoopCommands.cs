using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
        public static void RegisterTo(IVirtualMachine vm)
        {
            Search(vm);
            vm.RegisterCommand('[', b =>
            {
                if (b.Memory[b.MemoryPointer] == 0)
                {
                    b.InstructionPointer = PairsNormal[b.InstructionPointer];
                }
            });
            vm.RegisterCommand(']', b =>
            {
                if (b.Memory[b.MemoryPointer] != 0)
                {
                    b.InstructionPointer = PairsReverse[b.InstructionPointer];
                }
            });
        }
        private static Dictionary<int, int> PairsNormal = new Dictionary<int, int>();
        private static Dictionary<int, int> PairsReverse = new Dictionary<int, int>();
        private static Stack<int> BreakersIndex = new Stack<int>();
        private static void Search(IVirtualMachine vm)
        {
            for (int i = 0; i < vm.Instructions.Length; i++)
            {
                if (vm.Instructions[i] == '[') 
                    BreakersIndex.Push(i);
                if (vm.Instructions[i] == ']')
                {
                    var index = BreakersIndex.Pop();
                    PairsNormal[index] = i;
                    PairsReverse[i] = index;
                }
            }
        }
    }
}