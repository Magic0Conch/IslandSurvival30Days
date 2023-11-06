using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public GameObject getItem;
    public int ItemId;
    public Transform FollowPlayer;
    virtual protected void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name.ToString() == "Player")
        {
            if(GameObject.Find("UI").GetComponent<BagController>().GetItem(ItemId))
            {
                Destroy(gameObject);
                if (FollowPlayer == null) FollowPlayer = GameObject.Find("FollowPlayer").transform;
                print(FollowPlayer);
                GameObject go = Instantiate(getItem, FollowPlayer, false);
                //go.transform.rotation = collision.transform.rotation;
                //go.transform.SetParent(null);
            }
        }
    }
}
