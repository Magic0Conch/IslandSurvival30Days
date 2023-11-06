using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagController : MonoBehaviour
{
    public ItemData itemData;
    public Transform bagTransform;
    public Transform propTransform;
    public Button bagPannelButton;
    Transform[] childTransform;
    Transform[] childPropTransform;
    [HideInInspector]
    public int dragId;
    int propGridId = -1;
    public GameObject dragItem;
    public GameObject chooseSquare;
    public List<GridItem> itemList;
    public  int[] propsFromItemList = new int[5] { 0,1,2,3,4};
    float buildPannelEmergeDistance;
    bool isMirroed = false;
    public GameObject ItemShowImage;

  
    IEnumerator ShowItemAfterTime(GameObject go,float interval)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(interval);
        go.SetActive(false);
    }
   
    public class GridItem
    {
        public int id;
        public int number;
        public bool hasEndurance = false;
        public int endurance;
        public GridItem(int id,int number)
        {
            this.id = id;
            this.number = number;
        }
        public GridItem(int id)
        {
            this.id = id;
            hasEndurance = true;
        }
        public void AddItem()
        {
            number++;
        }
        public void AddItem(int num)
        {
            number += num;
        }
    }

    IEnumerator ShowBagPannel()
    {
        float targetDistance = 0f;
        float frameDistance = 0f;
        while (true)
        {
            frameDistance = targetDistance;
            targetDistance = Mathf.Clamp(targetDistance + 3, 0, buildPannelEmergeDistance);
            frameDistance = targetDistance - frameDistance;
            bagTransform.GetComponent<RectTransform>().localPosition += Vector3.left * (isMirroed ? -frameDistance : frameDistance);
            if (targetDistance == buildPannelEmergeDistance)
            {
                bagPannelButton.interactable = true;
                break;
            }
            yield return null;
        }
    }
    public bool ExistProp(int id)
    {
        for(int i = 0;i<5;i++)
        {
            if (propsFromItemList[i] >= 0 && propsFromItemList[i] < itemList.Count )
            {
                    if(itemList[propsFromItemList[i]].id == id)
                        return true;
            }
        }
                print("f");
        return false;
    }
    public void buildStrech()
    {
        bagPannelButton.interactable = false;
        isMirroed = !isMirroed;
        StartCoroutine("ShowBagPannel");
    }

    private void Start()
    {
        buildPannelEmergeDistance = bagTransform.GetComponent<RectTransform>().rect.width;
        UpdateProp();
        UpdateBag();
    }
    #region 背包
    public void BeginBagGridDrag(int bagGridIndex)
    {
        if (bagGridIndex >= itemList.Count) return;
        dragItem.SetActive(true);
        dragItem.GetComponent<Image>().sprite = itemData.itemDict[itemList[bagGridIndex].id].sprite;
        dragId = bagGridIndex;
        //childTransform[i].gameObject.GetComponent<Image>().sprite = itemData.itemDict[itemList[i - 1].id].sprite;
        //childTransform[i].GetChild(0).GetComponent<Text>().text = itemList[i - 1].number.ToString();
    }
    public void BagGridDrag()
    {
        dragItem.transform.position = Input.mousePosition;        
    }
    public void EndBagGridDrag()
    {
        dragItem.SetActive(false);
        if (propGridId == -1) return;
        if(childPropTransform==null)
            childPropTransform = propTransform.GetComponentsInChildren<Transform>();
        propsFromItemList[propGridId] = dragId;
        UpdateProp(false);
        propGridId = -1;
    }
    public void Drop()
    {
        if (!dragItem.activeInHierarchy) return;
        if(BasicData.dragedItemInfo.itemId>=0)
        {
            if(BasicData.dragedItemInfo.canPile)
                GetItem(BasicData.dragedItemInfo.itemId,BasicData.dragedItemInfo.num);
            else
            {
                GetItem(BasicData.dragedItemInfo.itemId);
                itemList[itemList.Count - 1].endurance = BasicData.dragedItemInfo.endurance;
            }

            UpdateBag();
        }
    }

    public void Robbed()
    {
        for(int i = 0;i<itemList.Count;i++)
        {
            if(itemList[i].id == 6|| itemList[i].id == 11|| itemList[i].id == 14|| itemList[i].id == 16|| itemList[i].id == 20)
            {
                ConsumeItem(itemList[i].id, (int)(itemList[i].number*0.5f));
            }
        }
    }

    public void UseItem(int idInItemList)
    {
        //if(itemData.itemDict[itemList[idInItemList].id].itemType != ItemData.ItemType.Tool)
        //{
        //    ItemShowImage.GetComponent<Image>().sprite = itemData.itemDict[itemList[idInItemList].id].sprite;
        //    StartCoroutine(ShowItemAfterTime(ItemShowImage, 0.4f));
        //}
        try
        {
            if (!itemData.itemDict[itemList[idInItemList].id].canUse) return; 

        }
        catch
        {
            return;
        }
        itemData.itemDict[itemList[idInItemList].id].action?.Invoke();
        if (itemList[idInItemList].hasEndurance)
        {
            if(--itemList[idInItemList].endurance<=0)
            {
                itemList.RemoveAt(idInItemList);
            }
        }
        else
        {
            if(--itemList[idInItemList].number<=0)
            {
                itemList.RemoveAt(idInItemList);
            }
        }
        //print("use" + itemList[idInItemList].id + "remain:" + itemList[idInItemList].number);
        UpdateBag();
        //UpdateProp();
    }

    void GetBagInfo()
    {
        itemList = new List<GridItem>();
        childTransform = bagTransform.GetComponentsInChildren<Transform>();
    }


    public bool ConsumeItem(int id,int num)
    {
        if (itemList == null)
            GetBagInfo();
        int listLength = itemList.Count;
        for (int i = 0; i < listLength; i++)
        {
            if (itemList[i].id == id&&!itemList[i].hasEndurance&&num<= itemList[i].number)
            {
                itemList[i].number-=num;
                if(itemList[i].number==0)
                    itemList.RemoveAt(i);
                UpdateBag();
                return true;
            }
            if(itemList[i].id == id && itemList[i].hasEndurance)
            {
                itemList.RemoveAt(i);
                UpdateBag();
                return true;
            }
        }
        UpdateBag();
        return false;
    }
    public bool existSpace()
    {
        if (itemList.Count < 10)
            return true;
        return false;
    }
    public bool ExistItem(int id,int num)
    {
        if (itemList == null)
            GetBagInfo();
        int listLength = itemList.Count;
        for (int i = 0; i < listLength; i++)
        {
            if (itemList[i].id == id&&!itemList[i].hasEndurance && num <= itemList[i].number)
                return true;
            if (itemList[i].id == id && itemList[i].hasEndurance) return true;
        }
        return false;
    }
    public int featherCodeTop()
    {
        if (itemList == null)
            GetBagInfo();
        int listLength = itemList.Count;
        for (int i = 0; i < listLength; i++)
        {
            if (itemList[i].id == 15)
                return itemList[i].number;
        }
        return -1;
        
    }
    public bool GetItem(int id,int num = 1)
    {
        
        if (itemList==null)
            GetBagInfo();
        int listLength = itemList.Count;
        bool flag = false;
        if (itemData.itemDict[id].canPile)
        {
            for(int i = 0;i<listLength;i++)
            {
                if(itemList[i].id==id)
                {
                    itemList[i].AddItem(num);
                    flag = true;
                }
            }
            if (!flag&&itemList.Count >= 10) return false;
            if (!flag)
                itemList.Add(new GridItem(id, num));
        }
        else
        {
            if (itemList.Count >= 10) return false;
            GridItem gridItem = new GridItem(id);
            if (id == 15)
                gridItem.number = num;
            gridItem.endurance = itemData.itemDict[id].endurance;
            itemList.Add(gridItem);
        }
        UpdateBag();
        return true;
    }

    public void UpdateBag()
    {
        if (itemList == null)
            GetBagInfo();
        int listLength = itemList.Count;
        
        for (int i = 1; i <= 10; i++)
        {
            childTransform[i].gameObject.GetComponent<Image>().sprite = null;
            childTransform[i].gameObject.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            childTransform[i].GetChild(0).gameObject.SetActive(false);
            childTransform[i].GetChild(1).gameObject.SetActive(false);
        }
        for(int i = 1;i<=listLength;i++)
        {
            childTransform[i].gameObject.GetComponent<Image>().sprite = itemData.itemDict[itemList[i-1].id].sprite;
            childTransform[i].gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            if(itemData.itemDict[itemList[i - 1].id].hasEndurance)
            {
                Image fill = childTransform[i].GetChild(1).GetChild(0).GetComponent<Image>();
                fill.fillAmount = itemList[i - 1].endurance*1.0f / itemData.itemDict[itemList[i - 1].id].endurance;
                childTransform[i].GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                childTransform[i].GetChild(0).GetComponent<Text>().text = itemList[i-1].number.ToString();
                childTransform[i].GetChild(0).gameObject.SetActive(true);
            }
        }
        UpdateProp();
    }

    private void OnEnable()
    {
        UpdateBag();
    }
    #endregion
    #region 道具栏
    public void UpdateProp(bool remain = true)
    {
        if (childPropTransform == null)
            childPropTransform = propTransform.GetComponentsInChildren<Transform>();
        for (int i = 0;i<5;i++)
        {
            if(propsFromItemList[i]>=0)
            {
                Sprite targetSp = childPropTransform[i + 1].gameObject.GetComponent<Image>().sprite;
                childPropTransform[i + 1].gameObject.GetComponent<Image>().sprite = null;
                childPropTransform[i + 1].gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                childPropTransform[i + 1].GetChild(0).gameObject.SetActive(false);
                childPropTransform[i + 1].GetChild(1).gameObject.SetActive(false);
                if(targetSp!=null&&remain)
                {
                    propsFromItemList[i] = -1;
                    try
                    {
                        for (int j = 0;j< itemList.Count;j++)
                        {
                            if(itemData.itemDict[itemList[j].id].sprite == targetSp&& (childTransform[j+1].GetChild(0).gameObject.activeInHierarchy|| childTransform[j + 1].GetChild(1).gameObject.activeInHierarchy))
                            {
                                if ((itemList[j].number == 0 && !itemList[j].hasEndurance) || (itemList[j].endurance <= 0) && itemList[j].hasEndurance) break;
                                propsFromItemList[i] = j;
                                break;
                            }
                        }

                    }
                    finally { };

                }
                if (propsFromItemList[i] < 0) continue;
                childPropTransform[i + 1].gameObject.GetComponent<Image>().sprite = itemData.itemDict[itemList[propsFromItemList[i]].id].sprite;
                childPropTransform[i + 1].gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                if(itemData.itemDict[itemList[propsFromItemList[i]].id].hasEndurance)
                {
                    childPropTransform[i + 1].GetChild(1).gameObject.SetActive(true);
                    childPropTransform[i + 1].GetChild(1).GetChild(0).GetComponent<Image>().fillAmount = itemList[propsFromItemList[i]].endurance*1.0f/ itemData.itemDict[itemList[propsFromItemList[i]].id].endurance;
                }
                else
                {
                    childPropTransform[i + 1].GetChild(0).GetComponent<Text>().text = itemList[propsFromItemList[i]].number.ToString();
                    childPropTransform[i + 1].GetChild(0).gameObject.SetActive(true);

                }
            }
        }
    }

    public void PropBarClick(int id)
    {
        BasicData.choosePropId = id;
        chooseSquare.transform.position = childPropTransform[id + 1].position;
    }
    #endregion
    #region 道具栏与背包栏交互


    public void OnPointerEnter(int propGridIndex)
    {
        if (!dragItem.activeInHierarchy)
            return;
        propGridId = propGridIndex;
    }

    #endregion
}
