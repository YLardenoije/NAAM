using System.Collections;
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
        projectile = gameObject.GetComponent<Projectile>();
        projectile.Target = Target;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
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
}
