using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeSystem.NPBee
{
    [RequireComponent(typeof(BeeController))]
    public class NPBeeController : MonoBehaviour
    {
        public INPBee State;

        public Vector3 TargetPosition;

        public BeeController Bee;
        public Color DrawLineColor;
        public Collider Other;

        private void Start()
        {
            Bee = GetComponent<BeeController>();

            // Add to NPBeeList & Initialize Destinations
            NPBeeManager.NPBees.Add(this);
            NPBeeManager.UpdateDestination(this);

            //State = new FlyTo(FlowerSystem.FlowerManager.FlowerControllers[0].GetComponent<Transform>().position);
        }

        private void Update()
        {
            if (State != null)
            {
                State.Update(this);
            }
            else
            {
                DetermineState();
            }
        }

        // + + + + + | Functions | + + + + +
        public void DetermineState()
        {
            // Find flowers to harvest max pollen!
            if (Bee.Pollen < Bee.MaxPollenAmount)
            {
                // Debug.Log("To the nearest Flower!");
                if (NPBeeManager.IsFlowerAvailable(FlowerSystem.FlowerManager.GetNearestFlower(transform).GetComponent<Transform>().position))
                {
                    State = new FlyTo(FlowerSystem.FlowerManager.GetNearestFlower(transform));
                }
                else
                {
                    //Debug.Log(gameObject.name + ": Unavailable...");
                    State = new IdleWait(0.5f);
                }
            }
            // If pollen is max, go to the hive!
            else if (Bee.Pollen == Bee.MaxPollenAmount)
            {
                // Debug.Log("To the Hive!");
                State = new FlyTo(HiveSystem.HiveManager.GetNearestHive(transform));
            }
            else
            {
                Debug.Log("how did you even reach this...");
            }
        }

        // + + + + + | Collision Handling | + + + + +

        // When triggering a collider, make it my collider
        public void OnTriggerEnter(Collider other)
        {
            Other = other;
        }

        // When leaving a collider, let it go
        public void OnTriggerExit(Collider other)
        {
            Other = null;
        }
    }
}