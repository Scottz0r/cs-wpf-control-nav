using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfNoPageNav
{
    /// <summary>
    /// Generic stack that drops objects after reaching a maximum capacity.
    /// </summary>
    /// <typeparam name="T">Type the collection holds.</typeparam>
    public class DropOutStack<T>
    {
        private T[] _items;
        private int _top;

        /// <summary>
        /// Get the number of items held in the stack.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Returns true if the stack can be popped.
        /// </summary>
        public bool CanPop
        {
            get
            {
                return Count > 0;
            }
        }

        /// <summary>
        /// Initializes the stack with the given maximum capacity.
        /// </summary>
        /// <param name="capacity">Maximum number of items the stack can hold.</param>
        public DropOutStack(int capacity)
        {
            _top = 0;
            _items = new T[capacity];
            Count = 0;
        }

        /// <summary>
        /// Remove all items from the stack.
        /// </summary>
        public void Clear()
        {
            if(Count > 0)
            {
                // Clear out items so references are released.
                Array.Clear(_items, 0, _items.Length);

                Count = 0;
                _top = 0;
            }
        }

        /// <summary>
        /// Push an item to the top of the stack. If this action causes the maximum number of items to be exceed, the
        /// least recently item added will be dropped.
        /// </summary>
        /// <param name="item">Item to push to the top of the stack.</param>
        public void Push(T item)
        {
            _items[_top] = item;
            _top = (_top + 1) % _items.Length;

            if(Count < _items.Length)
            {
                Count += 1;
            }
        }

        /// <summary>
        /// Pop an item from the stack.
        /// </summary>
        /// <returns>Item popped</returns>
        /// <exception cref="InvalidOperationException">Thrown when an empty stack is popped.</exception>
        public T Pop()
        {
            if(Count < 0)
            {
                throw new InvalidOperationException();
            }

            Count -= 1;

            _top = (_items.Length + _top - 1) % _items.Length;
            T val = _items[_top];

            // Set top to default to allow references to be garbage collected.
            _items[_top] = default;

            return val;
        }
    }
}
