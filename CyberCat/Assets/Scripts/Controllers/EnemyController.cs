using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
	[SerializeField] float _range;
	[SerializeField] float _speedTimer;
	[SerializeField] Transform _target;
	Image _image;

	float _time;
	bool _detected;
	bool _slowMotion;
	Vector2 _direction;

	void Start()
	{
		_image = GetComponentInChildren<Image>();
		_image.fillAmount = 1;
		_time = 1;
	}

	// Update is called once per frame
	void Update()
	{
		DetectTarget();

	}
	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, _range);
	}
	private void DetectTarget()
	{
		Vector2 targetPosition = _target.position;
		_direction = targetPosition - (Vector2)transform.position;
		RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, _direction, _range);
		if (_detected)
		{
			SlowMotion();
		}
		if (rayInfo)
		{
			if (rayInfo.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
			{
				if (!_detected)
				{
					_image.enabled = true;
					_detected = true;
					_slowMotion = true;
					Debug.Log("Target Found");
				}
			}
			else
			{
				_detected = false;
				_image.enabled = false;
				_time = 1;
				_image.fillAmount = 1;
			}
			if (_slowMotion&&!_detected)
			{
				Time.timeScale = 1f;
				_slowMotion = false;
			}
		}

	}
	void SlowMotion()
	{
		Time.timeScale = 0.1f;
		Time.fixedDeltaTime = 0.02F * Time.timeScale;
		_time -= Time.deltaTime*_speedTimer;
		_image.fillAmount -= Time.deltaTime * _speedTimer;
		if (_time < 0)
		{
			Debug.Log("Zaman Doldu");
			_time = 1;
			_image.fillAmount = 1;
		}
	}
}
