using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

namespace OManip.game
{
    public class FPSCameraView : View
    {
        private Vector3 _oldMousePos;
 
        public float flySpeed = 1.0f;

        public float rotationSpeed = 10.0f;

        void Update()
        {
            if (Input.GetKey(KeyCode.W))
                transform.position += transform.forward * flySpeed * Time.deltaTime;
            else if (Input.GetKey(KeyCode.S))
                transform.position += transform.forward * (-flySpeed) * Time.deltaTime;

            if (Input.GetKey(KeyCode.D))
                transform.position += transform.right * flySpeed * Time.deltaTime;
            else if (Input.GetKey(KeyCode.A))
                transform.position += transform.right * (-flySpeed) * Time.deltaTime;

            if (Input.GetMouseButtonDown(1))
            {
                _oldMousePos = Input.mousePosition;
            }
            else if (Input.GetMouseButton(1))
            {
                Vector3 newMousePos = Input.mousePosition;
                Vector3 deltaMousePos = (newMousePos - _oldMousePos) * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, deltaMousePos.x, Space.World);
                transform.Rotate(transform.right, -deltaMousePos.y, Space.World);
                _oldMousePos = newMousePos;
            }
        }
    }
}