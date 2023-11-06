using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text charNameText, dialogueLineText;
    public GameObject Continue, dialoguePanel;
    public KeyCode dialogKey = KeyCode.E;

    enum GameMode
    {
        Gameplay,
        DialogueMoment
    }
    private PlayableDirector activeDirector;
    GameMode gameMode = GameMode.Gameplay;
    static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {

                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
        set { }
    }
    // Start is called before the first frame update    
    public void PauseTimeline(PlayableDirector whichOne)
    {
        Continue.SetActive(true);
        activeDirector = whichOne;
        activeDirector.Pause();
        gameMode = GameMode.DialogueMoment; //InputManager will be waiting for a spacebar to resume
        //UIManager.Instance.TogglePressSpacebarMessage(true);
    }
    public void SetDialogue(string charName, string lineOfDialogue, int sizeOfDialogue)
    {
        Continue.SetActive(false);
        charNameText.text = charName;
        dialogueLineText.text = lineOfDialogue;
        dialogueLineText.fontSize = sizeOfDialogue;
        dialoguePanel.SetActive(true);
    }
    public void ToggleDialoguePanel(bool state)
    {
        dialoguePanel.SetActive(state);
    }
    private void Update()
    {
        if (dialoguePanel.activeInHierarchy)
        {
            if (Input.GetKeyDown(dialogKey) && gameMode == GameMode.DialogueMoment)
            {
                activeDirector.Play();
                dialoguePanel.SetActive(false);
                gameMode = GameMode.Gameplay;
            }
        }
    }
}
