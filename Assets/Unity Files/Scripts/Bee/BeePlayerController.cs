using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeSystem
{
    public class BeePlayerController : MonoBehaviour
    {
        public Vector3 Acceleration;
        public float FlySpeed, TurnSpeed, BrakingSpeed;
        public float yawDirection;

        private Rigidbody rb;

        private void Start()
        {
            FlySpeed = 20f;
            TurnSpeed = 2f;
            BrakingSpeed = 10f;

            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (Input.GetAxis("Horizontal") != 0) // X Movement
            {
                Acceleration.x = Input.GetAxis("Horizontal");
            }

            if (Input.GetAxis("Vertical") != 0) // Z Movement
            {
                Acceleration.z = Input.GetAxis("Vertical");
            }

            // Y Movement
            int verticalInput = 0;
            if (Input.GetKey(KeyCode.E)) verticalInput = 1;
            else if (Input.GetKey(KeyCode.Q)) verticalInput = -1;

            Acceleration.y = verticalInput;

            // Turn on Y / Yaw
            yawDirection = 0;
            if (Input.GetKey(KeyCode.J)) yawDirection = -1;
            else if (Input.GetKey(KeyCode.L)) yawDirection = 1;

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
    }
}