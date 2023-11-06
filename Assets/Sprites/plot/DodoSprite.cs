using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
public class DodoSprite : MonoBehaviour
{

    public float sleepminu = 480f;
    public float resHp = 40;
    public float resRp = -60;
    public float resEg = 200;

    public Transform hint;
    public PlayableDirector director;
    public Transform player;
    public Image curtain;
    SpriteRenderer sr;
    UIController1 uIController;
    bool iniTalk = true;
    bool canEnter = true;
    float coolTime = 3f;
    bool first = false;
    IEnumerator Cool()
    {
        canEnter = false;
        yield return new WaitForSeconds(coolTime);
        canEnter = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
            hint.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
            hint.gameObject.SetActive(false);
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(lookat());
    }

    IEnumerator lookat()
    {
        while(true)
        {
            sr.flipX = transform.position.x < player.transform.position.x;
            yield return null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name=="Player"&&Input.GetKeyDown(KeyCode.E)&&canEnter)
        {
            StartCoroutine(Cool());
            if(BasicData.Instance.date<25)
            {
                if(iniTalk && director.state != PlayState.Playing)
                {
                    CommonMethod.Instance.ShowDialog("此后您可以在精灵出休息，休息的效果类似于床，会恢复您的hp和rp，但是会消耗eg。");
                    Plot.Instance.StartPlot();
                    iniTalk = false;
                    Plot.Instance.directors[1].Play();
                }
                else if(!iniTalk)
                {
                    StartCoroutine(StartSleep());
                }
            }
            else
            {
                if(!first)
                {
                    first = true;
                    CommonMethod.Instance.ShowDialog("精灵担忧又期望的冲你点了点头。n请确认您的状态和携带的物资，再次跟精灵对话进入最终决战。");
                }
                else
                {
                    Plot.Instance.enterEnd();
                    //SceneManager.LoadScene("battle");
                }
            }
        }

    }

    IEnumerator StartSleep()
    {
        BasicData.Instance.banMove = true;
        while (curtain.color.a < 1)
        {
            curtain.color += new Color(0, 0, 0, 0.04f);
            yield return null;
        }
        uIController = GameObject.Find("UI").GetComponent<UIController1>();
        uIController.nowTime += sleepminu;
        BasicData.Instance.playerHp += resHp;
        BasicData.Instance.playerRepletion += resRp;
        Stove[] stoves = GameObject.FindObjectsOfType<Stove>();
        foreach (Stove s in stoves)
        {
            s.nowFire = 500;
        }
        BasicData.Instance.playerEnergy += resEg;

        yield return new WaitForSeconds(2);
        while (curtain.color.a > 0)
        {
            curtain.color -= new Color(0, 0, 0, 0.04f);
            yield return null;
        }
        if (BasicData.Instance.playerRepletion <= 10)
            CommonMethod.Instance.ShowDialog("再不吃东西，你就要饿死了");
        CommonMethod.Instance.ShowDialog("你在精灵的庇护下休息了很长时间。n你的体力和精力回复了，但肚子更饿了。");
        BasicData.Instance.banMove = false;
    }
}
