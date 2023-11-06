using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;
public class UIController1 : MonoBehaviour
{
    public Light2D gloabalLight;
    //public Light2D partLight;
    
    GameObject go;
    Vector3 pos;
    int buildId;
    public Diary diary;

    //时间模块
    public RectTransform Clock;
    public Text Date;
    public bool TimeFly = true;
    public float TimeFlySpeed = 0.0032f;
    //public LWRPAdditionalLightData lightData;

    [HideInInspector]
    public float nowTime = 800;//1440minutes 一天  480min为8:00
    const float DayMinutes = 1440;
    public float mutiplesRate = 1.0f;

    private void Start()
    {
        BasicData.TimeMutipleRate = mutiplesRate;
    }
    public void ControlViewL(RectTransform rect)
    {
        float w = rect.sizeDelta.x;
        //print("y:" + rect.position.x);
        print("x:" + rect.sizeDelta.x);
        print("y:" + rect.localPosition.x);
        //print("y:" + rect.anchoredPosition.x);
        //print("x1:" + rect.rect.size.x);
        //print("y1:" + rect.rect.size.y);
        //print("wid:" + rect.rect.width);
        //print("x2:" + rect.rect.x);
        //print("x3:" + rect.rect.xMin);
        //print("x4:" + rect.rect.xMax);
        float temp = Mathf.Clamp(rect.localPosition.x + 600, -w, 0);
        rect.localPosition = new Vector3(temp, rect.localPosition.y, rect.localPosition.z);
    }
    public void ControlViewR(RectTransform rect)
    {
        float w = rect.sizeDelta.x;
        float temp = Mathf.Clamp(rect.localPosition.x - 600, -w, 0);
        rect.localPosition = new Vector3(temp, rect.localPosition.y, rect.localPosition.z);
    }
    private void Update()
    {        
        if (!TimeFly) return;
        nowTime += TimeFlySpeed*mutiplesRate;

        if (nowTime> DayMinutes)
        {
            int td = (int)(nowTime / DayMinutes);
            BasicData.Instance.date += td;
            nowTime = nowTime - td * DayMinutes;
            Date.text = BasicData.Instance.date.ToString();
            switch (BasicData.Instance.date)
            {
                case 1:
                    diary.AddDiary(0,Diary.AddType.Story, "我来到了这个陌生的世界，这里的一切对于我来说都是陌生的。我不知道我为什么来这里，我也不知道我该怎么回去，当我清醒过来的时候，我出现在那艘船上，船上的人对我很不友好，他们把我丢下了我。我很饿，很害怕。好在，一只小精灵出现了，她送给了一个果实，还送给了我一些工具，她还说她是因为有事情求我才帮助我的，她会在五天后再来见我，到底是为什么救我呢？算了，不管那么多了，我要努力活下去！n");
                    break;
                case 2:
                    diary.AddDiary(0,Diary.AddType.Story, "今天我因为太饿了，想去吃鸟蛋，虽然不知道能不能吃。几只老鼠从鸟蛋中蹦了出来，吓我一跳，我赶走了他们。这时，一只鸟出现了，居然是渡渡鸟！渡渡鸟不是已经灭绝了吗？小渡渡鸟破壳而出，渡渡鸟妈妈送给了我一根金色的羽毛。后来，小精灵又出现了，原来她是渡渡鸟精灵，想让我帮助她的族人才救我的。她告诉我可以用金色羽毛去渡渡鸟王那里换卷轴，真是太神奇了！n");
                    break;
                case 5:
                    diary.AddDiary(0,Diary.AddType.Story, "那群人真的欺人太甚！他们抢走了我的食物，那可是我辛辛苦苦攒下来的！他们走后，我又发现了一根金色羽毛，小精灵也出现了。她告诉我，那群人破坏了她们的家，他们带来了猪、羊以及偷渡来的老鼠，这些外来生物在这里肆无忌惮，还跟她们抢食物！小精灵想让我帮助他们，我同意了。n");
                    break;
                case 6:
                    diary.AddDiary(0,Diary.AddType.Story, "救了一只渡渡鸟，入侵者真的太可恶了！n");
                    break;
                case 15:
                    diary.AddDiary(0,Diary.AddType.Story, "可恶的入侵者！n");
                    break;
                case 12:
                    diary.AddDiary(0,Diary.AddType.Story, "我发现了这座岛上的土著人部落，而且只要给他们肉，他们就会给我一些有用的东西，希望会有用。n");
                    break;
                case 20:
                    diary.AddDiary(0,Diary.AddType.Story, "可恶的入侵者！n");
                    break;
                case 25:
                    diary.AddDiary(0,Diary.AddType.Story, "入侵者来向我求和了，他们认识到了他们的错误，愿意帮助渡渡鸟，保护岛上的生态。小精灵说，我可以回去了。终于，我要回家了！n");
                    break;
                default:
                    diary.AddDiary(0,Diary.AddType.Story, "努力生存的一天！n");
                    break;
            }


        }
        if(nowTime>600&&nowTime<1100)//day 600-1000 白天
        {
            gloabalLight.intensity = Mathf.Clamp(gloabalLight.intensity + (nowTime-600)/DayMinutes,0,0.8f);
            gloabalLight.color = new Color32(255, 255, 255, 255);

            BasicData.Instance.playerRepletion -= 0.002f * mutiplesRate;
            BasicData.Instance.playerEnergy -= 0.001f * mutiplesRate;
            //partLight.intensity = 1f;
            //partLight.color = new Color32(228, 194, 0,255);

        }
        else if((nowTime>150&& nowTime < 600)|| (nowTime > 1000 && nowTime < 1250))//dawn
        {
            if(nowTime<600)
                gloabalLight.intensity = Mathf.Clamp(gloabalLight.intensity + (nowTime - 150) / DayMinutes, 0, 0.2f);
            else
                gloabalLight.intensity = Mathf.Clamp(gloabalLight.intensity - (nowTime - 1000) / DayMinutes, 0.2f, 0.8f);
            gloabalLight.color = new Color32(255, 105, 70, 255);
            //partLight.intensity = 0.3f;
            //partLight.color = new Color32(255, 50, 0, 255);
            BasicData.Instance.playerRepletion -= 0.003f * mutiplesRate;
            BasicData.Instance.playerEnergy -= 0.002f * mutiplesRate;
        }
        else//night
        {
            if(nowTime>1250)
                gloabalLight.intensity = Mathf.Clamp(gloabalLight.intensity - (nowTime - 1250) / DayMinutes/2, 0.1f, 0.2f);
            gloabalLight.color = new Color32(100, 10, 140, 255);
            //partLight.intensity = 0.05f;
            //partLight.color = new Color32(228, 194, 255, 255);
            BasicData.Instance.playerRepletion -= 0.0026f * mutiplesRate;
            BasicData.Instance.playerEnergy -= 0.0066f * mutiplesRate;
        }
        Clock.localEulerAngles = new Vector3(0, 0, nowTime / DayMinutes * 360);
        BasicData.Instance.nowTime = nowTime;
        //全局光照强度范围0-2
        //0.05-1 
        //0-1 通道
    }
}
