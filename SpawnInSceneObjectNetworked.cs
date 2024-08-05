using UnityEngine;
using Unity.Netcode;
using HutongGames.PlayMaker;

namespace CustomActions
{
    [ActionCategory("Netcode")]
    [HutongGames.PlayMaker.Tooltip("Spawns an in-scene object over the network using Netcode for GameObjects.")]
    public class SpawnInSceneObjectNetworked : FsmStateAction
    {
        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("The in-scene GameObject to spawn on the network.")]
        public FsmGameObject targetObject;

        public override void Reset()
        {
            targetObject = null;
        }

        public override void OnEnter()
        {
            if (targetObject.Value != null)
            {
                NetworkObject networkObject = targetObject.Value.GetComponent<NetworkObject>();
                if (networkObject != null)
                {
                    networkObject.Spawn();
                }
                else
                {
                    Debug.LogWarning("Target object does not have a NetworkObject component.");
                }
            }
            else
            {
                Debug.LogWarning("Target object is null.");
            }

            Finish();
        }
    }
}
