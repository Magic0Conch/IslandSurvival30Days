using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Picking : MonoBehaviour
{
    public Sprite[] sprites;
    public GameObject hint;
    public GameObject fruit;

    public float growMinutes = 1440f;
    protected bool haveFruit = true;
    protected int day;
    protected float minutes;
    protected SpriteRenderer sr;
    protected PlayerController playerController;
    protected Transform Player = null;
    protected Image buildProcess;

    protected enum BuildState
    {
        building,notBuilding
    }
    protected BuildState buildState = BuildState.notBuilding;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        playerController = FindObjectOfType<PlayerController>();
        Player = playerController.transform;

        
        buildProcess = GameObject.Find("FollowPlayer/Canvas/buildProgress").GetComponent<Image>();
        StartCoroutine(Fresh());
    }

    IEnumerator Fresh()
    {
        while (true)
        {
            if ((BasicData.Instance.nowTime + BasicData.Instance.date * 1440) >= (day * 1440 + minutes))
            {
                haveFruit = true;
                if(sprites.Length>0)
                    sr.sprite = sprites[0];//有果子是0
            }
            //else
            //{
            //    haveFruit = false;
            //    if(sprites.Length>1)
            //        sr.sprite = sprites[1];//没果子是1
            //}
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(hint!=null&&haveFruit&&collision.gameObject.name=="Player")
            hint.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (!canEnter) return;
        if (collision.gameObject.name == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E) && buildState == BuildState.notBuilding&&haveFruit)
            {
                //StartCoroutine(Cool());
                StartCoroutine(StartBuild(delegate ()
                {
                    
                    Instantiate(fruit, transform.position, Quaternion.Euler(Vector3.zero));
                    Destroy(gameObject);

                }));
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
            buildState = BuildState.building;
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
        buildState = BuildState.notBuilding;
        buildProcess.gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if(hint!=null && collision.gameObject.name == "Player")
            hint.SetActive(false);
    }
}
