using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    string r;
    public Transform buttonParent;
    private void Awake()
    {
        //PlayerPrefs.SetString("record", "000000111");
        r = PlayerPrefs.GetString("record", "000000000");
    }
    private void Start()
    {
        buttons = buttonParent.GetComponentsInChildren<Button>();
        for(int i = 0;i<r.Length;i++)
        {
            if (r[i] == '1')
                buttons[i].interactable = true;
        }
        BasicData.Instance.Reset();
    }
    public Text Theme, Introduction;
    public string[] themes;
    public string[] intros;
    Button[] buttons;
    public void Open(GameObject go)
    {
        go.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Close(GameObject go)
    {
        go.SetActive(false);
    }

    public void onClick(int id)
    {
        Theme.text = themes[id];
        Introduction.text = intros[id].Replace('n','\n');
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
