using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;

public class EndPlot : MonoBehaviour
{
    public PlayableDirector[] directors;
    public Text ssssqw;
    public GameObject yincang;
    string[] s = { "泛滥的物种·猪", "泛滥的物种·羊", "饿死异乡的旅行者", "大地精灵的宽恕", "精灵的庇护", "大地精灵的愤怒", "大地精灵的惩罚", "沉默的精灵", "时空旅行者" };
    string r;
    // Start is called before the; first frame update
    //死亡 HE 反思
    void Start()
    {
        //BasicData.Instance.dieIndex = 1;
        int ind = BasicData.Instance.dieIndex;
        r = PlayerPrefs.GetString("record", "000000000");
        char[] cr = r.ToCharArray();
        cr[ind] = '1';
        r = new string(cr);


        if(r== "111111110"||r== "111111111")
        {
            yincang.SetActive(true);
            r = "111111111";
        }

        PlayerPrefs.SetString("record",r);
        PlayerPrefs.Save();
        if (ind>2&&ind<6)
        {
            directors[1].gameObject.SetActive(true);
        }
        else
        {
            directors[0].gameObject.SetActive(true);
        }
        ssssqw.text = "新的记忆碎片：" + s[ind] +"已解锁。";
        directors[0].stopped += delegate
        {
            directors[2].gameObject.SetActive(true);
            directors[0].gameObject.SetActive(false);
        };
        directors[1].stopped += delegate
        {
            directors[2].gameObject.SetActive(true);
            directors[1].gameObject.SetActive(false);
        };
        directors[2].stopped += delegate
        {
            directors[2].gameObject.SetActive(false);
            ssssqw.transform.parent.gameObject.SetActive(true);
            if(r == "111111111")
            {
                yincang.SetActive(true);
            }
            Invoke("Load", 1f);
        };

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            for (int i = 0; i < 3; i++)
                directors[i].gameObject.SetActive(false);
            ssssqw.transform.parent.gameObject.SetActive(true);
            if (r == "111111111")
            {
                yincang.SetActive(true);
            }
            Invoke("Load", 1f);
        }
    }

    void Load()
    {
            SceneManager.LoadScene(0);

    }
}
