using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    public Vector2 Target; // where to go to.
    public GameObject Source;
    public Vector2 Origin; // where we came from
    public RaycastHit2D hit; // the actual hit location
    private bool HasHitSomething;
    [SerializeField] private float Speed;
    [SerializeField] private AimBehaviour aim;
    private int magicNumber = 100; //this makes sure the projectile keeps flying forever, until it hits.
    public UnityEvent ValuesGotSet = new UnityEvent();


    private void Awake()
    {
        
    }

    void Start()
    {
        if( Source != null && Target != null )
        {
            ValuesGotSet.Invoke();
            Debug.Log("Projectile values were set and passed to subbed functions. ");
        }
        HasHitSomething = false;

        int layermask = 1 << gameObject.layer;
        layermask = ~layermask;
        if( aim != null )
        {
            Target = aim.Offset(Target);
        }
        

        hit = Physics2D.Raycast(transform.position,
            Target - new Vector2(transform.position.x, transform.position.y),
            magicNumber, layermask);
        Origin = transform.position;
    }
    
    void Update()
    {

        if (!HasHitSomething)
        {
            if( Target != null )
            {
                transform.Translate(
                (hit.point - Origin).normalized * Speed * Time.deltaTime
                , Space.Self);
            }
        }        
    }

    public void Collided() // the specific projectile code ( ex. Grapple ) should call this function on collision.
    {
        HasHitSomething = true;
    }

    public void SetValues( Vector2 Target, GameObject Source )
    {
        this.Target = Target;
        this.Source = Source;
        ValuesGotSet.Invoke();
        Debug.Log("Projectile values were set and passed to subbed functions. ");
    }
}
