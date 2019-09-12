using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    private Player Source; // the player who created the grapple
    private Vector2 Target; // where the grapple is aimed
    private bool Attached; // whether the grapple actually hit or not
    private Vector2 HitLocation; // where the grapple hit
    private Enemy EnemyHit; // if the enemy hit an enemy, find its reference here. otherwise null.
    private float GrapplePower; // how strong the grapple pulls.
    
    void Start()
    {
        Source = FindObjectOfType<Player>();
        Attached = false;
        HitLocation = new Vector2();
        EnemyHit = null;
        
    }

    
    void Update()
    {
        if( Attached )
        {
            if (EnemyHit == null)
            {
                Source.gameObject.GetComponent<Rigidbody2D>().AddForce(
                    HitLocation - (Vector2)Source.transform.position, ForceMode2D.Force);
            }
                
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Target, 5 * Time.deltaTime); // move towards the aim point.
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( Attached == false )
        {
            EnemyHit = collision.gameObject.GetComponent<Enemy>(); //if it has hit an enemy, it gets stored in Enemyhit.
            HitLocation = collision.transform.position;
            Attached = true;
            Debug.Log("Hit an object!");
        }
    }
}
