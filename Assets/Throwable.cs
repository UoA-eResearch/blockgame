using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Throwable : MonoBehaviour
{
	private Rigidbody2D rigidbody;
	private Vector3 lastPos;
	private int throwSpeed = 1000;
	void Awake()
	{
		rigidbody = gameObject.GetComponent<Rigidbody2D>();
	}
    void OnMouseDown()
	{
		lastPos = transform.position;
		rigidbody.isKinematic = true;
	}
    void OnMouseDrag()
	{      
        lastPos = transform.position;
		Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = cursorPosition;
    }
    void OnMouseUp()
	{
		var direction = transform.position - lastPos;
		rigidbody.isKinematic = false;
		rigidbody.AddForce(direction * throwSpeed);
	}
}