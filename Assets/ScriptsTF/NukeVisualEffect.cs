using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NukeVisualEffect : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private Image nukeOverlayImage; // Arrastra aquí tu imagen blanca

    [Header("Settings")]
    [SerializeField] private float flashSpeed = 3f; // Qué tan rápido parpadea
    [SerializeField] private float effectDuration = 2.5f; // Cuánto dura el efecto en total
    [Range(0, 1)]
    [SerializeField] private float maxAlpha = 0.8f; // Qué tan blanca se pone (1 es totalmente blanco)

    private void Start()
    {
        // Asegurarnos de que empiece invisible y desactivada
        if (nukeOverlayImage != null)
        {
            Color c = nukeOverlayImage.color;
            c.a = 0f;
            nukeOverlayImage.color = c;
            nukeOverlayImage.gameObject.SetActive(false);
        }
    }

    public void PlayNukeEffect()
    {
        // 1. ¡DESPERTAR! Prende el propio objeto donde está este script PRIMERO
        this.gameObject.SetActive(true);

        // 2. Asegurarnos que la imagen hija también esté prendida (por si acaso)
        if (nukeOverlayImage != null) nukeOverlayImage.gameObject.SetActive(true);

        // 3. Ahora que ya está activo, Unity nos deja iniciar la corrutina sin errores
        StopAllCoroutines();
        StartCoroutine(AnimateFlash());
    }

    private IEnumerator AnimateFlash()
    {
        nukeOverlayImage.gameObject.SetActive(true);
        float timer = 0f;

        while (timer < effectDuration)
        {
            timer += Time.deltaTime;

            // Esta formula crea una onda suave (Seno) que sube y baja entre 0 y 1
            float alpha = Mathf.PingPong(timer * flashSpeed, maxAlpha);

            // Aplicamos el alpha
            Color c = nukeOverlayImage.color;
            c.a = alpha;
            nukeOverlayImage.color = c;

            yield return null; // Esperar al siguiente frame
        }

        // Al terminar, desvanecer suavemente hasta 0
        yield return StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        Color c = nukeOverlayImage.color;
        while (c.a > 0f)
        {
            c.a -= Time.deltaTime * 2f; // Se apaga rápido
            nukeOverlayImage.color = c;
            yield return null;
        }

        nukeOverlayImage.gameObject.SetActive(false);
    }
}