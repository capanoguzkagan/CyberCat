using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TrajectoryController : MonoBehaviour
{
	[SerializeField]
	GameObject _TrajectoryLine;
	[Header("Line renderer veriables")]
	public LineRenderer line;
	[Range(2, 30)]
	public int resolution;
	public GameObject ArrowR;
	Vector3 ArrowRightScale;
	public GameObject ArrowL;
	Vector3 ArrowLeftScale;
	float ArrowSize;
	public PlayerInput _playerInput;
	public Vector2 _endPoint;

	[Header("Formula variables")]
	public Vector2 velocity;
	public float yLimit; //for later
	private float g;

	[Header("Linecast variables")]
	[Range(2, 30)]
	public int linecastResolution;
	public LayerMask canHit;

	TrajectorySystem TrajectorySystem;
	[SerializeField] EventScript _eventScript;
	[SerializeField]DelegateScript _delegateScript;
	PlayerController _playerController;
	private void Awake()
	{

		_playerInput = new PlayerInput();
		TrajectorySystem = GetComponent<TrajectorySystem>();
		_playerController = GetComponent<PlayerController>();
	}
	private void OnEnable()
	{
		_playerInput.Enable();
		GameManager.Instance.OnHold += ChanceGravity;
	}
	private void OnDisable()
	{
		_playerInput.Disable();
		GameManager.Instance.OnHold -= ChanceGravity;
	}
	private void ChanceGravity()
    {
		_playerController.rb.gravityScale = 0;
    }


	private void Start()
	{
		g = Mathf.Abs(Physics2D.gravity.y);
		ArrowRightScale = ArrowR.transform.localScale;
		ArrowLeftScale = ArrowL.transform.localScale;
	}

	private void Update()
	{
		_endPoint = _playerInput.PlayerMovementController.PlayerMovementControl.ReadValue<Vector2>();
		StartCoroutine(RenderArc());
		ArrowSize = velocity.x/10;
	}
	private IEnumerator RenderArc()
	{
		line.positionCount = resolution + 1;
		line.SetPositions(CalculateLineArray());
		yield return null;
	}

	private Vector3[] CalculateLineArray()
	{
		Vector3[] lineArray = new Vector3[resolution + 1];

		var lowestTimeValue = MaxTimeX() / resolution;

		for (int i = 0; i < lineArray.Length; i++)
		{
			var t = lowestTimeValue * i;
			lineArray[i] = CalculateLinePoint(t);
		}

		return lineArray;
	}

	private Vector2 HitPosition()
	{
		var lowestTimeValue = MaxTimeY() / linecastResolution;

		for (int i = 0; i < linecastResolution + 1; i++)
		{
			var t = lowestTimeValue * i;
			var tt = lowestTimeValue * (i + 1);

			var hit = Physics2D.Linecast(CalculateLinePoint(t), CalculateLinePoint(tt), canHit);

			if (hit)
				return hit.point;
		}

		return CalculateLinePoint(MaxTimeY());
	}

	private Vector3 CalculateLinePoint(float t)
	{
		float x = velocity.x * t;
		float y = (velocity.y * t) - (g * Mathf.Pow(t, 2) / 2);
		return new Vector3(x + transform.position.x, y + transform.position.y);
	}

	private float MaxTimeY()
	{
		var v = velocity.y;
		var vv = v * v;

		var t = (v + Mathf.Sqrt(vv + 2 * g * (transform.position.y - yLimit))) / g;
		return t;
	}

	private float MaxTimeX()
	{
		var x = velocity.x;
		if (x == 0)
		{
			//velocity.x = 000.1f;
			x = velocity.x;
		}

		var t = (HitPosition().x - transform.position.x) / x;
		return t;
	}
	public void Show()
	{
		if (_endPoint.y<0.35f && _endPoint.y > -0.35f&& _endPoint.x<0)
		{
			ArrowRight();
		}
		else if (_endPoint.y<0.35f && _endPoint.y > -0.35f && _endPoint.x > 0)
		{
			ArrowLeft();
		}
		else if (_endPoint.y < -0.35f || _endPoint.y>0.35f )
		{
			_TrajectoryLine.SetActive(true);
			ArrowR.SetActive(false);
			ArrowR.transform.localScale = ArrowRightScale;
			ArrowL.SetActive(false);
			ArrowL.transform.localScale = ArrowLeftScale;
		}
		else
		{
			_TrajectoryLine.SetActive(false);
			ArrowR.SetActive(false);
			ArrowR.transform.localScale = ArrowRightScale;
			ArrowL.SetActive(false);
			ArrowL.transform.localScale = ArrowLeftScale;
		}
	}
	public void Hide()
	{
		ArrowR.SetActive(false);
		ArrowR.transform.localScale = ArrowRightScale;
		ArrowL.SetActive(false);
		ArrowL.transform.localScale = ArrowLeftScale;
		_TrajectoryLine.SetActive(false);
	}
	private void ArrowRight()
	{
		ArrowR.SetActive(true);
		ArrowR.transform.localScale = new Vector3(ArrowSize, ArrowR.transform.localScale.y);
		ArrowL.SetActive(false);
		ArrowL.transform.localScale = ArrowLeftScale;
		_TrajectoryLine.SetActive(false);
	}
	private void ArrowLeft()
	{
		ArrowL.SetActive(true);
		ArrowL.transform.localScale = new Vector3(ArrowSize*-1, ArrowL.transform.localScale.y);
		ArrowR.SetActive(false);
		ArrowR.transform.localScale = ArrowRightScale;
		_TrajectoryLine.SetActive(false);
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
		{
			TrajectorySystem.isGround = true;
			//_eventScript.gameObject.SetActive(true);
			//_delegateScript.gameObject.SetActive(true);
		}
		if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
		{
			TrajectorySystem.isGround = true;
			GameManager.Instance.GravityScaleMethod();
		}
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle") || collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
		{
			TrajectorySystem.isGround = false;
			_playerController.rb.gravityScale = 1;
		}
	}

}