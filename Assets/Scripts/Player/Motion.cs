using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Motion : MonoBehaviour
    {

        private float speed;
        private Rigidbody rb;

        public float Speed { get => speed; set => speed = value; }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            speed = 200;
        }

        private void FixedUpdate()
        {
            float tHMove = Input.GetAxisRaw("Horizontal");
            float tVMove = Input.GetAxisRaw("Vertical");

            Vector3 tDirection = new(tHMove, 0, tVMove);
            tDirection.Normalize();

            rb.velocity = Speed * Time.deltaTime * transform.TransformDirection(tDirection);
        }
    }
}
