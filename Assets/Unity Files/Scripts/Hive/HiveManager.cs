using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiveSystem
{
    public class HiveManager : MonoBehaviour
    {
        public static List<HiveController> HiveControllers;

        private void Awake()
        {
            HiveControllers = new List<HiveController>();
        }

        // + + + + + | Functions | + + + + +

        // Returns Transform of nearest flower to bee Transform
        public static Transform GetNearestHive(Transform bee)
        {
            Transform current, min = null;
            float minDistance = Mathf.Infinity;

            foreach (HiveController fc in HiveControllers)
            {
                current = fc.GetComponent<Transform>();
                float distanceTo = Vector3.Distance(bee.position, current.position);

                if (distanceTo < minDistance)
                {
                    min = current;
                    minDistance = distanceTo;
                }
            }

            return min;
        }
    }
}