using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeSystem.NPBee
{
    public class NPBeeManager : MonoBehaviour
    {
        public static Dictionary<NPBeeController, Vector3> NPBeeDestinations;
        public static List<NPBeeController> NPBees;

        private void Awake()
        {
            NPBees = new List<NPBeeController>();
            NPBeeDestinations = new Dictionary<NPBeeController, Vector3>();
        }

        // + + + + + | Functions | + + + + +

        // Updates Destination for Bees
        public static void UpdateDestination(NPBeeController bee)
        {
            if (NPBeeDestinations.ContainsKey(bee))
            {
                NPBeeDestinations[bee] = bee.TargetPosition;
            }
            else
            {
                NPBeeDestinations.Add(bee, bee.TargetPosition);
            }
        }

        // Checks if a flower is already being targeted
        // TODO: Doesn't check for self's flowerPosition
        public static bool IsFlowerAvailable(Vector3 flowerPosition)
        {
            foreach (Vector3 targetPosition in NPBeeDestinations.Values)
            {
                if (targetPosition == flowerPosition) return false;
            }
            return true;
        }
    }
}