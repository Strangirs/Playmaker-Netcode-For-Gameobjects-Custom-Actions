using UnityEngine;
using Unity.Netcode;
using System.Linq;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("Netcode")]
    public class GetPlayerPositions : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.Vector3)]
        public FsmArray playerPositions;

        [Tooltip("Check to update player positions every frame")]
        public FsmBool everyFrame;

        public override void OnEnter()
        {
            UpdatePlayerPositions();

            if (!everyFrame.Value)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            if (everyFrame.Value)
            {
                UpdatePlayerPositions();
            }
        }

        private void UpdatePlayerPositions()
        {
            Debug.Log("UpdatePlayerPositions called");
            if (NetworkManager.Singleton.IsServer)
            {
                Debug.Log("Running on server");
                var positions = new Vector3[PlayerPositionSync.Players.Count];
                for (int i = 0; i < PlayerPositionSync.Players.Count; i++)
                {
                    Debug.Log($"Player {i} position: {PlayerPositionSync.Players[i].transform.position}");
                    positions[i] = PlayerPositionSync.Players[i].transform.position;
                }
                playerPositions.Values = positions.Cast<object>().ToArray();
                Debug.Log($"Player positions array length: {playerPositions.Values.Length}");
            }
            else
            {
                Debug.Log("Not running on server");
            }
        }
    }
}