using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    public class DayTwelve
    {
        public string ProcessData()
        {
            var data = File.ReadAllLines("./Day12/Data.txt");

            Node firstNode = new Node();

            foreach (var line in data)
            {
                var newNodes = line.Split('-');
                if (string.IsNullOrWhiteSpace(firstNode.Name))
                {
                    firstNode.Name = newNodes[0];
                    Node nextNode = new Node();
                    nextNode.Name = newNodes[1];
                    nextNode.ConnectedNodes.Add(firstNode);
                    firstNode.ConnectedNodes.Add(nextNode);
                }
                else
                {

                }
            }

            return "";
        }

        public string ProcessDataPt2()
        {
            var data = File.ReadAllLines("./Day12/Data.txt");

            return "";
        }
    }

    public class Node
    {
        public List<Node> ConnectedNodes { get; set; }
        public NodeType NodeType 
        { 
            get
            {
                var evalChar = Name.FirstOrDefault();
                if (char.IsLower(evalChar))
                {
                    return NodeType.Small;
                }
                else
                {
                    return NodeType.Large;
                }
            }
        }
        public bool IsStart
        {
            get
            {
                return Name == "start";
            }
        }
        public bool IsEnd
        {
            get
            {
                return Name == "end";
            }
        }
        public string Name { get; set; }

        public Node()
        {
            ConnectedNodes = new List<Node>();
        }

        public Node FindNode(string name, List<Node> searchedNodes = null)
        {
            if(searchedNodes == null)
            {
                searchedNodes = new List<Node>();
            }

            foreach(Node node in ConnectedNodes)
            {
                if(node.Name == name)
                {
                    return node;
                }
                else
                {
                    searchedNodes.Add(node);
                    FindNode(name, searchedNodes);
                }
            }
        }
    }

    public enum NodeType
    {
        Small,
        Large
    }
}
