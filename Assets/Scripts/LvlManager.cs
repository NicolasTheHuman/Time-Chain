using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlManager : MonoBehaviour
{
	GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
		manager = FindObjectOfType<GameManager>();
    }

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			manager.Victory();
		}
	}
}
