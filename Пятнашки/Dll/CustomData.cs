using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dll
{
    public class HeapNode<T>
    {
        public double Key;
        public T Value;

        public HeapNode(double key, T o)
        {
            Key = key;
            Value = o;
        }

        public override string ToString()
        {
            return $"{Key}:{Value}";
        }
    }

    public class BinaryHeap<T>
    {
        private List<HeapNode<T>> list = new List<HeapNode<T>>();

        public int Size
        {
            get { return list.Count; }
        }

        public void Clear()
        {
            list.Clear();
        }

        public override string ToString()
        {
            string s = "";
            foreach (HeapNode<T> a in list)
                s += $"{a.Value}({a.Key}) ";
            return s;
        }
        public void DecreaseOrAdd(double key, T o)
        {
            HeapNode<T> kv = list.Find((KV) => { return KV.Value.Equals(o); });
            if (kv != null)
            {
                int i = list.IndexOf(kv);
                if (kv.Key > key)
                    kv.Key = key;
                while (i > 1 && list[i / 2].Key > list[i].Key)
                {
                    HeapNode<T> temp = list[i];
                    list[i] = list[i / 2];
                    list[i / 2] = temp;
                }
            }
            else
            {
                Add(key, o);
            }
        }

        public void Add(double key, T value)
        {
            list.Add(new HeapNode<T>(key, value));
            int i = Size - 1;
            int parent = (i - 1) / 2;
            while (i > 0 && list[parent].Key > list[i].Key)
            {
                HeapNode<T> temp = list[i];
                list[i] = list[parent];
                list[parent] = temp;
                //
                i = parent;
                parent = (i - 1) / 2;
            }
        }

        public void heapify(int i)
        {
            int leftChild;
            int rightChild;
            int smallestChild;

            for (; ; )
            {
                leftChild = 2 * i + 1;
                rightChild = 2 * i + 2;
                smallestChild = i;

                if (leftChild < Size && list[leftChild].Key < list[smallestChild].Key)
                {
                    smallestChild = leftChild;
                }

                if (rightChild < Size && list[rightChild].Key < list[smallestChild].Key)
                {
                    smallestChild = rightChild;
                }

                if (smallestChild == i)
                {
                    break;
                }

                HeapNode<T> temp = list[i];
                list[i] = list[smallestChild];
                list[smallestChild] = temp;
                i = smallestChild;
            }
        }

        public T GetMin()
        {
            HeapNode<T> result = list[0];
            list[0] = list[Size - 1];
            list.RemoveAt(Size - 1);
            heapify(0);
            return result.Value;
        }
    }
}
