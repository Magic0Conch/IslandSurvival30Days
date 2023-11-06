using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BossColider : MonoBehaviour
{
    public int attackStrength = 0;    
    public float interval = 0.2f;
    public int invicibleFrame = 40;
    SpriteRenderer spriteRenderer;
    bool isInvicible = false;
    public Image hpPanel;
    Image hpImag;
    Collider2D selfCol;
    Transform player;
    PlayerController playerController;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        selfCol = GetComponent<Collider2D>();
        player = GameObject.Find("Player").transform;
        playerController = player.GetComponent<PlayerController>();
    }
    int t = 60;

    IEnumerator RestoreCollision(Collider2D collider1, Collider2D collider2)
    {
        yield return new WaitForSeconds(interval);
        Physics2D.IgnoreCollision(collider1, collider2, false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.ToString() == "Player"&&!BasicData.Instance.isInvicible)
        {
            Physics2D.IgnoreCollision(selfCol, collision.collider, true);
            BasicData.Instance.playerHp -= attackStrength;
            StartCoroutine(RestoreCollision(selfCol, collision.collider));
            playerController.hurt();
        }
        print(collision.gameObject.tag.ToString());
        if (collision.gameObject.tag.ToString() == "Sword")
        {
            BasicData.Instance.bossHp -= 15;
            print(BasicData.Instance.bossHp);
                
        }
        else if (collision.gameObject.tag.ToString() == "Axe")
        {
            BasicData.Instance.bossHp -= 8;
        }
        else if (collision.gameObject.tag.ToString() == "tweezer")
        {        
            BasicData.Instance.bossHp -= 5;
        }

        //print(BasicData.Instance.bossHp);
        hpPanel.fillAmount = BasicData.Instance.bossHp * 1.0f / BasicData.Instance.bossHpMax;
        if(BasicData.Instance.bossHp<=0)
        {
            if (BasicData.Instance.ecologicalValue > 30&& !BasicData.Instance.jiu)
            {
                BasicData.Instance.dieIndex = 3;
            }
            else if(BasicData.Instance.ecologicalValue > 30 && BasicData.Instance.jiu)
            {

                BasicData.Instance.dieIndex = 4;
            }
            else
                BasicData.Instance.dieIndex = 5;                
            SceneManager.LoadScene(3);
        }
    }
}
