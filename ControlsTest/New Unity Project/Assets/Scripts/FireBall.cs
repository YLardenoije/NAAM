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

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision detected!");
        LivingThing livingThing = col.gameObject.GetComponent<LivingThing>();
        if ( livingThing != null && livingThing.gameObject != Source )
        {
            livingThing.Die(); //OOF
            Destroy(gameObject); //FireBall hit something and disappears.
            projectile.Collided();
        }
        else if( livingThing.gameObject != Source )
        {
            Destroy(gameObject); //FireBall hit something and disappears.
            projectile.Collided();
        }
        Destroy(gameObject); //FireBall hit something and disappears.
    }
}
