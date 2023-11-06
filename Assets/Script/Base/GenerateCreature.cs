using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCreature : MonoBehaviour
{
    public Transform player;
    public GameObject[] monsterList;
    public float[] weight;
    public float generateInterval = 8f;
    public float maxMapHeight = 500f;
    public int max_creatureNum = 8;
    public Vector2 range;
    float totalWeight = 0;
    int len;
    int nowNum = 0;
    float[] wrecord;
    IEnumerator Generate()
    {
        Vector2 pos = Vector2.up * maxMapHeight;
        WaitForSeconds waitForSeconds = new WaitForSeconds(generateInterval);
        int layerMask = 1 << 11;
        while(true)
        {
            pos = Vector2.up * maxMapHeight;
            if (BasicData.Instance.ecologicalValue>150)
            {
                weight[0] = 2;//du
                weight[1] = 1;//zhu
                weight[2] = 1;//yang
                max_creatureNum = 8;
            }
            else if(BasicData.Instance.ecologicalValue>100)
            {

                weight[0] = 1.5f;//du
                weight[1] = 1;//zhu
                weight[2] = 1;//yang
                max_creatureNum = 6;
            }
            else if(BasicData.Instance.ecologicalValue > 50)
            {
                max_creatureNum = 5;
                weight[0] = 1;//du
                weight[1] = 1;//zhu
                weight[2] = 1;//yang
            }
            else if(BasicData.Instance.ecologicalValue > 20)
            {
                max_creatureNum = 5;
                weight[0] = 0.7f;//du
                weight[1] = 1;//zhu
                weight[2] = 1;//yang
            }
            else
            {
                max_creatureNum = 4;
                weight[0] = 0.4f;//du
                weight[1] = 1;//zhu
                weight[2] = 1;//yang
            }
            yield return waitForSeconds;
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
            nowNum = gos.Length;
            if (BasicData.Instance.noEnemy)
            {
                foreach(GameObject go in gos)
                {
                    Destroy(go.gameObject);
                }
            }
            gos = GameObject.FindGameObjectsWithTag("Dudu");
            nowNum += gos.Length;
            if (BasicData.Instance.noEnemy)
            {
                foreach(GameObject go in gos)
                {
                    Destroy(go.transform.parent.gameObject);
                }
                continue;
            }
            if (nowNum > max_creatureNum) continue;
            float rdx = Random.Range(player.position.x- range.y, player.position.x+range.y);
            while(rdx>player.position.x - range.x&& rdx < player.position.x + range.x)
            {
                rdx = Random.Range(player.position.x - range.y, player.position.x + range.y);
            }
            pos.x = rdx;
            print(rdx);
            RaycastHit2D hit2D = Physics2D.Raycast(pos, Vector2.down, 1000f, layerMask);
            if (hit2D==false|| hit2D.collider.gameObject.tag == "cave") continue;
            pos = hit2D.point;
            print("在x=" + pos.x + "y=" + pos.y + "生成了一个生物");
            float monsterIndex = Random.Range(0f, totalWeight);
            for(int i = 0;i<len;i++)
            {
                if(monsterIndex<wrecord[i])
                {
                    GameObject go = Instantiate(monsterList[i]);
                    go.transform.position = pos + Vector2.up * 100f;
                    go.SetActive(true);
                    break;
                }
            }

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        len = monsterList.Length;
        wrecord = new float[len];
        for (int i = 0; i < len; i++)
        {
            totalWeight += weight[i];
            wrecord[i] = totalWeight;
        }
        StartCoroutine(Generate());
    }
}
