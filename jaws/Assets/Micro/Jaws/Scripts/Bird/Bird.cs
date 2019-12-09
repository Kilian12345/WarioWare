using UnityEngine;

namespace Game.Jaws
{
    public class Bird : MonoBehaviour
    {
        [SerializeField] Transform endPositionBird;
        Vector3 startPoint;

        public float mouvTime;
        float t = 0;

        private void Awake()
        {
            startPoint = transform.position;
        }

        private void Update()
        {
            t += Time.deltaTime;
            float alede = t / mouvTime;

            transform.position = Vector3.Lerp(startPoint, endPositionBird.position, alede);


            Debug.Log(startPoint);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 0)
            {
                Debug.Log("ALLAAAAAAAAAAAAAAAAAAAAH");
            }
        }

    }
}