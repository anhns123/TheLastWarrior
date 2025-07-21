using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Skill Prefabs")]
    [SerializeField] private GameObject normalHitPrefab;
    [SerializeField] private float normalHitCooldown = 0.5f;

    [SerializeField] private GameObject ultiHitPrefab;
    [SerializeField] private float ultiHitCooldown = 1.5f;

    private Player owner;
    private bool canAttack = true;
    private void Awake()
    {
        owner = GetComponentInParent<Player>();
    }
    private void Update()
    {
        if (owner.transform.localScale.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void NormalAttack()
    {
        if (!canAttack || normalHitPrefab == null) return;
        SpawnHit(normalHitPrefab, normalHitCooldown);
    }

    public void UltimateAttack()
    {
        if (!canAttack || ultiHitPrefab == null) return;
        SpawnHit(ultiHitPrefab, ultiHitCooldown);
    }

    void SpawnHit(GameObject prefab, float duration)
    {
        GameObject hitObj = Instantiate(prefab, transform.position, transform.rotation);
        HitColliderBase hit = hitObj.GetComponent<HitColliderBase>();
        if (hit != null)
        {
            hit.Init(owner, duration);
        }

        canAttack = false;
        Invoke(nameof(ResetAttack), duration);
    }
    void ResetAttack()
    {
        canAttack = true;
    }
}


