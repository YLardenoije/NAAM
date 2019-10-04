using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    public Player Source; // the player who created the grapple
    public Vector2 Target; // where the grapple is aimed
    private bool Attached; // whether the grapple actually hit or not
    [SerializeField]private Enemy EnemyHit; // if the grapple hit an enemy, find its reference here. otherwise null.
    [SerializeField] private float GrapplePower; // how strong the grapple pulls.
    [SerializeField] private float GrappleTravelSpeed;
    RaycastHit2D hit;
    void Start()
    {
        Source = FindObjectOfType<Player>();
        Attached = false;
        EnemyHit = null;

        int magicNumber = 100;
        int layermask = 1 << LayerMask.NameToLayer("Player");
        layermask = ~layermask;
        hit = Physics2D.Raycast(transform.position,
            Target - new Vector2(transform.position.x, transform.position.y),
            magicNumber, layermask);

    }

    
    void Update()
    {
        if( Attached )
        {
            if (EnemyHit != null)
            {
                EnemyHit.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                EnemyHit.gameObject.GetComponent<Rigidbody2D>().AddForce(
                    (Source.gameObject.transform.position - EnemyHit.gameObject.transform.position).normalized * GrapplePower, ForceMode2D.Impulse);
            }
            Source.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            Source.gameObject.GetComponent<Rigidbody2D>().AddForce(
                (transform.position - Source.gameObject.transform.position).normalized * GrapplePower, ForceMode2D.Impulse);
        }
        else
        {
           transform.position = Vector3.MoveTowards(transform.position, hit.point, GrappleTravelSpeed * Time.deltaTime);
        }
    }

    private void OnDestroy() //TODO: remove any grapple effects before being destroyed. OOF.
    {
        Source.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        if( EnemyHit != null )
        {
            EnemyHit.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision detected!");
        if( Attached == false && col.gameObject.GetComponent<Player>() == null )
        {
            EnemyHit = col.gameObject.GetComponent<Enemy>(); //if it has hit an enemy, it gets stored in Enemyhit.
            Attached = true;
            Debug.Log("Hit an object!");
            //Destroy(gameObject.GetComponent<BoxCollider2D>());
            //Destroy(gameObject.GetComponent<Rigidbody2D>());
        }
    }
}
