using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : ScriptableObject
{
    [SerializeField] Projectile FriendlyPrefab, EnemyProjectilePrefab;
    private List<Projectile> unusedFriendlyProjectiles, unusedEnemyProjectiles;

    private void Awake()
    {   
        unusedFriendlyProjectiles = new List<Projectile>();
        unusedEnemyProjectiles = new List<Projectile>();
    }

    public Projectile GetFriendlyProjectile()
    {
        if (unusedFriendlyProjectiles.Count > 0 )
        {
            Projectile ret = unusedFriendlyProjectiles[0];
            unusedFriendlyProjectiles.RemoveAt(0);
            ret.gameObject.SetActive(false);
            return ret;
        }
        return Instantiate(FriendlyPrefab);
    }
    public void StoreFriendlyProjectile(Projectile p)
    {
        p.gameObject.SetActive(false);
        unusedFriendlyProjectiles.Add(p);
    }
    public Projectile GetEnemyProjectile()
    {
        if (unusedFriendlyProjectiles.Count > 0)
        {
            Projectile ret = unusedEnemyProjectiles[0];
            unusedEnemyProjectiles.RemoveAt(0);
            ret.gameObject.SetActive(false);
            return ret;
        }
        return Instantiate(EnemyProjectilePrefab);
    }
    public void StoreEnemyProjectile(Projectile p)
    {
        p.gameObject.SetActive(false);
        unusedEnemyProjectiles.Add(p);
    }
}
