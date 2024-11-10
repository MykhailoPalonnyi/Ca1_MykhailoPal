using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    public float rotationSpeed = 5f;

    void Update()
    {
        //rotation
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
