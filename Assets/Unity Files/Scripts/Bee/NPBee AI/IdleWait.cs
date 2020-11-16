using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeSystem.NPBee
{
    public class IdleWait : INPBee
    {
        private float m_WaitTime;

        private INPBee m_NextState;

        // If no next state is defined, wait again.
        public IdleWait(float waitTime)
        {
            //Debug.Log("Waiting for " + waitTime + " seconds...");
            m_WaitTime = Time.time + waitTime;
            m_NextState = null;
        }

        public IdleWait(float waitTime, INPBee nextState)
        {
            m_WaitTime = Time.time + waitTime;
            m_NextState = nextState;
        }

        public void Update(NPBeeController wrapper)
        {
            if (Time.time > m_WaitTime)
            {
                // Done!
                wrapper.State = m_NextState;
            }
            else
            {
                // Otherwise,
                wrapper.Bee.Move(Vector3.zero);
                Debug.DrawLine(wrapper.transform.position, Vector3.up, wrapper.DrawLineColor);
                // TODO: Call only once
                wrapper.TargetPosition = Vector3.zero;
                NPBeeManager.UpdateDestination(wrapper);
            }
        }
    }
}