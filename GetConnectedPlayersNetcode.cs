using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;
using HutongGames.PlayMaker;

[ActionCategory("Netcode")]
[HutongGames.PlayMaker.Tooltip("Gets a list of connected players and stores it in a Playmaker array variable.")]

public class GetConnectedPlayersNetcode : FsmStateAction

{
    [UIHint(UIHint.Variable)]
    [ArrayEditor(VariableType.GameObject)]
    [HutongGames.PlayMaker.Tooltip("Store the list of connected player GameObjects.")]
    public FsmArray playerList;

    public override void Reset()
    {
        playerList = null;
    }

    public override void OnEnter()
    {
        List<GameObject> connectedPlayers = new List<GameObject>();

        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {

            if (client.PlayerObject != null)
            {
                connectedPlayers.Add(client.PlayerObject.gameObject);
            }
        }

        playerList.Values = connectedPlayers.ToArray();

        Finish();
    }
}
