using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text;

public class BuildController : MonoBehaviour
{
    /// <summary>
    /// 此处为所有建筑物的信息
    /// </summary>
    /// 
    public GameObject buildColider;
    public Image buildProcess;
    public GameObject[] buildList;
    public Sprite[] thumbnails;
    public bool[] isBuilding;
    public int[] isOutDoor;//0随意 1室内 2室外
    public string[] buildingDescription;
    public Transform Player;
    PlayerController playerController;
    public GameObject buidGridPrefab;
    public GameObject buildPartical;
    public GameObject buildPannel;
    public GameObject noticePannel;
    public Transform content;
    public RectTransform scrollView;
    public BagController bagController;
    public GraphicRaycaster gr;
    float buildPannelEmergeDistance;
    bool isMirroed = false;
    GameObject go;
    Vector3 pos;
    int buildId;
    bool canBuild = false;
    float offsetY;
    Text complainText;
    SpriteRenderer[] sr;
    PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
    Transform theParent;
    class BuildInfo
    {
        public int id;
        public bool isBuilding = false;
    }
    List<BuildInfo> buildInfos = new List<BuildInfo>();
    public bool addBuildItem(int id)
    {
        int len = buildInfos.Count;
        for(int i = 0;i<len;i++)
        {
            if (buildInfos[i].id == id)
                return false;//已存在
        }
        GameObject go = Instantiate(buidGridPrefab, content);
        go.transform.GetChild(0).GetComponent<Image>().sprite = thumbnails[id];
        EventTrigger eventTrigger = go.GetComponentInChildren<EventTrigger>();

        eventTrigger.triggers = new List<EventTrigger.Entry>();
        EventTrigger.Entry[] entrys = new EventTrigger.Entry[6];
        for (int i = 0; i < 6; i++)
            entrys[i] = new EventTrigger.Entry();

        entrys[0].eventID = EventTriggerType.BeginDrag;
        UnityAction<BaseEventData> callback = new UnityAction<BaseEventData>(delegate {onBeginDrag(id);});
        entrys[0].callback.AddListener(callback);

        entrys[1].eventID = EventTriggerType.Drag;
        callback = new UnityAction<BaseEventData>(delegate { onBuildButtonDrag(); });
        entrys[1].callback.AddListener(callback);

        entrys[2].eventID = EventTriggerType.EndDrag;
        callback = new UnityAction<BaseEventData>(delegate { onBuildButtonUp(); });
        entrys[2].callback.AddListener(callback);

        entrys[3].eventID = EventTriggerType.PointerEnter;
        callback = new UnityAction<BaseEventData>(delegate { OnPointerIn(id); });
        entrys[3].callback.AddListener(callback);

        entrys[4].eventID = EventTriggerType.PointerExit;
        callback = new UnityAction<BaseEventData>(delegate { OnPointerOut(); });
        entrys[4].callback.AddListener(callback);

        entrys[5].eventID = EventTriggerType.PointerClick;
        callback = new UnityAction<BaseEventData>(delegate { OnBuildingClick(id); });
        entrys[5].callback.AddListener(callback);
        
        //eventTrigger.triggers.ad

        eventTrigger.triggers.Add(entrys[3]);
        eventTrigger.triggers.Add(entrys[4]);
        if (isBuilding[id])
        {
            eventTrigger.triggers.Add(entrys[0]);
            eventTrigger.triggers.Add(entrys[1]);
            eventTrigger.triggers.Add(entrys[2]);
        }
        else
        {
            eventTrigger.triggers.Add(entrys[5]);
        }
        return true;
        
    }

