using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animal : MonoBehaviour
{
    public int AnimalIndex = 0;
    public int max_hp;
    public int attackStrength = 0;
    public int nowHp;
    public float interval = 0.2f;
    public int freshTime = 50*60;
    public int invicibleFrame = 20;
    public bool isParent = false;
    SpriteRenderer spriteRenderer;
    bool isInvicible = false;
    public Image hpPanel;
    Image hpImag;
    Collider2D selfCol;
    Transform player;
    PlayerController playerController;

    IEnumerator Fresh()
    {
        while(--freshTime>0)
        {
            yield return null;
        }
        if(!isParent)
        Destroy(gameObject);
        else
        Destroy(transform.parent.gameObject);
    }

    IEnumerator hurt()
    {
        int t = 0;
        if(player.position.x - transform.position.x>0)
            transform.GetComponent<Rigidbody2D>().velocity = (Vector2.up*1.5f + Vector2.left) * 4;
        else
            transform.GetComponent<Rigidbody2D>().velocity = (Vector2.up * 1.5f + Vector2.right) * 4;
        isInvicible = true;
        bool isMirr = false;
        while (t++<invicibleFrame)
        {
            if(t<13&&t%2==0)
            {
                if (isMirr)
                    spriteRenderer.color = new Color(1, 1, 1, 0.5f);
                else
                    spriteRenderer.color = new Color(1, 1, 1, 1);
                isMirr = !isMirr;

            }
            yield return null;
        }
        spriteRenderer.color = Color.white;

        isInvicible = false;
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(hpPanel==null)
        {
            hpPanel = transform.GetChild(1).GetComponent<Image>();
        }
        nowHp = max_hp;
        hpImag = hpPanel.transform.GetChild(0).GetComponent<Image>();
        selfCol = GetComponent<Collider2D>();
        player = GameObject.Find("Player").transform;
        StartCoroutine(Fresh());
        playerController = player.GetComponent<PlayerController>();
    }
    int t = 60;
    IEnumerator HideHpPannel()
    {
        if (t == 60) t = 0;
        for (; t < 60; t++)
        {
            yield return null;
        }
        hpPanel.gameObject.SetActive(false);
    }

    IEnumerator RestoreCollision(Collider2D collider1,Collider2D collider2)
    {
        yield return new WaitForSeconds(interval);
        Physics2D.IgnoreCollision(collider1, collider2,false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.ToString()=="Player"&&!BasicData.Instance.isInvicible)
        {
            Physics2D.IgnoreCollision(selfCol,collision.collider,true);
            BasicData.Instance.playerHp -= attackStrength;
            StartCoroutine(RestoreCollision(selfCol, collision.collider));
            BasicData.Instance.lAttack = AnimalIndex;
            if (attackStrength>=5)
            playerController.hurt();
        }

        if (!isInvicible)
        {

            if (collision.gameObject.tag.ToString() == "Sword")
            {
                StartCoroutine(hurt());
                freshTime += 50;
                if (BasicData.Instance.playerRepletion < 2 || BasicData.Instance.playerEnergy < 2.5f)
                {
                    nowHp -= 4;
                }
                else
                {
                    nowHp -= 15;
                    BasicData.Instance.playerRepletion -= 2;
                    BasicData.Instance.playerEnergy -= 2.5f;
                }
                hpPanel.gameObject.SetActive(true);
                if (t == 60)
                {
                    StartCoroutine(HideHpPannel());
                }
                t = 0;
            }
            else if (collision.gameObject.tag.ToString() == "Axe")
            {
                StartCoroutine(hurt());
                freshTime += 50;
                if (BasicData.Instance.playerRepletion < 2 || BasicData.Instance.playerEnergy < 2.5f)
                {
                    nowHp -= 2;
                }
                else
                {
                    nowHp -= 8;
                    BasicData.Instance.playerRepletion -= 2;
                    BasicData.Instance.playerEnergy -= 2.5f;
                }
                hpPanel.gameObject.SetActive(true);
                if (t == 60)
                {
                    StartCoroutine("HideHpPannel");
                }
                t = 0;
                nowHp -= 8;
            }
            else if (collision.gameObject.tag.ToString() == "tweezer")
            {
                StartCoroutine(hurt());
                freshTime += 50;
                if (BasicData.Instance.playerRepletion < 2 || BasicData.Instance.playerEnergy < 2.5f)
                {
                    nowHp -= 5;
                }
                else
                {
                    nowHp -= 2;
                    BasicData.Instance.playerRepletion -= 2;
                    BasicData.Instance.playerEnergy -= 2.5f;
                }
                hpPanel.gameObject.SetActive(true);
                if (t == 60)
                {
                    StartCoroutine("HideHpPannel");
                }
                t = 0;
                nowHp -= 5;
            }
            if (nowHp <= 0&&isParent)
                BasicData.Instance.ecologicalValue -= 1.5f;
            else if(nowHp<=0&&!isParent)
                BasicData.Instance.ecologicalValue += 0.2f;

        }
        hpImag.fillAmount = nowHp * 1.0f / max_hp;
    }

    
}
