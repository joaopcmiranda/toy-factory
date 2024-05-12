using System;
using System.Collections.Generic;
using machines;
using UnityEngine;

namespace managers
{
    public class MachineManager : MonoBehaviour
    {
        public List<GameObject> machines;
        private readonly List<Tuple<GameObject, Machine_Level4>> _machines = new List<Tuple<GameObject, Machine_Level4>>();

        private List<Tuple<GameObject, Machine_Level4>> machinesInRadius = new List<Tuple<GameObject, Machine_Level4>>();
        private float dropRadius = 1.5f;

        private void Start()
        {
            foreach (var machine in machines)
            {
                _machines.Add(new Tuple<GameObject, Machine_Level4>(machine, machine.GetComponent<Machine_Level4>()));
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

        public List<Tuple<GameObject, Machine_Level4>> getMachinesInRadius()
        {
            return machinesInRadius;
        }

    }
}
