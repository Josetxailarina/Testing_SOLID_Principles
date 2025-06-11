using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    [SerializeField] private Image grayHealthBar;
    [SerializeField] private Image redHealthBar;

    private Coroutine grayBarCoroutine;

    private void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthBars();
    }
    [ContextMenu("Probar TakeDamage 10")]
    private void TestTakeDamage()
    {
        TakeDamage(10);
    }
    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        UpdateRedBar();

        if (grayBarCoroutine != null)
            StopCoroutine(grayBarCoroutine);

        grayBarCoroutine = StartCoroutine(AnimateGrayBar());
    }



    private void UpdateRedBar()
    {
        float fillAmount = currentHealth / maxHealth;
        redHealthBar.fillAmount = fillAmount;
    }

    private void UpdateHealthBars()
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

   
