using System;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Node Root;
    public Transform target1;
    public Transform target2;
    public float speed = 2f;
    public float threshold = 1f;

    TurnBasedSystem turnBasedSystem;
    CardPulling cardPulling;
    public bool hasItReached1 = false;
    public bool hasItReached2 = false;
    public bool playIt = false;
    public bool startProcess = false;

    public int selectedCard = -1;
    public int temp = 0;
    public int destroyBonus;
    public bool canDestroyedBy = false;
    private Node bestNode = null;
    private int highestTemp = 0;
    private List<Node> emptyNodes = new List<Node>();

    public void Start()
    {
        turnBasedSystem = UnityEngine.Object.FindAnyObjectByType<TurnBasedSystem>();
        cardPulling = UnityEngine.Object.FindAnyObjectByType<CardPulling>();
        CreateTree();
    }

    public void Update()
    {
        if (!turnBasedSystem.playerTurn)
        {
            MakeMove();
        }
    }

    private void MakeMove()
    {
        if (!hasItReached1) //Kart çekme yerine ulaşma
        {
            transform.position = Vector3.Lerp(transform.position, target1.position, speed * Time.deltaTime);//Kart çekmeye gidiş
            if (Vector2.Distance(transform.position, target1.position) < threshold)
            {
                hasItReached1 = true;
            }
        }
        else if (!cardPulling.isAICardPulled) 
        {
            cardPulling.x = UnityEngine.Random.Range(0, 6); 
            Debug.Log($"AI pulled card: {cardPulling.x}");
            cardPulling.isAICardPulled = true;
            playIt = true; //Kartı çektikten sonra dönüş için izin verme
        }
        else if (playIt && !hasItReached2)//Kartı çektikten sonra ilk pozisyonuna dönüş
        {
            transform.position = Vector3.Lerp(transform.position, target2.position, speed * Time.deltaTime);//Kartı çektikten sonra dönüş
            if (Vector2.Distance(transform.position, target2.position) < threshold)
            {
                hasItReached2 = true;
            }
            if (hasItReached2)
            {
                playIt = false;
                cardPulling.isDone = true;
                startProcess = true;//Kart yerleştirme işlemini başlat
            }
        }

        if (hasItReached1 && startProcess)
        {
            CardPlacement();
            startProcess = false;
            ResetBools(); //Bool değerlerini sıfırlama
            turnBasedSystem.Turning();
        }
    }

    private void CardPlacement()
    {
        bestNode = null;
        emptyNodes.Clear();
        highestTemp = 0;

        selectedCard = cardPulling.GetAIPulledCardIndexs(); 
        Debug.Log($"AI placing card: {selectedCard}");

        DFS(Root, null, true);
        PlaceCardAtBestNode();
    }

    private void ResetBools()
    {
        hasItReached1 = false;
        hasItReached2 = false;
        cardPulling.isAICardPulled = false; 
        cardPulling.isDone = false; 
        canDestroyedBy = false;
    }

    private void DFS(Node currentNode, Node parentNode, bool isAINode)
    {
        if (currentNode == null) return;

        if (currentNode.Left == null && currentNode.Middle == null && currentNode.Right == null)
        {
            int currentValue;

            if (isAINode)//Stack veya karşılaştırma işlemi için selectedPrefabNumber değeri çekme işlemi
                currentValue = PullValueFromNode(currentNode);
            else 
                currentValue = PullValueFromPlayerNode(currentNode);

            if (currentValue == -1)
            {
                emptyNodes.Add(currentNode);
                SimulateAndCompare(currentNode, parentNode);
            }
        }
        DFS(currentNode.Left, currentNode, isAINode);
        DFS(currentNode.Middle, currentNode, isAINode);
        DFS(currentNode.Right, currentNode, isAINode);
    }

    private void SimulateAndCompare(Node currentNode, Node parentNode)
    {
        if (parentNode == null) return;

        //AI'ın kendi kartlarını analiz etme kısmı
        int leftValue = PullValueFromNode(parentNode.Left);
        int middleValue = PullValueFromNode(parentNode.Middle);
        int rightValue = PullValueFromNode(parentNode.Right);

        int matchCount = 0;

        if (currentNode == parentNode.Left)
        {
            if (selectedCard == middleValue) matchCount++;
            if (selectedCard == rightValue) matchCount++;
        }
        else if (currentNode == parentNode.Middle)
        {
            if (selectedCard == leftValue) matchCount++;
            if (selectedCard == rightValue) matchCount++;
        }
        else if (currentNode == parentNode.Right)
        {
            if (selectedCard == leftValue) matchCount++;
            if (selectedCard == middleValue) matchCount++;
        }

        int stackScore;//Stack puanı hesaplama 2 kart için 3x, 3 kart için 6x 

        if (matchCount == 1)
            stackScore = (selectedCard + 1) * 2;
        else if (matchCount == 2) 
            stackScore = (selectedCard + 1) * 3;
        else 
            stackScore = 0;
        
        //AI'ın rakip kartları analiz etme kısmı
        destroyBonus = 0;
        int enemyLeftValue = PullValueFromPlayerNode(parentNode.Left);
        int enemyMiddleValue = PullValueFromPlayerNode(parentNode.Middle);
        int enemyRightValue = PullValueFromPlayerNode(parentNode.Right);

        if (currentNode == parentNode.Left)
        {
            if (enemyMiddleValue != -1)
                CanDestroyCard(selectedCard, enemyMiddleValue); 
            if (enemyRightValue != -1)
                CanDestroyCard(selectedCard, enemyRightValue);
        }
        else if (currentNode == parentNode.Middle)
        {
            if (enemyLeftValue != -1)
                CanDestroyCard(selectedCard, enemyLeftValue);
            if (enemyRightValue != -1)
                CanDestroyCard(selectedCard, enemyRightValue); 
        }
        else if (currentNode == parentNode.Right)
        {
            if (enemyLeftValue != -1)
                CanDestroyCard(selectedCard, enemyLeftValue); 
            if (enemyMiddleValue != -1)
                CanDestroyCard(selectedCard, enemyMiddleValue); 
        }

        temp = stackScore + destroyBonus;

        if (temp > highestTemp)
        {
            highestTemp = temp;
            bestNode = currentNode;
        }
    }

    private void CanDestroyCard(int selectedCard, int enemyCard)
    {
        if (enemyCard == 0) 
        {
            if(selectedCard == 1 || selectedCard == 3)  
                canDestroyedBy = true;  
        }
        if (enemyCard == 1)
        {
            if(selectedCard == 0)
                destroyBonus += 2;
            if(selectedCard == 2 || selectedCard == 4)
                canDestroyedBy = true;    
        }
        if (enemyCard == 2)
        {
            if(selectedCard == 1)
                destroyBonus += 3;
            if(selectedCard == 3)
                canDestroyedBy = true;   
        }   
        if (enemyCard == 3)
        {
            if(selectedCard == 0 || selectedCard == 2)
                destroyBonus += 4;
            if(selectedCard == 4)
                canDestroyedBy = true;  
        } 
        if (enemyCard == 4)
        {
            if(selectedCard == 1 || selectedCard == 3)
                destroyBonus += 5;
            if(selectedCard == 5)
                canDestroyedBy = true;   
        }
        if (enemyCard == 5)
        {
            if(selectedCard == 4)
                destroyBonus += 6;
        }
    }


    private void PlaceCardAtBestNode()
    {
        Node targetNode;
        if (bestNode != null)
            targetNode = bestNode;
        else 
            targetNode = GetRandomEmptyNode();

        GameObject targetObject = GameObject.Find("Node" + targetNode.Value);
        if (targetObject != null)
        {
            var plotScript = targetObject.GetComponent<Plot>();
            if (plotScript != null)
            {
                plotScript.selectedPrefabNumber = selectedCard;
                plotScript.ChangeSpriteInstant();
                Debug.Log($"Card {selectedCard} placed at Node {targetNode.Value}");
            }
        }
    }

    private Node GetRandomEmptyNode()
    {
        foreach (var node in emptyNodes)
        {
            int nodeValue = PullValueFromNode(node);

            if (nodeValue != -1)
            {
                CanDestroyCard(selectedCard, nodeValue);
            }

            // Eğer patlama riski varsa düğümü atla
            if (canDestroyedBy == true)
            {
                canDestroyedBy = false;
                continue;
            }

            return node;
        }

        // Uygun bir düğüm bulunamazsa null döner
        return null;
    }

    private int PullValueFromNode(Node node)
    {
        if (node == null) return -1;

        GameObject targetObject = GameObject.Find("Node" + node.Value);
        if (targetObject != null)
        {
            var plotScript = targetObject.GetComponent<Plot>();
            return plotScript != null ? plotScript.selectedPrefabNumber : -1;
        }
        return -1;
    }

    private int PullValueFromPlayerNode(Node node)
    {
        if (node == null) return -1;

        GameObject targetObject = GameObject.Find("Node" + (node.Value + 9)); //Player node değerleri için +9 (Node değerleri 10 ile 18)
        if (targetObject != null)
        {
            var plotScript = targetObject.GetComponent<Plot>();
            return plotScript != null ? plotScript.selectedPrefabNumber : -1;
        }
        return -1;
    }

    private void CreateTree()
    {
        Node leftChild1 = new Node(1);
        Node leftChild2 = new Node(2);
        Node leftChild3 = new Node(3);

        Node leftParent = new Node(10) { Left = leftChild1, Middle = leftChild2, Right = leftChild3 };

        Node middleChild1 = new Node(4);
        Node middleChild2 = new Node(5);
        Node middleChild3 = new Node(6);

        Node middleParent = new Node(20) { Left = middleChild1, Middle = middleChild2, Right = middleChild3 };

        Node rightChild1 = new Node(7);
        Node rightChild2 = new Node(8);
        Node rightChild3 = new Node(9);

        Node rightParent = new Node(30) { Left = rightChild1, Middle = rightChild2, Right = rightChild3 };

        Root = new Node(100) { Left = leftParent, Middle = middleParent, Right = rightParent };
    }
}

public class Node
{
    public int Value;
    public Node Left, Middle, Right;
    public Node(int value) { Value = value; }
}
