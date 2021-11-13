using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript : MonoBehaviour
{
    public event System.Action<int> HoldActionEvent;
    PlayerController playerController;
    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    private void OnEnable()
    {
        HoldActionEvent?.Invoke(0);
    }
    private void OnDisable()
    {
        HoldActionEvent?.Invoke(1);
    }
}
