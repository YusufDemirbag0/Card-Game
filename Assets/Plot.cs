using System;
using UnityEngine;

public class Plot : MonoBehaviour
{
    private CardPulling cardPulling;
    private AudioSource audioSource;
    AI aı;
    public Sprite[] prefabs;
    public int selectedPrefabNumber = -1;
    private Boolean isInThePlot = false;
    public Boolean isEmpty = true;
    TurnBasedSystem turnBasedSystem;
    public AudioClip cardPlaceSound;
    

    void Start()
    {
        cardPulling = UnityEngine.Object.FindAnyObjectByType<CardPulling>();
        turnBasedSystem = UnityEngine.Object.FindAnyObjectByType<TurnBasedSystem>();
        aı = UnityEngine.Object.FindAnyObjectByType<AI>();
        audioSource = GetComponent<AudioSource>(); 
    }

    void Update()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (isInThePlot == true && Input.GetMouseButtonDown(0) && isEmpty == true && cardPulling.Card != null)
        {
            Debug.Log("Card Placed");
            selectedPrefabNumber = cardPulling.x;
            spriteRenderer.sprite = prefabs[selectedPrefabNumber];
            isEmpty = false;
            cardPulling.isDone = true;
            cardPulling.isCardPulled = false;
            cardPulling.x = -1;
            turnBasedSystem.Turning();
            audioSource.PlayOneShot(cardPlaceSound);
        }

        if (spriteRenderer.sprite == null && !isEmpty)
        {
            isEmpty = true;
            selectedPrefabNumber = -1; 
        }

        if (selectedPrefabNumber != -1 && spriteRenderer.sprite != prefabs[selectedPrefabNumber])
        {
            spriteRenderer.sprite = prefabs[selectedPrefabNumber];
        }
    }


    public void ChangeSpriteInstant()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (selectedPrefabNumber != -1 && selectedPrefabNumber < prefabs.Length)
        {
            spriteRenderer.sprite = prefabs[selectedPrefabNumber];
            isEmpty = false;
            Debug.Log($"Sprite set to: {selectedPrefabNumber}");
            audioSource.PlayOneShot(cardPlaceSound);
        }
        else
        {
            Debug.LogError("Invalid prefab index for ChangeSpriteInstant.");
        }
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Cursor"))
            isInThePlot = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Cursor"))
            isInThePlot = false;
    }
}
