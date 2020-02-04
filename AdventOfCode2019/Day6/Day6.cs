using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019
{
    class Day6 : ProgramBase
    {
        private int _orbitCounter;
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

            Console.WriteLine($"Orbit count: {_orbitCounter}");

            CalculateShortestPath();
        }

        private void CalculateShortestPath()
        {
            string you = "YOU";
            string santa = "SAN";

            string youOrbitObject = _allRelations.Single(r => r.Child == you).Parent;
            string santaOrbitObject = _allRelations.Single(r => r.Child == santa).Parent;

            string[] vertices = GetAllNodes();
            var edges = GetAllEdges();
            var graph = new Graph<string>(vertices, edges);

            var bfs = new Bfs();
            Func<string, IEnumerable<string>> shortestPath = bfs.ShortestPathFunction(graph, youOrbitObject);

            Console.WriteLine($"shortest path to {santaOrbitObject}: {string.Join(", ", shortestPath(santaOrbitObject))}");
            Console.WriteLine($"count of steps: {shortestPath(santaOrbitObject).Count() - 1}");
        }

        private IEnumerable<Tuple<string,string>> GetAllEdges()
        {
            List<Tuple<string, string>> tuples = new List<Tuple<string, string>>();
            foreach (Relation relation in _allRelations)
            {
                tuples.Add(Tuple.Create(relation.Child, relation.Parent));
            }

            return tuples;
        }

        private string[] GetAllNodes()
        {
            var allNodes = new List<string>();
            allNodes.AddRange(_allRelations.Select(r => r.Parent));
            allNodes.AddRange(_allRelations.Select(r => r.Child));
            
            return allNodes.Distinct().ToArray();
        }
    }

    public class Relation
    {
        public string Parent { get; set; }
        public string Child { get; set; }
    }

    public class Node
    {
        private readonly List<Node> _orbits = new List<Node>();

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
        private readonly List<Relation> _relations;
        private readonly Node _parent;

        public Tree(Node root, List<Relation> relations)
        {
            _relations = relations;

            _parent = root;
            BuildOrbits(_parent);
        }

        private void BuildOrbits(Node parent)
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

        public Node ParentNode => _parent;
    }
}
