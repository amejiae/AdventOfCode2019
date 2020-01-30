using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace AdventOfCode2019
{
    class Day6 : ProgramBase
    {
        private int _orbitCounter = 0;
        private string[] _input;
        private readonly List<Relation> _allRelations = new List<Relation>();

        public override void Solve()
        {
            _input = File.ReadAllLines(".\\Inputs\\06.txt");
            foreach (string relation in _input)
            {
                string[] parentAndChild = relation.Split(')');
                _allRelations.Add(new Relation {Parent = parentAndChild[0], Child = parentAndChild[1]});
            }

            var tree = new Tree(_allRelations);
        }

        private void FindOrbits(string[] orbitObject)
        {
            
        }
    }

    public class Relation
    {
        public string Parent { get; set; }
        public string Child { get; set; }
    }

    public class Node
    {
        public string Value { get; set; }
        public List<Node> Children { get; set; }
    }

    public class Tree
    {
        private List<Node> _nodes = new List<Node>();
        private List<Relation> _relations;

        public Tree(List<Relation> relations)
        {
            _relations = relations;

            var parent = new Node { Value = "COM", Children = new List<Node>()};
            BuildTree(parent);
        }

        public List<Node> Nodes
        {
            get { return _nodes; }
        }

        private void BuildTree(Node parentNode)
        {
            IEnumerable<Node> directChildren = from r in _relations
                                                where r.Parent == parentNode.Value
                                                select new Node() {Value = r.Child, Children = new List<Node>()};

            if (!directChildren.Any())
                return;

            parentNode.Children.AddRange(directChildren);
            _nodes.Add(parentNode);

            foreach (Node child in directChildren)
            {
                BuildTree(child);
            }
        }
    }
}
