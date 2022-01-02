using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		EnemyController _enemy = collision.GetComponent<EnemyController>();
		TrajectoryController trajectoryController=collision.GetComponent<TrajectoryController>();
		if (_enemy != null)
		{
			Debug.Log("Bullet Hit an Enemy");
			Destroy(this.gameObject);
			Destroy(_enemy.gameObject.transform.parent.gameObject);
			GameManager.Instance.NormalGameSpeed();

		}
		else if (trajectoryController!=null)
		{

		}
		else
		{
			Destroy(this.gameObject);
		}
	}


}
