using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    GameObject attackColider;
    private void Start()
    {
        attackColider = transform.GetChild(0).gameObject;
    }

    public void emergeColider()
    {
        attackColider.SetActive(true);
    }
    public void DestoryColider()
    {
        attackColider.SetActive(false);
        gameObject.SetActive(false);
    }

}
