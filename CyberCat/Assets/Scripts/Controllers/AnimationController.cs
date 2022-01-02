using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator _anim;
    public Vector3 offset;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        CharacterAnimationMethod();
    }
    void CharacterAnimationMethod()
    {
        if (!(GameManager.isGround)&&!(GameManager.isWall))
        {
            _anim.SetBool("isJump", true);
        }
        else if (GameManager.isWall)
        {
            if (GameManager.Instance.RightArmBoolean)
            {
                _anim.SetBool("RightArmBool", true);
            }
            if (GameManager.Instance.LeftArmBoolean)
            {
                _anim.SetBool("LeftArmBool", true);
            }
            
        }
        else
        {
            _anim.SetBool("isJump", false);
            _anim.SetBool("onWall", false);
            _anim.SetBool("RightArmBool", false);
            _anim.SetBool("LeftArmBool", false);
        }
        
    }
    public void characterRotation()
    {
        transform.rotation = Quaternion.Euler(0, 225, 0);
    }
}
