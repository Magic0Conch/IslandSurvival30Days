using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEngine : MonoBehaviour
{
    public GameObject tree;
    public Transform fatherTransform;

    int produceNum = 50;
    float leftBorder;
    float rightBorder;
    float height = 80;

    void Start()
    {
        leftBorder = -300;    
        rightBorder = 300;
        
        for (int i = 0; i < produceNum; i++)
        {
            float rdx = Random.Range(leftBorder, rightBorder);
            Vector2 pos = new Vector2(rdx, height);
            int layermask = 1 << 11;
            RaycastHit2D hit2D = Physics2D.Raycast(pos, Vector2.down, 100f, layermask);
            pos = hit2D.point;
            GameObject go = Instantiate(tree);
            tree.transform.position = pos + Vector2.up * 1.5f;
        }
    }
}
