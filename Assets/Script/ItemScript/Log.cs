using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Log : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.name.ToString()=="Player")
        {
            GameObject.Find("UI").GetComponent<BagController>().GetItem(0);
            Destroy(gameObject);
        }
    }
}
