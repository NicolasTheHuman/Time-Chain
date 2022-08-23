using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonView : MonoBehaviour
{
	public float mouseSensitivity = 100f;
	public Transform playerBody;
	
	

	float xRotation = 0f;

	// Start is called before the first frame update
    void Start()
    {
		
		Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.unscaledDeltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.unscaledDeltaTime;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -80f, 70f);
		
		playerBody.Rotate(Vector3.up * mouseX);
		
		transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
	}

}
