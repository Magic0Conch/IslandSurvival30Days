using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public void Open(GameObject go)
    {
        go.SetActive(true);
    }

    public void Close(GameObject go)
    {
        go.SetActive(false);
    }
    public void Destory(GameObject go)
    {
        DestroyImmediate(go);
    }
}
