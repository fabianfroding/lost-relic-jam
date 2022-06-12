using System.Collections;
using UnityEngine;

public class DestroyWhenDone : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            StopCoroutine(DestroyAfterDelay());
            StartCoroutine(DestroyAfterDelay());
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        float delay = audioSource.clip.length;
        yield return new WaitForSeconds(delay);
        Destroy(this.gameObject);
    }
}
