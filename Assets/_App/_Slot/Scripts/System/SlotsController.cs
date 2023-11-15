using AxGrid.Base;
using AxGrid.Model;
using UnityEngine;

public sealed class SlotsController : MonoBehaviourExtBind
{
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
        foreach (var slot in _slots)
        {
            slot.DoAction();
        }
    }

    [Bind(Keys.StoppingState)]
    private void DeactivateSlots()
    {
        foreach (var slot in _slots)
        {
            slot.StopSlot(Random.Range(0, 3));
        }
    }
}
