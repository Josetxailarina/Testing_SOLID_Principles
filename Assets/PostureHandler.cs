using System.Collections;
using UnityEngine;

public class PostureHandler : MonoBehaviour
{
    private float currentPosture = 0;
    private float maxPosture = 100f;

    [SerializeField] private float minRecoverySpeed = 10f; // posture/seg (low health)
    [SerializeField] private float maxRecoverySpeed = 40f; // posture/seg (full health)

    [SerializeField] private PostureBar postureBar;

    private bool recoveryPaused = false;
    private Coroutine recoveryPauseCoroutine;
    IDamageable entityState;

    private bool isStunned = false; 

    private void Start()
    {
        entityState = GetComponent<IDamageable>();
        maxPosture = entityState.maxHealth;
        UpdatePostureBar();
    }
    private void Update()
    {
        if (!recoveryPaused && currentPosture > 0f)
        {
            float recoverySpeed = GetRecoverySpeed();
            currentPosture -= recoverySpeed * Time.deltaTime;
            if (currentPosture < 0f) currentPosture = 0f;
            UpdatePostureBar();
        }
    }

    public void AddPosture(float amount)
    {
        currentPosture += amount;
        if (currentPosture >= maxPosture)
        {
            currentPosture = maxPosture;
            isStunned = true;
            // Aquí puedes lanzar un evento o llamar a un método para notificar el aturdimiento
        }
        UpdatePostureBar();

        // Pausa la recuperación durante 0.5s
        if (recoveryPauseCoroutine != null)
            StopCoroutine(recoveryPauseCoroutine);
        recoveryPauseCoroutine = StartCoroutine(PauseRecoveryCoroutine());
    }
    private IEnumerator PauseRecoveryCoroutine()
    {
        recoveryPaused = true;
        yield return new WaitForSeconds(1.2f);
        recoveryPaused = false;
    }

    private float GetRecoverySpeed()
    {
        float healthPercent = Mathf.Clamp01(entityState.currentHealth / entityState.maxHealth);
        return Mathf.Lerp(minRecoverySpeed, maxRecoverySpeed, healthPercent);
    }

    private void UpdatePostureBar()
    {
        if (postureBar != null)
            postureBar.UpdatePostureBar(currentPosture, maxPosture);
    }

}
