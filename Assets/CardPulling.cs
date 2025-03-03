using System;
using UnityEngine;

public class CardPulling : MonoBehaviour
{
    AI aı;
    TurnBasedSystem turnBasedSystem;
    public GameObject[] PCard;      
    public GameObject[] PAICard;    
    public GameObject Card;        
    public GameObject AICard;    
    public AudioClip cardPullingSound;   
    private AudioSource audioSource; 

    public bool isCardPulled = false;
    public bool isAICardPulled = false;
    private bool isInTrigger = false;
    public bool isDone = false;     
    public int x = -1;             
    public bool Changer = false;     

    private int aiPulledCardIndex = -1; 

    void Start()
    {
        turnBasedSystem = UnityEngine.Object.FindAnyObjectByType<TurnBasedSystem>();
        aı = UnityEngine.GameObject.FindAnyObjectByType<AI>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isInTrigger && Input.GetMouseButtonDown(0) && !isCardPulled && turnBasedSystem.playerTurn)
        {
            Debug.Log("Player clicked to draw a card");
            x = UnityEngine.Random.Range(0, 6);
            Card = Instantiate(PCard[x], new Vector2(68f, 0f), Quaternion.identity);
            isCardPulled = true;
            isDone = false;
            audioSource.PlayOneShot(cardPullingSound);
        }

        //AI kart çekme işlemi
        if (aı.hasItReached1 && !isAICardPulled && !turnBasedSystem.playerTurn)
        {
            Debug.Log("AI pulling a card");
            x = UnityEngine.Random.Range(0, 6);
            aiPulledCardIndex = x;
            AICard = Instantiate(PAICard[x], new Vector2(68f, 0f), Quaternion.identity);
            isAICardPulled = true;
            isDone = false;
            aı.playIt = true;
            audioSource.PlayOneShot(cardPullingSound);

            //AI kartını yok et
            Invoke("DestroyAICard", 2f); 
        }

        //Kart resetleme işlemi
        if (isDone)
        {
            ResetCardState();
        }
    }

    private void DestroyAICard()
    {
        if (AICard != null)
        {
            Destroy(AICard);
            Debug.Log("AI card destroyed after placement.");
            isAICardPulled = false; 
        }
    }


    private void ResetCardState()
    {
        if (Card != null)
        {
            Destroy(Card);
        }

        isCardPulled = false;
        isDone = false;
        Changer = true;
        Debug.Log("Player's card states reset");
    }

    public int GetAIPulledCardIndexs()
    {
        return aiPulledCardIndex; 
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Cursor"))
        {
            isInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Cursor"))
        {
            isInTrigger = false;
        }
    }
}
