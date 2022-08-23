using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolEnemy : Enemies
{
	bool _right;

	// Start is called before the first frame update
	protected override void Start()
    {
		base.Start();
		fireRate = 3.5f;
	}

	public override void Move()
	{
		Vector3 direction = waypoints[currentWp].position - transform.position;

		if (direction.magnitude > speed * Time.deltaTime)
		{
			transform.position += direction.normalized * speed * Time.deltaTime;
		}
		else
		{
			transform.position = waypoints[currentWp].position;

			if (_right)
			{
				currentWp++;
				if (currentWp >= waypoints.Length)
				{
					_right = false;
					currentWp--;
				}
			}
			else
			{
				currentWp--;
				if (currentWp < 0)
				{
					_right = true;
					currentWp++;
				}
			}
		}
	}
}
