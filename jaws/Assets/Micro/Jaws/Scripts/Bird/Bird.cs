using UnityEngine;

namespace Game.Jaws
{
    public class Bird : MonoBehaviour
    {
        private void Update()
        {
            Vector3 pos = transform.position;
            pos.x -= 0.05f;
            transform.position = pos;
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