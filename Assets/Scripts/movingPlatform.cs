using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    //array of points that define the path for movement
    public Transform[] points;
    public float speed = 2f;
    private int currentPoint = 0;
   

    void Update()
    {
        //get the target point in the path based on the current index
        Transform targetPoint = points[currentPoint];
        //move the object toward the target point at a specified speed
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        //checks if the object is close enough to the target point (within 0.1 units)
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // Move to the next point in the path, using "%" to ensure loop
            currentPoint = (currentPoint + 1) % points.Length;
        }
        
    }
    
}

   
   

   

