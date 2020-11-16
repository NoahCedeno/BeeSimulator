using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlowerSystem
{
    public class FlowerSpinner : MonoBehaviour
    {
        public bool IsPlayerNear = false;
        private float m_RotationSpeed = 1f;

        public float DistanceFromPlayer = -1f;
        private float m_PlayerDistanceThreshold = 7.5f;

        private Transform m_PlayerTransform;
        public TextMesh m_PollenText;

        private void Start()
        {
            m_PlayerTransform = GameObject.Find("PlayerBee").GetComponent<Transform>();
            m_PollenText = GetComponentInChildren<TextMesh>();
        }

        private void Update()
        {
            // Calculate Distance from Player
            DistanceFromPlayer = Vector3.Distance(m_PlayerTransform.position, transform.position);
            // Activate PollenText if close enough
            m_PollenText.gameObject.SetActive(IsPlayerNear);

            // If the player is close,
            if (DistanceFromPlayer < m_PlayerDistanceThreshold)
            {
                // Begin rotating, make it known that the player is near!
                IsPlayerNear = true;
                m_RotationSpeed = Mathf.Clamp(m_PlayerDistanceThreshold - DistanceFromPlayer, 0, 1.5f);
                transform.Rotate(Vector3.up * m_RotationSpeed);
            }
            else
            {
                // Otherwise,
                IsPlayerNear = false;
                return;
            }
        }
    }
}