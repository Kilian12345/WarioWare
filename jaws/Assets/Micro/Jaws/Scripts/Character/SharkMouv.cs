using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Game.Jaws
{
    public class SharkMouv : MicroMonoBehaviour
    {
        [SerializeField] Transform PointA;
        [SerializeField] Transform PointB;

        [SerializeField] Rigidbody2D sharkBody;
        [SerializeField] Collider2D isGround;

        SpriteRenderer childRender;

        [SerializeField] Vector2 directionRight;
        [SerializeField] Vector2 directionLeft;
        Vector2 jumpDirection;


        public float sharkSpeed = 0.01f;
        float mouseBasePosition;

        [Space(10)]
        [Header("Curve")]
        public float curveHeight;
        public float curveWidth;

        float basePosition;
        float targetPosition;

        bool isGrounded;



        private void Start()
        {
            mouseBasePosition = 0;

            childRender = GetComponentInChildren<SpriteRenderer>();
            sharkBody = GetComponent<Rigidbody2D>();
            isGround = GetComponentInChildren<Collider2D>();

            Vector3 pos = transform.position;
            basePosition = pos.y;
            targetPosition = curveHeight;
            transform.position = pos;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space) && isGrounded == true)
            {
                Jump();
                isGrounded = false;
            }

            if (isGrounded == true)
            {
                Mouvement();
            }

            Flip();

        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.layer == 4)
            {
                isGrounded = true;
            }
        }

        void Flip()
        {
            if(mouseBasePosition > 0)
            {
                childRender.flipX = true;
            }
            else
            {
                childRender.flipX = false;
            }
        }


        void Mouvement()
        {
            mouseBasePosition += Input.GetAxis("Mouse X") * sharkSpeed;

            Vector3 pos = transform.position;
            pos.x += mouseBasePosition;
            pos.x = Mathf.Clamp(pos.x, PointA.position.x, PointB.position.x);
            transform.position = pos;
        }

        void Jump()
        {
            if (mouseBasePosition < 0)
            { jumpDirection = directionLeft; }
            else if (mouseBasePosition > 0)
            {jumpDirection = directionRight;}

            if (childRender.flipX == true)
            {
                mouseBasePosition = 0.1f;
            }
            else
            {
                mouseBasePosition = 0;
            }

            sharkBody.AddForce(jumpDirection * 500.0f);
        }


    }
}