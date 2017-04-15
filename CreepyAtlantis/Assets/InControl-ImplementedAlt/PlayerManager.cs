﻿namespace MultiplayerBasicExample
{
	using System.Collections.Generic;
	using InControl;
	using UnityEngine;

	public class PlayerManager : MonoBehaviour
	{
		public GameObject playerPrefab;
		
		const int maxPlayers = 4;

		List<Vector3> playerPositions = new List<Vector3>() {
			new Vector3( -1, 1, -10 ),
			new Vector3( 1, 1, -10 ),
			new Vector3( -1, -1, -10 ),
			new Vector3( 1, -1, -10 ),
		};

		List<Player> players = new List<Player>( maxPlayers );



		void Start()
		{
			InputManager.OnDeviceDetached += OnDeviceDetached;
		}


		void Update()
		{
			var inputDevice = InputManager.ActiveDevice;

			if (JoinButtonWasPressedOnDevice( inputDevice ))
			{
				if (ThereIsNoPlayerUsingDevice( inputDevice ))
				{
					CreatePlayer( inputDevice );
				}
			}
		}


		bool JoinButtonWasPressedOnDevice( InputDevice inputDevice )
		{
			return inputDevice.Action1.WasPressed || inputDevice.Action2.WasPressed || inputDevice.Action3.WasPressed || inputDevice.Action4.WasPressed;
		}


		Player FindPlayerUsingDevice( InputDevice inputDevice )
		{
			var playerCount = players.Count;
			for (var i = 0; i < playerCount; i++)
			{
				var player = players[i];
				if (player.Device == inputDevice)
				{
					return player;
				}
			}

			return null;
		}


		bool ThereIsNoPlayerUsingDevice( InputDevice inputDevice )
		{
			return FindPlayerUsingDevice( inputDevice ) == null;
		}


		void OnDeviceDetached( InputDevice inputDevice )
		{
			var player = FindPlayerUsingDevice( inputDevice );
			if (player != null)
			{
				RemovePlayer( player );
			}
		}

        int MissingID (List<Player> P)
        {
            if (P.Count == 0)
                return 0;
            else if (P.Count == 1)
            {
                foreach (Player p in P)
                {
                    if (p.playerNum == 0)
                        return 1;
                    else
                        return 0;
                }
            }
            return 99;
        }

		Player CreatePlayer( InputDevice inputDevice )
		{
			if (players.Count < maxPlayers)
			{
				// Pop a position off the list. We'll add it back if the player is removed.
				var playerPosition = playerPositions[0];
				playerPositions.RemoveAt( 0 );

                int hold = MissingID(players);

                var gameObject = (GameObject) Instantiate( playerPrefab, playerPosition, Quaternion.identity );
				var player = gameObject.GetComponent<Player>();
				player.Device = inputDevice;
				players.Add( player );

                player.playerNum = hold;

				return player;
			}

			return null;
		}


		void RemovePlayer( Player player )
		{
			playerPositions.Insert( 0, player.transform.position );
			players.Remove( player );
			player.Device = null;
			Destroy( player.gameObject );
		}


		//void OnGUI()
		//{
		//	const float h = 22.0f;
		//	var y = 10.0f;

		//	GUI.Label( new Rect( 10, y, 300, y + h ), "Active players: " + players.Count + "/" + maxPlayers );
		//	y += h;

		//	if (players.Count < maxPlayers)
		//	{
		//		GUI.Label( new Rect( 10, y, 300, y + h ), "Press a button to join!" );
		//		y += h;
		//	}
		//}
	}
}