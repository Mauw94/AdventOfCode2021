using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.AoC
{
    class Node
    {
        public int? Value = null;
        public Node Left;
        public Node Right;
        public bool HasValue => Value.HasValue;
        public int Magnitude() => Value ?? 3 * Left.Magnitude() + 2 * Right.Magnitude();

        public Node DeepCopy()
        {
            return new Node
            {
                Left = Left,
                Right = Right,
                Value = Value.Value,
            };
        }
    }

    public class Day18 : AdventBase
    {
        private List<Node> _nodes;

        public Day18()
        {
            _nodes = Common.GetInput(18)
                .Select(CreateTreeNodes)
                .ToList();
        }

        bool Split(Node node)
        {
            return false;
        }

        void Explode(Node node)
        {

        }

        void Reduce(Node node)
        {
            bool changed;
            do
            {
                Explode(node);
                changed = Split(node);
            } while (changed);
        }

        Node Add(Node a, Node b)
        {
            Node newNode = new();
            newNode.Left = a.DeepCopy();
            newNode.Right = b.DeepCopy();
            Reduce(newNode);

            return newNode;
        }

        Node CreateTreeNodes(string input)
        {
            var root = new Node();
            Stack<Node> stack = new();
            stack.Push(root);

            for (int i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '[':
                        var leftNode = new Node();
                        stack.Peek().Left = leftNode;
                        stack.Push(leftNode);
                        break;
                    case ']':
                        stack.Pop();
                        break;
                    case ',':
                        stack.Pop();
                        var rightNode = new Node();
                        stack.Peek().Right = rightNode;
                        stack.Push(rightNode);
                        break;
                    default:
                        stack.Peek().Value = int.Parse(input[i].ToString());
                        break;
                }
            }
            return root;
        }

        public override void Solution1()
        {

        }
    }
}
