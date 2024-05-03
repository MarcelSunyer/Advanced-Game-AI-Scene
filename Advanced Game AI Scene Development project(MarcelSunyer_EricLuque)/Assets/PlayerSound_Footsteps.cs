using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound_Footsteps : MonoBehaviour
{
    [Header("Footsteps")]
    public List<AudioClip> grassFS;
    public List<AudioClip> stoneFS;
    public List<AudioClip> concreteFS;
    public List<AudioClip> groundFS;
    public List<AudioClip> woodFS;

    enum FSMaterial
    {
        Grass, Stone, Concrete, Ground, Wood, Empty
    }

    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();   
    }

    private FSMaterial SurfaceSelect()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, -Vector3.up);
        Material surfaceMaterial;

        if (Physics.Raycast(ray, out hit, 1.0f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            Renderer surfaceRenderer = hit.collider.GetComponentInChildren<Renderer>();
            if (surfaceRenderer)
            {
                surfaceMaterial = surfaceRenderer ? surfaceRenderer.sharedMaterial : null;
                if (surfaceMaterial.name.Contains("Grass"))
                {
                    return FSMaterial.Grass;
                }
                else if (surfaceMaterial.name.Contains("Stone"))
                {
                    return FSMaterial.Stone;
                }
                else if (surfaceMaterial.name.Contains("texMadera4") || surfaceMaterial.name.Contains("Wood"))
                {
                    return FSMaterial.Wood;
                }
                else if (surfaceMaterial.name.Contains("Material.005") || surfaceMaterial.name.Contains("Concrete"))
                {
                    return FSMaterial.Concrete;
                }
                else if (surfaceMaterial.name.Contains("Ground"))
                {
                    return FSMaterial.Ground;
                }
                else
                {
                    return FSMaterial.Empty;
                }
            }
        }
        
        return FSMaterial.Empty;
    }

    void PlayFootsteps()
    {
        AudioClip clip = null;

        FSMaterial surface = SurfaceSelect();

        switch (surface)
        {
            case FSMaterial.Grass:
                clip = grassFS[Random.Range(0, grassFS.Count)];
                break;
            case FSMaterial.Stone:
                clip = stoneFS[Random.Range(0, grassFS.Count)];
                break;
            case FSMaterial.Wood:
                clip = woodFS[Random.Range(0, grassFS.Count)];
                break;
            case FSMaterial.Concrete:
                clip = concreteFS[Random.Range(0, grassFS.Count)];
                break;
            case FSMaterial.Ground:
                clip = groundFS[Random.Range(0, grassFS.Count)];
                break;
            case FSMaterial.Empty:
                break;
        }

        Debug.Log("Surface: " + surface);

        if (surface != FSMaterial.Empty)
        {
            source.clip = clip;
            source.volume = Random.Range(0.8f, 1.0f);
            source.pitch = Random.Range(0.8f, 1.2f);
            source.Play();
        }
    }
}
