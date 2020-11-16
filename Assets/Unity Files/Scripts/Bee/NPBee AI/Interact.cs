using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeSystem.NPBee
{
    public class Interact : INPBee
    {
        public void Update(NPBeeController wrapper)
        {
            // TODO: Call only once
            wrapper.TargetPosition = Vector3.zero;
            NPBeeManager.UpdateDestination(wrapper);

            if (wrapper.Other != null)
            {
                wrapper.Bee.Interact(wrapper.Other);
                //Debug.Log(wrapper.gameObject.name + ": Interaction Successful!");
            }
            else
            {
                //Debug.Log(wrapper.gameObject.name + ": Can't interact...");
            }

            // Let Wrapper decide
            wrapper.State = null;
        }
    }
}