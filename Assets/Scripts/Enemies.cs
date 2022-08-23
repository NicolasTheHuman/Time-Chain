using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemies : MonoBehaviour, IMove
{
	protected int currentWp;
	protected float fireRate;
	protected float currentTime;
	protected Rigidbody _enemyRb;
	protected Player target;

	public float speed;
	public float shootDistance;
	public Transform[] firePoints;
	public Transform[] waypoints;
	public Projectile bullet;

	GameManager _gameManager;
	bool _aboutToDie;
	float _timeToDie;

	// Start is called before the first frame update
	protected virtual void Start()
	{
		_enemyRb = GetComponent<Rigidbody>();
		target = FindObjectOfType<Player>();
		_gameManager = FindObjectOfType<GameManager>();
	}

	// Update is called once per frame
	protected virtual void Update()
	{
		if (!_aboutToDie)
		{
			Move();

			float distance = Vector3.Distance(transform.position, target.transform.position);
			if (distance < shootDistance)
			{
				currentTime += Time.deltaTime;
				Aim();
				if (fireRate <= currentTime && _gameManager.gameState == State.PLAY)
				{
					Shoot();
				}
			}
		}
		else if(_aboutToDie)
		{
			Die();
			_timeToDie += Time.deltaTime;
		}
		
	}

	public abstract void Move();

	public virtual void Aim()
	{
		transform.LookAt(target.transform.position);
	}

	public virtual void Shoot()
	{
		currentTime = 0;
		
		for (int i = 0; i < firePoints.Length; i++)
		{
			Transform whereToSpawn = firePoints[i].transform;
			Instantiate(bullet, whereToSpawn.position, whereToSpawn.rotation);
		}
		
	}

	public virtual void Die()
	{
		
		if(currentTime >= _timeToDie)
		{
			_enemyRb.useGravity = true;
			Destroy(gameObject, 1);
		}
	}

	
	private void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.CompareTag("Chain"))
		{
			_aboutToDie = true;
			currentTime = 0f;
		}
	}

}
