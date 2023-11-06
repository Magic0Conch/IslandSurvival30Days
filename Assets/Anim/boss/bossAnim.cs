using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAnim : MonoBehaviour
{
    public GameObject zhu;
    public GameObject yezi;
    public Transform zhuPos;
    public Transform yeziPos;
    public Collider2D zuoColider;
    public Collider2D youColider;
    public Collider2D shenti;
    public Transform player;
    public void ZhaoZhu()
    {
        Instantiate(zhu,zhuPos.position,Quaternion.Euler(Vector3.zero)).SetActive(true);

    }

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    public void zuoshouyouxiao()
    {
        zuoColider.enabled = true;
    }
    public void zuowuxiao()
    {
        zuoColider.enabled = false;
    }
    public void youshouyouxiao()
    {
        youColider.enabled = true;
    }
    public void youwuxiao()
    {
        youColider.enabled = false;
    }
    public void shentiyouxiao()
    {
        shenti.enabled = true;
    }
    public void shentiwuxiao()
    {
        shenti.enabled = false;

    }
    public void erjieduan()
    {

    }
    public void EmmitYezi()
    {
        StartCoroutine(Emmit());
    }
    public void Shunyi()
    {
        transform.position = new Vector3(player.position.x,transform.position.y, transform.position.z);
    }
    IEnumerator Emmit()
    {
        int t = 8;
        while(t-->=0)
        {
            print("ok!");
            Instantiate(yezi, yeziPos.position, Quaternion.Euler(Vector3.zero));
            yield return new WaitForSeconds(0.5f);
        }
    }
}
