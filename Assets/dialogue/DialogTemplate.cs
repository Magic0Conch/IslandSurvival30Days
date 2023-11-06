using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class DialogTemplate : PlayableBehaviour
{
    public string characteName;
    public string dialogueLine;
    public int dialogueSize = 14;

    public bool hasToPause = false;

    private bool clipPlayed = false;
    private bool pauseScheduled = false;
    private PlayableDirector director;

    public override void OnPlayableCreate(Playable playable)
    {
        director = (playable.GetGraph().GetResolver() as PlayableDirector);
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (!clipPlayed && info.weight > 0f)
        {
            GameManager.Instance.SetDialogue(characteName, dialogueLine, dialogueSize);
            if (Application.isPlaying)
            {
                if (hasToPause) pauseScheduled = true;
            }

            clipPlayed = true;
        }
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (pauseScheduled)
        {
            pauseScheduled = false;
            GameManager.Instance.PauseTimeline(director);
        }
        else
        {

            GameManager.Instance.ToggleDialoguePanel(false);
        }
        clipPlayed = false;
    }
}