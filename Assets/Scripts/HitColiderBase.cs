using UnityEngine;

public class HitColliderBase : MonoBehaviour
{
    protected Player owner;
    private float aliveTime;
    private float damage;
    private void Update()
    {
        aliveTime -= (Time.deltaTime / 3);
        if (aliveTime < 0)
        {
            Destroy(gameObject);
        }
    }
    public virtual void Init(Player owner, float duration, float damage)
    {
        this.owner = owner;
        this.aliveTime = duration;
        this.damage = damage;
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

            aliveTime = 0;
        }
    }
}
