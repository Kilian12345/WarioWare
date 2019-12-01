using UnityEngine;

namespace Game.Jaws
{
    public class SharkMouv : MonoBehaviour
    {
        [SerializeField] Transform PointA;
        [SerializeField] Transform PointB;

        public float sharkSpeed = 0.01f;
        float mouseBasePosition;

        private void Start()
        {
            mouseBasePosition = 0;
        }

        private void Update()
        {
            mouseBasePosition += Input.GetAxis("Mouse X") * sharkSpeed;

            Vector3 pos = transform.position;
            pos.x += mouseBasePosition;
            pos.x = Mathf.Clamp(pos.x, PointA.position.x, PointB.position.x);
            transform.position = pos;
        }
    }
}