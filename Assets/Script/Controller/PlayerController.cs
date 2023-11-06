using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class PlayerController : MonoBehaviour
{
    public VolumeProfile profile;

    public KeyCode leftMove = KeyCode.A;
    public KeyCode rightMove = KeyCode.D;
    public KeyCode jump;
    public KeyCode Attack = KeyCode.Mouse0;
    float invicibleTime = 2f;
    public float maxMoveSpeed;//速度上限
    public float jumpForce;//跳跃力
    public float acceleration;//加速度
    public Transform mainCameraT;
    public GameObject bagPannel;
    public GameObject getItemParticle;
    public BagController bagController;
    public Image hpImage;
    public Image repletionImage;
    public Image EnergyImage;
    [HideInInspector]
    public float nowSpeed;//当前速度
    float rayDistance;
    private Rigidbody2D rigidbody2D;
    //GameObject Sword;
    public Animator animator0;
    public Animator animator1;
    public Animator animator2;

    public GameObject p_0;//普通
    public GameObject p_1;//axe
    public GameObject p_2;//sword
    public GameObject p_3;//squzzer
    public GameObject p_4;//torch
    [HideInInspector]
    public int currentItemId = 0;
    [HideInInspector]
    public int idInItemList = 0;
    [HideInInspector]
    public bool CameraFllow = true;
    public Vibration vibration;
    
    private void Start()
    {
        
        BasicData.Instance.playerHp = 30;
        BasicData.Instance.playerHpMax = 100;
        BasicData.Instance.playerRepletionMax = 200;
        BasicData.Instance.playerEnergyMax = 300;
        BasicData.Instance.playerRepletion = 30;
        BasicData.Instance.playerEnergy = 20;
        rigidbody2D = GetComponent<Rigidbody2D>();
        //Sword = transform.GetChild(0).gameObject;
        rayDistance = 1.4f;
    }
    private void Update()
    {
        if (BasicData.Instance.banMove) return;
        if (Input.GetKeyDown(Attack))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (idInItemList < 0 || currentItemId < 0) return;
            //print(currentItemId);
            if (bagController.itemData.itemDict[currentItemId].itemType != ItemData.ItemType.Tool)
                bagController.UseItem(idInItemList);
            else
            {
                if (p_0.activeInHierarchy)
                    bagController.UseItem(idInItemList);

            }
            if(bagController.itemData.itemDict[currentItemId].itemType ==ItemData.ItemType.Prop)
            {
                animator0.Play("eat");
            }
            switch (currentItemId)
            {
                case 2:
                    if (BasicData.Instance.isIn) break;
                    p_0.SetActive(false);
                    p_1.SetActive(true);
                    break;
                case 3:
                    if (BasicData.Instance.isIn) break;
                    p_0.SetActive(false);
                    p_3.SetActive(true);
                    break;
                case 4:
                    if (BasicData.Instance.isIn) break;
                    p_0.SetActive(false);
                    p_2.SetActive(true);
                    break;
                case 8:
                    p_4.SetActive(true);
                    p_0.SetActive(false);
                    break;
            }
        }
    }
    void FixedUpdate()
    {
        #region 玩家控制
        bool isPress = false;
        float playerScreenX = Camera.main.WorldToScreenPoint(transform.position).x;
        float InputScreenX = Input.mousePosition.x;
        //if(CameraFllow)
        //    mainCameraT.transform.position = transform.position + Vector3.up * 3.0f + Vector3.back*5;

        //if (InputScreenX > playerScreenX)
        //{
        //}
        //else if (InputScreenX < playerScreenX)
        //{
        //}
        if(!BasicData.Instance.banMove)
        {

            //if(Input.GetKeyDown(KeyCode.Escape))
            //{
            //    bagPannel.SetActive(!bagPannel.activeInHierarchy);
            //}
            if (Input.GetKey(leftMove))
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                nowSpeed = Mathf.Clamp(nowSpeed - acceleration*Time.fixedDeltaTime, -maxMoveSpeed,maxMoveSpeed);
                isPress = true;
            }
            if(Input.GetKey(rightMove))
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                nowSpeed = Mathf.Clamp(nowSpeed + acceleration*Time.fixedDeltaTime, -maxMoveSpeed,maxMoveSpeed);
                isPress = true;
            }
        
            if(!isPress)
            {
                if(nowSpeed>0)
                {
                    nowSpeed = Mathf.Clamp(nowSpeed - acceleration * Time.fixedDeltaTime * 3f, 0, maxMoveSpeed);
                }
                else if(nowSpeed<0)
                {
                    nowSpeed = Mathf.Clamp(nowSpeed + acceleration * Time.fixedDeltaTime * 3f, -maxMoveSpeed, 0);
                }
            }
            if(Input.GetKeyDown(jump)&&isGrounded())
            {
                rigidbody2D.AddForce(Vector2.up * jumpForce);
                //animator0.SetBool("isLanded", false);
            }

            transform.position+= (nowSpeed * Vector3.right);
        }
        #endregion
        try
        {
            idInItemList = bagController.propsFromItemList[BasicData.choosePropId];
            currentItemId = bagController.itemList[idInItemList].id;
        }
        catch
        {
            
        }
        
        animator0.SetFloat("nowSpeed", Mathf.Abs(nowSpeed));
        BasicData.Instance.playerHp = BasicData.Instance.playerHp > BasicData.Instance.playerHpMax ? BasicData.Instance.playerHpMax : BasicData.Instance.playerHp;
        BasicData.Instance.playerRepletion = BasicData.Instance.playerRepletion > BasicData.Instance.playerRepletionMax ? BasicData.Instance.playerRepletionMax : BasicData.Instance.playerRepletion;
        BasicData.Instance.playerEnergy = BasicData.Instance.playerEnergy > BasicData.Instance.playerEnergyMax? BasicData.Instance.playerEnergyMax : BasicData.Instance.playerEnergy;
        BasicData.Instance.playerHp = BasicData.Instance.playerHp < 0 ? 0 : BasicData.Instance.playerHp;
        BasicData.Instance.playerRepletion = BasicData.Instance.playerRepletion < 0 ? 0 : BasicData.Instance.playerRepletion;
        BasicData.Instance.playerEnergy = BasicData.Instance.playerEnergy < 0 ? 0 : BasicData.Instance.playerEnergy;
        hpImage.fillAmount = BasicData.Instance.playerHp / BasicData.Instance.playerHpMax;
        repletionImage.fillAmount = BasicData.Instance.playerRepletion / BasicData.Instance.playerRepletionMax;
        EnergyImage.fillAmount = BasicData.Instance.playerEnergy / BasicData.Instance.playerEnergyMax;
        if(BasicData.Instance.playerHp<=30)
        {
            profile.components[12].active = true;
        }
        else
        {
            profile.components[12].active = false;
        }

        if(BasicData.Instance.ecologicalValue <= 30)
        {

            profile.components[3].active = true;
            profile.components[7].active = true;
        }
        else
        {
            profile.components[3].active = false;
            profile.components[7].active = false;

        }
        if (BasicData.Instance.playerRepletion <= 0)
        {
            BasicData.Instance.playerHp -= 0.02f;
        }
        if(BasicData.Instance.playerHp<=0)
        {
            if(BasicData.Instance.isInBoss&&!BasicData.Instance.jiu&&BasicData.Instance.ecologicalValue>30)
            {
                BasicData.Instance.jiu = true;
                BasicData.Instance.playerHp = BasicData.Instance.playerHpMax;
                CommonMethod.Instance.ShowDialog("精灵在你奄奄一息的时候救助了你。");
                return;
            }
            BasicData.Instance.banMove = true;
            Destroy(GetComponent<BoxCollider2D>());
            p_0.SetActive(true);
            p_1.SetActive(false);
            p_2.SetActive(false);
            p_3.SetActive(false);
            p_4.SetActive(false);
            animator0.Play("Die");
            //SceneManager.LoadScene("GameOver");
        }
        if(BasicData.Instance.playerRepletion==10)
        {
            CommonMethod.Instance.ShowDialog("再不吃东西，你就要饿死了");
        }
        if (BasicData.Instance.playerEnergy <= 50)
        {
            maxMoveSpeed = 0.069f;
        }
        else
            maxMoveSpeed = 0.12f;
    }

    bool isGrounded()
    {
        int layerMask = 1 << 11;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.down, rayDistance, layerMask);
        Debug.DrawLine(transform.position, transform.position + Vector3.down*rayDistance, Color.yellow);
        if (raycastHit2D)
            return true;
        else
            return false;
    }

    public void hurt()
    {
        p_0.SetActive(true);
        p_1.SetActive(false);
        p_2.SetActive(false);
        p_3.SetActive(false);
        p_4.SetActive(false);
        animator0.Play("hurt");
        BasicData.Instance.banMove = true;
        vibration.StartShaking();
        StartCoroutine(invicible());
        //StartCoroutine(ShakeCamera(30, 0.5f,2));
    }

    IEnumerator invicible()
    {
        BasicData.Instance.isInvicible = true;
        yield return new WaitForSeconds(invicibleTime);
        BasicData.Instance.isInvicible = false;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}