using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAuidoPlay : MonoBehaviour
{
    private AudioSource audioSource;

    public float tiempoMinimo = 5f; // Tiempo mínimo de espera en segundos
    public float tiempoMaximo = 20f; // Tiempo máximo de espera en segundos

    private void Start()
    {
        // Obtener el componente AudioSource del GameObject
        audioSource = GetComponent<AudioSource>();

        // Reproducir el audio en el inicio
        ReproducirAudio();
    }

    private void Update()
    {
        // Verifica si el audio está completo y es momento de reproducirlo nuevamente
        if (!audioSource.isPlaying)
        {
            // Calcula el tiempo para la próxima reproducción
            float tiempoSiguienteReproduccion = Random.Range(tiempoMinimo, tiempoMaximo);

            // Espera el tiempo antes de reproducir el audio nuevamente
            Invoke("ReproducirAudio", tiempoSiguienteReproduccion);
        }
    }

    private void ReproducirAudio()
    {
        // Reproduce el audio
        audioSource.Play();
    }
}
