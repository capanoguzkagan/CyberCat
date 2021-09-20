﻿
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Singleton class: GameManager

	public static GameManager Instance;

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		}
	}

	#endregion

	#region Variables
	[Header("Trajectory Settings")]
	Camera cam;
	bool isDragging = false;
	Vector2 startPoint;
	Vector2 endPoint;
	Vector2 direction;
	Vector2 force;
	float distance;
	[SerializeField] private Ball ball;
	[SerializeField] TrajectoryController trajectory;
	[SerializeField] float pushForce = 4f;

	[Header("Shooting Settings")]
	[SerializeField] Transform firePoint;
	[SerializeField] GameObject Bullet;
	[SerializeField] float bulletForce = 20f;
	#endregion


	void Start ()
	{
		cam = Camera.main;
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
			isDragging = true;
			Shooting();
		}
		if (Input.GetMouseButtonUp (0)) {
			isDragging = false;
			startPoint = endPoint;
			OnDragEnd ();
		}
		if (isDragging)
		{
			endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
		}
		if (((int)startPoint.x-(int)endPoint.x)!= 0|| ((int)startPoint.y - (int)endPoint.y)!=0) 
		{
			OnDrag();
		}
	}

	void OnDragStart ()
	{
		startPoint = cam.ScreenToWorldPoint (Input.mousePosition);

	//	trajectory.Show ();
	}

	void OnDrag ()
	{
		trajectory.Show();
		endPoint = cam.ScreenToWorldPoint (Input.mousePosition);
		distance = Vector2.Distance (startPoint, endPoint);
		direction = (startPoint - endPoint).normalized;
		force = direction * distance * pushForce;

		//just for debug
		Debug.DrawLine (startPoint, endPoint);

		trajectory.velocity=new Vector2(force.x,force.y);
	}

	void OnDragEnd ()
	{
		ball.Push (force);

		trajectory.Hide ();
	}
	void Shooting()
	{
		GameObject bullet = Instantiate(Bullet, firePoint.position, firePoint.rotation);
		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
		rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
		Destroy(bullet, 1f);
	}
}
