using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SampleDotNet._3_Behavioral
{
    internal abstract class IteratorExample
    {
        public static void Run()
        {
            //   1
            //  / \
            // 2   3

            // in-order: 213
            // preorder: 123

            var root = new Node<int>(1, new Node<int>(2), new Node<int>(3));

            var it = new InOrderIterator<int>(root);
            while (it.MoveNext())
            {
                Console.WriteLine(it.Current.Value);
            }

            var tree = new BinaryTree<int>(root);
            Console.WriteLine(string.Join(",", tree.InOrder.Select(x => x.Value)));

            foreach (var node in tree)
                Console.WriteLine(node.Value);
        }

        public class Node<T>
        {
            public T Value;
            public Node<T> Left, Right;
            public Node<T> Parent;

            public Node(T value)
            {
                Value = value;
            }

            public Node(T value, Node<T> left, Node<T> right)
            {
                Value = value;
                Left = left;
                Right = right;

                left.Parent = right.Parent = this;
            }
        }

        public class InOrderIterator<T>
        {
            private readonly Node<T> _root;
            public Node<T> Current { get; set; }
            private bool yieldedStart;

            public InOrderIterator(Node<T> root)
            {
                _root = root;
                Current = root;
                while (Current.Left != null)
                    Current = Current.Left;

                //   1 <- root
                //  / \
                // 2   3
                // ^ Current
            }

            public bool MoveNext()
            {
                if (!yieldedStart)
                {
                    yieldedStart = true;
                    return true;
                }

                if (Current.Right != null)
                {
                    Current = Current.Right;
                    while (Current.Left != null)
                        Current = Current.Left;
                    return true;
                }
                else
                {
                    var p = Current.Parent;
                    while (p! != null && Current == p.Right)
                    {
                        Current = p;
                        p = p.Parent;
                    }

                    Current = p;
                    return Current != null;
                }
            }

            public void Reset()
            {
                Current = _root;
                yieldedStart = false;
            }
        }

        public class BinaryTree<T>
        {
            private Node<T> root;

            public BinaryTree(Node<T> root)
            {
                this.root = root;
            }

            public IEnumerable<Node<T>> InOrder
            {
                get
                {
                    IEnumerable<Node<T>> Traverse(Node<T> current)
                    {
                        if (current.Left != null)
                        {
                            foreach (var left in Traverse(current.Left))
                                yield return left;
                        }

                        yield return current;

                        if (current.Right != null)
                        {
                            foreach (var right in Traverse(current.Right))
                                yield return right;
                        }
                    }

                    foreach (var node in Traverse(root))
                        yield return node;
                }
            }

            public InOrderIterator<T> GetEnumerator()
            {
                return new InOrderIterator<T>(root);
            }
        }

        public class Creature : IEnumerable<int>
        {
            private int[] stats = new int[3];

            private const int strength = 0;

            public int Strength
            {
                get => stats[strength];
                set => stats[strength] = value;
            }

            public int Agility { get; set; }
            public int Intelligence { get; set; }

            // public double AverageStat => (Strength + Agility + Intelligence) / 3.0;
            public double AverageStat => stats.Average();

            public IEnumerator<int> GetEnumerator()
            {
                return stats.AsEnumerable().GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public int this[int index]
            {
                get => stats[index];
                set => stats[index] = value;
            }
        }
    }
}
