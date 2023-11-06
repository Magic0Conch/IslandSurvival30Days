using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.childCount >= 5) return;
        Animal animal = collision.gameObject.GetComponent<Animal>();
        if(animal!=null&&animal.gameObject.layer==13&&(animal.nowHp/animal.max_hp)<0.5f)
        {
            if(collision.gameObject.name.ToString()[0]=='S')
            {
                Destroy(collision.gameObject);
                BasicData.Instance.ecologicalValue += 1f;
                Instantiate(GameObject.Find("Creature").transform.GetChild(5).gameObject, transform,false).SetActive(true);
            }
            else if(collision.gameObject.name.ToString()[0] == 'Z')
            {
                Destroy(collision.gameObject);
                BasicData.Instance.ecologicalValue += 1.5f;
                Instantiate(GameObject.Find("Creature").transform.GetChild(4).gameObject, transform,false).SetActive(true);
            }
            BasicData.Instance.ecologicalValue += 2;
        }
    }
}
