using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMachineManager
{
    float dropRadius { get; }
    void HoldItem(GameObject item);
    GameObject TakeItem();
    bool IsHoldingItem();
    Transform MachineTransform { get; }
}
