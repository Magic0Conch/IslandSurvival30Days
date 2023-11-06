using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class feed : MonoBehaviour
{
    float coolTime = 0.3f;
    bool canEnter = true;
    public GameObject shit;
    public BagController bg; 
    public Image buildProcess;
    public Transform Player;
    public Animal animal;
    IEnumerator Cool()
    {
        canEnter = false;
        yield return new WaitForSeconds(coolTime);
        canEnter = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "Player" && bg.ExistItem(1, 1))
            transform.GetChild(0).gameObject.SetActive(true);
    }
    private void FixedUpdate()
    {
        if (animal.nowHp < animal.max_hp)
        {

            transform.GetChild(0).gameObject.SetActive(false);
            Destroy(this);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
            transform.GetChild(0).gameObject.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!canEnter) return;
        if (collision.gameObject.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E)&&bg.ExistItem(1,1))
            {
                StartCoroutine(Cool());
                StartCoroutine(StartBuild(delegate() { Instantiate(shit, transform.position, Quaternion.Euler(Vector3.zero)); Destroy(transform.parent. gameObject); bg.ConsumeItem(1, 1); }));
            }
        }
    }

    public IEnumerator StartBuild(params Action[] action)
    {
        float tolerace = 0.2f;
        float oriX = Player.position.x;
        buildProcess.gameObject.SetActive(true);
        while (true)
        {
            if (buildProcess.fillAmount >= 1)
                break;
            if (Mathf.Abs(Player.position.x - oriX) > tolerace)
            {
                buildProcess.fillAmount = 0;
                goto end;
            }
            yield return null;
            buildProcess.fillAmount += 0.02f;
        }
        foreach (Action a in action)
            a?.Invoke();
        buildProcess.fillAmount = 0;
        end:
        buildProcess.gameObject.SetActive(false);
    }
}
