using UnityEngine;

namespace Game.Jaws
{
    public class Bird : MicroMonoBehaviour
    {
        [SerializeField] GameObject bite;
        [SerializeField] GameObject feather;
        [SerializeField] GameObject Shark;
        Transform parentTrans;
        GameManager gameMana;

        [SerializeField] Transform endPositionBird;
        [SerializeField] Transform BeginPositionBird;

        Animator animator;
        Animator animatorShark;

        Vector3 startPoint;

        float mouvTime;
        float t = 0;

        public int timeInBpm = 8;
        float bpm;

        AudioSource audioSource;
        public AudioClip LaughClip;

        bool doneOnce = false;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            animator = GetComponentInChildren<Animator>();
            animatorShark = Shark.GetComponent<Animator>();
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
                animatorShark.SetBool("IsDeath", true);
                animator.SetBool("StopLaugh", false);
                GetComponentInChildren<SpriteRenderer>().flipX = true;
                gameMana.lose = true;
            }

            AnimTiming();

            if(animator.GetBool("StopLaugh") == false && doneOnce == false)
            {
                audioSource.clip = LaughClip;
                audioSource.Play();
                doneOnce = true;
            }
        }

        void AnimTiming()
        {
            if(transform.position.x < BeginPositionBird.position.x && transform.position.x > BeginPositionBird.position.x - 0.5f)
            {
                animator.SetBool("StopLaugh", true);
                doneOnce = false;
            }

            if (transform.position.x < BeginPositionBird.position.x + 1.5f && transform.position.x > BeginPositionBird.position.x + 1.3f)
            {
                animator.SetBool("StopLaugh", false);
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