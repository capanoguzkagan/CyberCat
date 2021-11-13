using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelegateScript : MonoBehaviour
{
    [SerializeField]EventScript _eventScript;
    PlayerController _playerController;
    TrajectoryController _trajectoryController;
    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _trajectoryController = FindObjectOfType<TrajectoryController>();
    }

    private void OnEnable()
    {
        _eventScript.HoldActionEvent += CharacterHoldEventMethod;
    }
    private void OnDisable()
    {
        _eventScript.HoldActionEvent -= CharacterHoldEventMethod;
    }

    private void CharacterHoldEventMethod(int sayi)
    {

        _playerController.rb.gravityScale = sayi;
    }
}