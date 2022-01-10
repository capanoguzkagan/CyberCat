using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Animations.Rigging;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	#region Action Events
	public event Action OnPressEvent;
	public event Action ReleaseEvent;
	public event Action ShootEnemyEvent;
	#endregion

	[SerializeField] LayerMask _layerMask;
	public PlayerInput _playerInput;
	public Vector2 pressPosition;
	Camera camera;

	public static bool isGround;
	public static bool isWall;

	//Eren-eklemeler
	private bool rightBool;
	private bool leftBool;
    public bool RightArmBoolean { get { return rightBool; } set { rightBool = value; } }
	public bool LeftArmBoolean { get { return leftBool; } set { leftBool = value; } }
	[Header("Target System")]
	public ChainIKConstraint leftShoulder = null;
	public ChainIKConstraint rightShoulder = null;
	public ChainIKConstraint rifleRLArm = null;
	public ChainIKConstraint rifleRRArm = null;
	public ChainIKConstraint rifleLLArm = null;
	public ChainIKConstraint rifleLRArm = null;
	public MultiAimConstraint headRotation = null;
	public Transform Rifletarget2=null;
	public Transform rfLeftBone= null;
	public Transform lfRightBone;

	public float aiminigSpeed=1f;
	bool rowbyrow = false;
	public bool rollingAnim = false;
	private bool rightLeftBool=false;
	public bool rightLeftboolean { get { return rightLeftBool; } set { rightLeftBool = value; } }

	public enum RigAnimMode
    {
		off,
		inc,
		dec,
    }
	public RigAnimMode mode = RigAnimMode.off;
    TrajectoryController tController;
	AnimationController animationController;
	public RaycastHit hit;
	private void OnEnable()
	{
		_playerInput.Enable();
	}
	private void OnDisable()
	{
		_playerInput.Disable();
	}

	#region Singleton
	private void SingletonThisObject()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    private void Awake()
    {
        SingletonThisObject();
		_playerInput = new PlayerInput();
		tController = GameObject.Find("Player2").transform.GetChild(1).GetComponent<TrajectoryController>();
		animationController = GameObject.Find("Player2").transform.GetChild(1).GetComponent<AnimationController>();
	}
	private void Start()
	{
		_playerInput.PlayerMovementController.Press.performed += (context) => Pressed();
		_playerInput.PlayerMovementController.Release.performed += (context) => ReleaseEvent?.Invoke();
		camera = Camera.main;
		Pressed();
	}

	private void Pressed()
	{
		pressPosition= _playerInput.PlayerMovementController.Position.ReadValue<Vector2>();
		Ray ray = camera.ScreenPointToRay(pressPosition);
		if (Physics.Raycast(ray, out hit,1000, _layerMask))
		{
			ShootEnemyEvent?.Invoke();
			Debug.Log("ShootEnemyEvent");
			//target.position = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z);
            if (tController.gunType == GunType.Pistol)
            {
				armChanging();
			}
		    

		}
		else
		{
			OnPressEvent?.Invoke();
			Debug.Log("OnPressEvent");
		}
	}

    private void FixedUpdate()
    {
        if (tController.gunType==GunType.Pistol)
        {
			rifleWeightZero();
			switch (mode)
			{
				case RigAnimMode.inc:
					if (!rightLeftboolean)
					{
						armAnimation();

					}
					else if (rightLeftboolean)
					{
						armAnimation();
					}
					break;
				case RigAnimMode.dec:
					leftShoulder.weight = Mathf.Lerp(leftShoulder.weight, 0, aiminigSpeed * Time.deltaTime);
					rightShoulder.weight = Mathf.Lerp(rightShoulder.weight, 0, aiminigSpeed * Time.deltaTime);
					if (leftShoulder.weight < 0.1f)
					{
						pistolWeightZero();
						mode = RigAnimMode.off;
						headRotation.weight = 0;
					}
					break;
			}
		}
		else if (tController.gunType == GunType.Rifle)			
        {
			pistolWeightZero();
			if (animationController.rightRifle.activeSelf)
            {
				Rifletarget2.position = new Vector3(lfRightBone.transform.position.x, lfRightBone.transform.position.y, lfRightBone.transform.position.z);
			}
			else if (animationController.leftRifle.activeSelf)
            {
				Rifletarget2.position = new Vector3(rfLeftBone.transform.position.x, rfLeftBone.transform.position.y, rfLeftBone.transform.position.z);
			}
			switch (mode)
            {
				case RigAnimMode.inc:
                    if (animationController.rightRifle.activeSelf)
                    {
						rifleRRArm.weight = Mathf.Lerp(rifleRRArm.weight, 1, aiminigSpeed);
						rifleRLArm.weight = Mathf.Lerp(rifleRLArm.weight, 1, aiminigSpeed);
						headRotation.weight = .5f;
						rifleLRArm.weight = 0;
						rifleLLArm.weight = 0;

						if (rifleRRArm.weight > 0.8f)
						{
							StartCoroutine(waiting());
						}
					}
					else if (animationController.leftRifle.activeSelf)
                    {
						rifleLRArm.weight = Mathf.Lerp(rifleRRArm.weight, 1, aiminigSpeed);
						rifleLLArm.weight = Mathf.Lerp(rifleRLArm.weight, 1, aiminigSpeed);
						headRotation.weight = .5f;
						rifleRRArm.weight = 0;
						rifleRLArm.weight = 0;
						if (rifleLLArm.weight > 0.8f)
                        {
							StartCoroutine(waiting());
                        }
					}
					
					break;
				case RigAnimMode.dec:
					rifleRRArm.weight = Mathf.Lerp(rifleRRArm.weight, 0, aiminigSpeed*Time.deltaTime);
					rifleRLArm.weight = Mathf.Lerp(rifleRLArm.weight, 0, aiminigSpeed * Time.deltaTime);
					rifleLRArm.weight = Mathf.Lerp(rifleRRArm.weight, 0, aiminigSpeed*Time.deltaTime);
					rifleLLArm.weight = Mathf.Lerp(rifleRLArm.weight, 0, aiminigSpeed*Time.deltaTime);
					if (rifleRRArm.weight < 0.1f)
					{
						rifleWeightZero();
						mode = RigAnimMode.off;
						headRotation.weight = 0;
					}
					break;

			}
        }
        
    }
    public void SlowMotion()
	{
		Time.timeScale = 0.1f;
		Time.fixedDeltaTime = 0.02F * Time.timeScale;
	}
	public void NormalGameSpeed()
	{
		Time.timeScale = 1f;
	}
		
	IEnumerator waiting()
    {
		yield return new WaitForSeconds(1.5f);
		mode = RigAnimMode.dec;
	}
	void armChanging()
    {
		if (rowbyrow)
		{
			rowbyrow = false;
		}
		else rowbyrow = true;
    }
	void armAnimation()
    {
        if (rowbyrow)
        {
			leftShoulder.weight = Mathf.Lerp(leftShoulder.weight, 1, aiminigSpeed);
			headRotation.weight = .5f;
			if (leftShoulder.weight > 0.8f)
			{
				StartCoroutine(waiting());
			}
		}
		else if (!rowbyrow)
        {
			rightShoulder.weight = Mathf.Lerp(rightShoulder.weight, 1, aiminigSpeed);
			headRotation.weight = .5f;
			if (rightShoulder.weight > 0.9f)
			{
				StartCoroutine(waiting());
			}
		}
	}
	void rifleWeightZero()
    {
		rifleRRArm.weight = 0;
		rifleRLArm.weight = 0;
		rifleLRArm.weight = 0;
		rifleLLArm.weight = 0;
	}
	void pistolWeightZero()
    {
		leftShoulder.weight = 0;
		rightShoulder.weight = 0;
	}
}
