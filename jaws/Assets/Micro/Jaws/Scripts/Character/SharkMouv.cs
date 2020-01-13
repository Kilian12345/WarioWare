using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Game.Jaws
{
    public class SharkMouv : MicroMonoBehaviour
    {
        GameManager gameMana;

        [SerializeField] Transform PointA;
        [SerializeField] Transform PointB;

        [SerializeField] Rigidbody2D sharkBody;
        Collider2D isGround;

        SpriteRenderer childRender;
        Animator animator;
        bool jumpAnimOnce = false;

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
        bool isGroundedObstacle;



        private void Start()
        {
            gameMana = FindObjectOfType<GameManager>();

            mouseBasePosition = 0;

            childRender = GetComponentInChildren<SpriteRenderer>();
            animator = GetComponent<Animator>();

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
                gameMana.IsSharkGrounded = false;
            }

            if (isGrounded == true)
            {
                gameMana.IsSharkGrounded = true;
                animator.SetBool("IsJumpingRight", false);
                animator.SetBool("IsJumpingLeft", false);
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

        private void OnCollisionEnter2D(Collision2D collision)
        {

            if (collision.gameObject.layer == 2)
            {
                isGroundedObstacle = true;
                StartCoroutine(CheckCollision());
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.layer == 2)
            {
                isGroundedObstacle = false;
            }
        }

        IEnumerator CheckCollision()
        {
            if (isGroundedObstacle == true)
            {

                yield return new WaitForSeconds(0.5f);

                if (isGroundedObstacle == true)
                {
                    animator.SetBool("IsDeath", true);
                    gameMana.lose = true;
                }
            }
        }

        void Flip()
        {
            if(mouseBasePosition > 0)
            {
                childRender.flipX = true;
                gameMana.IsSharkFliped = true;
            }
            else
            {
                childRender.flipX = false;
                gameMana.IsSharkFliped = false;
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
                    animator.SetBool("IsJumpingRight", true);
                    animator.SetBool("IsJumpingLeft", false);
                    mouseBasePosition = 0.1f;
                }
                else
                {
                    animator.SetBool("IsJumpingRight", false);
                    animator.SetBool("IsJumpingLeft", true);
                    mouseBasePosition = 0;
                }

            sharkBody.AddForce(jumpDirection * 500.0f);
        }


    }
}