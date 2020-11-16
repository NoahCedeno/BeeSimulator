using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlowerSystem
{
    public class FlowerManager : MonoBehaviour
    {
        private float m_TargetTime;
        public float SecondsUntilPollenTick = 5f;

        public static List<FlowerController> FlowerControllers;

        // PollenTick Event
        public delegate void PollenTick();

        public static event PollenTick OnPollenTick;

        // Awake is called before start!
        private void Awake()
        {
            FlowerControllers = new List<FlowerController>();
        }

        private void Start()
        {
            m_TargetTime = Time.time + SecondsUntilPollenTick;

            // Call an initial OnPollenTick
            OnPollenTick();
        }

        private void Update()
        {
            // Ticks pollen growth randomly from 5-9 seconds.
            if (Time.time > m_TargetTime)
            {
                OnPollenTick();
                SecondsUntilPollenTick = Random.Range(5f, 9f);
                m_TargetTime = Time.time + SecondsUntilPollenTick;
                // Debug.Log("PollenTick!");
            }
        }

        // + + + + + | Functions | + + + + +

        // Returns Transform of nearest flower to bee Transform
        public static Transform GetNearestFlower(Transform bee)
        {
            Transform current, min = null;
            float minDistance = Mathf.Infinity;

            foreach (FlowerController fc in FlowerControllers)
            {
                current = fc.GetComponent<Transform>();
                float distanceTo = Vector3.Distance(bee.position, current.position);

                // If shortest AND has pollen,
                if (distanceTo < minDistance && fc.CurrentPollenYield > 0)
                {
                    min = current;
                    minDistance = distanceTo;
                }
            }

            return min;
        }
    }
}