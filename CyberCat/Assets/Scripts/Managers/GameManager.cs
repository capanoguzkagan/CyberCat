using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


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
	private bool rightBool;
	private bool leftBool;
    public bool RightArmBoolean { get { return rightBool; } set { rightBool = value; } }
	public bool LeftArmBoolean { get { return leftBool; } set { leftBool = value; } }

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
		}
		else
		{
			OnPressEvent?.Invoke();
			Debug.Log("OnPressEvent");
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
}
