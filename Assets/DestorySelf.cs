using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestorySelf : MonoBehaviour
{
    public float existTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, existTime);    
    }

    // Update is called once per frame
    
}
