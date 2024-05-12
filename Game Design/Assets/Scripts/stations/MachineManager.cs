using System;
using System.Collections.Generic;
using machines;
using UnityEngine;

namespace managers
{
    public class MachineManager : MonoBehaviour
    {
        public List<GameObject> machines;
        private readonly List<Tuple<GameObject, Level4_Machines>> _machines = new List<Tuple<GameObject, Level4_Machines>>();

        private List<Tuple<GameObject, Level4_Machines>> machinesInRadius = new List<Tuple<GameObject, Level4_Machines>>();
        private float dropRadius = 1.5f;

        private void Start()
        {
            foreach (var machine in machines)
            {
                _machines.Add(new Tuple<GameObject, Level4_Machines>(machine, machine.GetComponent<Level4_Machines>()));
            }
        }

        public void GetNearestMachineTuplesWithinDropRadius(Transform target)
        {
            machinesInRadius.Clear();
            var nearestDistance = Mathf.Infinity;
            foreach (var machine in _machines)
            {
                var distance = Vector2.Distance(machine.Item1.transform.position, target.position);
                var machineComponent = machine.Item2;
                if (machineComponent && distance <= dropRadius && distance < nearestDistance)
                {
                    machinesInRadius.Add(machine);
                }
            }
        }

        public List<Tuple<GameObject, Level4_Machines>> getMachinesInRadius()
        {
            return machinesInRadius;
        }

    }
}
