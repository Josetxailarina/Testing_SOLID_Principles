public interface IDamageable 
{
    /// <summary>
    /// Applies hit. Returns true if the hit was parried, false otherwise.
    /// </summary>
    /// 
    public float currentHealth { get; }
    public float maxHealth { get; }
    public bool TakeHit(float amount);

    public void GetStunned();
    
}
