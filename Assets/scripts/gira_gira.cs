using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gira_gira : MonoBehaviour
{
    void Update()
    {
        
        transform.Rotate(0.0f, -75f * Time.deltaTime, 0.0f);
    }
}
