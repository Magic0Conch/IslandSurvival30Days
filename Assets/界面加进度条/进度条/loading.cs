using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class loading : MonoBehaviour
{
    public GameObject loadScreen;
    //public GameObject StartScreen;
    public Slider slider;
    public Text text;//进度及提示
   // public Timeline TimeL;//开场剧情
    

    public void LoadNext()
    {//异步加载
     // TimeL.pl

        StartCoroutine(loadlevel());
    }

    IEnumerator loadlevel()
    {
        //StartScreen.SetActive(false);
        loadScreen.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        //游戏场景放在开始界面场景下面
        operation.allowSceneActivation = false;
        while(!operation.isDone)//在场景没有加载完之前，显示过度场景
        {
            slider.value = operation.progress;
            text.text = operation.progress * 100 + "%";
            if(operation.progress>=0.9f)
            {//会在加载完成时卡在90，需要手动调节到100%，按任意键才能进入下一场景
                slider.value = 1;
                text.text = "按任意键进入游戏";
                if(Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
