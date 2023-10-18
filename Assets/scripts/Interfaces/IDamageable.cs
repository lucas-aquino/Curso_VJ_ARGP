
public interface IDamageable 
{
    string Tag {  get; }
    float MaxLife { get; }
    float CurrentLife { get; }
    bool IsDead { get; }
    bool IsTackingDamage { get; }
    void TakeDamage(float damage, HitType tipo);
    void Destruir();
}
