using System;
using System.IO;
using System.Collections.Generic;

namespace AdventOfCodeDay6
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = ReadData("D:\\Mykola\\Pictures\\inputDay6.txt");
            List<Node<string>> nodes = new List<Node<string>>();
            nodes = ParseLines(input,nodes);
            Console.WriteLine(CountOrbits(nodes));
            CalculateOrbitalTransfers(nodes);
        }

        static void CalculateOrbitalTransfers(List<Node<string>> nodes)
        {
            string rootValue;
            string santaPath;
            string personalPath;
            string[] personalPathArray = new string[nodes.Count];
            string[] santaPathArray = new string[nodes.Count];
            string commonNode = "";
            bool foundCommon = false;

            foreach (Node<string> node in nodes)
            {
                if (node.parentNode == null)
                {
                    rootValue = node.value;
                    break;
                }
            }

            foreach(Node<string> node in nodes)
            {
                if (node.value == "YOU")
                {
                    personalPath = GetParentPath(node);
                    personalPathArray = personalPath.Split(",");
                }
                else if (node.value == "SAN")
                {
                    santaPath = GetParentPath(node);
                    santaPathArray = santaPath.Split(",");
                }
            }

            int i = 0, j = 0, distance = -1;
            foreach(string node1 in personalPathArray)
            {
                j = 0;
                foreach(string node2 in santaPathArray)
                {
                    if (node1 == node2)
                    {
                        commonNode = node1;
                        foundCommon = true;
                        distance = i + j - 2;
                        break;
                    }
                    j++;
                }
                if (foundCommon)
                    break;
                i++;
            }

            Console.WriteLine(distance);
        }

        static string GetParentPath(Node<string> node)
        {
            //int amountAwayFromRoot = 0;
            string path = "";

            path = node.value + ",";

            if (node.parentNode != null)
            {
                path += GetParentPath(node.parentNode);
            }

            //amountAwayFromRoot++;
            /*if (node.parentNode != null)
            {
                amountAwayFromRoot += CountParent(node.parentNode);
            }

            return amountAwayFromRoot;*/
            return path;
        }

        static string ReadData(string filePath)
        {
            string input = "";
            try
            {
                input = File.ReadAllText(filePath);
            }
            catch (Exception)
            {

                throw;
            }
            return input;
        }

        static List<Node<string>> ParseLines(string input, List<Node<string>> nodes)
        {
            string[] lines = input.Split('\n');
            bool isNodePresent = false;
            //create nodes
            foreach (string line in lines)
            {
                string[] subline = line.Split(')');
                Node<string> node = new Node<string>();
                node.value = subline[0];
                foreach (Node<string> node1 in nodes)
                {
                    if (node1.value == node.value)
                    {
                        isNodePresent = true;
                    }
                }    

                if (!isNodePresent)
                {
                    nodes.Add(node);
                }
                isNodePresent = false;
            }

            foreach (string line in lines)
            {
                string[] subline = line.Split(')');
                Node<string> node = new Node<string>();
                node.value = subline[1];
                foreach (Node<string> node1 in nodes)
                {
                    if (node1.value == node.value)
                    {
                        isNodePresent = true;
                    }
                }

                if (!isNodePresent)
                {
                    nodes.Add(node);
                }
                isNodePresent = false;
            }

            //create children
            foreach (string line in lines)
            {
                string[] subline = line.Split(')');
                foreach(Node<string> node in nodes)
                {
                    if (node.value == subline[0])
                    {
                        foreach(Node<string> node2 in nodes)
                        {
                            if (node2.value == subline[1])
                            {
                                if (node.childNodes.Contains(node2))
                                {
                                    continue;
                                }
                                else
                                {
                                    node.childNodes.Add(node2);
                                    node2.parentNode = node;
                                }
                            }
                        }
                    }
                }
            }
                /*bool childNodeCreated = false;
                string[] subline = line.Split(')');
                foreach (Node<string> node in nodes)
                {
                    if (node.value == subline[0])
                    {
                        Node<string> childNode = new Node<string>();
                        childNode.value = subline[1];
                        node.childNodes.Add(childNode);
                        childNodeCreated = true;
                    }
                }
                if (!childNodeCreated)
                {
                    Node<string> node1 = new Node<string>();
                    Node<string> node2 = new Node<string>();
                    node2.value = subline[1];
                    node1.value = subline[0];
                    node1.childNodes.Add(node2);
                    nodes.Add(node1);
                }*/

            return nodes;
        }

        static int CountOrbits(List<Node<string>> nodes)
        {
            int numberOfOrbits = 0;

            foreach (Node<string> node in nodes)
            {
                numberOfOrbits += CountChildOrbits(node);
            }

            return numberOfOrbits;
        }

        static int CountChildOrbits(Node<string> node)
        {
            int numberOfChildOrbits = 0;

            foreach (Node<string> childNode in node.childNodes)
            {
                numberOfChildOrbits++;
                numberOfChildOrbits += CountChildOrbits(childNode);
            }

            return numberOfChildOrbits;
        }
    }
}
