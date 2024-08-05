// Requires "NetworkedObjectController" script in order to work.

using UnityEngine;
using HutongGames.PlayMaker;
using Unity.Netcode;

[ActionCategory("Netcode")]
public class NetworkToggleActiveState : FsmStateAction
{
    [RequiredField]
    [CheckForComponent(typeof(NetworkedObjectController))]
    [HutongGames.PlayMaker.Tooltip("This requires the NetworkedObjectController Script attached AND configured - The GameObject with the NetworkedObjectController component.")]
    public FsmOwnerDefault gameObject;

    [HutongGames.PlayMaker.Tooltip("The target GameObject to activate/deactivate.")]
    public FsmGameObject targetGameObject;

    [HutongGames.PlayMaker.Tooltip("Check to activate the GameObject, uncheck to deactivate.")]
    public FsmBool activate;

    private NetworkedObjectController networkedObjectController;

    public override void Reset()
    {
        gameObject = null;
        targetGameObject = null;
        activate = true;
    }

    public override void OnEnter()
    {
        DoToggleActiveState();
        Finish();
    }

    private void DoToggleActiveState()
    {
        var go = Fsm.GetOwnerDefaultTarget(gameObject);
        if (go == null) return;

        networkedObjectController = go.GetComponent<NetworkedObjectController>();
        if (networkedObjectController == null) return;

        networkedObjectController.targetGameObject = targetGameObject.Value;

        if (NetworkManager.Singleton.IsHost)
        {
            networkedObjectController.SetActiveState(activate.Value);
        }
    }
}