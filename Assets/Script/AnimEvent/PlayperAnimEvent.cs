using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayperAnimEvent : MonoBehaviour
{
    public GameObject p_0;
    public GameObject p_1;
    //public GameObject p_2;

    public void AxeAnim_end()
    {
        p_0.SetActive(true);
        p_0.GetComponent<Animator>().Play("idile-");
        p_1.SetActive(false);
    }

    public void Axe_ColiderStart()
    {
        p_1.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Axe_ColiderEnd()
    {
        p_1.transform.GetChild(0).gameObject.SetActive(false);
    }
}
