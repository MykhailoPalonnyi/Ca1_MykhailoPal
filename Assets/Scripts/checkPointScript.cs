using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPointScript : MonoBehaviour
{
    GameController gameController;
    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameController.UpdateCheckPoint(transform.position);
        }
        
    }
}
