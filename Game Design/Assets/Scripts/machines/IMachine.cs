using UnityEngine;

namespace machines
{

    public interface IMachine
    {
        float DropRadius { get; }
        void HoldItem(GameObject item);
        GameObject TakeItem();
        bool IsHoldingItem();
        Transform MachineTransform { get; }
    }

}
