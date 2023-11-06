using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _withTorch : MonoBehaviour
{
    public Transform _parent;
    public Transform _flame,p_0;
    PlayerController playerController;
    int idInList = 0;
    Animator animatorSelf;
    // Start is called before the first frame update
    void Start()
    {
        playerController = _parent.GetComponent<PlayerController>();
        idInList = playerController.idInItemList;
        animatorSelf = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animatorSelf.SetFloat("speed", Mathf.Abs(playerController.nowSpeed));
        playerController.bagController.UseItem(idInList);
        if (Mathf.Abs(_parent.transform.eulerAngles.y-180)<10)
        {
            _flame.localPosition = new Vector2(_flame.localPosition.x, -1.35f);
        }
        else
        {
            _flame.localPosition = new Vector2(_flame.localPosition.x, 1.22f);
        }
        if(playerController.currentItemId!=8)
        {
            p_0.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

    }
}
