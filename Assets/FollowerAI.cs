using System;
using UnityEngine;

public class FollowerAI : MonoBehaviour
{
    public Transform target; 
    public float speed = 5f; 

    void Start()
    {
        target = GameObject.FindWithTag("AI").transform; 
    }

    void Update()
    {
        Vector3 targetPosition = target.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

    }
}
