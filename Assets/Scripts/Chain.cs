using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
	private Player myPlayer;
	
	private void Awake()
	{
		myPlayer = FindObjectOfType<Player>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Enemy"))
		{
			myPlayer.StopAndRetrieveChain();
		}
	}
}
