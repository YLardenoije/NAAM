﻿using System.Collections;
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
    private Projectile projectile;

    void Start()
    {
        projectile = gameObject.GetComponent<Projectile>();
        projectile.Target = Target;

        Attached = false;
        HitObject = null;
        HitObjectIsEnemy = false;
        
    }

    
    void Update()
    {
        if( Attached )
        {
            if( HitObjectIsEnemy )
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
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision detected!");
        if( Attached == false && col.gameObject.GetComponent<Player>() == null )
        {
            projectile.Collided();
            Debug.Log("Hit an object!");
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
