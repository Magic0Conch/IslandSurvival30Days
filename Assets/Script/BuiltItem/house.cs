using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class house : MonoBehaviour
{
    public Transform target;
    public Transform c0;
    public Transform c1;
    public Transform c2;
    public bool isIndoor = false;
    bool canEnter = true;
    float coolTime = 2f;
    IEnumerator Cool()
    {
        canEnter = false;
        yield return new WaitForSeconds(coolTime);
        canEnter = true;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag.ToString() == "Player")
        {
            c2.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.transform.gameObject.tag.ToString() == "Player")
        {
            c2.gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        canEnter = false;
        StartCoroutine(Cool());
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Transform player;
        if (!canEnter) return;
        if(collision.gameObject.name=="Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Cool());
                player = GameObject.Find("Player").transform;
                if(!isIndoor)
                {
                    GameObject[] emes = GameObject.FindGameObjectsWithTag("Enemy");
                    foreach (GameObject g in emes)
                        Destroy(g);
                    emes = GameObject.FindGameObjectsWithTag("Dudu");
                    foreach (GameObject g in emes)
                        Destroy(g.transform.parent.gameObject);
                    c1.gameObject.SetActive(true);
                    c0.gameObject.SetActive(false);
                    player.gameObject.layer = 1;
                    player.position = new Vector3(target.transform.position.x, target.transform.position.y,player.transform.position.z) + Vector3.up * 4;
                    SpriteRenderer sr = null;
                    for (int i = 0;i<player.transform.childCount;i++)
                    {
                        sr = null;
                        sr = player.GetChild(i).GetComponent<SpriteRenderer>();
                        if(sr!=null)
                            sr.sortingLayerName = "house";
                    }
                    BasicData.Instance.isIn = true;
                }
                else if(isIndoor)
                {
                    StartCoroutine(Cool());                    
                    player.position = new Vector3(target.transform.position.x, target.transform.position.y, player.transform.position.z) + Vector3.up * 4;
                    c1.gameObject.SetActive(false);
                    c0.gameObject.SetActive(true);
                    player.gameObject.layer = 0;
                    SpriteRenderer sr = null;
                    for (int i = 0; i < player.transform.childCount; i++)
                    {
                        sr = null;
                        sr = player.GetChild(i).GetComponent<SpriteRenderer>();
                        if (sr != null)
                            sr.sortingLayerName = "Player";
                    }
                    BasicData.Instance.isIn = false;
                }
            }
        }
    }
}
