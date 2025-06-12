using UnityEngine;

public class SoundsManager : MonoBehaviour
{
   public static SoundsManager Instance;

    public AudioSource bloodHitSound;
    public AudioSource swordSwingSound;
    public AudioSource blockSound;
    public AudioSource parrySound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayRandomParry()
    {
        float randomPitch = Random.Range(0.8f, 1.2f);
        parrySound.pitch = randomPitch;
        parrySound.Play();
    }
    public void PlayRandomBlock()
    {
        float randomPitch = Random.Range(0.8f, 1.2f);
        blockSound.pitch = randomPitch;
        blockSound.Play();
    }
    public void PlayRandomSwing()
    {
        float randomPitch = Random.Range(0.8f, 1.2f);
        swordSwingSound.pitch = randomPitch;
        swordSwingSound.Play();
    }
}
