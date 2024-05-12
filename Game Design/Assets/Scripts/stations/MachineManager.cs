using System;
using System.Collections.Generic;
using machines;
using UnityEngine;

namespace managers
{
    public class MachineManager : MonoBehaviour
    {
        public List<GameObject> machines;
        private readonly List<Tuple<GameObject, Machine>> _machines = new List<Tuple<GameObject, Machine>>();

        private List<Tuple<GameObject, Machine>> machinesInRadius = new List<Tuple<GameObject, Machine>>();
        private float dropRadius = 1.5f;

        private void Start()
        {
            foreach (var machine in machines)
            {
                _machines.Add(new Tuple<GameObject, Machine>(machine, machine.GetComponent<Machine>()));
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

        public List<Tuple<GameObject, Machine>> getMachinesInRadius()
        {
            return machinesInRadius;
        }

    }
}
