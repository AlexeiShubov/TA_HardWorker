using UnityEngine;

[CreateAssetMenu(fileName = "GlobalSettings", menuName = "SOGlobalSettings")]
public sealed class SOGlobalSettings : ScriptableObject
{
    [field: SerializeField] public States DefaultStateName { get; private set; }
    [field: SerializeField] public float SpendCurrencyDelay { get; private set; }
    [field: SerializeField] public float AddCurrencyDelay { get; private set; }
    [field: SerializeField] public int DefaultCurrencyAmount { get; private set; }
    [field: SerializeField] public int CurrencyAmountAdd { get; private set; }
    [field: SerializeField] public int CurrencyAmountSpend { get; private set; }
    
    [field: SerializeField] public float DelayAnimationCharacterMove { get; private set; }
    [field: SerializeField] public float DelayToChangeColorBG { get; private set; }
}
