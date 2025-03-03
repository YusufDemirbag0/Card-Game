using System;
using UnityEngine;

public class TurnBasedSystem : MonoBehaviour
{
    Plot plot;
    public Boolean playerTurn = true;
    public Boolean systemOn = false;
    public GameObject playerCircle;
    public GameObject aICircle;

    void Start()
    {
        plot = UnityEngine.Object.FindAnyObjectByType<Plot>();
        playerCircle.GetComponent<SpriteRenderer>().color = Color.green;
        aICircle.GetComponent<SpriteRenderer>().color = Color.red;
    }

    void Update()
    {
              
    }

    public void Turning()
    {
        playerTurn = !playerTurn;
        systemOn = true;
        if(aICircle.GetComponent<SpriteRenderer>().color == Color.green)
            aICircle.GetComponent<SpriteRenderer>().color = Color.red;
        else if(aICircle.GetComponent<SpriteRenderer>().color == Color.red)
            aICircle.GetComponent<SpriteRenderer>().color = Color.green;
        if(playerCircle.GetComponent<SpriteRenderer>().color == Color.green)
            playerCircle.GetComponent<SpriteRenderer>().color = Color.red;
        else if(playerCircle.GetComponent<SpriteRenderer>().color == Color.red)
            playerCircle.GetComponent<SpriteRenderer>().color = Color.green;
    }

    

}
