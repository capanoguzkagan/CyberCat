using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator _anim;
    public Vector3 offset;
    TrajectoryController tConroller;
    public Transform rightHandSkeleton = null;

    void Start()
    {
        _anim = GetComponent<Animator>();
        tConroller = GetComponent<TrajectoryController>();
        rightHandSkeleton = _anim.GetBoneTransform(HumanBodyBones.RightHand);
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
        if(GameManager.Instance.detectedBoolean && tConroller.arrowLR == 0)
        {
            if (GameManager.Instance.rightLeftboolean)
            {
                transform.rotation = Quaternion.Euler(0, 225, 0);
            }
            else if (!GameManager.Instance.rightLeftboolean)
            {
                transform.rotation = Quaternion.Euler(0, 135, 0);
            }
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
