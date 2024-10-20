using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{
    [SerializeField] DeckManager deck;
    Dictionary<Node, int> nodes = new();
    float TargettingTendencyIndicator = 0;
    Node max;
    //Buat dipake pas Random.Range (0 smpe 1), makin tinggi indicator makin narget
    //Kalo ga ya random
    public void Prep()
    {
        SetNodes();
        FindLongestSequence();
        ConvertSequenceToDeployData(max, nodes[max], null);
    }
    void GetSequence()
    {
        Debug.Log($"{nodes[max] - 1}\n=========================");
        Debug.Log($"{PrintSequence(max, nodes[max] - 1)}");
    }
    void ConvertSequenceToDeployData(Node node, int value, Node prev_node)
    {
        if (node == null) return;
        value -= 1;
        var Neighbor = node.neighbors.Find(x => nodes[x] == value);
        bool ability = prev_node != null && node.data.CheckCondition(prev_node.data);
        TargetManager.instance.SetTargetInfo(node.data, ability);
        TargetManager.instance.EnemyGetTargets();
        DeployData deployData = new()
        {
            link = node.data,
            isAbility = ability,
            targets = TargetManager.instance.Target
        };
        deck.EnqueueData(deployData);
        ConvertSequenceToDeployData(Neighbor, value, node);
    }
    string PrintSequence(Node node, int value)
    {
        string sequenceName = $"{node.data.Linker} --[{node.data.Nexus}]-->";
        foreach(var a in node.neighbors)
        {
            if (nodes[a] == value)
            {
                sequenceName += PrintSequence(a, value - 1);
                break;
            }
        }
        return sequenceName;
    }
    void SetNodes()
    {
        nodes = new();
        var datas = deck.GetHandData();
        foreach (var data in datas) nodes.Add(new Node(data), 0);
        var nodesList = nodes.Keys.ToList();
        foreach (var node in nodesList)
        {
            max = node;
            node.neighbors = nodesList.FindAll(x => x.data.Linker == node.data.Nexus && x != node);
        }
    }
    void FindLongestSequence()
    {
        foreach(var node in nodes.Keys.ToList())
        {
            GetSequenceLength(node);
            max = nodes[node] < nodes[max] ? max : node;
        }
    }
    void GetSequenceLength(Node node)
    {
        if (nodes[node] > 0) return;
        nodes[node]++;

        int max = 0;
        foreach (Node N in node.neighbors)
        {
            GetSequenceLength(N);
            if (nodes[N] > max) max = nodes[N];
        }
        //Debug.Log($"Node : {nodes[node]} + {max}");
        nodes[node] += max;
    }

    class Node
    {
        public List<Node> neighbors;
        public Link data;
        public Node(Link data)
        {
            this.data = data;
            neighbors = new();
        }
    }
}

