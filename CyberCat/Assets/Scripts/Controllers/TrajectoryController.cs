using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TrajectoryController : MonoBehaviour
{
	#region TrajectoryController Variables

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
	public Vector2 _endPoint;

	[Header("Formula variables")]
	public Vector2 velocity;
	public float yLimit;
	private float g;

	[Header("Linecast variables")]
	[Range(2, 30)]
	public int linecastResolution;
	public LayerMask canHit;

	[Header("Physics")]
	Rigidbody2D rb;
	#endregion

	#region TrajectorySystem Variables
	[Header("Trajectory Settings")]
	Camera cam;
	bool isDragging = false;
	Vector2 startPoint;
	Vector2 endPoint;
	Vector2 direction;
	Vector2 force;
	float distance;
	[SerializeField] float pushForce = 4f;
	#endregion

	[Header("Shooting Settings")]
	[SerializeField] Transform firePoint;
	[SerializeField] GameObject Bullet;
	[SerializeField] float bulletForce = 20f;

	[Header("Variables")]
	Vector2 mousePosition;
	private Joystick joystick;
	bool isTrajectoryOn;

	#region Enable-Disable

	private void OnDisable()
	{
		GameManager.Instance.OnPressEvent -= TrajectoryOn;
		GameManager.Instance.ReleaseEvent -= RelaseEventTriggered;
	}

	#endregion

	private void Start()
	{
		GameManager.Instance.OnPressEvent += TrajectoryOn;
		GameManager.Instance.ReleaseEvent += RelaseEventTriggered;
		GameManager.Instance.ShootEnemyEvent += Shooting;

		cam = Camera.main;

		rb = GetComponent<Rigidbody2D>();
		joystick = FindObjectOfType<Joystick>();

		g = Mathf.Abs(Physics2D.gravity.y);
		ArrowRightScale = ArrowR.transform.localScale;
		ArrowLeftScale = ArrowL.transform.localScale;
	}

	#region Trajectory Controller
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
		if (_endPoint.y < 0.35f && _endPoint.y > -0.35f && _endPoint.x < 0 && GameManager.isWall == false)
		{
			ArrowRight();
		}
		else if (_endPoint.y < 0.35f && _endPoint.y > -0.35f && _endPoint.x > 0 && GameManager.isWall == false)
		{
			ArrowLeft();
		}
		else if (_endPoint.y < -0.35f)
		{
			lineActive();//On Ground
		}
		else if (GameManager.isWall)
        {
			lineActive();//On Wall
		}
		else
		{
			Hide();//Aynýsý hide() functionunda da var direk .
			/*
			_TrajectoryLine.SetActive(false);
			ArrowR.SetActive(false);
			ArrowR.transform.localScale = ArrowRightScale;
			ArrowL.SetActive(false);
			ArrowL.transform.localScale = ArrowLeftScale;
			*/

		}
	}
	public void lineActive()
    {
		_TrajectoryLine.SetActive(true);
		ArrowR.SetActive(false);
		ArrowR.transform.localScale = ArrowRightScale;
		ArrowL.SetActive(false);
		ArrowL.transform.localScale = ArrowLeftScale;
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
		ArrowL.transform.localScale = new Vector3(ArrowSize * -1, ArrowL.transform.localScale.y);
		ArrowR.SetActive(false);
		ArrowR.transform.localScale = ArrowRightScale;
		_TrajectoryLine.SetActive(false);
	}
	#endregion

	#region Trajectory System

	void OnDragEnd()
	{
		Push(force);
		Hide();
	}
	public void Push(Vector2 force)
	{
		rb.AddForce(force, ForceMode2D.Impulse);
	}

	#endregion


	#region Event Functions

	private void RelaseEventTriggered()
	{
		isTrajectoryOn = false;
	}

	void TrajectoryOn()
	{
		if (GameManager.isGround)
		{
			StartCoroutine(TrajectoryShow());
		}
	}

	void Shooting()
	{

		GameObject bullet = Instantiate(Bullet, firePoint.position, firePoint.rotation);
		Vector3 dir = GameManager.Instance.hit.point - bullet.transform.position;
		Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
		rbBullet.AddForce(dir * bulletForce, ForceMode2D.Impulse);
		Destroy(bullet, 1f);
	}

	#endregion

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
		{
			GameManager.isGround = true;
		}

		if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") && !(collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")))
		{
			GameManager.isWall = true;
			GameManager.isGround = true;
			StartCoroutine(WallGravity());
		}
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle") || collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
		{
			GameManager.isGround = false;
			GameManager.isWall = false;
			GameManager.Instance.NormalGameSpeed();
		}
	}
	IEnumerator WallGravity()
	{
		float index = 0.1f;
		rb.gravityScale = index;
		yield return new WaitForSeconds(0.5f);
		while (index < 1)
		{
			index += 0.2f;
			rb.gravityScale = index;
			yield return new WaitForSeconds(0.05f);
		}
		GameManager.Instance.NormalGameSpeed();
	}

	IEnumerator TrajectoryShow()
	{
		GameManager.Instance.SlowMotion();
		isTrajectoryOn = true;

		while (isTrajectoryOn)
		{
			_endPoint = joystick.Direction;
			StartCoroutine(RenderArc());
			ArrowSize = velocity.x / 10;
			Show();
			endPoint = _endPoint * 2.2f;
			distance = Vector2.Distance(startPoint, endPoint);
			direction = (startPoint - endPoint);
			force = direction * distance * pushForce / 2;
			velocity = new Vector2(force.x, force.y);
			yield return null;
		}

		OnDragEnd();
		force = Vector2.zero;
		startPoint = Vector2.zero;
		endPoint = Vector2.zero;
		GameManager.Instance.NormalGameSpeed();
	}

}