    private void Start()
    {
        buildPannelEmergeDistance = scrollView.rect.height;
        complainText = noticePannel.GetComponentInChildren<Text>();
        playerController = Player.GetComponent<PlayerController>();
        //addBuildItem(0);
        //addBuildItem(1);
        //addBuildItem(2);
        addBuildItem(3);
        addBuildItem(4);
        addBuildItem(5);
        //addBuildItem(6);
        //addBuildItem(7);
        //addBuildItem(8);
        //addBuildItem(9);
        //addBuildItem(10);
        //addBuildItem(11);
        //addBuildItem(12);
        //addBuildItem(13);
        //addBuildItem(14);
        //addBuildItem(15);
        //addBuildItem(16);
        //addBuildItem(17);
        //addBuildItem(18);
        //addBuildItem(19);
        //addBuildItem(20);
        //addBuildItem(21);
    }

    public Transform GetOverUI()
    {
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(pointerEventData, results);
        if (results.Count != 0)
        {
            return results[0].gameObject.transform;
        }

        return null;
    }


    float maxSize = 0;
    float maxSize2 = 0;
    public void onBeginDrag(int id)
    {
        maxSize = maxSize2 = 0;
        go = Instantiate(buildList[id]);
        sr = go.transform.GetComponentsInChildren<SpriteRenderer>();
        buildId = id;
        foreach (SpriteRenderer s in sr)
        {
            maxSize2 = maxSize2 > s.sprite.bounds.size.x * s.transform.localScale.x? maxSize2 : s.sprite.bounds.size.x * s.transform.localScale.x;
            maxSize = maxSize > s.bounds.size.x ? maxSize : s.bounds.size.x ;
        }
        print(maxSize +"/" + maxSize2);
    }

    public void onBuildButtonDrag()
    {
        Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int layerMask = 1 << 11;
        if (Player.gameObject.layer == 1) layerMask = 1 << 14;
        pos = new Vector3(vec.x, vec.y, 0);
        layerMask |=  1 << 15;
        RaycastHit2D[] raycastHit2D = Physics2D.RaycastAll(pos, Vector2.down, 100, layerMask);
        RaycastHit2D[] ds = Physics2D.BoxCastAll(pos,new Vector2(maxSize,4),0 ,Vector2.down, 100, layerMask);
        bool flag = true;
        foreach(RaycastHit2D ray in ds)
        {
            if(!BasicData.Instance.isIn)
            {
                if (ray.collider.transform.gameObject.layer == 15||isOutDoor[buildId]==1) flag = false;
            }
            else
            {
                if (isOutDoor[buildId] == 2)
                {
                    flag = false;
                    break;
                }
                print(ray.collider. transform.gameObject.layer + ":" + ray.collider.transform.parent.name + ":" + ray.collider.transform.name);
                if ((ray.collider.transform.gameObject.layer == 15 && !ray.collider.transform.parent.name.Contains("房子")))
                {

                    flag = false;
                }
                if (ray.collider.transform.parent.name.Contains("房子")) theParent = ray.collider.transform.parent.transform;
            }
        }
        Vector2 goPos = Vector2.down * 100;
        foreach (RaycastHit2D ray in raycastHit2D)
        {
            if ((ray.transform.gameObject.layer == 14 || ray.transform.gameObject.layer == 11) && goPos == Vector2.down * 100)
                goPos = ray.point;
        }
        if(!flag)
        {
            canBuild = false;
            foreach (SpriteRenderer s in sr)
                s.color = Color.red;
        }
        else
        {
            canBuild = true;
            foreach (SpriteRenderer s in sr)
                s.color = Color.green;
        }
        if (go != null)   go.transform.position = goPos;
        
    }

    struct NeededItem
    {
        public int id;
        public int num;
        public NeededItem(int id,int num)
        {
            this.id = id;
            this.num = num;
        }
    }

    bool build(float consumRep, float energy, params NeededItem[] neededItems)
    {
        foreach (NeededItem ni in neededItems)
        {
            if (!bagController.ExistItem(ni.id, ni.num)) 
                return false;
        }
        if (BasicData.Instance.playerRepletion < consumRep || BasicData.Instance.playerEnergy < energy) return false;
        BasicData.Instance.playerRepletion -= consumRep;
        BasicData.Instance.playerEnergy -= energy;
        foreach (NeededItem ni in neededItems)
        {
            bagController.ConsumeItem(ni.id, ni.num);
        }
        return true;
    }

