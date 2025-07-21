using UnityEngine;

public class HitColliderBase : MonoBehaviour
{
    protected Player owner;
    private float aliveTime;
    private float damage;

    public virtual void Init(Player owner, float duration, float damage)
    {
        this.owner = owner;
        this.aliveTime = duration;
        this.damage = damage;
        Destroy(gameObject, aliveTime);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        Player target = other.GetComponent<Player>();
        if (target != null && target != owner)
        {
            HealthManager health = target.GetComponent<HealthManager>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
