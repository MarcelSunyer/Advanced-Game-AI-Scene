using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class footSteeps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip grass;
    public AudioClip dirt;
    public AudioClip wood;
    public AudioClip rock;
    public AudioClip concret;

    RaycastHit hit;
    public Transform RayStart;
    public float range;
    public LayerMask layerMask;

    public void Footstep()
    {
        if(Physics.Raycast(RayStart.position, RayStart.transform.up*1, out hit, range, layerMask))
        {
            if(hit.collider.CompareTag("grass"))
            {
                PlayFootstepSoundL(grass);
            }
            if (hit.collider.CompareTag("concret"))
            {
                PlayFootstepSoundL(concret);
            }
            if (hit.collider.CompareTag("dirt"))
            {
                PlayFootstepSoundL(dirt);
            }
            if (hit.collider.CompareTag("wood"))
            {
                PlayFootstepSoundL(wood);
            }


        }
    }
    void PlayFootstepSoundL(AudioClip audio)
    {
        audioSource.pitch = Random.Range(0.8f, 1f);
        audioSource.PlayOneShot(audio);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
