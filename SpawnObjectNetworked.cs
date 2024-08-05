using UnityEngine;
using Unity.Netcode;
using HutongGames.PlayMaker;

namespace PlaymakerNGO
{
    [ActionCategory("Netcode")]
    [HutongGames.PlayMaker.Tooltip("Spawns an object over the network using Netcode for GameObjects.")]
    public class SpawnObjectNetworked : FsmStateAction
    {
        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("The prefab to spawn.")]
        public FsmGameObject prefab;

        [HutongGames.PlayMaker.Tooltip("The position to spawn the object.")]
        public FsmVector3 spawnPosition;

        [HutongGames.PlayMaker.Tooltip("The rotation to spawn the object.")]
        public FsmQuaternion spawnRotation;

        public override void Reset()
        {
            prefab = null;
            spawnPosition = null;
            spawnRotation = null;
        }

        public override void OnEnter()
        {
            if (prefab.Value != null)
            {
                GameObject instance = GameObject.Instantiate(prefab.Value, spawnPosition.Value, spawnRotation.Value);
                NetworkObject networkObject = instance.GetComponent<NetworkObject>();
                if (networkObject != null)
                {
                    networkObject.Spawn();
                }
                else
                {
                    Debug.LogWarning("Prefab does not have a NetworkObject component.");
                }
            }
            else
            {
                Debug.LogWarning("Prefab is null.");
            }

            Finish();
        }
    }
}
