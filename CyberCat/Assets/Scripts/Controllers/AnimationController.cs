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
    public GameObject[] pistol;

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
        if (tController.gunType == GunType.Rifle)
        {
            Debug.Log(tController.arrowLR);
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
        gunTypeSelection();
    }
    public void bodyRotation()
    {
        if(tController.arrowLR == 0)
        {
            if (GameManager.Instance.rightLeftboolean)
            {
                transform.rotation = Quaternion.Euler(0, 225, 0);
                _anim.SetFloat("RifleJumpParam", 1);
                if (tController.gunType == GunType.Rifle)
                {
                    leftRifle.SetActive(true);
                    rightRifle.SetActive(false);
                }

            }
            else if (!GameManager.Instance.rightLeftboolean)
            {
                transform.rotation = Quaternion.Euler(0, 135, 0);
                _anim.SetFloat("RifleJumpParam", 0);
                if(tController.gunType == GunType.Rifle)
                {
                    rightRifle.SetActive(true);
                    leftRifle.SetActive(false);
                }

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

    void pistolActive(bool activeBool)
    {
        pistol[0].SetActive(activeBool);
        pistol[1].SetActive(activeBool);
        leftRifle.SetActive(!activeBool);
        rightRifle.SetActive(!activeBool);
    }
    void rifleActive(bool activeBool)
    {
        if (transform.rotation == Quaternion.Euler(0, 135, 0))
        {
            rightRifle.SetActive(activeBool);
        }
        else if(transform.rotation == Quaternion.Euler(0, 225, 0))
        {
            leftRifle.SetActive(activeBool);
        }
        if (tController.arrowLR == 2)
        {
            rightRifle.SetActive(!activeBool);
            leftRifle.SetActive(activeBool);
        }
        else if(tController.arrowLR == 1)
        {
            rightRifle.SetActive(activeBool);
            leftRifle.SetActive(!activeBool);
        }
        pistol[0].SetActive(!activeBool);
        pistol[1].SetActive(!activeBool);

    }
    void gunTypeSelection()
    {
        if (tController.gunType == GunType.Pistol)
        {
            pistolActive(true);
            _anim.SetBool("PistolActive", true);
            _anim.SetBool("RifleActive", false);

        }
        else if (tController.gunType == GunType.Rifle)
        {
            rifleActive(true);
            _anim.SetBool("RifleActive", true);
            _anim.SetBool("PistolActive", false);
        }
    }
}
