using UnityEngine;

namespace Game.Jaws
{
    public class SharkMouv : MonoBehaviour
    {
        [SerializeField] Transform PointA;
        [SerializeField] Transform PointB;

        float mouseBasePosition;
        private void Start()
        {
            mouseBasePosition = 0;
        }

        private void Update()
        {
            mouseBasePosition += Input.GetAxis("Mouse X") * 0.5f;

            Vector3 pos = transform.position;
            pos.x = mouseBasePosition;
            transform.position = pos;
        }
    }
}