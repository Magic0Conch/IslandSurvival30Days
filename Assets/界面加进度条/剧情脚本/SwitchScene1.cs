using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class SwitchScene1 : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayableDirector director;
    loading load;
    void Start()
    {
        load = GameObject.FindObjectOfType<loading>();
        director.stopped += delegate { print("ye"); load.LoadNext(); };
    }

    // Update is called once per frame
    void Update()
    {
    }
}
