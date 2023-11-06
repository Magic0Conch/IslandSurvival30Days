using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playerainm : MonoBehaviour
{
    public void activeMove()
    {
        BasicData.Instance.banMove = false;
    }
    public void Gameover()
    {
        if(BasicData.Instance.isInBoss)
        {
            if(BasicData.Instance.ecologicalValue<30)
            {
                BasicData.Instance.dieIndex = 6;
            }
            else
            {
                BasicData.Instance.dieIndex = 7;

            }
        }
        else if (BasicData.Instance.playerRepletion <= 0.1f)
            BasicData.Instance.dieIndex = 2;
        else if(BasicData.Instance.lAttack==0)//^(*￣(oo)￣)^
        {
            BasicData.Instance.dieIndex = 0;

        }
        else if(BasicData.Instance.lAttack==1)//sheep
        {
            BasicData.Instance.dieIndex = 1;
        }
        SceneManager.LoadScene(3);

    }

}
