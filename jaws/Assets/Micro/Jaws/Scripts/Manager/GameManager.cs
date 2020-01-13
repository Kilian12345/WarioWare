using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Game.Jaws
{
    public class GameManager : MicroMonoBehaviour
    {

        [SerializeField] Transform Shark;

        [Space(20)]

        [SerializeField] GameObject Obstacle1;
        [SerializeField] GameObject Obstacle2;
        [SerializeField] GameObject Obstacle3;

        [Space(20)]

        [SerializeField] GameObject SharkHead1;
        [SerializeField] GameObject SharkHead2;
        [SerializeField] GameObject BubbleParticle;

        [HideInInspector] public bool win;
        [HideInInspector] public bool lose;

        [HideInInspector] public bool IsSharkGrounded;
        [HideInInspector] public bool IsSharkFliped;

        [Header("Sound")]
        [Space(20)]

        public AudioClip RotClip;
        public AudioClip CrocClip;
        [HideInInspector] public AudioSource audioSource;

        bool doneOnce = false;
        bool doneOnce2 = false;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();

            if(Macro.Difficulty == 2)
            {
                Instantiate(Obstacle1, new Vector3(Random.Range(-5.4f, 5.4f), -0.9f, 0), Quaternion.identity, transform);
            }
            else if (Macro.Difficulty == 3)
            {
                Instantiate(Obstacle3, new Vector3(Random.Range(-5.4f, 0), -0.9f, 0), Quaternion.identity, transform);
                Instantiate(Obstacle2, new Vector3(Random.Range(0, 5.4f), -0.9f, 0), Quaternion.identity, transform);
            }

        }

        private void Update()
        {
            if(lose == true && doneOnce == false)
            {
                Macro.Lose();
                StartCoroutine(LoseEnd());
                doneOnce = true;

                WinEndForced();
            }

            if (win == true && doneOnce == false)
            {
                if (doneOnce2 == false)
                {
                    audioSource.clip = CrocClip;
                    audioSource.Play();
                    doneOnce2 = true;
                }

                Macro.Win();

                if (IsSharkGrounded == true)
                {
                    StartCoroutine(WinEnd());
                    doneOnce = true;
                }

                WinEndForced();
            }

        }

        IEnumerator LoseEnd()
        {
            yield return new WaitForSeconds(1);
            Macro.EndGame();
        }

        IEnumerator WinEnd()
        {
                if (IsSharkFliped == true)
                {
                    Instantiate(BubbleParticle, SharkHead2.transform.position, Quaternion.identity, Shark);
                }
                else
                {
                    Instantiate(BubbleParticle, SharkHead1.transform.position, Quaternion.identity, Shark);
                }

            audioSource.clip = RotClip;
            audioSource.Play();

            yield return new WaitForSeconds(1.8f);
            Macro.EndGame();
        }

        IEnumerator WinEndForced()
        {
            yield return new WaitForSeconds(3f);
            Macro.EndGame();
        }
    }
}