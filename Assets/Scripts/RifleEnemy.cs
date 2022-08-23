using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleEnemy : Enemies
{
	// Start is called before the first frame update
	protected override void Start()
    {
		base.Start();
		fireRate = 4f;
    }

	public override void Move()	{}
}
