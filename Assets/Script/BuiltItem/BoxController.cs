using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BoxController : MonoBehaviour
{
    public Transform Panel;
    public Sprite[] sprites;
    Canvas canvas;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag.ToString() == "Player")
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.transform.gameObject.tag.ToString() == "Player")
        {
            Panel.gameObject.SetActive(false);
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[0];
             transform.GetChild(2).gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag.ToString() == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Panel.gameObject.SetActive(true);
                transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprites[1];
                transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }
    public class ItemInBox
    {
        public int id;
        public int num;
        public int endurance;
        public int max_endurance;
        public bool canPile;
        public bool isEmpty;
        public ItemInBox()
        {
            isEmpty = true;
            canPile = true;
            num = 0;
        }
    }
    public Dictionary<int, ItemInBox> dicBox = new Dictionary<int, ItemInBox>();
    ItemData itemData;
    BagController bagController;
    GameObject[] allGrids= new GameObject[9];
    RectTransform rect;
    private void Start()
    {
        canvas = transform.GetChild(1).GetComponent<Canvas>();
        canvas.worldCamera = GameObject.Find("c").transform.GetChild(0).GetComponent<Camera>();
        rect = GameObject.Find("背包栏").GetComponent<RectTransform>();
        itemData = GameObject.Find("Engine").GetComponent<ItemData>();
        for (int i = 0; i < 9; i++)
            dicBox.Add(i, new ItemInBox());
        bagController = GameObject.Find("UI").GetComponent<BagController>();
        for (int i = 0; i < 9; i++)
        {
            allGrids[i] = Panel.GetChild(i).gameObject;
        }
    }

    public void Show()
    {
        if (bagController == null)
        {
            bagController = GameObject.Find("UI").GetComponent<BagController>();
            print(bagController);
        }
        foreach (GameObject go in allGrids)
        {
            go.GetComponent<Image>().sprite = null;
            go.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            go.transform.GetChild(0).gameObject.SetActive(false);
            go.transform.GetChild(1).gameObject.SetActive(false);
        }
        for(int i = 0;i<9;i++)
        {
            allGrids[i].GetComponent<Image>().sprite = itemData.itemDict[dicBox[i].id].sprite;
            if (dicBox[i].isEmpty) continue;
            allGrids[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            if (dicBox[i].canPile)//显示数量
            {
                allGrids[i].transform.GetChild(0).gameObject.SetActive(true);
                allGrids[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dicBox[i].num.ToString();
            }
            else//显示耐久
            {
                allGrids[i].transform.GetChild(1).gameObject.SetActive(true);
                allGrids[i].transform.GetChild(1).GetChild(0).GetComponent<Image>().fillAmount = dicBox[i].endurance * 1.0f / dicBox[i].max_endurance;
            }
        }
    }
    public void EndDrag(Transform nowGrid)
    {
        if (bagController == null)
        {
            bagController = GameObject.Find("UI").GetComponent<BagController>();
        }
        if (bagController.dragId < 0)   return;
        int boxGridIndex = int.Parse(nowGrid.gameObject.name.ToString());
        BagController.GridItem gridItemInBag = bagController.itemList[bagController.dragId];
        int itemId = gridItemInBag.id; //根据dragid获取物品id;

        if(dicBox[boxGridIndex].isEmpty)
        {
            dicBox[boxGridIndex].id = itemId;
            dicBox[boxGridIndex].isEmpty = false;
            if (gridItemInBag.hasEndurance)//如果是不能堆叠的
            {
                dicBox[boxGridIndex].max_endurance = itemData.itemDict[itemId].endurance;
                dicBox[boxGridIndex].endurance = gridItemInBag.endurance;
                dicBox[boxGridIndex].canPile = false;
            }
            else
            {
                dicBox[boxGridIndex].num = gridItemInBag.number;
                dicBox[boxGridIndex].canPile = true;
            }
        }
        else
        {
            if (gridItemInBag.hasEndurance || dicBox[boxGridIndex].id != itemId) return;
            dicBox[boxGridIndex].num += gridItemInBag.number;
        }
        bagController.itemList.Remove(gridItemInBag);
        Show();
        bagController.UpdateProp();
        bagController.UpdateBag();
    }

    public void beginDrag(Transform nowGrid)
    {
        if (nowGrid.GetComponent<Image>().sprite == null|| nowGrid.GetComponent<Image>().color.a==0) return;
        int id = int.Parse(nowGrid.gameObject.name.ToString());
        int ItemId = dicBox[id].id;
        bagController.dragItem.SetActive(true);
        bagController.dragItem.GetComponent<Image>().sprite = itemData.itemDict[ItemId].sprite;
        BasicData.dragedItemInfo.canPile = itemData.itemDict[ItemId].canPile;
        BasicData.dragedItemInfo.maxendurance = itemData.itemDict[ItemId].endurance;
        BasicData.dragedItemInfo.num = dicBox[id].num;
        BasicData.dragedItemInfo.endurance = dicBox[id].endurance;
        BasicData.dragedItemInfo.itemId = ItemId;
    }

    public void OnDrag()
    {
        bagController.dragItem.transform.position = Input.mousePosition;
             
    }
    public void EndDrag(int nowGridId)
    {

        bagController.dragItem.SetActive(false);
        if(RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition))
            dicBox[nowGridId].isEmpty = true;
        Show();
    }
}
