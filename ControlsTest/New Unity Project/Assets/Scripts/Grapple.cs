using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public GameObject Source; // the player who created the grapple
    public Vector2 Target; // where the grapple is aimed
    private bool Attached; // whether the grapple actually hit or not
    [SerializeField] private GameObject HitObject; // if the grapple hit something, find its reference here. otherwise null.
    private Vector3 HitLoc;
    [SerializeField] private float GrapplePower; // how strong the grapple pulls.
    private bool HitObjectIsEnemy;
    [SerializeField] private Projectile projectile;
    private float attachedTime = 0;
    [SerializeField] private float maxAttachedTime=1;


    private void Awake()
    {
        Attached = false;
        HitObject = null;
        HitObjectIsEnemy = false;
    }

    void Start()
    {
        projectile = GetComponent<Projectile>();
        projectile.ValuesGotSet.AddListener(UpdateValues);
    }
        
    public void UpdateValues()
    {
        Target = projectile.Target;
        Source = projectile.Source;
        Debug.Log("Source is: " + Source);  
    }


    void Update()
    {
        if( Attached )
        {
            attachedTime += Time.deltaTime;
            if( HitObjectIsEnemy && HitObject != null )
            {
                
                //HitObject.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                HitObject.gameObject.GetComponent<Rigidbody2D>().AddForce(
                (Source.gameObject.transform.position - HitObject.gameObject.transform.position).normalized * GrapplePower, ForceMode2D.Impulse);
                //Source.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                Source.gameObject.GetComponent<Rigidbody2D>().AddForce(
                    (HitObject.transform.position - Source.gameObject.transform.position).normalized * GrapplePower, ForceMode2D.Impulse);
                transform.position = HitObject.transform.position;
            }
            else
            {
                Source.gameObject.GetComponent<Rigidbody2D>().AddForce(
                    (transform.position - Source.gameObject.transform.position).normalized * GrapplePower, ForceMode2D.Impulse);
                transform.position = HitLoc;
            }
            if (attachedTime > 1)
            {
                Destroy(gameObject);
            }
        }
        else if (attachedTime!=0)
        {
            attachedTime = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if( Attached == false && col.gameObject.GetComponent<Player>() == null )
        {
            projectile.Collided();
            Attached = true;
            HitObject = col.gameObject;
            HitLoc = transform.position;
            if( HitObject.GetComponent<Enemy>() != null )
            {
                HitObjectIsEnemy = true;
            }
            BoxCollider2D tmp = GetComponent<BoxCollider2D>();
            tmp.enabled = false;
        }
    }
}
