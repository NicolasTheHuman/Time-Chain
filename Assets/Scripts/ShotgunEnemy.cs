using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunEnemy : Enemies
{
    // Start is called before the first frame update
    protected override void Start()
    {
		base.Start();
		fireRate = 4f;
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

			var nextRandomWp = Random.Range(0, waypoints.Length);


			if (nextRandomWp != currentWp)
			{
				currentWp = nextRandomWp;

			}
			else
			{
				nextRandomWp = Random.Range(0, waypoints.Length);

			}
		}
	}
}
