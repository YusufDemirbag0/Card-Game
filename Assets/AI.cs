using System;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Node Root;
    public Transform target1, target2;
    public float speed = 2f, threshold = 1f;

    TurnBasedSystem turnBasedSystem;
    CardPulling cardPulling;
    public bool hasItReached1, hasItReached2, playIt, startProcess;
    public int selectedCard = -1, highestTemp;
    private Node bestNode;
    private List<Node> emptyNodes = new List<Node>();

    public void Start()
    {
        turnBasedSystem = UnityEngine.Object.FindAnyObjectByType<TurnBasedSystem>();
        cardPulling = UnityEngine.Object.FindAnyObjectByType<CardPulling>();
        CreateTree();
    }

    public void Update()
    {
        if (!turnBasedSystem.playerTurn) MakeMove();
    }

    private void MakeMove()
    {
        if (!hasItReached1)
        {
            transform.position = Vector3.Lerp(transform.position, target1.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, target1.position) < threshold) hasItReached1 = true;
        }
        else if (!cardPulling.isAICardPulled)
        {
            cardPulling.x = UnityEngine.Random.Range(0, 6);
            cardPulling.isAICardPulled = true;
            playIt = true;
        }
        else if (playIt && !hasItReached2)
        {
            transform.position = Vector3.Lerp(transform.position, target2.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, target2.position) < threshold)
            {
                hasItReached2 = true;
                playIt = false;
                cardPulling.isDone = true;
                startProcess = true;
            }
        }
        if (hasItReached1 && startProcess)
        {
            CardPlacement();
            startProcess = false;
            ResetBools();
            turnBasedSystem.Turning();
        }
    }

    private void CardPlacement()
    {
        bestNode = null;
        emptyNodes.Clear();
        highestTemp = 0;
        selectedCard = cardPulling.GetAIPulledCardIndexs();
        DFS(Root, null);
        PlaceCardAtBestNode();
    }

    private void ResetBools()
    {
        hasItReached1 = hasItReached2 = cardPulling.isAICardPulled = cardPulling.isDone = false;
    }

    private void DFS(Node currentNode, Node parentNode)
    {
        if (currentNode == null) return;
        if (currentNode.Left == null && currentNode.Middle == null && currentNode.Right == null)
        {
            int currentValue = PullValueFromNode(currentNode);
            if (currentValue == -1)
            {
                emptyNodes.Add(currentNode);
                SimulateAndCompare(currentNode, parentNode);
            }
        }
        DFS(currentNode.Left, currentNode);
        DFS(currentNode.Middle, currentNode);
        DFS(currentNode.Right, currentNode);
    }

    private void SimulateAndCompare(Node currentNode, Node parentNode)
    {
        if (parentNode == null) return;
        int matchCount = 0, stackScore;
        
        if (selectedCard == PullValueFromNode(parentNode.Middle)) matchCount++;
        if (selectedCard == PullValueFromNode(parentNode.Right)) matchCount++;
        
        stackScore = (selectedCard + 1) * (matchCount + 1);
        int temp = stackScore;

        if (temp > highestTemp)
        {
            highestTemp = temp;
            bestNode = currentNode;
        }
    }

    private void PlaceCardAtBestNode()
    {
        Node targetNode = bestNode ?? GetRandomEmptyNode();
        GameObject targetObject = GameObject.Find("Node" + targetNode.Value);
        if (targetObject != null)
        {
            var plotScript = targetObject.GetComponent<Plot>();
            if (plotScript != null)
            {
                plotScript.selectedPrefabNumber = selectedCard;
                plotScript.ChangeSpriteInstant();
            }
        }
    }

    private Node GetRandomEmptyNode()
    {
        foreach (var node in emptyNodes)
        {
            if (PullValueFromNode(node) == -1) return node;
        }
        return null;
    }

    private int PullValueFromNode(Node node)
    {
        if (node == null) return -1;
        GameObject targetObject = GameObject.Find("Node" + node.Value);
        var plotScript = targetObject?.GetComponent<Plot>();
        return plotScript?.selectedPrefabNumber ?? -1;
    }

    private void CreateTree()
    {
        Root = new Node(100)
        {
            Left = new Node(10) { Left = new Node(1), Middle = new Node(2), Right = new Node(3) },
            Middle = new Node(20) { Left = new Node(4), Middle = new Node(5), Right = new Node(6) },
            Right = new Node(30) { Left = new Node(7), Middle = new Node(8), Right = new Node(9) }
        };
    }
}

public class Node
{
    public int Value;
    public Node Left, Middle, Right;
    public Node(int value) { Value = value; }
}
