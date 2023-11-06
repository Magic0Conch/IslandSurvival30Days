using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class lightAnim : MonoBehaviour
{
    Light2D gloabalLight;
    void lightTo1()
    {
        gloabalLight = GetComponent<Light2D>();
        gloabalLight.intensity = 1;
        DestroyImmediate(this);
    }
}