    bool ExistItem(float consumRep, float energy, params NeededItem[] neededItems)
    {
        foreach (NeededItem ni in neededItems)
        {
            if (!bagController.ExistItem(ni.id, ni.num))
                return false;
        }
        if (BasicData.Instance.playerRepletion < consumRep || BasicData.Instance.playerEnergy < energy) return false;
        return true;
    }

    void ConsumeItem(float consumRep, float energy, params NeededItem[] neededItems)
    {
        BasicData.Instance.playerRepletion -= consumRep;
        BasicData.Instance.playerEnergy -= energy;
        foreach (NeededItem ni in neededItems)
        {
            bagController.ConsumeItem(ni.id, ni.num);
        }
    }

    public void onBuildButtonUp()
    {
        Vector3 builtPos = go.transform.position;
        Action action = delegate () {
            GameObject tmp = Instantiate(buildPartical);
            tmp.transform.position = builtPos;
            GameObject go;
            if (BasicData.Instance.isIn)
            {
                go = Instantiate(buildList[buildId], builtPos, Quaternion.Euler(Vector3.zero),theParent.GetChild(1).transform);
                go.transform.localScale = new Vector3(go.transform.localScale.x/ theParent.localScale.x, go.transform.localScale.y/ theParent.localScale.y, go.transform.localScale.z / theParent.localScale.z);
            }
            else
                go = Instantiate(buildList[buildId], builtPos, Quaternion.Euler(Vector3.zero));
            BoxCollider2D boxCollider2D = Instantiate(buildColider, go.transform, false).GetComponent<BoxCollider2D>();
            boxCollider2D.size = new Vector2(maxSize2, 0.2f);
        };
        Destroy(go);
        if (go.transform.position == pos|| !canBuild)   return;
        else
        {
            switch (buildId)
            {
                case 0:
                    if (ExistItem(20, 30, new NeededItem(0, 15), new NeededItem(7, 8), new NeededItem(2, 1), new NeededItem(3, 1)))
                        StartCoroutine(MoveTo(go.transform.position.x, action, delegate () { ConsumeItem(12, 17, new NeededItem(0, 15), new NeededItem(7, 8), new NeededItem(2, 1), new NeededItem(3, 1)); BasicData.Instance.buildExp += 50; }));
                    break;
                case 1:
                    if (ExistItem(3, 3, new NeededItem(12, 1)))
                        StartCoroutine(MoveTo(go.transform.position.x, action, delegate () { ConsumeItem(3, 3, new NeededItem(12, 1)); BasicData.Instance.ecologicalValue += 0.75f; }));
                    break;
                case 2:
                    if (ExistItem(8, 10, new NeededItem(0, 3), new NeededItem(7, 5)))
                        StartCoroutine(MoveTo(go.transform.position.x, action, delegate () { ConsumeItem(8, 10, new NeededItem(0, 3), new NeededItem(7, 5)); BasicData.Instance.buildExp += 3; }));
                    break;
                case 8:
                    if (ExistItem(3, 2, new NeededItem(0, 3)))
                        StartCoroutine(MoveTo(go.transform.position.x, action, delegate () { ConsumeItem(3, 2, new NeededItem(0, 3)); }));
                    break;
                case 11:
                    if (ExistItem(8, 10, new NeededItem(0, 3), new NeededItem(7, 10)))
                        StartCoroutine(MoveTo(go.transform.position.x, action, delegate () { ConsumeItem(8, 10, new NeededItem(0, 3), new NeededItem(7, 10)); BasicData.Instance.buildExp += 8; }));
                    break;
                case 12:
                    if (ExistItem(10, 10, new NeededItem(0, 5), new NeededItem(5, 5)))
                        StartCoroutine(MoveTo(go.transform.position.x, action, delegate () { ConsumeItem(10, 10, new NeededItem(0, 5), new NeededItem(5, 5)); BasicData.Instance.buildExp += 8; }));
                    break;
                case 13:
                    if (ExistItem(4, 2, new NeededItem(0, 5), new NeededItem(7, 3)))
                        StartCoroutine(MoveTo(go.transform.position.x, action, delegate () { ConsumeItem(4, 2, new NeededItem(0, 5), new NeededItem(7, 3)); BasicData.Instance.buildExp += 5; }));
                    break;
                case 14:
                    if (ExistItem(12, 12, new NeededItem(0, 8)))
                        StartCoroutine(MoveTo(go.transform.position.x, action, delegate () { ConsumeItem(12, 12, new NeededItem(0, 8)); BasicData.Instance.buildExp += 15; }));
                    break;
                case 15:
                    if (ExistItem(8, 8, new NeededItem(0, 5)))
                        StartCoroutine(MoveTo(go.transform.position.x, action, delegate () { ConsumeItem(8, 8, new NeededItem(0, 5)); BasicData.Instance.buildExp += 10; }));
                    break;
                case 16:
                    if (ExistItem(4, 2, new NeededItem(0, 2), new NeededItem(7, 1)))
                        StartCoroutine(MoveTo(go.transform.position.x, action, delegate () { ConsumeItem(4, 2, new NeededItem(0, 2), new NeededItem(7, 1)); BasicData.Instance.buildExp += 3; }));
                    break;
                case 17:
                    if (ExistItem(12, 17, new NeededItem(0, 5), new NeededItem(5, 5)))
                        StartCoroutine(MoveTo(go.transform.position.x, action, delegate () { ConsumeItem(12, 17, new NeededItem(0, 5), new NeededItem(5, 5)); BasicData.Instance.buildExp += 20; }));
                    break;
                case 18:
                    if (ExistItem(5, 7, new NeededItem(5, 5)))
                        StartCoroutine(MoveTo(go.transform.position.x, action, delegate () { ConsumeItem(5, 7, new NeededItem(5, 5)); BasicData.Instance.buildExp += 10; }));
                    break;
                case 19:
                    if (ExistItem(5, 7, new NeededItem(5, 5)))
                        StartCoroutine(MoveTo(go.transform.position.x, action, delegate () { ConsumeItem(5, 7, new NeededItem(5, 5)); BasicData.Instance.buildExp += 10; }));
                    break;
                case 20:
                    if (ExistItem(4, 2, new NeededItem(0, 2), new NeededItem(18, 1)))
                        StartCoroutine(MoveTo(go.transform.position.x, action, delegate () { ConsumeItem(4, 2, new NeededItem(0, 2), new NeededItem(18, 1)); BasicData.Instance.buildExp += 7; }));
                    break;
                case 21:
                    if (ExistItem(4, 3, new NeededItem(0, 5), new NeededItem(18, 1)))
                        StartCoroutine(MoveTo(go.transform.position.x, action, delegate () { ConsumeItem(4, 3, new NeededItem(0, 5), new NeededItem(18, 1)); BasicData.Instance.buildExp += 10; }));
                    break;
                default:
                    StartCoroutine(MoveTo(go.transform.position.x, action));
                    break;
            }
        }
    }
    public void OnBuildingClick(int buildId)
    {
        switch (buildId)
        {
            case 3:
                if(ExistItem(5, 10, new NeededItem(0, 2),new NeededItem(7,2)))
                {
                    StartCoroutine(StartBuild(delegate () { Instantiate(buildList[buildId],Player.position,Quaternion.Euler(Vector3.zero)); ConsumeItem(5, 10, new NeededItem(0, 2), new NeededItem(7, 2)); }));
                }
                break;
            case 4:
                if (ExistItem(5, 10, new NeededItem(0, 1), new NeededItem(7, 2)))
                {
                    StartCoroutine(StartBuild(delegate () { Instantiate(buildList[buildId], Player.position, Quaternion.Euler(Vector3.zero)); ConsumeItem(5, 10, new NeededItem(0, 1), new NeededItem(7, 2)); }));
                }
                break;
            case 5:
                if (ExistItem(5, 10, new NeededItem(0, 2), new NeededItem(7, 2)))
                    StartCoroutine(StartBuild(delegate () { Instantiate(buildList[buildId], Player.position, Quaternion.Euler(Vector3.zero)); ConsumeItem(5, 10, new NeededItem(0, 2), new NeededItem(7, 2)); }));
                break;
            case 6:
                if (ExistItem(0, 5, new NeededItem(0, 2)))
                    StartCoroutine(StartBuild(delegate () { Instantiate(buildList[buildId], Player.position, Quaternion.Euler(Vector3.zero)); ConsumeItem(0, 5, new NeededItem(0, 2)); }));

                break;
            case 7:
                if (ExistItem(2, 3, new NeededItem(19, 2), new NeededItem(16, 1)))
                    StartCoroutine(StartBuild(delegate () { Instantiate(buildList[buildId], Player.position, Quaternion.Euler(Vector3.zero)); ConsumeItem(2, 3, new NeededItem(19, 2),new NeededItem(16,1)); }));
                break;
            case 9:
                if (ExistItem(2, 3, new NeededItem(17, 3)))
                    StartCoroutine(StartBuild(delegate () { Instantiate(buildList[buildId], Player.position, Quaternion.Euler(Vector3.zero)); ConsumeItem(2, 3, new NeededItem(17, 3)); }));
                break;
            case 10:
                {
                    bool nearFire = false;
                    Stove[] stoves = GameObject.FindObjectsOfType<Stove>();
                    foreach (Stove s in stoves)
                    {
                        if (Mathf.Abs(s.transform.position.x - Player.position.x) < 3.6f && s.isFired)
                            nearFire = true;
                    }
                    if (nearFire && ExistItem(0, 0, new NeededItem(6, 1)))
                        StartCoroutine(StartBuild(delegate() { bagController.GetItem(11); ConsumeItem(0, 0, new NeededItem(6, 1)); }));
                }
                break;
        }
    }


