using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public Transform[] points;
    public float speed = 2f;      
    private int currentPoint = 0;
    void Start()
    {
        
    }

    
    void Update()
    {
        Transform targetPoint = points[currentPoint];
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
          
            currentPoint = (currentPoint + 1) % points.Length;
        }
    }
}
