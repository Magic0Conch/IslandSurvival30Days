using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Diary : MonoBehaviour
{
    public GameObject gridPrefab, gridPrefabCreature;
    public Text propText,creatureText;
    public string[] propIntroduction;
    public Sprite[] propSprites;
    public string[] creatureIntroduction;
    public Sprite[] creatureSprites;
    public string[] storyIntroduction;
    public Transform propContent;
    public Transform creatureContent;
    public Text StoryText;
    //public Transform missionContent;
    List<int> propList = new List<int>();
    List<int> creatureList = new List<int>();
    List<int> storyList = new List<int>();


    public Image builtImage,duduImage,ecoImage;
    public Text builtText, duduText, ecoText,builtGrade;
    public enum AddType
    {
        Prop,Creature,Story
    }


    private void OnEnable()
    {
        //DayText.text = "天数：" + BasicData.Instance.date;
        if(BasicData.Instance.buildExp==0)
        {
            BasicData.Instance.buildLev = 0;
            builtGrade.text = "0";
            builtImage.fillAmount = 0;
            builtText.text = BasicData.Instance.buildExp.ToString()+"/"+"0";

        }
        else if(BasicData.Instance.buildExp<50)
        {
            BasicData.Instance.buildLev = 1;
            builtGrade.text = "1";
            builtImage.fillAmount = BasicData.Instance.buildExp / 50; ;
            builtText.text = BasicData.Instance.buildExp.ToString() + "/" + "50";

        }
        else if(BasicData.Instance.buildExp < 120)
        {

            BasicData.Instance.buildLev = 2;
            builtGrade.text = "2";
            builtImage.fillAmount = BasicData.Instance.buildExp / 120; ;
            builtText.text = BasicData.Instance.buildExp.ToString() + "/" + "120";
        }
        else if(BasicData.Instance.buildExp < 200)
        {

            BasicData.Instance.buildLev = 3;
            builtGrade.text = "3";
            builtImage.fillAmount = BasicData.Instance.buildExp / 200; ;
            builtText.text = BasicData.Instance.buildExp.ToString() + "/" + "200";

        }
        else if(BasicData.Instance.buildExp < 350)
        {

            BasicData.Instance.buildLev = 4;
            builtGrade.text = "4";
            builtImage.fillAmount = BasicData.Instance.buildExp / 350;
            builtText.text = BasicData.Instance.buildExp.ToString() + "/" + "350";

        }
        else if(BasicData.Instance.buildExp < 500)
        {

            BasicData.Instance.buildLev = 5;
            builtGrade.text = "5";
            builtImage.fillAmount = BasicData.Instance.buildExp / 500 ;
            builtText.text = BasicData.Instance.buildExp.ToString() + "/" + "500";

        }
        else
        {

            BasicData.Instance.buildLev = 6;
            builtGrade.text = "6";
            builtImage.fillAmount = 1 ;
            builtText.text = BasicData.Instance.buildExp.ToString() + "/" + "???";
        }
        builtGrade.text = "建设等级："+BasicData.Instance.buildLev;
        BasicData.Instance.ecologicalValue = BasicData.Instance.ecologicalValue > 200 ? 200 : BasicData.Instance.ecologicalValue;
        BasicData.Instance.ecologicalValue = BasicData.Instance.ecologicalValue < 0 ? 0 : BasicData.Instance.ecologicalValue;
        ecoText.text = BasicData.Instance.ecologicalValue.ToString("F1") + "/" + "200";//生态值
        ecoImage.fillAmount = BasicData.Instance.ecologicalValue / 200;
        duduText.text = BasicData.Instance.duduIndex.ToString("F1") + "/" + "2000";
        duduImage.fillAmount = BasicData.Instance.duduIndex / 2000;
    }

    public void Open(GameObject go)
    {
        go.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Close(GameObject go)
    {
        go.SetActive(false);
    }
    public bool AddDiary(int id, AddType addType,string intro = "")
    {

        EventTrigger eventTrigger = null;
        if(addType==AddType.Prop)
        {

            int len = propList.Count;
            for(int i = 0;i<len;i++)
                if (id == propList[i]) return false;
            GameObject go = Instantiate(gridPrefab, propContent);
            go.transform.GetChild(0).GetComponent<Image>().sprite = propSprites[id];
            eventTrigger = go.GetComponentInChildren<EventTrigger>();
        }
        else if(addType ==AddType.Creature)
        {
            int len = creatureList.Count;
            for (int i = 0; i < len; i++)
                if (id == creatureList[i]) return false;
            GameObject go = Instantiate(gridPrefabCreature, creatureContent);
            go.transform.GetChild(0).GetComponent<Image>().sprite = creatureSprites[id];
            eventTrigger = go.GetComponentInChildren<EventTrigger>();
        }
        else if(addType == AddType.Story)
        {
            StoryText.text += "第"+ BasicData.Instance.date + "天：n";
            StoryText.text += intro+"n";
            StoryText.text = StoryText.text.Replace('n', '\n');
        }
        if(addType!=AddType.Story)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();

            entry.eventID = EventTriggerType.PointerClick;
            UnityAction<BaseEventData> callback = new UnityAction<BaseEventData>(delegate { onClick(id,addType); });
            entry.callback.AddListener(callback);        
            eventTrigger.triggers.Add(entry);

        }
        return true;
    }

    void onClick(int id,AddType addType)
    {
        if(addType == AddType.Prop)
        {
            propText.text = propIntroduction[id].Replace('n', '\n');
        }
        else if(addType == AddType.Creature)
        {
            creatureText.text = creatureIntroduction[id].Replace('n', '\n');
        }
        else if(addType == AddType.Story)
        {

        }
    }
    private void Start()
    {
        for(int i = 0;i<propSprites.Length;i++)
            AddDiary(i, AddType.Prop);
        for (int i = 0; i < creatureSprites.Length; i++)
            AddDiary(i, AddType.Creature);
    }
}
