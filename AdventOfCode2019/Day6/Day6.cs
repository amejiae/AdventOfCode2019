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
                _allRelations.Add(new Relation { Parent = parentAndChild[0], Child = parentAndChild[1] });
            }

            foreach (Relation relation in _allRelations)
            {
                var root = new Node(relation.Child);
                var tree = new Tree(root, _allRelations);
                _orbitCounter += tree.ParentNode.Orbits.Count;
            }

            Console.WriteLine(_orbitCounter);
        }
    }

    public class Relation
    {
        public string Parent { get; set; }
        public string Child { get; set; }
    }

    public class Node
    {
        private List<Node> _orbits = new List<Node>();

        public Node(string value)
        {
            this.Value = value;
        }

        public List<Node> Orbits => _orbits;

        public string Value { get; }

        public void AddOrbit(Node node)
        {
            _orbits.Add(node);
        }

        public override string ToString()
        {
            return Value;
        }
    }

    public class Tree
    {
        private List<Relation> _relations;
        private Node _parent;

        public Tree(Node root, List<Relation> relations)
        {
            _relations = relations;

            _parent = root;
            BuildTree(_parent);
        }

        private void BuildTree(Node parent)
        {
            var directOrbits = from r in _relations
                where r.Child == parent.Value
                select new Node(r.Parent);

            var directOrbit = directOrbits.SingleOrDefault();
            if (directOrbit == null)
                return;

            parent.AddOrbit(directOrbit);
            AddOrbit(parent, directOrbit);
        }

        private void AddOrbit(Node root, Node node)
        {
            var directOrbits = from r in _relations
                where r.Child == node.Value
                select new Node(r.Parent);

            var directOrbit = directOrbits.SingleOrDefault();
            if (directOrbit == null)
                return;

            root.AddOrbit(directOrbit);
            AddOrbit(root, directOrbit);
        }

        public Node ParentNode 
        {
            get { return _parent; }
        }
    }
}
