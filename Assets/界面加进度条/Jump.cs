using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Jump : MonoBehaviour
{
    private PlayableDirector director;

    private void Start()
    {
        director = GetComponent<PlayableDirector>();
    }
    // Start is called before the first frame update

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            director.time = director.duration-1;
        }
    }
}
