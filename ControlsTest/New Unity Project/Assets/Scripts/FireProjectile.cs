using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof (PlayerController))]
[RequireComponent(typeof (Transform))]

public class FireProjectile : MonoBehaviour
{
    private Transform tf;
    private Rigidbody2D rb;
    [SerializeField] private Projectile projectilePrefab;
    //private PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {
        tf = this.gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void shoot()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Touch tap = Input.GetTouch(0);
            Vector2 direction = tap.position - new Vector2( tf.position.x,tf.position.y);
            
        }
        
        
    }
}
