using UnityEngine;

namespace Game.Jaws
{
    public class SharkMouv : MonoBehaviour
    {
        [SerializeField] Transform PointA;
        [SerializeField] Transform PointB;

        public float sharkSpeed = 0.01f;
        float mouseBasePosition;

        [Space(10)]
        [Header("Curve")]
        public float curveHeight;
        public float curveWidth;



        private void Start()
        {
            mouseBasePosition = 0;
        }

        private void Update()
        {
            Mouvement();

            if(Input.GetKey(KeyCode.Space))
            {
                Jump();
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
            Vector3 pos = transform.position;
            pos.y = Mathf.Sin(pos.x * curveWidth);
            transform.position = pos;
        }
    }
}