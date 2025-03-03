using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Calculator : MonoBehaviour
{
    CardPlots cardPlots;
    public GameObject[] cardSlots;
    public int[] score;
    public Text[] pointTexts;
    public Text playerText;
    public Text AIText;
    public int playerPoint = 0;
    public int AIPoint = 0;
    public Button winnerButton;
    private TextMeshProUGUI winnerText;

    public void Start()
    {
        cardPlots = UnityEngine.Object.FindAnyObjectByType<CardPlots>();
        winnerButton.gameObject.SetActive(false);
        winnerText = winnerButton.GetComponentInChildren<TextMeshProUGUI>();
    }
    public void Update()
    {
        for(int j=0; j<6; j++)
            pointTexts[j].text = score[j].ToString();
        playerPoint = score[0] + score[1] + score[2];
        AIPoint = score[3] + score[4] + score[5];
        playerText.text = playerPoint.ToString();
        AIText.text = AIPoint.ToString();
        PointCalculator();

        Invoke("GameEnding", 2f);
    }

    public void GameEnding()
    {
        if (cardPlots.playerSlotsFull || cardPlots.aISlotsFull)
        {
            winnerButton.gameObject.SetActive(true); 

            if (playerPoint > AIPoint)
            {
                winnerText.text = "Player Won!"; 
            }
            else if (AIPoint > playerPoint)
            {
                winnerText.text = "AI Won!"; 
            }
            else
            {
                winnerText.text = "It's a Tie!"; 
            }

            Time.timeScale = 0;
        }
    }

    public void PointCalculator()
    {
        Plot plot = GetComponent<Plot>();

        for(int i=0; i<16; i++)
        {
            int a = cardSlots[i].GetComponent<Plot>().selectedPrefabNumber;
            int b = cardSlots[i+1].GetComponent<Plot>().selectedPrefabNumber;
            int c = cardSlots[i+2].GetComponent<Plot>().selectedPrefabNumber;
            if(a == b && a == c)
            {
                if(i == 0)
                    score[0] = (a+1)*6;
                else if(i == 3)
                    score[1] = (a+1)*6;
                else if(i == 6)
                    score[2] = (a+1)*6;
                else if(i == 9)
                    score[3] = (a+1)*6;
                else if(i == 12)
                    score[4] = (a+1)*6;
                else if(i == 15)
                    score[5] = (a+1)*6;
            }
            else if(a == c)
            {
                if(i == 0)
                    score[0] = (a+1)*3 + (b+1);
                else if(i == 3)
                    score[1] = (a+1)*3 + (b+1);
                else if(i == 6)
                    score[2] = (a+1)*3 + (b+1);
                else if(i == 9)
                    score[3] = (a+1)*3 + (b+1);
                else if(i == 12)
                    score[4] = (a+1)*3 + (b+1);
                else if(i == 15)
                    score[5] = (a+1)*3 + (b+1);
            }
            else if(b == c)
            {
                if(i == 0)
                    score[0] = (b+1)*3 + (a+1);
                else if(i == 3)
                    score[1] = (b+1)*3 + (a+1);
                else if(i == 6)
                    score[2] = (b+1)*3 + (a+1);
                else if(i == 9)
                    score[3] = (b+1)*3 + (a+1);
                else if(i == 12)
                    score[4] = (b+1)*3 + (a+1);
                else if(i == 15)
                    score[5] = (b+1)*3 + (a+1);
            }
            else if(a == b)
            {
                if(i == 0)
                    score[0] = (a+1)*3 + (c+1);
                else if(i == 3)
                    score[1] = (a+1)*3 + (c+1);
                else if(i == 6)
                    score[2] = (a+1)*3 + (c+1);
                else if(i == 9)
                    score[3] = (a+1)*3 + (c+1);
                else if(i == 12)
                    score[4] = (a+1)*3 + (c+1);
                else if(i == 15)
                    score[5] = (a+1)*3 + (c+1);
            }
            else
            {
                if(i == 0)
                    score[0] = a+b+c+3;
                else if(i == 3)
                    score[1] = a+b+c+3;
                else if(i == 6)
                    score[2] = a+b+c+3;
                else if(i == 9)
                    score[3] = a+b+c+3;
                else if(i == 12)
                    score[4] = a+b+c+3;
                else if(i == 15)
                    score[5] = a+b+c+3;
            }
        }      
    }
}

