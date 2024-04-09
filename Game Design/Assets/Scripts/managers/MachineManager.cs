using System;
using System.Collections.Generic;
using machines;
using UnityEngine;

namespace managers
{
    public class MachineManager : MonoBehaviour
    {
       public List<GameObject> machines;
       private List<IMachine> _machines = new();

       private void Start()
       {
              foreach (var machine in machines)
              {
                _machines.Add(machine.GetComponent<IMachine>());
              }
       }
    }
}
