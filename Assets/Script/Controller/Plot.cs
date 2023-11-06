using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Experimental.Rendering.Universal;

public class Plot : MonoBehaviour
{
    
    public GameObject[] bg;
    public GameObject[] bossInvicible;
    public GameObject hpBoss;
    public GameObject[] BossActive;
    public Light2D bossLight;
    static Plot _instance;
    public static Plot Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Plot>();
            }
            return _instance;
        }
    }
    public GameObject feather;
    //37.12 -17.54
    public BagController bagController;
    public UIController1 timeController;
    public BuildController build;
    public Transform player;
    public Sprite[] Plots;
    public bool[] rec = new bool[30];
    public Transform plotCameraPos;
    public Transform Cam;
    Transform UIt;
    GameObject engine;
    public PlayableDirector[] directors;
    bool jintianqianguole = false;
    private void Start()
    {
        InitEvent();
        CommonMethod.Instance.ShowImg(Plots[0], "欢迎来到荒岛30天。请记住A/D是移动，那么现在，将精灵赠予的果子捡起来吧！");
        StartPlot();
        directors[0].Play();
        //enterEnd();
    }

    void InitEvent()
    {
        UIt = timeController.transform;
        rec = new bool[30];
        for (int i = 0;i<rec.Length;i++)
            rec[i] = false;
        directors[0].stopped += delegate 
        {
            print("director0 over!");
            rec[0] = true;
            EndPlot();
            //Destroy(directors[0].gameObject);
        };
        directors[1].stopped += delegate
        {
            rec[4] = true;//4号 ：玩家与精灵交谈完毕 首次开始独自生存
            EndPlot();
            //Destroy(directors[1].gameObject);
        };
        directors[2].stopped += delegate
        {
            CommonMethod.Instance.ShowDialog("您在剧情中获得了金色羽毛，在以后的生存中，随着剧情的发展和建筑等级的提高，你都有机会获得金色羽毛。把它交给渡渡鸟王，就会解锁新的配方。加油去收集它们吧！");
            rec[5] = true;
            player.transform.position = new Vector3(-1520f, 10, 0);
            EndPlot();
            GameObject go = Instantiate(feather, player.position, Quaternion.Euler(Vector3.zero));
            go.GetComponent<GoldFeather>().id = 0;
            //Destroy(directors[2].gameObject);
        };
        directors[8].stopped += delegate
        {
            if(!rec[6])
            {
                GameObject go = Instantiate(feather, player.position, Quaternion.Euler(Vector3.zero));
                go.GetComponent<GoldFeather>().id = 1;
            }
            rec[6] = true;//5天刚抢过
            EndPlot();
        };
        directors[9].stopped += delegate
        {
            print("9 stop");
            UIt.GetComponent<Canvas>().enabled = true;
            BasicData.Instance.banMove = false;
            //BasicData.Instance.noEnemy = true;
            BasicData.TimeMutipleRate = 0;
            BasicData.Instance.cameraFollow = true;
            rec[14] = true;//14：jinruboss
            BasicData.Instance.isInBoss = true;
            bossLight.intensity = 1;
            hpBoss.SetActive(true);
            foreach (GameObject go in bossInvicible)
            {
                go.SetActive(false);
            }
        };
        directors[5].stopped += delegate
        {
            rec[8] = true;//8：抢完东西劝完了
            EndPlot();
        };
        //directors[1].stopped += delegate
        //{
        //    rec[3] = true;//4号 ：玩家与精灵交谈完毕 首次开始独自生存
        //    EndPlot();
        //};
        directors[3].stopped += delegate
        {
            rec[10] = true;//10：野猪打完了
            GameObject go = Instantiate(feather, player.position, Quaternion.Euler(Vector3.zero));
            go.GetComponent<GoldFeather>().id = 2;
            EndPlot();
        };
        directors[6].stopped += delegate
        {
            rec[12] = true;//12:入侵者求助结束
            EndPlot();
            CommonMethod.Instance.ShowDialog("最终的决战要来了！请准备好战斗吧。注意最近的生态值等条件，他可能会对你的决战产生某种影响。你可以在渡渡鸟精灵处选择立刻进入战斗或者是在30天强制进入战斗。");
        };
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            foreach(PlayableDirector director in directors)
            {
                if(director.state == PlayState.Playing)
                {
                    director.time = director.duration - 1;
                }
            }
        }
        if (rec[0] && !rec[1] && bagController.ExistItem(14, 1))
        {
            rec[1] = true;
            CommonMethod.Instance.ShowImg(Plots[1], "太棒了！您捡起了果子，现在请试着把道具拖入道具栏吧。");
        }
        else if (!rec[2] && bagController.ExistProp(14))
        {
            rec[2] = true;
            CommonMethod.Instance.ShowImg(Plots[2], "请看左上角从上到下，依次为血量（小于0游戏会结束），饱食度（过低将无法进行很多操作，到0会减少血量），精力值（过低将无法建造、攻击，并且移动速度减慢），现在您状态不佳，把果子吃掉吧。");
        }
        else if (!rec[3] && rec[1] && !bagController.ExistItem(14, 1))
        {
            rec[3] = true;
            CommonMethod.Instance.ShowImg(Plots[2], "精灵好像有话要说，去和她交谈一下吧。");
        }
        else if (((BasicData.Instance.date >= 2 &&BasicData.Instance.nowTime>800)||BasicData.Instance.date > 2) && !rec[5])//5号： 玩家获得羽毛
        {
            rec[5] = true;
            StartPlot();
            directors[2].Play();
        }
        else if( rec[6]&&!rec[7]&& (BasicData.Instance.date == 5 && BasicData.Instance.nowTime>1100|| BasicData.Instance.date > 5))
        {
            rec[7] = true;//7：抢完东西的劝告
            StartPlot();
            directors[5].Play();
        }
        else if((BasicData.Instance.date>=6&&BasicData.Instance.nowTime>600|| BasicData.Instance.date>6) &&rec[8]&&!rec[9])
        {
            rec[9] = true;
            StartPlot();
            directors[3].Play();//打野猪开始
        }
        else if(BasicData.Instance.date == 25&&!rec[11])
        {
            rec[11] = true;//入侵者求救
            StartPlot();
            directors[6].Play();
        }
        else if(BasicData.Instance.date == 25&& BasicData.Instance.ecologicalValue<30&&!rec[13])
        {
            enterEnd();
        }
        else if(BasicData.Instance.date >= 30&& !rec[13] )
        {
            enterEnd();
        }
        #region 常见事件
        if (BasicData.Instance.date != 0 &&BasicData.Instance.date<25&& BasicData.Instance.date%5==0&&!jintianqianguole)//抢东西
        {
            //37.12 - 17.54
            jintianqianguole = true;
            directors[8].Play();//抢东西啦！
            StartPlot();
            Cam.transform.position = new Vector3(37.12f, -17.54f, -10);
            foreach(GameObject go in bg)
            {
                go.gameObject.SetActive(false);
            }
            if(BasicData.Instance.date<10)
            {
                bg[0].SetActive(true);
            }
            else if(BasicData.Instance.date < 15)
            {

                bg[1].SetActive(true);
            }
            else if(BasicData.Instance.date < 20)
            {
                bg[2].SetActive(true);

            }
            else
            {
                bg[3].SetActive(true);
            }
            bagController.Robbed();
            BasicData.Instance.playerEnergy -= 150;
            CommonMethod.Instance.ShowDialog("入侵者把你的背包中的食物洗劫一空，你拼了命留下了一点东西。n你感觉十分疲惫。");
        }
        else if(BasicData.Instance.date % 5 != 0)
        {
            jintianqianguole = false;
        }
        //rec 22 - 30建造等级事件

        if(BasicData.Instance.buildLev==1&&!rec[22])
        {
            rec[22] = true;
            GameObject go = Instantiate(feather, player.position, Quaternion.Euler(Vector3.zero));
            go.GetComponent<GoldFeather>().id = 3;
        }
        else if(BasicData.Instance.buildLev==2&&!rec[23])
        {
            rec[23] = true;
            GameObject go = Instantiate(feather, player.position, Quaternion.Euler(Vector3.zero));
            go.GetComponent<GoldFeather>().id = 4;
        }
        else if(BasicData.Instance.buildLev==3&&!rec[24])
        {
            rec[24] = true;
            GameObject go = Instantiate(feather, player.position, Quaternion.Euler(Vector3.zero));
            go.GetComponent<GoldFeather>().id = 5;
        }
        else if(BasicData.Instance.buildLev==4&&!rec[25])
        {
            rec[25] = true;
            GameObject go = Instantiate(feather, player.position, Quaternion.Euler(Vector3.zero));
            go.GetComponent<GoldFeather>().id = 6;
        }
        else if(BasicData.Instance.buildLev==5&&!rec[26])
        {
            rec[26] = true;
            GameObject go = Instantiate(feather, player.position, Quaternion.Euler(Vector3.zero));
            go.GetComponent<GoldFeather>().id = 7;
        }
        else if(BasicData.Instance.buildLev==6&&!rec[27])
        {
            rec[27] = true;
            GameObject go = Instantiate(feather, player.position, Quaternion.Euler(Vector3.zero));
            go.GetComponent<GoldFeather>().id = 8;
        }
        #endregion
    }
    #region 辅助方法

    public void enterEnd()
    {
        engine = GameObject.Find("Engine");
        engine.GetComponent<GenerateCreature>().enabled = false;
        StartPlot();
        rec[13] = true;
        BasicData.Instance.noEnemy = false;
        foreach(GameObject go in BossActive)
        {
            go.SetActive(true);
        }
        //-2121.08 7.04
        player.transform.position = new Vector3(-2121.08f, 7.04f, player.transform.position.z);
        Cam.transform.position = new Vector3(-2121.08f, 7.04f, -10);
       
        directors[9].Play();
    }

    public void StartPlot()
    {
        BasicData.Instance.banMove = true;
        BasicData.Instance.noEnemy = true;
        BasicData.TimeMutipleRate = 0;
        UIt.GetComponent<Canvas>().enabled = false;
        BasicData.Instance.cameraFollow = false;
        Cam.transform.position = plotCameraPos.transform.position;
    }

    public void EndPlot()
    {
        BasicData.Instance.banMove = false;
        BasicData.Instance.noEnemy = false;
        BasicData.TimeMutipleRate = 2;
        UIt.GetComponent<Canvas>().enabled = true;
        BasicData.Instance.cameraFollow = true;
        //Cam.transform.position = plotCameraPos.transform.position;
    }
    #endregion
}