    public IEnumerator StartBuild(params Action[] action)
    {
        float tolerace = 0.2f;
        float oriX = Player.position.x;
        buildProcess.gameObject.SetActive(true);
        while(true)
        {
            if (buildProcess.fillAmount >= 1)
                break;
            if (Input.anyKeyDown||Mathf.Abs(Player.position.x-oriX)>tolerace)
            {
                buildProcess.fillAmount = 0;
                goto end;
            }
            yield return null;
            buildProcess.fillAmount += 0.02f;
        }
        foreach (Action a in action)
            a?.Invoke();
        buildProcess.fillAmount = 0;
        end:
        buildProcess.gameObject.SetActive(false);
    }
    IEnumerator MoveTo(float targetX,params Action[] action)
    {
        float toletance = 0.5f;
        if (targetX - Player.position.x > 0)
            Player.eulerAngles = Vector3.zero;
        else
            Player.eulerAngles = new Vector3(0, 180, 0);
        while (Mathf.Abs(Player.position.x-targetX) >toletance)
        {
            if (Input.anyKeyDown) goto end;
            playerController.nowSpeed = targetX - Player.position.x > 0 ? 1 : -1;
            //Player.position = new Vector2(Mathf.Lerp(Player.position.x, targetX, t), Player.position.y);
            yield return null;
        }
        Player.position = new Vector2(targetX, Player.position.y);
        playerController.nowSpeed = 0;
        StartCoroutine(StartBuild(action));
        end:
        { }
    }
    public void OnPointerIn(int id)
    {
        
        buildId = id;
        noticePannel.transform.position = new Vector2(GetOverUI().position.x, noticePannel.transform.position.y);
        noticePannel.GetComponentInChildren<Text>().text = buildingDescription[id].Replace('n', '\n');
        noticePannel.SetActive(true);
    }
    public void OnPointerOut()
    {
        noticePannel.SetActive(false);
    }
    
}
