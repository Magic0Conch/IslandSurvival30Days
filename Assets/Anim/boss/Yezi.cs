using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yezi : MonoBehaviour
{
    public float upForce = 1f;
    PlayerController playerController;
    Transform player;
    Rigidbody2D rigidbody;
    GameObject baozha;
    private void Start()
    {
        print("kaishi");
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(Random.Range(-5, 5), upForce);

        player = GameObject.Find("Player").transform;
        playerController = player.GetComponent<PlayerController>();
        baozha = transform.GetChild(0).gameObject;
        Invoke("Zha", 4f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.ToString() == "Player")
        {
            BasicData.Instance.playerHp -= 5;
            playerController.hurt();
        }
    }

    void Zha()
    {
        Destroy(gameObject,0.5f);
        GetComponent<Collider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = false;
        baozha.SetActive(true);
    }
}
