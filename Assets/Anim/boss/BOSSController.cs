using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSSController : MonoBehaviour
{
    float moveSpeed = 1f;

    enum State
    {
        IDLE,zhaozhu,砸人右,砸人左,横扫,过渡,跳起,晃动
    }
    State nowstate;
    Transform player;
    bool isAgre = false;
    Animator animator;
    AnimatorClipInfo[] animatorClipInfo;
    private void Start()
    {
        animator = GetComponent<Animator>();
        //animator.Play()
        player = GameObject.Find("Player").transform;
    }
    int restFrame = 40;

    


    void Update()
    {
        animatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        print(animatorClipInfo[0].clip.name);
        if (BasicData.Instance.bossHp <= BasicData.Instance.bossHpMax/2&& !isAgre)
        {
            //animator.GetCurrentAnimatorClipInfo();
            isAgre = true;
            animator.Play("过渡");
        }
        if(animatorClipInfo[0].clip.name == "IDLE")
        {
            transform.Translate((player.position.x - transform.position.x)* moveSpeed*Time.deltaTime,0,0);
        }
        if(animatorClipInfo[0].clip.name=="IDLE"|| animatorClipInfo[0].clip.name == "累")
        {
            if(restFrame--<=0)
            {
                if (!isAgre)
                    nowstate = (State)UnityEngine.Random.Range(1, 5);
                else
                    nowstate = (State)UnityEngine.Random.Range(6, 8);
                animator.Play(nowstate.ToString());
            }
        }
        else
        {
            restFrame = 40;
        }
        
    }
}
