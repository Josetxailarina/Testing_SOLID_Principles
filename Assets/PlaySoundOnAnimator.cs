using UnityEngine;

public class PlaySoundOnAnimator : MonoBehaviour
{
   public void PlaySound()
    {
        SoundsManager.Instance.PlayRandomSwing();
    }
}
