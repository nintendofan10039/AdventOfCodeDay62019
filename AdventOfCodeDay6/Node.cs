using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeDay6
{
    class Node<T>
    {
        public T value;
        public List<Node<T>> childNodes = new List<Node<T>>();
        public Node<T> parentNode;
    }
}
