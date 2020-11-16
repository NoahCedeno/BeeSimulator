using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeSystem
{
    public class BeeController : MonoBehaviour
    {
        private Vector3 Acceleration;
        private float FlySpeed, TurnSpeed;
        private float yawDirection;

        public float Pollen, MaxPollenAmount;

        private Rigidbody rb;

        private void Start()
        {
            Pollen = 0;
            MaxPollenAmount = 10f;

            FlySpeed = 20f;
            TurnSpeed = 2f;

            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            // Braking
            if (Vector3.Distance(Acceleration, Vector3.zero) < 0.1f)
            {
                rb.drag = 10f;
            }
            else
            {
                rb.drag = 1f;
            }
        }

        private void FixedUpdate()
        {
            // Turn on Y / Yaw
            transform.Rotate(Vector3.up * yawDirection * TurnSpeed, Space.Self);

            // Finally, Apply Force one the Local Rotation

            rb.AddRelativeForce(Acceleration * FlySpeed, ForceMode.Acceleration);
        }

        // + + + + + | Functions | + + + + +

        // Sets the Acceleration to an input movement vector
        public void Move(Vector3 accelInput)
        {
            Acceleration = accelInput;
        }

        // Sets yawDirection according to a value of -1, 0, or 1.
        public void Turn(float yawDirectionInput)
        {
            yawDirection = yawDirectionInput;
        }

        // Interacts with whoever is in the collider
        public void Interact(Collider other)
        {
            // Flower -> Harvest Pollen
            if (other.CompareTag("Flower"))
            {
                AddPollen(other.gameObject.GetComponent<FlowerSystem.FlowerController>().HarvestPollen());
            }

            // Hive -> Deposit Pollen
            if (other.CompareTag("Hive"))
            {
                if (Pollen > 0)
                {
                    other.gameObject.GetComponent<HiveSystem.HiveController>().DepositPollen(Pollen, this);
                    // After Depositing Pollen, set to 0 again.
                    Pollen = 0;
                }
            }
        }

        // Adds Pollen, accounting for MaxPollenAmount
        private void AddPollen(float amount)
        {
            if (Pollen + amount > MaxPollenAmount)
            {
                Pollen = MaxPollenAmount;
            }
            else
            {
                Pollen += amount;
            }
        }
    }
}