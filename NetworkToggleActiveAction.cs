/*using UnityEngine;
using HutongGames.PlayMaker;
using Unity.Netcode;

[ActionCategory("Netcode")]
public class NetworkToggleActiveAction : FsmStateAction
{
    [RequiredField]
    public FsmOwnerDefault gameObject;

    public FsmBool isActive;

    private NetworkObject networkObject;

    public override void Reset()
    {
        gameObject = null;
        isActive = false;
    }

    public override void OnEnter()
    {
        GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
        if (go != null)
        {
            networkObject = go.GetComponent<NetworkObject>();
            if (networkObject != null && networkObject.IsOwner)
            {
                SetActiveState(networkObject.NetworkObjectId, isActive.Value);
            }
        }
        Finish();
    }

    private void SetActiveState(ulong networkObjectId, bool active)
    {
        if (networkObject.IsOwner)
        {
            ToggleActiveServerRpc(networkObjectId, active);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void ToggleActiveServerRpc(ulong networkObjectId, bool active)
    {
        ToggleActiveClientRpc(networkObjectId, active);
    }

    [ClientRpc]
    private void ToggleActiveClientRpc(ulong networkObjectId, bool active)
    {
        NetworkObject networkObject = NetworkManager.Singleton.SpawnManager.SpawnedObjects[networkObjectId];
        if (networkObject != null)
        {
            networkObject.gameObject.SetActive(active);
        }
    }
}
*/