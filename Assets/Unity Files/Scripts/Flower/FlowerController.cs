using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FlowerSystem
{
    public class FlowerController : MonoBehaviour
    {
        public float MaxPollenYield;
        public float CurrentPollenYield;

        private TextMesh m_PollenText;
        private ParticleSystem m_PollenPS;

        private void Start()
        {
            MaxPollenYield = Mathf.Round(Random.Range(1f, 10f));
            CurrentPollenYield = 0f;

            m_PollenText = transform.parent.GetComponentInChildren<TextMesh>();
            m_PollenPS = transform.parent.GetComponentInChildren<ParticleSystem>();

            // Initialize PollenText
            UpdatePollenText();

            // Add to FlowerManager List
            FlowerManager.FlowerControllers.Add(this);
        }

        private void OnEnable()
        {
            FlowerManager.OnPollenTick += Tick;
        }

        private void OnDisable()
        {
            FlowerManager.OnPollenTick += Tick;
            FlowerManager.FlowerControllers.Remove(this);
        }

        private void Update()
        {
            // Plays and Stops the Particle Effect ONLY once per pollen update.
            if (CurrentPollenYield > 0f && !m_PollenPS.isPlaying)
            {
                m_PollenPS.Play();
            }
            else if (CurrentPollenYield == 0f && m_PollenPS.isPlaying)
            {
                m_PollenPS.Stop();
            }
            else return;
        }

        // + + + + + | Functions | + + + + +

        // Add a PRETTY small amount of pollen each time.
        private void Tick()
        {
            // Get pollenGain
            float pollenGain = MaxPollenYield / (Random.Range(5f, 10f));
            // Round to 2 Places
            pollenGain = (float)Mathf.Round(pollenGain * 100f) / 100f;

            // Evaluate Pollen Amount, apply pollenGain
            if (CurrentPollenYield + pollenGain > MaxPollenYield)
            {
                CurrentPollenYield = MaxPollenYield;
                m_PollenText.color = Color.red;
            }
            else
            {
                CurrentPollenYield += pollenGain;
                m_PollenText.color = Color.yellow;
            }

            UpdatePollenText();
        }

        // Updates the PollenText
        private void UpdatePollenText()
        {
            m_PollenText.text = CurrentPollenYield.ToString();
        }

        // Gives Pollen to a Bee
        public float HarvestPollen()
        {
            float pollenToYield = CurrentPollenYield;
            CurrentPollenYield = 0f;
            UpdatePollenText();

            return pollenToYield;
        }
    }
}