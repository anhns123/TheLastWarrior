using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Hit Prefabs")]
    [SerializeField] private GameObject normalHitPrefab;
    [SerializeField] private GameObject ultiHitPrefab;

    [Header("Damage Values")]
    [SerializeField] private float normalDamage = 15f;
    [SerializeField] private float ultiDamage = 30f;

    [Header("Attack Cooldowns")]
    [SerializeField] private float normalHitCooldown = 0.5f;
    [SerializeField] private float ultiHitCooldown = 1.0f;

    private bool canAttack = true;
    private Player owner;

    private void Awake()
    {
        owner = GetComponentInParent<Player>();
    }

    public void NormalAttack()
    {
        if (!canAttack || normalHitPrefab == null) return;
        SpawnHit(normalHitPrefab, normalHitCooldown, normalDamage);
    }

    public void UltimateAttack()
    {
        if (!canAttack || ultiHitPrefab == null) return;
        SpawnHit(ultiHitPrefab, ultiHitCooldown, ultiDamage);
    }

    private void SpawnHit(GameObject prefab, float cooldown, float damage)
    {
        GameObject hitObj = Instantiate(prefab, transform.position, transform.rotation);
        HitColliderBase hit = hitObj.GetComponent<HitColliderBase>();
        if (hit != null)
        {
            hit.Init(owner, cooldown, damage);
        }

        canAttack = false;
        Invoke(nameof(ResetAttack), cooldown);
    }

    private void ResetAttack()
    {
        canAttack = true;
    }
}
