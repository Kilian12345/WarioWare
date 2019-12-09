using UnityEngine;

namespace Game.Jaws
{
    public class Bird : MonoBehaviour
    {
        [SerializeField] Transform endPositionBird;
        Vector3 startPoint;

        float mouvTime;
        float t = 0;

        public int timeInBpm = 8;
        int bpm;

        private void Awake()
        {
            startPoint = transform.position;
            bpm = Macro.BPM;
        }

        private void Update()
        {
            mouvTime = (60 / bpm) * timeInBpm;
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