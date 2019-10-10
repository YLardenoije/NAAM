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
        Debug.Log("Collision detected!");
        LivingThing livingThing = col.gameObject.GetComponent<LivingThing>();
        Debug.Log(livingThing.gameObject);
        if (livingThing != null && livingThing.gameObject != Source)
        {
            livingThing.Die(); //OOF
            projectile.Collided();
            Destroy(gameObject); //FireBall hit something and disappears.
        }
        else if (livingThing.gameObject != Source)
        {
            projectile.Collided();
            Destroy(gameObject); //FireBall hit something and disappears.
        }
    }
}
