using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepPlayer : MonoBehaviour
{
    public void OnStep()
    {
        AudioManager.instance.Step();
    }
}
