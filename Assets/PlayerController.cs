using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

	private Rigidbody2D currentRB;
	private bool holding = false;
	private Vector2 lastPos;
	private int throwSpeed = 1000;

	[Command]
	void CmdPickup(GameObject go, NetworkIdentity player)
	{
		Debug.Log("server got request to pickup " + go.name);
		var networkIdentity = go.GetComponent<NetworkIdentity>();
		var otherOwner = networkIdentity.clientAuthorityOwner;
		Debug.Log("got request to seize authority for " + go.name + " which is currently held by " + otherOwner);
		if (otherOwner == player.connectionToClient)
		{
			return;
		}
		else
		{
			if (otherOwner != null)
			{
				networkIdentity.RemoveClientAuthority(otherOwner);
			}
			networkIdentity.AssignClientAuthority(player.connectionToClient);
		}
	}

	[Command]
	void CmdDrop(GameObject go, NetworkIdentity player)
	{
		Debug.Log("server got request to drop " + go.name);
		var networkIdentity = go.GetComponent<NetworkIdentity>();
		networkIdentity.RemoveClientAuthority(player.connectionToClient);
	}

	// Update is called once per frame
	void Update()
	{
		if (!isLocalPlayer) return;
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			var hit = Physics2D.Raycast(cursorPosition, Vector2.zero);
			if (hit && hit.collider.tag == "Throwable")
			{
				Debug.Log("mouse down on " + hit.collider.name);
				lastPos = cursorPosition;
				var playerID = gameObject.GetComponent<NetworkIdentity>();
				CmdPickup(hit.collider.gameObject, playerID);
				holding = true;
				currentRB = hit.collider.gameObject.GetComponent<Rigidbody2D>();
				currentRB.isKinematic = true;
				currentRB.velocity = Vector2.zero;
			}
		}
		else if (Input.GetMouseButton(0) && holding)
		{
			Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (cursorPosition != lastPos)
			{
				currentRB.position = cursorPosition;
				lastPos = cursorPosition;
			}
		}
		else if (Input.GetMouseButtonUp(0) && holding)
		{
			Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			currentRB.isKinematic = false;
			var direction = cursorPosition - lastPos;
			currentRB.AddForce(direction * throwSpeed);
			var playerID = gameObject.GetComponent<NetworkIdentity>();
			CmdDrop(currentRB.gameObject, playerID);
			holding = false;
		}
	}
}
