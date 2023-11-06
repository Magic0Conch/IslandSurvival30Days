using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public ParticleSystem partical;
    public GameObject mainBody;
    public Animator animator;
    public GameObject[] dropItems;
    public float hpMax = 100;
    public string validTag;
    public string hitedAnimationName = "hit";
    public string DieAnimationName = "die";
    public float hittedDecline;
    float hp;
    private void Start()
    {
        hp = hpMax;
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
        if (BasicData.Instance.playerRepletion < 1.5f || BasicData.Instance.playerEnergy < 2) return;

        if (collision.tag.ToString() == validTag)
        {
            if(partical!=null)
                partical.Play();
            animator.Play(hitedAnimationName, 0, 0.0f);
            hp -= hittedDecline;
            BasicData.Instance.playerRepletion -= 1.5f;
            BasicData.Instance.playerEnergy -= 2;
            if (hp <= 0)
            {
                if(DieAnimationName!="")
                    animator.Play(DieAnimationName, 0, 0.0f);
                GetComponent<Collider2D>().enabled = false;
                int len = dropItems.Length;
                BasicData.Instance.ecologicalValue -= 0.5f;
                Destroy(gameObject, 0.5f);
                for(int i = 0;i<len;i++)
                {
                    GameObject go = Instantiate(dropItems[i], transform.position,Quaternion.Euler(Vector3.zero));
                }
            }
        }
    }
}
