using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript : MonoBehaviour
{
    public event System.Action<float> HoldActionEvent;
    private void OnEnable()
    {
        HoldActionEvent?.Invoke(0.05f);
    }
    private void OnDisable()
    {
        HoldActionEvent?.Invoke(1);
    }
}
