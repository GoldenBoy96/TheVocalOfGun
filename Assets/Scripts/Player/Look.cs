using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Look : MonoBehaviour
    {
        public static bool isCursorLocked;

        public Transform Player;
        public Transform Cams;

        private float xSensitivity;
        private float ySensitivity;

        private float maxAngle;
        private float minAngle;

        private Quaternion camsCenter;

        public float XSensitivity { get => xSensitivity; set => xSensitivity = value; }
        public float YSensitivity { get => ySensitivity; set => ySensitivity = value; }
        public float MaxAngle { get => maxAngle; set => maxAngle = value; }
        public float MinAngle { get => minAngle; set => minAngle = value; }

        void Start()
        {
            isCursorLocked = true;
            xSensitivity = 600;
            ySensitivity = 600;
            MaxAngle = 90;
            MinAngle = 75;
            camsCenter = Cams.localRotation;
        }

        void Update()
        {
            SetX();
            SetY();
            UpdateCursorLocked();
        }


        void SetY()
        {
            float input = Input.GetAxis("Mouse Y") * YSensitivity * Time.deltaTime;
            Quaternion adjustment = Quaternion.AngleAxis(input, -Vector3.right);
            Quaternion delta = Cams.localRotation * adjustment;

            if (Cams.localRotation.x <= 0)
            {
                if (Quaternion.Angle(camsCenter, delta) < MaxAngle)
                {
                    Cams.localRotation = delta;
                }
            }
            else
            {
                if (Quaternion.Angle(camsCenter, delta) < MinAngle)
                {
                    Cams.localRotation = delta;
                }
            }
            
        }

        void SetX()
        {
            float input = Input.GetAxis("Mouse X") * XSensitivity * Time.deltaTime;
            Quaternion adjustment = Quaternion.AngleAxis(input, Vector3.up);
            Quaternion delta = Player.localRotation * adjustment;

            Player.localRotation = delta;
        }

        void UpdateCursorLocked()
        {
            if (isCursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    isCursorLocked = false;
                }
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    isCursorLocked = true;
                }
            }
        }
    }
}