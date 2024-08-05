using UnityEngine;
using PlayMaker;
using Unity.Netcode;
using HutongGames.PlayMaker;

namespace PlaymakerNGO
{
    [ActionCategory("Netcode")]
    [HutongGames.PlayMaker.Tooltip("Not sure if this is functional as of now. Syncs a variable using Netcode for GameObjects.")]
    public class SyncNetworkVariableAction : FsmStateAction
    {
        public enum VariableType
        {
            Float,
            Int,
            Bool,
            String
        }

        [RequiredField]
        [HutongGames.PlayMaker.Tooltip("The type of variable to sync.")]
        public VariableType variableType;

        [HutongGames.PlayMaker.Tooltip("The PlayMaker float variable to sync.")]
        public FsmFloat floatVariable;

        [HutongGames.PlayMaker.Tooltip("The PlayMaker int variable to sync.")]
        public FsmInt intVariable;

        [HutongGames.PlayMaker.Tooltip("The PlayMaker bool variable to sync.")]
        public FsmBool boolVariable;

        [HutongGames.PlayMaker.Tooltip("The PlayMaker string variable to sync.")]
        public FsmString stringVariable;

        private NetworkVariable<float> networkFloatVariable;
        private NetworkVariable<int> networkIntVariable;
        private NetworkVariable<bool> networkBoolVariable;
        private NetworkVariable<string> networkStringVariable;

        public override void OnEnter()
        {
            switch (variableType)
            {
                case VariableType.Float:
                    networkFloatVariable = new NetworkVariable<float>(floatVariable.Value);
                    networkFloatVariable.OnValueChanged += OnNetworkFloatChanged;
                    break;
                case VariableType.Int:
                    networkIntVariable = new NetworkVariable<int>(intVariable.Value);
                    networkIntVariable.OnValueChanged += OnNetworkIntChanged;
                    break;
                case VariableType.Bool:
                    networkBoolVariable = new NetworkVariable<bool>(boolVariable.Value);
                    networkBoolVariable.OnValueChanged += OnNetworkBoolChanged;
                    break;
                case VariableType.String:
                    networkStringVariable = new NetworkVariable<string>(stringVariable.Value);
                    networkStringVariable.OnValueChanged += OnNetworkStringChanged;
                    break;
            }
        }

        public override void OnUpdate()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                switch (variableType)
                {
                    case VariableType.Float:
                        networkFloatVariable.Value = floatVariable.Value;
                        break;
                    case VariableType.Int:
                        networkIntVariable.Value = intVariable.Value;
                        break;
                    case VariableType.Bool:
                        networkBoolVariable.Value = boolVariable.Value;
                        break;
                    case VariableType.String:
                        networkStringVariable.Value = stringVariable.Value;
                        break;
                }
            }
        }

        private void OnNetworkFloatChanged(float oldValue, float newValue)
        {
            floatVariable.Value = newValue;
        }

        private void OnNetworkIntChanged(int oldValue, int newValue)
        {
            intVariable.Value = newValue;
        }

        private void OnNetworkBoolChanged(bool oldValue, bool newValue)
        {
            boolVariable.Value = newValue;
        }

        private void OnNetworkStringChanged(string oldValue, string newValue)
        {
            stringVariable.Value = newValue;
        }

        public override void OnExit()
        {
            switch (variableType)
            {
                case VariableType.Float:
                    networkFloatVariable.OnValueChanged -= OnNetworkFloatChanged;
                    break;
                case VariableType.Int:
                    networkIntVariable.OnValueChanged -= OnNetworkIntChanged;
                    break;
                case VariableType.Bool:
                    networkBoolVariable.OnValueChanged -= OnNetworkBoolChanged;
                    break;
                case VariableType.String:
                    networkStringVariable.OnValueChanged -= OnNetworkStringChanged;
                    break;
            }
        }
    }
}
