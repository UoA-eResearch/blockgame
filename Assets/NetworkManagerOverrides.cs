using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManagerOverrides : NetworkManager
{

	public Text m_Text;

	void Start()
	{
		if (Application.platform == RuntimePlatform.LinuxPlayer)
		{
			Debug.Log("Starting server");
			useWebSockets = true;
			networkPort = 8080;
			StartServer();
		}
		else if (Application.platform == RuntimePlatform.WebGLPlayer)
		{
			Debug.Log("Starting client");
			useWebSockets = true;
			networkPort = 8080;
			GetComponent<NetworkManagerHUD>().enabled = false;
			StartClient();
		}
	}

	void ClearText()
	{
		m_Text.text = "";
	}

	public override void OnServerConnect(NetworkConnection connection)
	{
		Debug.Log(connection + " connected");
		m_Text.text = "Client " + connection.connectionId + " Connected!";
		Invoke("ClearText", 2);

		base.OnServerConnect(connection);
	}

	public override void OnServerDisconnect(NetworkConnection connection)
	{
		Debug.Log(connection + " connection lost");
		m_Text.text = "Client " + connection.connectionId + "Connection Lost!";
		Invoke("ClearText", 2);
		NetworkInstanceId[] clientObjects = new NetworkInstanceId[connection.clientOwnedObjects.Count];
		connection.clientOwnedObjects.CopyTo(clientObjects);

		foreach (NetworkInstanceId objId in clientObjects)
		{
			var go = NetworkServer.FindLocalObject(objId);
			if (go.tag == "Throwable")
			{
				Debug.Log(connection + " had authority over " + go.name + " - releasing");
				NetworkIdentity netIdentity = go.GetComponent<NetworkIdentity>();
				netIdentity.RemoveClientAuthority(connection);
				go.GetComponent<Rigidbody2D>().isKinematic = false;
			}
		}

		base.OnServerDisconnect(connection);
	}

}
