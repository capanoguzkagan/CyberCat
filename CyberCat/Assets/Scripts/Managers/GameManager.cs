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

	public ChainIKConstraint leftShoulder = null;
	public ChainIKConstraint rightShoulder = null;
	public Transform target = null;
	public float aiminigSpeed=1f;
	private bool rightLeftBool=false;
	public bool rightLeftboolean { get { return rightLeftBool; } set { rightLeftBool = value; } }
	private enum RigAnimMode
    {
		off,
		inc,
		dec,
    }
	private RigAnimMode mode = RigAnimMode.off;

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
			target.position = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z);
			mode = RigAnimMode.inc;

		}
		else
		{
			OnPressEvent?.Invoke();
			Debug.Log("OnPressEvent");
		}
	}

    private void FixedUpdate()
    {
		Debug.Log(rightLeftboolean);
        switch (mode)
        {
            case RigAnimMode.inc:
                if (rightLeftboolean)
                {
                    leftShoulder.weight = Mathf.Lerp(leftShoulder.weight, 1, aiminigSpeed * Time.deltaTime);
                    if (leftShoulder.weight > 0.8f)
                    {
                        leftShoulder.weight = 0.85f;
                        mode = RigAnimMode.dec;
                    }
                }
                else if (!rightLeftboolean)
                {
                    rightShoulder.weight = Mathf.Lerp(rightShoulder.weight, 1, aiminigSpeed*Time.deltaTime);
                    if (rightShoulder.weight > 0.8f)
                    {
                        rightShoulder.weight = 0.85f;
                        mode = RigAnimMode.dec;
                    }
                }
                break;
            case RigAnimMode.dec:
                leftShoulder.weight = Mathf.Lerp(leftShoulder.weight, 0, aiminigSpeed * Time.deltaTime);
                rightShoulder.weight = Mathf.Lerp(rightShoulder.weight, 0, aiminigSpeed * Time.deltaTime);
                if (leftShoulder.weight < 0.1f)
                {
                    leftShoulder.weight = 0;
                    rightShoulder.weight = 0;
                    mode = RigAnimMode.off;
                }
                break;
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
		yield return new WaitForSeconds(0.5f);
    }
}
