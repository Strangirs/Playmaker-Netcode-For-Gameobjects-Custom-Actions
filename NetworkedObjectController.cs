using UnityEngine;
using Unity.Netcode;

public class NetworkedObjectController : NetworkBehaviour
{
    public GameObject targetGameObject;

/*    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8) && IsHost)
        {
            ToggleActiveStateServerRpc();
        }
    }*/

    public void SetActiveState(bool state)
    {
        SetActiveStateClientRpc(state);
    }

    [ClientRpc]
    private void SetActiveStateClientRpc(bool state)
    {
        if (targetGameObject != null)
        {
            targetGameObject.SetActive(state);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ToggleActiveStateServerRpc()
    {
        if (targetGameObject != null)
        {
            bool newState = !targetGameObject.activeSelf;
            SetActiveState(newState);
        }
    }
}
