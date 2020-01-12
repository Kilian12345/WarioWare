using UnityEngine;

namespace Game.Jaws
{
    public class Bird : MicroMonoBehaviour
    {
        [SerializeField] Transform endPositionBird;
        Vector3 startPoint;

        float mouvTime;
        float t = 0;

        public int timeInBpm = 8;
        float bpm;

        private void Start()
        {

            startPoint = transform.position;
            bpm = Macro.BPM;
            //bpm = 96;

            Macro.StartGame();
        }

        protected override void OnGameStart()
        {
            Macro.DisplayActionVerb("Eat!", 1);
        }

        protected override void OnActionVerbDisplayEnd()
        {
            Macro.StartTimer(8);

        }

        private void Update()
        {
            mouvTime = (60f / bpm) * timeInBpm;
            t += Time.deltaTime;
            float alede = t / mouvTime;


            transform.position = Vector3.Lerp(startPoint, endPositionBird.position, alede);


            Debug.Log(mouvTime);

            if(transform.position == endPositionBird.position)
            {
                Debug.Log("Lose");
                Macro.Lose();
                Macro.EndGame();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 0)
            {
                Debug.Log("WIN");
                Macro.Win();
                Macro.EndGame();

            }
        }
    }
}