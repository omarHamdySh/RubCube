using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    [SerializeField] private Animator myAnim;
    [SerializeField] private float effectDelay;
    [SerializeField] private GameObject effect;
    [SerializeField] private float effectDelayToDestroy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            // Open the animator to make chest anime work
            myAnim.enabled = true;

            // Instantiate the effect after end of the anime
            StartCoroutine(CreateChestEffect());
        }
    }

    IEnumerator CreateChestEffect()
    {
        yield return new WaitForSeconds(effectDelay);

        // Effect creation code
        GameObject go = Instantiate(effect, transform.position, effect.transform.rotation);
        Destroy(go, effectDelayToDestroy);
    }
}
