using UnityEngine;
using Unity.Netcode;
using HutongGames.PlayMaker;

[ActionCategory("Netcode")]
public class WaitForNetworkStart : FsmStateAction
{
    [HutongGames.PlayMaker.Tooltip("Event to send when the network is started.")]
    public FsmEvent networkStartedEvent;

    private bool isNetworkStarted = false;

    public override void OnEnter()
    {
        isNetworkStarted = NetworkManager.Singleton.IsListening;
        if (isNetworkStarted)
        {
            Fsm.Event(networkStartedEvent);
            Finish();
        }
    }

    public override void OnUpdate()
    {
        if (!isNetworkStarted && NetworkManager.Singleton.IsListening)
        {
            Fsm.Event(networkStartedEvent);
            Finish();
        }
    }

    public override void Reset()
    {
        networkStartedEvent = null;
    }
}
