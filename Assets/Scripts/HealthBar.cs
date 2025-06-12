using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image grayHealthBar;
    [SerializeField] private Image redHealthBar;

    private Coroutine grayBarCoroutine;


    
    public void ReduceHealth(float currentHealth, float maxHealth)
    {
        UpdateRedBar(currentHealth, maxHealth);

        if (grayBarCoroutine != null) StopCoroutine(grayBarCoroutine);

        grayBarCoroutine = StartCoroutine(AnimateGrayBar());
    }
    public void UpdateRedBar(float currentHealth, float maxHealth)
    {
        float fillAmount = currentHealth / maxHealth;
        redHealthBar.fillAmount = fillAmount;
        if (grayHealthBar.fillAmount < fillAmount)
        {
            grayHealthBar.fillAmount = fillAmount; // Ensure gray bar matches red bar if red is ahead
        }
    }

    public void UpdateHealthBars(float currentHealth, float maxHealth)
    {
        float fillAmount = currentHealth / maxHealth;
        redHealthBar.fillAmount = fillAmount;
        grayHealthBar.fillAmount = fillAmount;
    }

    private IEnumerator AnimateGrayBar()
    {
        // Espera 0.5 segundos antes de empezar a reducir la barra gris
        yield return new WaitForSeconds(1f);

        float elapsed = 0f;
        float duration = 0.7f; // Duración de la animación
        float startFill = grayHealthBar.fillAmount;
        float targetFill = redHealthBar.fillAmount;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            grayHealthBar.fillAmount = Mathf.Lerp(startFill, targetFill, elapsed / duration);
            yield return null;
        }

        grayHealthBar.fillAmount = targetFill;
    }

}
