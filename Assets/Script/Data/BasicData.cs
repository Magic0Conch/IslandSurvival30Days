using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicData
{
    public static BasicData _instance;
    public static BasicData Instance
    {
        get
        {
            if(null==_instance)
            {
                _instance = new BasicData();
            }
            return _instance;
        }
    }

    public void Reset()
    {
        cameraFollow = false;
        banMove = false;
        date = 0;
        nowTime = 0.0f;
        ecologicalValue = 70;
        buildLev = 0;
        dieIndex = 0;
        lAttack = 0;
        isInBoss = false;
        isIn = false;
        noEnemy = false;
        buildExp = 0f;
        bossHp = 500;
        bossHpMax = 500;
        jiu = false;
}

public bool cameraFollow = false;
    public bool banMove = false;
    public int date = 0;
    public float bossHp = 500;
    public bool jiu = false;
    public float bossHpMax = 500;
    public float nowTime = 0.0f;
    public float playerHpMax;
    public float playerHp;
    public float playerRepletionMax;
    public float playerRepletion;
    public float playerEnergyMax;
    public float playerEnergy;
    public float ecologicalValue = 70;
    public static int choosePropId = 0;
    public static float TimeMutipleRate = 1.0f;
    public float well_beingRate = 0;
    public bool isIn = false;
    public bool isInvicible = false;
    public bool noEnemy = false;
    public float buildExp = 0f;
    public float duduIndex = 1000f;
    public int buildLev = 0;
    public int dieIndex = 0;
    public int lAttack = 0;
    public bool isInBoss = false;
    #region 脚本之间的信息交流
    public static class dragedItemInfo
    {
        public static int num;
        public static bool canPile;
        public static int endurance;
        public static int maxendurance;
        public static int itemId;
    }
    #endregion
}
