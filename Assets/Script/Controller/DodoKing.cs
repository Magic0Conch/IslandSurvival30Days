using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodoKing : MonoBehaviour
{

    public Transform player;
    public Transform hint;
    public BagController bagController;
    public BuildController build;
    Animator animator;
    SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(lookat());
        animator = GetComponent<Animator>();
    }
    IEnumerator lookat()
    {
        while (true)
        {
            sr.flipX = transform.position.x > player.transform.position.x;
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
            hint.gameObject.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            if(bagController.ExistItem(15,1))
            {
                string s = "你归还了渡渡鸟的金色羽毛，做为答谢，渡渡鸟王告诉了你新配方。n新的建造配方解锁了。n";
                if (BasicData.Instance.ecologicalValue < 30)
                    s += "不过渡渡鸟王好像不太高兴的样子。";
                else if(BasicData.Instance.ecologicalValue > 150)
                    s += "渡渡鸟王亲昵的注视中你。";
                CommonMethod.Instance.ShowDialog(s);
                int code = bagController.featherCodeTop();
                animator.Play("交互");
                bagController.ConsumeItem(15,1);
                switch (code)
                {
                    case 0:
                        build.addBuildItem(2);
                        build.addBuildItem(10);
                        build.addBuildItem(6);
                        build.addBuildItem(8);

                        break;
                    case 1:
                        build.addBuildItem(0);
                        build.addBuildItem(1);
                        build.addBuildItem(12);
                        build.addBuildItem(13);
                        break;
                    case 2:
                        build.addBuildItem(7);
                        build.addBuildItem(9);

                        break;
                    case 3:

                        build.addBuildItem(14);
                        build.addBuildItem(11);
                        break;
                    case 4:
                        build.addBuildItem(15);
                        break;
                    case 5:
                        build.addBuildItem(20);
                        build.addBuildItem(21);
                        break;
                    case 6:
                        build.addBuildItem(17);
                        break;
                    case 7:
                        build.addBuildItem(18);
                        build.addBuildItem(19);
                        break;
                    case 8:
                        build.addBuildItem(16);
                        break;
                }

                return;
            }


        }

    }
}
