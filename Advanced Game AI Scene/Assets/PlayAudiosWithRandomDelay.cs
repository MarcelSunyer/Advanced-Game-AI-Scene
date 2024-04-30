using UnityEngine;
using System.Collections;

public class PlayAudiosWithRandomDelay : MonoBehaviour
{
    public AudioClip[] clips;
    public float minDelay = 0.5f; // M�nimo retraso
    public float maxDelay = 1.5f; // M�ximo retraso

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayDelayedClips());
    }

    IEnumerator PlayDelayedClips()
    {
        foreach (AudioClip clip in clips)
        {
            audioSource.clip = clip;
            audioSource.Play();

            float randomDelay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(randomDelay);
        }
    }
}
