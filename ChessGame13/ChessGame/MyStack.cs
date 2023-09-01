using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame
{
    class MyStack<T> // creating a stack that will be used for an "undo move" feature - generics used
    {
        private T[] content;
        private int top;
        private int size;
        private bool full;

        public int Count { 
            get {
                return top;
            }
        }
        public MyStack(int size)
        {
            content = new T[size];
            top = 0;
            full = false;
            this.size = size;
        }

        

        public T Get(int index)
        {
            return content[index];
        }


        public void Push(T e)
        {
            if (!(top == size))
            {
                content[top] = e;
                top++;
            }
            if (top == size)
            {
                full = true;
            }
        }



        public T Pop()
        {
            if (!(top == 0))
            {
                top--;
                full = false;
                return content[top];
            }
            else
            {
                return content[0];
            }
        }
    }

}
