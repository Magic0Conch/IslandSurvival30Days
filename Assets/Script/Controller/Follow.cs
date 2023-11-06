using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform followed;
    // Update is called once per frame
    void Update()
    {
        transform.position = followed.position;
    }
}
