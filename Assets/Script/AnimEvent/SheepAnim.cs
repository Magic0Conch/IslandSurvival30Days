using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepAnim : MonoBehaviour
{
    public void AttackFrameActive()
    {
        BasicData.Instance.playerHp -= 10;
    }
}
