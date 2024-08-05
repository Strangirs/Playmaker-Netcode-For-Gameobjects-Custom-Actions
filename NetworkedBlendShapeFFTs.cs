/*using UnityEngine;
using HutongGames.PlayMaker;
using Crest;
using Unity.Netcode;

[ActionCategory(ActionCategory.ScriptControl)]
public class NetworkedBlendShapeFFTs : FsmStateAction
{
    [RequiredField]
    [CheckForComponent(typeof(ShapeFFT))]
    public FsmOwnerDefault calmWaveFFT;

    [RequiredField]
    [CheckForComponent(typeof(ShapeFFT))]
    public FsmOwnerDefault aggressiveWaveFFT;

    [RequiredField]
    public FsmFloat blendDuration;

    private ShapeFFT calmWaveFFTComponent;
    private ShapeFFT aggressiveWaveFFTComponent;

    private NetworkVariable<float> calmWaveWeight = new NetworkVariable<float>();
    private NetworkVariable<float> aggressiveWaveWeight = new NetworkVariable<float>();

    private float blendTimer;
    private bool isBlending;
    private bool isBlendingToCalm;

    public override void Reset()
    {
        calmWaveFFT = null;
        aggressiveWaveFFT = null;
        blendDuration = 600f;
    }

    public override void OnEnter()
    {
        var calmGo = Fsm.GetOwnerDefaultTarget(calmWaveFFT);
        if (calmGo != null)
        {
            calmWaveFFTComponent = calmGo.GetComponent<ShapeFFT>();
        }

        var aggressiveGo = Fsm.GetOwnerDefaultTarget(aggressiveWaveFFT);
        if (aggressiveGo != null)
        {
            aggressiveWaveFFTComponent = aggressiveGo.GetComponent<ShapeFFT>();
        }

        if (calmWaveFFTComponent == null || aggressiveWaveFFTComponent == null)
        {
            Finish();
            return;
        }

        calmWaveWeight.OnValueChanged += OnCalmWaveWeightChanged;
        aggressiveWaveWeight.OnValueChanged += OnAggressiveWaveWeightChanged;

        if (IsServer)
        {
            blendTimer = 0f;
            isBlending = true;
            isBlendingToCalm = true;

            // Initialize weights
            calmWaveWeight.Value = 1f;
            aggressiveWaveWeight.Value = 0f;
        }
    }

    public override void OnUpdate()
    {
        if (!IsServer || !isBlending) return;

        blendTimer += Time.deltaTime;

        float blendFactor = blendTimer / blendDuration.Value;

        if (isBlendingToCalm)
        {
            calmWaveWeight.Value = Mathf.Lerp(0f, 1f, blendFactor);
            aggressiveWaveWeight.Value = Mathf.Lerp(1f, 0f, blendFactor);
        }
        else
        {
            calmWaveWeight.Value = Mathf.Lerp(1f, 0f, blendFactor);
            aggressiveWaveWeight.Value = Mathf.Lerp(0f, 1f, blendFactor);
        }

        if (blendTimer >= blendDuration.Value)
        {
            blendTimer = 0f;
            isBlendingToCalm = !isBlendingToCalm;
            Finish();
        }
    }

    private void OnCalmWaveWeightChanged(float oldValue, float newValue)
    {
        if (calmWaveFFTComponent != null)
        {
            calmWaveFFTComponent.Weight = newValue;
        }
    }

    private void OnAggressiveWaveWeightChanged(float oldValue, float newValue)
    {
        if (aggressiveWaveFFTComponent != null)
        {
            aggressiveWaveFFTComponent.Weight = newValue;
        }
    }

    public override void OnExit()
    {
        calmWaveWeight.OnValueChanged -= OnCalmWaveWeightChanged;
        aggressiveWaveWeight.OnValueChanged -= OnAggressiveWaveWeightChanged;
    }

    private bool IsServer
    {
        get
        {
            return NetworkManager.Singleton != null && NetworkManager.Singleton.IsServer;
        }
    }
}
*/