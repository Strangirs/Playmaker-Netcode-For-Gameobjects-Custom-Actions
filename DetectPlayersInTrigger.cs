// Below is good for on enter and exit (constant triggers not on demand)

using UnityEngine;
using Unity.Netcode;
using HutongGames.PlayMaker;
using System.Collections.Generic;
using System.Linq;

namespace PlaymakerNGO
{
    [ActionCategory("Netcode")]
    public class DetectPlayersInTrigger : FsmStateAction
    {
        [RequiredField]
        [CheckForComponent(typeof(Collider))]
        public FsmOwnerDefault triggerObject;

        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.Int)]
        public FsmArray playerIndexes;

        private List<int> playersInTrigger = new List<int>();

        public override void OnEnter()
        {
            playersInTrigger.Clear();
            GameObject go = Fsm.GetOwnerDefaultTarget(triggerObject);
            if (go != null)
            {
                Collider triggerCollider = go.GetComponent<Collider>();
                if (triggerCollider != null)
                {
                    triggerCollider.isTrigger = true;
                    TriggerDetector detector = triggerCollider.gameObject.AddComponent<TriggerDetector>();
                    detector.Initialize(this);
                }
                else
                {
                    Debug.LogError("Trigger object does not have a Collider component.");
                }
            }
            else
            {
                Debug.LogError("Trigger object is null.");
            }
            Finish();
        }

        public void AddPlayer(int playerIndex)
        {
            if (!playersInTrigger.Contains(playerIndex))
            {
                Debug.Log($"Adding player index: {playerIndex}");
                playersInTrigger.Add(playerIndex);
                UpdatePlayerIndexesArray();
            }
        }

        public void RemovePlayer(int playerIndex)
        {
            if (playersInTrigger.Contains(playerIndex))
            {
                Debug.Log($"Removing player index: {playerIndex}");
                playersInTrigger.Remove(playerIndex);
                UpdatePlayerIndexesArray();
            }
        }

        private void UpdatePlayerIndexesArray()
        {
            playerIndexes.Resize(playersInTrigger.Count);
            for (int i = 0; i < playersInTrigger.Count; i++)
            {
                playerIndexes.Set(i, playersInTrigger[i]);
            }
            Debug.Log($"playerIndexes array updated with {playersInTrigger.Count} players.");
        }

        public override void Reset()
        {
            triggerObject = null;
            playerIndexes = null;
        }
    }

    public class TriggerDetector : MonoBehaviour
    {
        private DetectPlayersInTrigger parentAction;

        public void Initialize(DetectPlayersInTrigger action)
        {
            parentAction = action;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player entered trigger.");
                NetworkObject netObject = other.GetComponent<NetworkObject>();
                if (netObject != null)
                {
                    int playerIndex = PlayerPositionSync.Players.IndexOf(netObject.GetComponent<PlayerPositionSync>());
                    if (playerIndex >= 0)
                    {
                        parentAction.AddPlayer(playerIndex);
                    }
                    else
                    {
                        Debug.LogError("PlayerPositionSync component not found on player.");
                    }
                }
                else
                {
                    Debug.LogError("NetworkObject component not found on player.");
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player exited trigger.");
                NetworkObject netObject = other.GetComponent<NetworkObject>();
                if (netObject != null)
                {
                    int playerIndex = PlayerPositionSync.Players.IndexOf(netObject.GetComponent<PlayerPositionSync>());
                    if (playerIndex >= 0)
                    {
                        parentAction.RemovePlayer(playerIndex);
                    }
                    else
                    {
                        Debug.LogError("PlayerPositionSync component not found on player.");
                    }
                }
                else
                {
                    Debug.LogError("NetworkObject component not found on player.");
                }
            }
        }
    }
}
