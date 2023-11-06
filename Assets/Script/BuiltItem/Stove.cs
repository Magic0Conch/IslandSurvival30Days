using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Stove : MonoBehaviour
{
    bool canEnter = true;
    float coolTime = 0.3f;
    public bool isFired = true;
    public int nowFire = 1500;
    public int maxFire = 2000;
    public Transform firePartical;
    public UnityEngine.Experimental.Rendering.Universal.Light2D light;
    public bool canVanish = false;
    IEnumerator Cool()
    {
        canEnter = false;
        yield return new WaitForSeconds(coolTime);
        canEnter = true;
    }

    IEnumerator onFire()
    {
        while (true)
        {
            if (nowFire > 500)
            {
                --nowFire;
                light.intensity = nowFire * 1.0f / 2000 * 2;
                firePartical.localScale = new Vector3(1, 1, 1) * nowFire * 1.0f / 2000*2;
                yield return null;
                isFired = true;
            }
            else
            {
                light.intensity = 0;
                firePartical.localScale = Vector3.zero;
                if (canVanish)
                    Destroy(gameObject);
                isFired = false;
                yield return null;
            }
        }
    }
    BagController bag;
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (!canEnter) return;
        if (collision.gameObject.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(Cool());
                bag = GameObject.Find("UI").GetComponent<BagController>();
                if(bag.ExistItem(0,1))
                {
                    bag.ConsumeItem(0, 1);
                    nowFire = Mathf.Clamp(nowFire+500, 0, maxFire);
                }
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag.ToString() == "Player")
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.transform.gameObject.tag.ToString() == "Player")
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        StartCoroutine(onFire());
    }
}
