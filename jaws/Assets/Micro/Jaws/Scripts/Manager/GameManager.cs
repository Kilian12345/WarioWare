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

        bool doneOnce = false;

        private void Start()
        {
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
                Macro.EndGame();
                doneOnce = true;
            }

            if (win == true && doneOnce == false)
            {
                Macro.Win();

                if (IsSharkGrounded == true)
                {
                    StartCoroutine(WinEnd());
                    doneOnce = true;
                }
            }
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

            yield return new WaitForSeconds(1.8f);
            Macro.EndGame();
        }
    }
}