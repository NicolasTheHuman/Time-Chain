using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IMove
{
	public float moveSpeed = 5f;
	public float jumpForce;
	public int maxLenght;
	public int health = 5;
	public bool moving;

	public Transform firePoint;
	public Transform lastPosition;
	public GameObject chainPrefab;
	public List<GameObject> chains;

	int _jumps = 1;
	bool _canSlow = false;
	bool _freezeTime = false;
	Rigidbody _rb;
	Animator _myAnim;
	Vector3 _velocity;
	Vector3 _pivot;

	
	private Coroutine createChainCoroutine;
	GameManager _manager;
	// Start is called before the first frame update
	void Start()
    {
		_manager = FindObjectOfType<GameManager>();
		_rb = GetComponent<Rigidbody>();
		_myAnim = GetComponent<Animator>();
		_pivot = transform.position - Vector3.up;
		lastPosition.parent = null;
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			_myAnim.SetTrigger("Throw");
			createChainCoroutine = StartCoroutine(GraplingHook());
		}

		if (_canSlow && Input.GetMouseButtonDown(1))
		{
			_freezeTime = true;
		}

		if (_freezeTime)
			BulletTime();
		else if (!_freezeTime || Input.GetKeyDown(KeyCode.Space))
			UnfreezeTime();

		if (_velocity == new Vector3(0, 0, 0))
			moving = false;
	}

	private void FixedUpdate()
	{

		Move();

		if (Input.GetKeyDown(KeyCode.Space) && _jumps > 0)
		{
			Jump();
			_freezeTime = false;
		}
		if (_manager.gameState == State.PLAY)
		{
			
		}
	}

	public void Move()
	{
		moving = true;
		_myAnim.SetFloat("Speed", Input.GetAxis("Vertical"));
		_velocity = (transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical")) * moveSpeed;
		_rb.MovePosition(_rb.position + _velocity * Time.fixedDeltaTime);
	}

	void Jump()
	{
		_myAnim.SetTrigger("Jump");
		_jumps--;
		_rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
		if(chains.Count != 0)
		{
			StopAndDestroyChain();
		}
	}

	IEnumerator GraplingHook()
	{
		_freezeTime = false;
		while (chains.Count < maxLenght)
		{
			
			if (chains.Count != 0)  
			{
				Transform lastChainTransform = chains[chains.Count - 1].transform;  
				Vector3 nextFirePointPosition = lastChainTransform.Find("MultiplicationPoint").transform.position;
				Quaternion nextFirePointRotation = lastChainTransform.Find("MultiplicationPoint").transform.rotation;
				GameObject clone = Instantiate(chainPrefab, nextFirePointPosition, nextFirePointRotation);
				chains.Add(clone.gameObject);
			}
			else
			{
				GameObject clone = Instantiate(chainPrefab, firePoint.position, firePoint.rotation);
				chains.Add(clone.gameObject);
			}

			yield return new WaitForSecondsRealtime(0.05f);
		}

		if (chains.Count >= maxLenght)
		{
			StopAndDestroyChain();
		}
	}	
	

	private IEnumerator Displacement()
	{
		for (int i = 0; i < chains.Count; i++)
		{
			float lerp = 0f;
			Vector3 playerPosition = transform.position;

			while (lerp < 1f)
			{
				_canSlow = true;
				lerp += moveSpeed  * Time.deltaTime;
				if(chains[i] != null)
					transform.position = Vector3.Lerp(playerPosition, chains[i].transform.position, lerp);

				yield return null;
			}
			Destroy(chains[i]);
		}
		_canSlow = false;
		chains.Clear();
	}

	public void StopAndRetrieveChain()
	{
		StopCoroutine(createChainCoroutine);
		StartCoroutine(Displacement());
	}

	public void StopAndDestroyChain()
	{
		StopAllCoroutines();
		StartCoroutine("DestroyChains");
	}

	IEnumerator DestroyChains()
	{
		for (int i = 0; i < chains.Count; i++)
		{
			Destroy(chains[i]);
			if(!_freezeTime)
				yield return new WaitForSecondsRealtime(0.1f);
		}
		chains.Clear();
	}
	
	void BulletTime()
	{
		StopAndDestroyChain();
		Time.timeScale = 0.01f;
	}

	void UnfreezeTime()
	{
		Time.timeScale = 1f;
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.layer == 11)
		{
			_jumps = 1;
			lastPosition.position = collision.transform.position;
		}

		if (collision.gameObject.CompareTag("Projectile"))
		{
			health--;
		}

		if(collision.gameObject.layer == 12)
		{
			health--;
			transform.position = lastPosition.position;
		}
	}
}
