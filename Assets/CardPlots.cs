using System;
using System.Collections;
using System.IO.Compression;
using JetBrains.Annotations;
using UnityEngine;

public class CardPlots : MonoBehaviour
{
    public GameObject[] cardSlots;
    public GameObject[] aIslots;
    TurnBasedSystem turnBasedSystem;
    int counter = 0;
    int counterAI = 0;
    GameObject enemy1 = null;
    GameObject enemy2 = null;
    GameObject enemy3 = null;
    char option = 'a';
    char type = 'z';
    public Boolean aISlotsFull = false;
    public Boolean playerSlotsFull = false;

    public void Start()
    {
        turnBasedSystem = UnityEngine.Object.FindFirstObjectByType<TurnBasedSystem>();
    }

    public void ResetSystem()
    {
        enemy1 = null;
        enemy2 = null;
        enemy3 = null;
        option = 'a';
        type = 'z';
    }
    public void Update()
    {
        CardClash();

        CheckSlots();
    }

    public void CheckSlots()
    {
        aISlotsFull = true;
        playerSlotsFull = true;

        foreach (var cardSlot in cardSlots)
        {
            Plot plotScript = cardSlot.GetComponent<Plot>();
            if (plotScript.isEmpty)
            {
                playerSlotsFull = false; 
            }
        }

        foreach (var aIslot in aIslots)
        {
            Plot plotScript = aIslot.GetComponent<Plot>();
            if (plotScript.isEmpty)
            {
                aISlotsFull = false; 
            }
        }
    }
    public void CardClash()
    {
        if(turnBasedSystem.systemOn == true)
        {
            if(turnBasedSystem.playerTurn == true)
            {
                foreach(var cardSlot in cardSlots)
                {
                    ResetSystem();
                    counter ++;
                    Plot plot = cardSlot.GetComponent<Plot>();
                    int n = plot.selectedPrefabNumber;

                    if(cardSlot == cardSlots[0] || cardSlot == cardSlots[1] || cardSlot == cardSlots[2])
                    {
                        option = 'x';
                    }
                    if(cardSlot == cardSlots[3] || cardSlot == cardSlots[4] || cardSlot == cardSlots[5])
                    {
                        option = 'y';
                    }
                    if(cardSlot == cardSlots[6] || cardSlot == cardSlots[7] || cardSlot == cardSlots[8])
                    {
                        option = 'z';
                    }

                    if(n == 0)
                    {
                        type = 'A';
                    }
                    else if(n == 1)
                    {
                        type = 'T';
                    }
                    else if(n == 2)
                    {
                        type = 'W';
                    }
                    else if(n == 3)
                    {
                        type = 'F';
                    }
                    else if(n == 4)
                    {
                        type = 'N';
                    }
                    else if(n == 5)
                    {
                        type = 'E';
                    }

                    switch(option)
                    {
                        case 'x': 
                            enemy1 = aIslots[0];
                            enemy2 = aIslots[1];
                            enemy3 = aIslots[2];
                            break;
                        case 'y':
                            enemy1 = aIslots[3];
                            enemy2 = aIslots[4];
                            enemy3 = aIslots[5];
                            break;
                        case 'z':
                            enemy1 = aIslots[6];
                            enemy2 = aIslots[7];
                            enemy3 = aIslots[8];
                            break;
                        default:
                            break;
                    }

                    switch(type)
                    {
                        case 'A':
                            if(enemy1.GetComponent<Plot>().selectedPrefabNumber == 1 || enemy1.GetComponent<Plot>().selectedPrefabNumber == 3)
                            {
                                enemy1.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            if(enemy2.GetComponent<Plot>().selectedPrefabNumber == 1 || enemy2.GetComponent<Plot>().selectedPrefabNumber == 3)
                            {
                                enemy2.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            if(enemy3.GetComponent<Plot>().selectedPrefabNumber == 1 || enemy3.GetComponent<Plot>().selectedPrefabNumber == 3)
                            {
                                enemy3.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            break;
                        case 'E':
                            if(enemy1.GetComponent<Plot>().selectedPrefabNumber == 4)
                            {
                                cardSlot.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            if(enemy2.GetComponent<Plot>().selectedPrefabNumber == 4)
                            {
                                cardSlot.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            if(enemy3.GetComponent<Plot>().selectedPrefabNumber == 4)
                            {
                                cardSlot.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            break;
                        case 'F':
                            if(enemy1.GetComponent<Plot>().selectedPrefabNumber == 4)
                            {
                                enemy1.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy2.GetComponent<Plot>().selectedPrefabNumber == 4)
                            {
                                enemy2.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy3.GetComponent<Plot>().selectedPrefabNumber == 4)
                            {
                                enemy3.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy1.GetComponent<Plot>().selectedPrefabNumber == 2 || enemy1.GetComponent<Plot>().selectedPrefabNumber == 0)
                            {
                                cardSlot.GetComponent<SpriteRenderer>().sprite = null;             
                            }
                            if(enemy2.GetComponent<Plot>().selectedPrefabNumber == 2 || enemy2.GetComponent<Plot>().selectedPrefabNumber == 0)
                            {
                                cardSlot.GetComponent<SpriteRenderer>().sprite = null;             
                            }
                            if(enemy3.GetComponent<Plot>().selectedPrefabNumber == 2 || enemy3.GetComponent<Plot>().selectedPrefabNumber == 0)
                            {
                                cardSlot.GetComponent<SpriteRenderer>().sprite = null;             
                            }
                            break;
                        case 'N':
                            if(enemy1.GetComponent<Plot>().selectedPrefabNumber == 5)
                            {
                                enemy1.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy2.GetComponent<Plot>().selectedPrefabNumber == 5)
                            {
                                enemy2.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy3.GetComponent<Plot>().selectedPrefabNumber == 5)
                            {
                                enemy3.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy1.GetComponent<Plot>().selectedPrefabNumber == 1 || enemy1.GetComponent<Plot>().selectedPrefabNumber == 3)
                            {
                                cardSlot.GetComponent<SpriteRenderer>().sprite = null;             
                            }
                            if(enemy2.GetComponent<Plot>().selectedPrefabNumber == 1 || enemy2.GetComponent<Plot>().selectedPrefabNumber == 3)
                            {
                                cardSlot.GetComponent<SpriteRenderer>().sprite = null;             
                            }
                            if(enemy3.GetComponent<Plot>().selectedPrefabNumber == 1 || enemy3.GetComponent<Plot>().selectedPrefabNumber == 3)
                            {
                                cardSlot.GetComponent<SpriteRenderer>().sprite = null;             
                            }
                            break;
                        case 'T':
                            if(enemy1.GetComponent<Plot>().selectedPrefabNumber == 2 || enemy1.GetComponent<Plot>().selectedPrefabNumber == 4)
                            {
                                enemy1.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy2.GetComponent<Plot>().selectedPrefabNumber == 2 || enemy2.GetComponent<Plot>().selectedPrefabNumber == 4)
                            {
                                enemy2.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy3.GetComponent<Plot>().selectedPrefabNumber == 2 || enemy3.GetComponent<Plot>().selectedPrefabNumber == 4)
                            {
                                enemy3.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy1.GetComponent<Plot>().selectedPrefabNumber == 0)
                            {
                                cardSlot.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            if(enemy2.GetComponent<Plot>().selectedPrefabNumber == 0)
                            {
                                cardSlot.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            if(enemy3.GetComponent<Plot>().selectedPrefabNumber == 0)
                            {
                                cardSlot.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            break;
                        case 'W':
                            if(enemy1.GetComponent<Plot>().selectedPrefabNumber == 3)
                            {
                                enemy1.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy2.GetComponent<Plot>().selectedPrefabNumber == 3)
                            {
                                enemy2.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy3.GetComponent<Plot>().selectedPrefabNumber == 3)
                            {
                                enemy3.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy1.GetComponent<Plot>().selectedPrefabNumber == 1)
                            {
                                cardSlot.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            if(enemy2.GetComponent<Plot>().selectedPrefabNumber == 1)
                            {
                                cardSlot.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            if(enemy3.GetComponent<Plot>().selectedPrefabNumber == 1)
                            {
                                cardSlot.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            if(turnBasedSystem.playerTurn == false)
            {
                foreach(var aIslot in aIslots)
                {
                    ResetSystem();
                    counterAI ++;
                    Plot plot = aIslot.GetComponent<Plot>();

                    int n = plot.selectedPrefabNumber;

                    if(aIslot == aIslots[0] || aIslot == aIslots[1] || aIslot == aIslots[2])
                    {
                        option = 'x';
                    }
                    if(aIslot == aIslots[3] || aIslot == aIslots[4] || aIslot == aIslots[5])
                    {
                        option = 'y';
                    }
                    if(aIslot == aIslots[6] || aIslot == aIslots[7] || aIslot == aIslots[8])
                    {
                        option = 'z';
                    }

                    if(n == 0)
                    {
                        type = 'A';
                    }
                    else if(n == 1)
                    {
                        type = 'T';
                    }
                    else if(n == 2)
                    {
                        type = 'W';
                    }
                    else if(n == 3)
                    {
                        type = 'F';
                    }
                    else if(n == 4)
                    {
                        type = 'N';
                    }
                    else if(n == 5)
                    {
                        type = 'E';
                    }

                    switch(option)
                    {
                        case 'x': 
                            enemy1 = cardSlots[0];
                            enemy2 = cardSlots[1];
                            enemy3 = cardSlots[2];
                            break;
                        case 'y':
                            enemy1 = cardSlots[3];
                            enemy2 = cardSlots[4];
                            enemy3 = cardSlots[5];
                            break;
                        case 'z':
                            enemy1 = cardSlots[6];
                            enemy2 = cardSlots[7];
                            enemy3 = cardSlots[8];
                            break;
                        default:
                            break;
                    }

                    switch(type)
                    {
                        case 'A':
                            if(enemy1.GetComponent<Plot>().selectedPrefabNumber == 1 || enemy1.GetComponent<Plot>().selectedPrefabNumber == 3)
                            {
                                enemy1.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            if(enemy2.GetComponent<Plot>().selectedPrefabNumber == 1 || enemy2.GetComponent<Plot>().selectedPrefabNumber == 3)
                            {
                                enemy2.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            if(enemy3.GetComponent<Plot>().selectedPrefabNumber == 1 || enemy3.GetComponent<Plot>().selectedPrefabNumber == 3)
                            {
                                enemy3.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            break;
                        case 'E':
                            if(enemy1.GetComponent<Plot>().selectedPrefabNumber == 4)
                            {
                                aIslot.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            if(enemy2.GetComponent<Plot>().selectedPrefabNumber == 4)
                            {
                                aIslot.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            if(enemy3.GetComponent<Plot>().selectedPrefabNumber == 4)
                            {
                                aIslot.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            break;
                        case 'F':
                            if(enemy1.GetComponent<Plot>().selectedPrefabNumber == 4)
                            {
                                enemy1.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy2.GetComponent<Plot>().selectedPrefabNumber == 4)
                            {
                                enemy2.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy3.GetComponent<Plot>().selectedPrefabNumber == 4)
                            {
                                enemy3.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy1.GetComponent<Plot>().selectedPrefabNumber == 2 || enemy1.GetComponent<Plot>().selectedPrefabNumber == 0)
                            {
                                aIslot.GetComponent<SpriteRenderer>().sprite = null;             
                            }
                            if(enemy2.GetComponent<Plot>().selectedPrefabNumber == 2 || enemy2.GetComponent<Plot>().selectedPrefabNumber == 0)
                            {
                                aIslot.GetComponent<SpriteRenderer>().sprite = null;             
                            }
                            if(enemy3.GetComponent<Plot>().selectedPrefabNumber == 2 || enemy3.GetComponent<Plot>().selectedPrefabNumber == 0)
                            {
                                aIslot.GetComponent<SpriteRenderer>().sprite = null;             
                            }
                            break;
                        case 'N':
                            if(enemy1.GetComponent<Plot>().selectedPrefabNumber == 5)
                            {
                                enemy1.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy2.GetComponent<Plot>().selectedPrefabNumber == 5)
                            {
                                enemy2.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy3.GetComponent<Plot>().selectedPrefabNumber == 5)
                            {
                                enemy3.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy1.GetComponent<Plot>().selectedPrefabNumber == 1 || enemy1.GetComponent<Plot>().selectedPrefabNumber == 3)
                            {
                                aIslot.GetComponent<SpriteRenderer>().sprite = null;             
                            }
                            if(enemy2.GetComponent<Plot>().selectedPrefabNumber == 1 || enemy2.GetComponent<Plot>().selectedPrefabNumber == 3)
                            {
                                aIslot.GetComponent<SpriteRenderer>().sprite = null;             
                            }
                            if(enemy3.GetComponent<Plot>().selectedPrefabNumber == 1 || enemy3.GetComponent<Plot>().selectedPrefabNumber == 3)
                            {
                                aIslot.GetComponent<SpriteRenderer>().sprite = null;             
                            }
                            break;
                        case 'T':
                            if(enemy1.GetComponent<Plot>().selectedPrefabNumber == 2 || enemy1.GetComponent<Plot>().selectedPrefabNumber == 4)
                            {
                                enemy1.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy2.GetComponent<Plot>().selectedPrefabNumber == 2 || enemy2.GetComponent<Plot>().selectedPrefabNumber == 4)
                            {
                                enemy2.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy3.GetComponent<Plot>().selectedPrefabNumber == 2 || enemy3.GetComponent<Plot>().selectedPrefabNumber == 4)
                            {
                                enemy3.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy1.GetComponent<Plot>().selectedPrefabNumber == 0)
                            {
                                aIslot.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            if(enemy2.GetComponent<Plot>().selectedPrefabNumber == 0)
                            {
                                aIslot.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            if(enemy3.GetComponent<Plot>().selectedPrefabNumber == 0)
                            {
                                aIslot.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            break;
                        case 'W':
                            if(enemy1.GetComponent<Plot>().selectedPrefabNumber == 3)
                            {
                                enemy1.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy2.GetComponent<Plot>().selectedPrefabNumber == 3)
                            {
                                enemy2.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy3.GetComponent<Plot>().selectedPrefabNumber == 3)
                            {
                                enemy3.GetComponent<SpriteRenderer>().sprite = null;              
                            }
                            if(enemy1.GetComponent<Plot>().selectedPrefabNumber == 1)
                            {
                                aIslot.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            if(enemy2.GetComponent<Plot>().selectedPrefabNumber == 1)
                            {
                                aIslot.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            if(enemy3.GetComponent<Plot>().selectedPrefabNumber == 1)
                            {
                                aIslot.GetComponent<SpriteRenderer>().sprite = null;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            if(counter == 9)
            {
                turnBasedSystem.systemOn = false;
                counter = 0;
            }
            if(counterAI == 9)
            {
                turnBasedSystem.systemOn = false;
                counterAI = 0;
            }
        }
    }
}