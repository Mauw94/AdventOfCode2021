﻿using System.Collections.Generic;
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
                Value = Value ?? null,
            };
        }
    }

    public class Day18 : AdventBase
    {
        private readonly List<Node> _nodes;

        public Day18()
        {
            _nodes = Common.GetInput(18)
                .Select(CreateTreeNodes)
                .ToList();
        }

        bool Split(Node node)
        {
            if (node.HasValue && node.Value > 10)
            {
                node.Left = new Node { Value = node.Value / 2 };
                node.Right = new Node
                {
                    Value = node.Value / 2
                    + (node.Value % 2 == 1 ? 1 : 0)
                };
                node.Value = null;

                return true;
            }

            return !node.HasValue && (Split(node.Left) || Split(node.Right));
        }

        (int left, int right) Explode(Node node, int depth = 0)
        {
            if (!node.HasValue && node.Left.HasValue && node.Right.HasValue && depth > 3)
            {
                var values = (node.Left.Value.Value, node.Right.Value.Value);
                node.Value = 0;
                node.Left = null;
                node.Right = null;

                return values;
            }

            if (node.HasValue)
                return (0, 0);

            var (ll, rl) = Explode(node.Left, depth + 1);
            if (rl > 0)
            {
                var child = node.Right;
                while (!child.HasValue)
                    child = child.Left; ;
                child.Value += rl;
            }

            var (lr, rr) = Explode(node.Right, depth + 1);
            if (lr > 0)
            {
                var child = node.Left;
                while (!child.HasValue)
                    child = child.Left;
                child.Value += lr;
            }
            return (ll, rr);
        }

        void Reduce(Node node)
        {
            bool changed;
            do
            {
                node.Left.Value = Explode(node).left;
                node.Right.Value = Explode(node).right;
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
            var result = _nodes.Aggregate(Add).Magnitude();

            LogResults(18, 1, result);
        }
    }
}
