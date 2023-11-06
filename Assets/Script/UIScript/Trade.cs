using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Trade : MonoBehaviour
{
    // Start is called before the first frame update
    public ItemData itemData;
    BagController bg;
    public int[] from;
    public int[] to;
    int nowId;
    public Image fromImage;
    public Image toImage;
    public Button Confirm;
    public GameObject grid;
    public Transform content;
    void Awake()
    {
        itemData = GameObject.Find("Engine").GetComponent<ItemData>();
        bg = GameObject.Find("UI").GetComponent<BagController>();

        Confirm.interactable = false;
    }

    public void AddTrade(int id)
    {
        print(id);
        GameObject go = Instantiate(grid, content);
        go.transform.GetChild(0).GetComponent<Image>().sprite = itemData.itemDict[to[id]].sprite;
        go.GetComponent<Button>().onClick.AddListener(delegate { Click(id); });
    }

    private void OnEnable()
    {
        int len = to.Length;

        for(int cc  = 0;cc<len;cc++)
        {
            AddTrade(cc);
            //print(to[i]);
            //print(itemData);
            //print(go.transform.GetChild(0).GetComponent<Image>().sprite);
            //go.transform.GetChild(0).GetComponent<Image>().sprite = itemData.itemDict[to[i]].sprite;
            //go.name = i.ToString();
            //UnityAction<BaseEventData> callback = new UnityAction<BaseEventData>(delegate { Click(i); });
            //go.GetComponent<Button>().onClick = callback;

        }
    }

    public void Click(int id)
    {
        nowId = id;
        print("id"+ id);
        print("from" + from[id]);
        print("data:"+ itemData.itemDict[from[id]].sprite);
        fromImage.sprite = itemData.itemDict[from[id]].sprite;
        toImage.sprite = itemData.itemDict[to[id]].sprite;
        if (bg.ExistItem(from[nowId], 1) && bg.existSpace())
            Confirm.interactable = true;
        else
            Confirm.interactable = false;
    }

    public void ConfirmTrade()
    {
        if (bg.ExistItem(from[nowId], 1)&&bg.existSpace())
        {
            bg.ConsumeItem(from[nowId], 1);
            bg.GetItem(to[nowId]);
        }
        if (bg.ExistItem(from[nowId], 1) && bg.existSpace())
            Confirm.interactable = true;
        else
            Confirm.interactable = false;
    }
    public void Cancel(GameObject go)
    {
        go.SetActive(false);
    }
    // Update is called once per frame
    
}
