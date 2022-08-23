using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
	public AudioClip walk;
	public AudioClip jump;
	public AudioClip chains;
	public AudioClip hurt;

	
	AudioSource sfx;
	Player myPlayer;
	int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
		sfx = GetComponent<AudioSource>();
		myPlayer = FindObjectOfType<Player>();
		maxHealth = myPlayer.health;
    }

    // Update is called once per frame
    void Update()
    {
		PlaySounds();
    }

	void PlaySounds()
	{
		if(Input.GetMouseButtonDown(0))
		{
			sfx.clip = chains;
			sfx.Play();
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			sfx.clip = jump;
			sfx.Play();
		}

		if (myPlayer.moving)
		{
			if (!sfx.isPlaying)
			{
				sfx.clip = walk;
				sfx.Play();

			}
			else if (sfx.isPlaying)
				sfx.UnPause();
			
		}
		else if (!myPlayer.moving && sfx.clip == walk)
			sfx.Pause();
		
		if(myPlayer.CompareTag("Projectile"))
		{
			sfx.clip = hurt;
			sfx.Play();
		}
		
	}
}
