using System;
using System.Collections.Generic;
using machines;
using UnityEngine;

namespace managers
{
    public class MachineManager_Level4 : MonoBehaviour
    {
        public List<GameObject> machines;
        public readonly List<Tuple<GameObject, Machine_Base_Level4>> _machines = new List<Tuple<GameObject, Machine_Base_Level4>>();

        public List<Tuple<GameObject, Machine_Base_Level4>> machinesInRadius = new List<Tuple<GameObject, Machine_Base_Level4>>();
        public Machine_Base_Level4 _previouslyHighlightedMachine;
        public float dropRadius = 1.5f;

        private void Start()
        {
            foreach (var machine in machines)
            {
                _machines.Add(new Tuple<GameObject, Machine_Base_Level4>(machine, machine.GetComponent<Machine_Level4>()));
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


        public void HighlightMousedOverMachineWithinRadius(Transform target)
        {
            GetNearestMachineTuplesWithinDropRadius(target);

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            bool foundMachineInRadius = false;

            if (hit.collider != null)
            {
                Machine_Base_Level4 machineComponent = hit.collider.GetComponent<Machine_Base_Level4>();

                foreach (var machine in machinesInRadius)
                {
                    if (machine.Item2 == machineComponent)
                    {
                        foundMachineInRadius = true;
                        if (machine.Item2 != _previouslyHighlightedMachine)
                        {
                            if (_previouslyHighlightedMachine)
                            {
                                _previouslyHighlightedMachine.SetMachineColor(Color.white);
                            }

                            machine.Item2.SetMachineColor(Color.grey);
                            _previouslyHighlightedMachine = machine.Item2;
                        }
                    }
                    else
                    {
                        machine.Item2.SetMachineColor(Color.white);
                    }
                }
            }

            if (!foundMachineInRadius)
            {
                // Dehighlight any previously highlighted machine if it is no longer in the radius
                if (_previouslyHighlightedMachine)
                {
                    _previouslyHighlightedMachine.SetMachineColor(Color.white);
                    _previouslyHighlightedMachine = null;
                }
            }
        }

        public List<Tuple<GameObject, Machine_Base_Level4>> getMachinesInRadius()
        {
            return machinesInRadius; 
        }

    }
}
