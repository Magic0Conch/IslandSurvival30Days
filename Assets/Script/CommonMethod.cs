using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonMethod : MonoBehaviour
{
    public GameObject hintPanel;
    public GameObject hintPanel2;
    public Transform UI;
    public Text dialogText;
    public Image dialogImg;
    public Text imgText;
    static CommonMethod _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = new CommonMethod();
            _instance.UI = GameObject.Find("UI").transform;
            _instance.hintPanel = _instance.UI.GetChild(0).gameObject;
            _instance.dialogText = _instance.hintPanel.GetComponentInChildren<Text>(true);
            _instance.hintPanel2 = _instance.UI.GetChild(1).gameObject;
            _instance.dialogImg = _instance.hintPanel2.transform.GetChild(1).GetComponent<Image>();
            _instance.imgText = _instance.hintPanel2.GetComponentInChildren<Text>(true);

        }
    }

    public static CommonMethod Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CommonMethod();
                _instance.UI = GameObject.Find("UI").transform;
                _instance.hintPanel = _instance.UI.GetChild(0).gameObject;
                _instance.dialogText = _instance.hintPanel.GetComponentInChildren<Text>(true);
                _instance.hintPanel2 = _instance.UI.GetChild(1).gameObject;
                _instance.dialogImg = _instance.hintPanel2.transform.GetChild(1).GetComponent<Image>();
                _instance.imgText= _instance.hintPanel2.GetComponentInChildren<Text>(true);
                
            }
            return _instance;
        }
    }

    

    public void ShowDialog(string content)
    {
        dialogText.text = content.Replace('n','\n');
        GameObject go = Instantiate(hintPanel, UI);
        go.SetActive(true);
    }

    public void ShowImg(Sprite img,string content = "")
    {
        dialogImg.sprite = img;
        imgText.text = content.Replace('n', '\n');
        GameObject go = Instantiate(hintPanel2, UI);
        go.SetActive(true);
    }
}
