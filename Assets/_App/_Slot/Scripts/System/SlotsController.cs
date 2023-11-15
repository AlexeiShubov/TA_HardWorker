using AxGrid.Base;
using AxGrid.Model;
using UnityEngine;

public sealed class SlotsController : MonoBehaviourExtBind
{
    [SerializeField] private GameObject _particleEffect;
    [SerializeField] private GameObject[] _resultEffect;
    [SerializeField] private Slot[] _slots;

    [OnAwake]
    private void SlotInitialize()
    {
        foreach (var slot in _slots)
        {
            slot.Init();
        }
    }

    [Bind(Keys.MovingState)]
    private void ActivateSlots()
    {
        _particleEffect.SetActive(true);
        
        foreach (var slot in _slots)
        {
            slot.DoAction();
        }
    }

    [Bind(Keys.StoppingState)]
    private void DeactivateSlots()
    {
        _particleEffect.SetActive(false);
        
        foreach (var slot in _slots)
        {
            slot.StopSlot(Random.Range(0, 3));
        }
    }

    [Bind(Keys.AllBlocksIsIdle)]
    private void ActiveResultAffect()
    {
        foreach (var effect in _resultEffect)
        {
            effect.SetActive(true);
        }
    }
}
