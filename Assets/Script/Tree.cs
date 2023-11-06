using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
    //public GameObject hpPannel;
    public ParticleSystem leafPartical;
    //public Image hpImage;
    public Animator treeAnimator;
    public GameObject Log;
    public GameObject Seed;
    public GameObject mainBody;

    float hp;
    float hpMax;
    int t = 60;
    private void Start()
    {
        hpMax = 100f;
        hp = hpMax;
        
    }

    IEnumerator HideHpPannel()
    {
        if (t == 60) t = 0;
        for(;t<60;t++)
        {
            yield return null;
        }
        //hpPannel.SetActive(false);
    }

    private void OnBecameVisible()
    {
        mainBody.SetActive(true);
    }

    private void OnBecameInvisible()
    {
        mainBody.SetActive(false);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag.ToString()=="Axe")
        {
            if (BasicData.Instance.playerRepletion < 1 || BasicData.Instance.playerEnergy < 2) return;
            leafPartical.Play();
            treeAnimator.Play("treeHited",0,0.0f);
            //hpPannel.SetActive(true);
            if(t == 60)
            {
                //StartCoroutine("HideHpPannel");
            }
            t = 0;
            hp -= 20;
            BasicData.Instance.playerRepletion -= 1;
            BasicData.Instance.playerEnergy -= 2;
            //hpImage.fillAmount = hp / hpMax;
            if(hp<=0)
            {
                BasicData.Instance.ecologicalValue -= 0.5f;
                treeAnimator.Play("treeFall", 0, 0.0f);
                GetComponent<Collider2D>().enabled = false;
                GameObject go = Instantiate(Log);
                go.transform.position = transform.position+Vector3.up*0.8f;
                Destroy(gameObject,0.5f);
                go = Instantiate(Seed);
                go.transform.position = transform.position + Vector3.up * 0.8f;
                int rd = Random.Range(0, 2);
                if(rd==1)
                {
                    go = Instantiate(Seed);
                    go.transform.position = transform.position;
                }
            }
        }
    }
}
