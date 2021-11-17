using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class TrajectorySystem : MonoBehaviour
{
	#region Variables
	[Header("Trajectory Settings")]
	Camera cam;
	bool isDragging = false;
	Vector2 startPoint;
	Vector2 endPoint;
	Vector2 direction;
	Vector2 force;
	float distance;



	[SerializeField] TrajectoryController trajectory;
	[SerializeField] float pushForce = 4f;

	[Header("Shooting Settings")]
	[SerializeField] Transform firePoint;
	[SerializeField] GameObject Bullet;
	[SerializeField] float bulletForce = 20f;
	#endregion



	void Start()
	{
		cam = Camera.main;
	}

	void Update()
	{
		TrajectoryOn();
	}

	void OnDrag()
	{
		if (GameManager.isGround)
		{
			trajectory.Show();
			distance = Vector2.Distance(startPoint, endPoint);
			direction = (startPoint - endPoint);
			force = direction * distance * pushForce / 2;
			trajectory.velocity = new Vector2(force.x, force.y);
		}
	}

	void OnDragEnd()
	{
		trajectory.Push(force);
		trajectory.Hide();
	}
	void Shooting()
	{
		GameObject bullet = Instantiate(Bullet, firePoint.position, firePoint.rotation);
		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
		rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
		Destroy(bullet, 1f);
	}
	void TrajectoryOn()
	{
		if (trajectory._playerInput.PlayerMovementController.Press.triggered)
		{
			isDragging = true;
			Time.timeScale = 0.1f;
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
		}
		if (trajectory._playerInput.PlayerMovementController.Release.triggered)
		{
			if (startPoint == endPoint)
			{
				Shooting();
			}
			isDragging = false;
			OnDragEnd();
			force = Vector2.zero;
			startPoint = Vector2.zero;
			endPoint = Vector2.zero;
			Time.timeScale = 1f;
		}
		if (isDragging)
		{
			endPoint = trajectory._endPoint * 2.2f;
		}
		if ((int)endPoint.x != 0 || (int)endPoint.y != 0)
		{
			OnDrag();
		}
	}
}
