using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrubs : Picking
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (!canEnter) return;
        if (collision.gameObject.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E)&& haveFruit&&buildState==BuildState.notBuilding)
            {
                //StartCoroutine(Cool());
                StartCoroutine(StartBuild(delegate () {
                    int ad = (int)((growMinutes + BasicData.Instance.nowTime) / 1440.0f);
                    day = ad + BasicData.Instance.date;
                    minutes = (growMinutes + BasicData.Instance.nowTime) - ad * 1440;
                    Instantiate(fruit, transform.position, Quaternion.Euler(Vector3.zero));
                    haveFruit = false;
                    sr.sprite = sprites[1];
                }));
            }
        }

    }
}
