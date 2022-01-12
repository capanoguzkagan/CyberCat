using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class DogAnimation : MonoBehaviour
{
    EnemyController enemyController;
    Animator _animator;
    TwoBoneIKConstraint DogLeftHandChain;
    MultiAimConstraint DogRightHandAim;
    void Start()
    {
        enemyController = this.gameObject.transform.GetChild(2).GetComponent<EnemyController>();
        _animator = GetComponent<Animator>();
        DogLeftHandChain = this.gameObject.transform.GetChild(3).gameObject.transform.GetChild(1).GetComponent<TwoBoneIKConstraint>();
        DogLeftHandChain.weight = 0;
        DogRightHandAim = this.gameObject.transform.GetChild(3).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<MultiAimConstraint>();
        DogRightHandAim.weight = 0;
    }

    
    void Update()
    {
        DogDetectedPlayer();
    }
    void DogDetectedPlayer()
    {
        
        if (enemyController._detected)
        {
            _animator.SetBool("DogDetectPlayer", true);
            this.DogLeftHandChain.weight = 1;
            this.DogRightHandAim.weight = 1;
        }
        else if (!enemyController._detected)
        {
            _animator.SetBool("DogDetectPlayer", false);
            this.DogLeftHandChain.weight = 0;
            this.DogRightHandAim.weight = 0;
        }
    }

}
