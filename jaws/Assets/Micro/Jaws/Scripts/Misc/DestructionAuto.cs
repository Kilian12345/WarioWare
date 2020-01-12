using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Jaws
{
    public class DestructionAuto : MonoBehaviour
    {

        [SerializeField] float secondeDestruction;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Auto());
        }

        IEnumerator Auto()
        {
            yield return new WaitForSeconds(secondeDestruction);
            Destroy(gameObject);
        }

    }
}
