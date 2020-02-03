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

            CalculateShortestPathWithDijkstra();
        }

        private void CalculateShortestPathWithDijkstra()
        {
            List<string> visitedNodes = new List<string>();
            List<string> unvisitedNodes = new List<string>();
            List<DijkstraTableRow> dijkstraTable = new List<DijkstraTableRow>();

            string origin = "YOU";
            
            //Get the object we (YOU node) are orbiting and set it as origin
            string vertex = _allRelations.Single(r => r.Child == origin).Parent;

            //Set the distance to all nodes to something large (int.MaxValue)
            var allNodes = GetAllNodes();
            foreach (string node in allNodes)
            {
                dijkstraTable.Add(new DijkstraTableRow {Vertex = node, ShortestDistanceFromOrigin = int.MaxValue});
            }

            //Set distance from origin to origin to 0
            dijkstraTable.Single(r => r.Vertex == vertex).ShortestDistanceFromOrigin = 0;
           
            //All unvisited nodes copied to unvisited list.
            unvisitedNodes.AddRange(allNodes);

            string currentVertex = vertex;

            while (unvisitedNodes.Count > 0)
            {
                List<string> neighborsNotVisitedOfCurrentVortex = _allRelations.Where(r => r.Parent == currentVertex || r.Child == currentVertex).Select(s => s.Parent).Union(_allRelations.Where(r => r.Parent == currentVertex || r.Child == currentVertex).Select(s => s.Child)).ToList();
                neighborsNotVisitedOfCurrentVortex.Remove(currentVertex);
                
                //Only visit unvisited neighbors
                foreach (var visitedNode in visitedNodes)
                {
                    neighborsNotVisitedOfCurrentVortex.Remove(visitedNode);
                }

                foreach (var node in neighborsNotVisitedOfCurrentVortex)
                {
                    var knownDistanceFromOrigin = dijkstraTable.Where(r => r.Vertex == node).Select(r => r.ShortestDistanceFromOrigin).Single();
                    if (knownDistanceFromOrigin == int.MaxValue)
                    {
                        knownDistanceFromOrigin = 0;
                    }

                    if (knownDistanceFromOrigin + 1 < dijkstraTable.Single(r => r.Vertex == node).ShortestDistanceFromOrigin)
                    {
                        dijkstraTable.Single(r => r.Vertex == node).ShortestDistanceFromOrigin = knownDistanceFromOrigin + 1;
                        dijkstraTable.Single(r => r.Vertex == node).PreviousVertex = currentVertex;

                        currentVertex = node;
                    }
                }

                unvisitedNodes.Remove(currentVertex);
                visitedNodes.Add(currentVertex);

                currentVertex = unvisitedNodes.Take(1).SingleOrDefault();
                if (currentVertex == null)
                    return;
            }
        }

        private IEnumerable<string> GetAllNodes()
        {
            var allNodes = new List<string>();
            allNodes.AddRange(_allRelations.Select(r => r.Parent));
            allNodes.AddRange(_allRelations.Select(r => r.Child));
            
            return allNodes.Distinct();
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

    public class DijkstraTableRow
    {
        public string Vertex { get; set; }
        public int ShortestDistanceFromOrigin { get; set; }
        public string PreviousVertex { get; set; }
    }
}
