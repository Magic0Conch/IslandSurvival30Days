using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nativer : MonoBehaviour
{

    float coolTime = 0.5f;
    bool canEnter = true;
    public Transform hint;
    public Transform player;
    //Animator animator;
    public GameObject tradePanel;
    SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(lookat());
        //animator = GetComponent<Animator>();
    }
    IEnumerator Cool()
    {
        canEnter = false;
        yield return new WaitForSeconds(coolTime);
        canEnter = true;
    }
    IEnumerator lookat()
    {
        while (true)
        {
            sr.flipX = transform.position.x < player.transform.position.x;
            yield return null;
        }
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
            hint.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            hint.gameObject.SetActive(false);
            tradePanel.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!canEnter) return;
        if (collision.gameObject.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Cool());
                tradePanel.SetActive(true);
            }
        }
    }
}
