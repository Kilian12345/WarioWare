using UnityEngine;

namespace Game.Jaws
{
    public class Bird : MicroMonoBehaviour
    {
        [SerializeField] GameObject bite;
        [SerializeField] GameObject feather;
        Transform parentTrans;
        GameManager gameMana;

        [SerializeField] Transform endPositionBird;
        [SerializeField] Transform BeginPositionBird;

        Animator animator;

        Vector3 startPoint;

        float mouvTime;
        float t = 0;

        public int timeInBpm = 8;
        float bpm;

        private void Start()
        {
            animator = GetComponent<Animator>();
            gameMana = FindObjectOfType<GameManager>();
            parentTrans = GameObject.Find("___Character___").transform;
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
                animator.SetBool("StopLaugh", false);
                GetComponentInChildren<SpriteRenderer>().flipX = true;
                gameMana.lose = true;
            }

            AnimTiming();
        }

        void AnimTiming()
        {
            if(transform.localPosition.x < BeginPositionBird.localPosition.x && transform.localPosition.x > BeginPositionBird.localPosition.x - 0.5f)
            {
                animator.SetBool("StopLaugh", true);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            Instantiate(bite, transform.position, Quaternion.identity, parentTrans);
            Instantiate(feather, transform.position, Quaternion.identity, parentTrans);
            if (collision.gameObject.layer == 0)
            {
                gameMana.win = true;
                Destroy(gameObject);
            }
        }
    }
}