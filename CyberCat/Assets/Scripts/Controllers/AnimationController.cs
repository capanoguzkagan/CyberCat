using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator _anim;
    public Vector3 offset;
    TrajectoryController tConroller;
    EnemyController eController;
    void Start()
    {
        _anim = GetComponent<Animator>();
        tConroller = GetComponent<TrajectoryController>();
        eController = GetComponent<EnemyController>();
    }

    
    void Update()
    {
        CharacterAnimationMethod();
        bodyRotation();
        //rollingAnim();
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
        else if (tConroller.arrowLR==1 && GameManager.Instance.rollingAnim)
        {
            _anim.SetBool("rollingAnimBool", true);
        }
        else
        {
            _anim.SetBool("isJump", false);
            _anim.SetBool("onWall", false);
            _anim.SetBool("RightArmBool", false);
            _anim.SetBool("LeftArmBool", false);
            _anim.SetBool("rollingAnimBool", false);
        }
        
    }
    void bodyRotation()
    {
        if (eController._detected&&GameManager.Instance.rightLeftboolean&&tConroller.arrowLR==0)
        {
            transform.rotation = Quaternion.Euler(0, 225, 0);
        }
        else if (eController._detected && !GameManager.Instance.rightLeftboolean&& tConroller.arrowLR == 0)
        {
            transform.rotation = Quaternion.Euler(0, 135, 0);
        }
    }
    public void rollingAnim()
    {
        if (GameManager.Instance.rollingAnim)
        {
            _anim.SetBool("rollingAnimBool", true);
        }
        else if (!GameManager.Instance.rollingAnim)
        {
            _anim.SetBool("rollingAnimBool", false);
        }

    }
}
