using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    public Vector2 Target; // where to go to.
    public RaycastHit2D hit; // the actual hit location
    private bool HasHitSomething;
    [SerializeField] private float Speed;
    
    void Start()
    {
        HasHitSomething = false;

        int layermask = 1 << gameObject.layer;
        layermask = ~layermask;
        int magicNumber = 100; //this makes sure the projectile keeps flying forever, until it hits.

        hit = Physics2D.Raycast(transform.position,
            Target - new Vector2(transform.position.x, transform.position.y),
            magicNumber, layermask);
        Debug.Log(hit.point);
    }
    
    void Update()
    {
        if (!HasHitSomething)
        {
            transform.position = Vector3.MoveTowards(transform.position, hit.point, Speed * Time.deltaTime);
        }        
    }

    public void Collided() // the specific projectile code ( ex. Grapple ) should call this function on collision.
    {
        HasHitSomething = true;
    }
}
