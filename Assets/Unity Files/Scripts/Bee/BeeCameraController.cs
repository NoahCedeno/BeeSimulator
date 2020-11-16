using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeSystem
{
    public class BeeCameraController : MonoBehaviour
    {
        public float SmoothSpeed = 0.125f;
        private Vector3 CamOffset;

        private Transform m_Root;

        private void Start()
        {
            m_Root = GetComponentInParent<Transform>();
            CamOffset = transform.position - m_Root.position;
        }

        private void LateUpdate()
        {
            Vector3 desiredPosition = m_Root.position + CamOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, SmoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(m_Root);
        }
    }
}