using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator _anim;
    public Vector3 offset;
    TrajectoryController tController;
    public Transform rightHandSkeleton = null;
    public Transform leftHandSkeleton = null;
    public GameObject leftRifle;
    public GameObject rightRifle;

    void Start()
    {
        _anim = GetComponent<Animator>();
        tController = GetComponent<TrajectoryController>();
        rightHandSkeleton = _anim.GetBoneTransform(HumanBodyBones.RightHand);
    }

    
    void Update()
    {
        CharacterAnimationMethod();
        //bodyRotation();
        //rollingAnim();
    }
    void CharacterAnimationMethod()
    {
        if (!(GameManager.isGround) && !(GameManager.isWall))
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
        else if ((tController.arrowLR == 1 || tController.arrowLR==2) && GameManager.Instance.rollingAnim)
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
        if (tController.arrowLR == 2)
        {
            _anim.SetFloat("RifleIdleParam", 1);
            rightRifle.SetActive(false);
            leftRifle.SetActive(true);
        }
        else if (tController.arrowLR == 1)
        {
            _anim.SetFloat("RifleIdleParam", 0);
            rightRifle.SetActive(true);
            leftRifle.SetActive(false);

        }
    }
    private void LateUpdate()
    {
        if (tController.gunType == GunType.Rifle)
        {
            if (GameManager.Instance.mode == GameManager.RigAnimMode.inc && rightRifle.activeSelf)
            {
                rightHandSkeleton.localEulerAngles = new Vector3
                                        (rightHandSkeleton.localEulerAngles.x, 0, rightHandSkeleton.localEulerAngles.z);
            }
            else if(GameManager.Instance.mode == GameManager.RigAnimMode.inc && leftRifle.activeSelf)
            {
                leftHandSkeleton.localEulerAngles = new Vector3
                                       (leftHandSkeleton.localEulerAngles.x, 15, leftHandSkeleton.localEulerAngles.z);
            }

        }
    }
    public void bodyRotation()
    {
        if(tController.arrowLR == 0)
        {
            if (GameManager.Instance.rightLeftboolean)
            {
                transform.rotation = Quaternion.Euler(0, 225, 0);
                _anim.SetFloat("RifleJumpParam", 1);
                leftRifle.SetActive(true);
                rightRifle.SetActive(false);
            }
            else if (!GameManager.Instance.rightLeftboolean)
            {
                transform.rotation = Quaternion.Euler(0, 135, 0);
                _anim.SetFloat("RifleJumpParam", 0);
                rightRifle.SetActive(true);
                leftRifle.SetActive(false);
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
