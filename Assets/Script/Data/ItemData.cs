using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    public Sprite[] sprites;
    
    /// <summary>
    /// 0原木
    /// 1种子
    /// 2斧子
    /// 3矿稿
    /// 4剑
    /// 5羊毛
    /// 6肉
    /// </summary>
    /// 

    public enum ItemType
    {
        Default,Tool,Prop,Material
    }

    public class BaseItem
    {
        public int id;
        public Sprite sprite;
        public int endurance;
        public bool hasEndurance;
        public bool canPile = true;
        public bool canUse = false;
        public ItemType itemType = ItemType.Default;
        public BaseItem(int id)
        {
            this.id = id;
        }
        public Action action = null;
        public bool use()
        {
            return true;
        }

    }

    public Dictionary<int, BaseItem> itemDict;
    
    void activeTool(int id,int endurance = 42)
    {
        itemDict[id].itemType = ItemType.Tool;
        itemDict[id].hasEndurance = true;
        itemDict[id].endurance = endurance;
        itemDict[id].canPile = false;
        itemDict[id].canUse = true;
    }

    void activeProp(int id)
    {
        itemDict[id].itemType = ItemType.Prop;
        itemDict[id].hasEndurance = false;
        itemDict[id].canPile = true;
        itemDict[id].canUse = true;
    }


    private void Awake()
    {
        itemDict = new Dictionary<int, BaseItem>();

        for(int i = 0;i<sprites.Length;i++)
        {
            BaseItem baseItem = new BaseItem(i);
            baseItem.id = i;
            baseItem.sprite = sprites[i];
            itemDict.Add(i, baseItem);
        }
        activeTool(2);
        activeTool(3);
        activeTool(4);
        itemDict[0].itemType = ItemType.Material;
        itemDict[1].itemType = ItemType.Material;
        itemDict[5].itemType = ItemType.Material;
        activeProp(6);
        itemDict[6].action = delegate () {print("meat"); BasicData.Instance.playerRepletion += 25; BasicData.Instance.playerHp -= 5;BasicData.Instance.playerEnergy += 8; };
        itemDict[7].itemType = ItemType.Material;
        activeTool(8, 10000);
        activeProp(9);
        itemDict[9].action = delegate () { print("药水"); BasicData.Instance.playerHp += 35; };
        activeProp(10);
        itemDict[10].action = delegate () { print("药膏"); BasicData.Instance.playerHp += 50; };
        activeProp(11);
        itemDict[11].action = delegate () { print("熟肉"); BasicData.Instance.playerRepletion += 45; BasicData.Instance.playerHp += 6; BasicData.Instance.playerEnergy += 20; };
        itemDict[12].itemType = ItemType.Material;
        activeProp(14);
        itemDict[14].action = delegate () { BasicData.Instance.playerHp = BasicData.Instance.playerHpMax; BasicData.Instance.playerRepletion = BasicData.Instance.playerRepletionMax; BasicData.Instance.playerEnergy = BasicData.Instance.playerEnergyMax; };
        activeTool(15);
        activeProp(16);
        itemDict[16].action = delegate () { BasicData.Instance.playerRepletion += 15; BasicData.Instance.playerHp += 3; BasicData.Instance.playerEnergy += 15; };
        activeProp(17);
        itemDict[17].action = delegate () { BasicData.Instance.playerRepletion += 2; BasicData.Instance.playerHp += 5; };
        activeProp(18);
        itemDict[18].action = delegate () { BasicData.Instance.playerHp += 3; };
        activeProp(19);
        itemDict[19].action = delegate () {  BasicData.Instance.playerRepletion += 15; BasicData.Instance.playerHp -= 3; BasicData.Instance.playerEnergy += 50; };
        activeProp(20);
        itemDict[20].action = delegate () { BasicData.Instance.playerRepletion += 20; BasicData.Instance.playerHp += 2; BasicData.Instance.playerEnergy += 18; };

    }
}
