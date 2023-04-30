using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataStructures
{
    internal class Program
    {
        static void Main()
        {
            Stack<int> myStack;
            Queue<int> myQueue;

            Console.Write("Choose the type of your datastructure: S for stack and Q for queue: ");
            char choice = ChooseDataStructure(Console.ReadKey().KeyChar);
            Console.Clear();
            Console.Write("Choose the size of your datastructure: ");
            int size = ChooseSize(Console.ReadLine());

            if (choice == 's')
            {
               myStack = new Stack<int>(size);
               DataStructureMenu(myStack);
            }
            else if (choice == 'q')
            {
                myQueue = new Queue<int>(size);
                DataStructureMenu(myQueue);
            }
        }

        static char ChooseDataStructure(char input)
        {
            switch (input)
            {
                case 's':
                case 'S':
                    return 's';

                case 'q':
                case 'Q':
                    return 'q';

                default:
                    Console.Clear();
                    Console.Write("Choose the type of your datastructure: S for stack and Q for queue: ");
                    return ChooseDataStructure(Console.ReadKey().KeyChar);
            }
        }

        static int ChooseSize(string input)
        {
            int size;

            if (int.TryParse(input, out size)) 
            {
                if(size > 0) 
                    return size;
            }

            Console.Clear();
            Console.Write("Choose the size of your datastructure: ");
            return ChooseSize(Console.ReadLine());
        }

        static void DataStructureMenu(DataStructure<int> myDataStructure)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Accepted commands:\n");
                Console.WriteLine("add       >> Adds an element to your data structure");
                Console.WriteLine("substract >> Substracts an element of your data structure");
                Console.WriteLine("view      >> Prints all current elementes in your data structure");
                Console.WriteLine("exit      >> Closes the program");

                if (OperationHandler(Console.ReadLine(), myDataStructure)) return;
            }
        }

        static bool OperationHandler(string input, DataStructure<int> myDataStructure)
        {
            Console.Clear();
            switch (input)
            {
                case "add":
                    Console.Write("Insert the numer you want to add: ");
                    myDataStructure.AddElement(AddNumber(Console.ReadLine()));
                    Console.ReadKey();
                    break;

                case "substract":
                    myDataStructure.SubstractElement();
                    Console.ReadKey();
                    break;

                case "view":
                    foreach(int element in myDataStructure.elements)
                        Console.Write(element + " ");
                    Console.ReadKey();
                    break;

                case "exit":
                    return true;
            }
            return false;
        }

        static int AddNumber(string input)
        {
            int number;

            if (int.TryParse(input, out number))
            {
               return number;
            }

            Console.Clear();
            Console.Write("Insert the numer you want to add: ");
            return AddNumber(Console.ReadLine());
        }
    }

    abstract class DataStructure<T>
    {
        protected int size;
        public T[] elements {get;}
        protected int pointer;

        public DataStructure(int size)
        {
            if (size <= 0) throw new ArgumentException("Invalid size");
            this.size = size;
            elements = new T[size];
            pointer = 0;
        }

        public abstract void AddElement(T element);
        public abstract void SubstractElement();
    }

    class Stack<T>: DataStructure<T>
    {
        public Stack(int size): base(size){}

        public override void AddElement(T element)
        {
            if (pointer == size) Console.WriteLine("Cannot push on a full stack");
            else
            {
                elements[pointer] = element;
                Console.WriteLine($"New element pushed successfuly at position {pointer}");
                pointer++;
            }
        }

        public override void SubstractElement()
        {
            if (pointer == 0) Console.WriteLine("Cannot pop an empty stack");
            else
            {
                pointer--;
                Console.WriteLine($"Element {elements[pointer]} at {pointer} popped successfuly");
                elements[pointer] = default(T);
            }
        }
       
    }

    class Queue<T> : DataStructure<T>
    {
        public Queue(int size) : base(size){}

        public override void AddElement(T element)
        {
            if (pointer == size) Console.WriteLine("Cannot add another member to the full queue ");
            else
            {
                elements[pointer] = element;
                Console.WriteLine($"New element added to the queue successfuly at position {pointer}");
                pointer++;
            }
        }

        public override void SubstractElement()
        {
            if (pointer == 0) Console.WriteLine("Cannot substract an element from an empty queue");
            else
            {
                Console.WriteLine($"Element {elements[0]} from the top of the queue was successfuly substracted");
                for (int i = 0; i < pointer-1; i++)
                    elements[i] = elements[i + 1];
                
                elements[pointer-1] = default(T);
                pointer--;
            }
        }
    }
}
