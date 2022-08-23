using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
	public static bool isCompleted;
	GameManager manager;
	// Start is called before the first frame update
	void Start()
	{
		isCompleted = false;
		manager = FindObjectOfType<GameManager>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			manager.PlayGame();
			isCompleted = true;
		}
	}
}
