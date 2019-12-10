using UnityEngine;

namespace Game.Jaws
{
    public class GameManager : MicroMonoBehaviour
    {
        [SerializeField] GameObject Obstacle1;
        [SerializeField] GameObject Obstacle2;

        private void Start()
        {
            if(Macro.Difficulty == 2)
            {
                Instantiate(Obstacle1, new Vector3(Random.Range(-5.4f, 5.4f), -0.9f, 0), Quaternion.identity, transform.parent);
            }
            else if (Macro.Difficulty == 3)
            {
                Instantiate(Obstacle1, new Vector3(Random.Range(-5.4f, 0), -0.9f, 0), Quaternion.identity, transform.parent);
                Instantiate(Obstacle2, new Vector3(Random.Range(0, 5.4f), -0.9f, 0), Quaternion.identity, transform.parent);
            }

        }   
    }
}