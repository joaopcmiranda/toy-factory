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

        private void Start()
        {
            foreach (var machine in machines)
            {
                _machines.Add(new Tuple<GameObject, Machine>(machine, machine.GetComponent<Machine>()));
            }
        }

        private Machine _previouslyHighlightedMachine;

        public Tuple<GameObject, Machine> GetNearestMachineTupleWithinDropRadius(Transform target)
        {
            Tuple<GameObject, Machine> nearestMachine = null;
            var nearestDistance = Mathf.Infinity;

            foreach (var machine in _machines)
            {
                var distance = Vector2.Distance(machine.Item1.transform.position, target.position);
                Debug.Log($"Checking machine {machine.Item1.name} at distance {distance}");

                if (machine.Item2 && distance <= machine.Item2.dropRadius && distance < nearestDistance)
                {
                    Debug.Log($"New nearest machine: {machine.Item1.name}");
                    nearestMachine = machine;
                    nearestDistance = distance;
                }
            }
            return nearestMachine;
        }


        public Machine HighlightNearestMachineWithinRadius(Transform target)
        {
            Debug.Log("HighlightNearestMachineWithinRadius called");
            var nearestMachineTuple = GetNearestMachineTupleWithinDropRadius(target);
            var nearestMachineComponent = nearestMachineTuple?.Item2;
            var nearestMachine = nearestMachineTuple?.Item1;


            if (nearestMachineComponent != _previouslyHighlightedMachine)
            {
                if (_previouslyHighlightedMachine)
                {
                    _previouslyHighlightedMachine.SetMachineColor(Color.white);
                }

                if (nearestMachine)
                {
                    nearestMachineComponent.SetMachineColor(Color.grey);
                    Debug.Log(nearestMachine.ToString());
                    _previouslyHighlightedMachine = nearestMachineComponent;
                }
                else
                {
                    _previouslyHighlightedMachine = null;
                }
            }
            return nearestMachineComponent;
        }

    }
}
