
public interface IDamager
{
    float Damage { get; }
    bool IsAttack { get; }
    void Destruir();
    void Attack(IDamageable damageable, HitType hitType);
}
