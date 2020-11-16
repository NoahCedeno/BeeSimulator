using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeSystem.NPBee
{
    public class FlyTo : INPBee
    {
        private Vector3 m_TargetPosition;
        private float m_MinDistanceThreshold = 1.5f;

        public FlyTo(Transform target)
        {
            m_TargetPosition = target.position;
        }

        public FlyTo(Vector3 target)
        {
            // Debug.Log("Flying to " + target + "!");
            m_TargetPosition = target;
        }

        public void Update(NPBeeController wrapper)
        {
            // Acknowledge Target

            // TODO: Call only once
            wrapper.TargetPosition = m_TargetPosition;
            NPBeeManager.UpdateDestination(wrapper);

            // Point towards the target
            if (wrapper.Bee.transform.rotation != Quaternion.LookRotation(m_TargetPosition - wrapper.Bee.transform.position, Vector3.up))
            {
                //Debug.Log("Not looking at target");

                // Lock Onto it
                //wrapper.Bee.transform.rotation = Quaternion.LookRotation(m_TargetPosition - wrapper.Bee.transform.position, Vector3.up);

                // Look At it
                wrapper.Bee.transform.LookAt(m_TargetPosition);

                // Yaw Towards it
                //wrapper.Bee.Turn(Vector3.SignedAngle(m_TargetPosition, wrapper.Bee.transform.position, Vector3.forward) > 0 ? 0.25f : -0.25f);
            }
            else
            {
                //Debug.Log("Looking at target, firing RayCast!");
                //Debug.DrawLine(wrapper.transform.position, wrapper.transform.forward, Color.yellow);

                // If there's a clear path to the target
                if (Physics.Raycast(wrapper.transform.position, wrapper.transform.forward, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Default"), QueryTriggerInteraction.Collide))
                {
                    //Debug.Log("RayCast hit a: " + hit.collider.gameObject.tag);

                    // AND if that hit is a trigger collider of a Flower or Hive,
                    if (hit.collider.isTrigger && (hit.collider.gameObject.CompareTag("Hive") || hit.collider.gameObject.CompareTag("Flower")))
                    {
                        //Debug.Log("We've hit a valid target!");
                        // TODO: Use math to account for distance left at current speed to break
                        if (Vector3.Distance(wrapper.transform.position, m_TargetPosition) > m_MinDistanceThreshold)
                        {
                            // Fly towards it
                            //wrapper.Bee.Move((m_TargetPosition - wrapper.transform.position).normalized);
                            wrapper.Bee.Move(Vector3.forward);

                            Debug.DrawLine(wrapper.transform.position, m_TargetPosition, wrapper.DrawLineColor);
                        }
                        else
                        {
                            // Break
                            // Debug.Log("Done!");
                            wrapper.Bee.Move(Vector3.zero);
                            // We are done, let NPBee determine State.
                            wrapper.State = new Interact();
                        }
                    }
                    else
                    {
                        /* Where even are we?
                         *
                         * We are looking at the target
                         * There is a clear path to the target
                         * It's not a trigger collider or a Hive or Flower
                         *
                         * So what do we do?
                         *
                         * If I leave this undefined, we won't move
                         *
                         */
                        //Debug.Log("Hit an invalid target, moving around!");
                        wrapper.Bee.Move(Vector3.one);
                    }
                }
                else
                {
                    //Debug.Log("Raycast failed, didn't hit target, must fix height.");
                    // Compute how to move accordingly
                    Vector3 losAcceleration;
                    // Vertical Difference
                    losAcceleration.y = (m_TargetPosition.y > wrapper.transform.position.y) ? 0.5f : -0.5f;

                    // At this point, we've failed a Raycast for a clear path, so we may be too low.
                    // I should try navigating to the front of the mesh, however.
                }
            }
        }
    }
}