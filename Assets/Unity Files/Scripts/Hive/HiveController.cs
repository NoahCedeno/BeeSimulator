using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeeSystem;

namespace HiveSystem
{
    public class HiveController : MonoBehaviour
    {
        public float TotalPollen = 0;
        public Dictionary<BeeController, float> Contributions;

        private BeeController m_TopContributor;
        private TextMesh PollenText;

        private void Start()
        {
            m_TopContributor = null;
            Contributions = new Dictionary<BeeController, float>();
            PollenText = transform.parent.GetComponentInChildren<TextMesh>();

            // Initialize PollenText
            UpdatePollenText();

            // Add Hive to List
            HiveManager.HiveControllers.Add(this);
        }

        // + + + + + | Functions | + + + + +

        // Deposits Pollen to the Hive
        public void DepositPollen(float amount, BeeController bee)
        {
            // If we know the bee, add the amount
            if (Contributions.ContainsKey(bee))
            {
                Contributions[bee] += amount;
            }
            // If not, create a new entry.
            else
            {
                Contributions.Add(bee, amount);
            }

            TotalPollen += amount;

            Debug.Log("Contribution of: " + amount + " made by: " + bee.gameObject.name);
            PrintContributions();

            UpdatePollenText();
        }

        // Update PollenText TextMesh above the Hive
        private void UpdatePollenText()
        {
            GetTopContributor();

            if (m_TopContributor != null)
            {
                PollenText.text = TotalPollen.ToString() + "\n" + "Top Contributor: " + m_TopContributor.gameObject.name;
            }
            else
            {
                PollenText.text = TotalPollen.ToString();
            }
        }

        // Find Top Contributor
        private void GetTopContributor()
        {
            float maxPollen = 0f;

            if (Contributions.Keys.Count > 0)
            {
                foreach (BeeController bee in Contributions.Keys)
                {
                    if (Contributions[bee] > maxPollen)
                    {
                        m_TopContributor = bee;
                    }
                }
            }

            /*
            if (m_TopContributor == null)
            {
                Debug.Log("No top contributor.");
            }
            else
            {
                Debug.Log("TopContributor at Hive " + transform.parent.name + " is " + m_TopContributor.gameObject.name);
            }
            */
        }

        // Prints Contributions in the Console
        private void PrintContributions()
        {
            string finalMessage = "";

            foreach (BeeController bee in Contributions.Keys)
            {
                finalMessage += bee.gameObject.name + ": " + Contributions[bee] + "\n";
            }

            Debug.Log(finalMessage);
        }
    }
}