using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sap : MonoBehaviour
{
    public int passDay = 1;
    int day = BasicData.Instance.date;

    public GameObject tree;

    private void Start()
    {
        day += passDay;
    }

    void Update()
    {
        if(BasicData.Instance.date>=day)
        {
            Instantiate(tree, transform.position, Quaternion.Euler(Vector3.zero));
            Destroy(gameObject);
        }
    }
}
