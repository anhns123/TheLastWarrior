using System;
using UnityEngine;

public class HitColliderBase : MonoBehaviour
{
    protected Player owner;
    float aliveTime;
    private void Update()
    {
        aliveTime -= (Time.deltaTime/3);
        if(aliveTime < 0)
        {
            Destroy(gameObject);
        }
    }
    public virtual void Init(Player owner, float duration)
    {
        this.owner = owner;
        aliveTime = duration;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        // Logic gây sát thương
        Player target = other.GetComponent<Player>();
        if (target != null && target != owner)
        {
            // Gây sát thương ở đây
        }
    }
}
