using UnityEngine;
using Unity.Netcode;
using HutongGames.PlayMaker;

namespace PlaymakerNGO
{
    [ActionCategory("Netcode")]
    [HutongGames.PlayMaker.Tooltip("Checks if the current instance is the server and transitions to the next state depending on the result.")]
    public class CheckIfServerAction : FsmStateAction
    {
        [HutongGames.PlayMaker.Tooltip("Event to send if is the server.")]
        public FsmEvent isServerEvent;

        [HutongGames.PlayMaker.Tooltip("Event to send if not the server.")]
        public FsmEvent isNotServerEvent;

        public override void OnEnter()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                Fsm.Event(isServerEvent);
            }
            else
            {
                Fsm.Event(isNotServerEvent);
            }

            Finish();
        }

        public override void Reset()
        {
            isServerEvent = null;
            isNotServerEvent = null;
        }
    }
}
