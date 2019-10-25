﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Vector2 Target;
    public GameObject Source;
    private Projectile projectile;
    // Start is called before the first frame update
    void Start()
    {
        projectile = GetComponent<Projectile>();
        projectile.ValuesGotSet.AddListener(UpdateValues);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Hit:" + col.gameObject.name);
        if (col.gameObject != Source )
        {
            LivingThing livingThing = col.gameObject.GetComponent<LivingThing>();
            if( livingThing != null )
            {
                livingThing.Die(); // OOF
            }
            Destroy(gameObject);
        }
    }

    public void UpdateValues()
    {
        Target = projectile.Target;
        Source = projectile.Source;
    }
}
