using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followingChar : MonoBehaviour
{
    GameObject _player;
    Vector3 _temp;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _temp = transform.position - _player.transform.position;
    }

    
    void Update()
    {
        transform.position = _temp + _player.transform.position;
    }
}
