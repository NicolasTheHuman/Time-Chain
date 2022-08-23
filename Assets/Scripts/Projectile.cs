using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float speed = 4;
	Player target;
	Enemies enemy;
	Vector3 _direction;
	private void Awake()
	{
		target = FindObjectOfType<Player>();
	}
	// Start is called before the first frame update
	void Start()
    {
		_direction = target.transform.position -transform.position; 
	}

    // Update is called once per frame
    void Update()
    {
		transform.position += _direction * speed * Time.deltaTime;
		Destroy(gameObject, 2f);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Chain"))
			Destroy(gameObject);
		
			
	}
}
