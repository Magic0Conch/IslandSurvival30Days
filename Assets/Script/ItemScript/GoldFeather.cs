using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFeather : ItemScript
{
    public int id;
    private void Start()
    {
        ItemId = 15;

    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name.ToString() == "Player")
        {
            if (GameObject.Find("UI").GetComponent<BagController>().GetItem(ItemId,id))
            {
                Destroy(gameObject);
                GameObject go = Instantiate(getItem, FollowPlayer, false);
            }
        }
    }

}
