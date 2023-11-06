using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bed : MonoBehaviour
{
    bool canEnter = true;
    public float sleepminu = 480f;
    public float resHp = 40;
    public float resRp = -60;
    public float resEg = 200;
    Image curtain;
    UIController1 uIController;
    float coolTime = 0.3f;

    IEnumerator Cool()
    {
        canEnter = false;
        yield return new WaitForSeconds(coolTime);
        canEnter = true;
    }
    private void Start()
    {
        curtain = GameObject.Find("curtain").GetComponent<Image>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag.ToString() == "Player")
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.transform.gameObject.tag.ToString() == "Player")
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!canEnter) return;
        if (collision.gameObject.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Cool());
                StartCoroutine(StartSleep());
                
            }
        }
    }
    IEnumerator StartSleep()
    {
        BasicData.Instance.banMove = true;
        while(curtain.color.a<1)
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
        if(BasicData.Instance.playerRepletion<=10)
            CommonMethod.Instance.ShowDialog("再不吃东西，你就要饿死了");
        CommonMethod.Instance.ShowDialog("你睡了8个小时。n你的体力和精力回复了，但肚子更饿了。");
        BasicData.Instance.banMove = false;
    }
}
