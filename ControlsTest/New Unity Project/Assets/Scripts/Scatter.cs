using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scatter : MonoBehaviour
{
    public Vector2 Target;
    public GameObject Source;
    public bool ParentShot;
    [SerializeField] private int AdditionalShots;
    [SerializeField] private float speed;
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
        if( ParentShot )
        {
            ParentShot = false;
            for( int i = 1; i < 1+AdditionalShots; i++ )
            {
                Scatter s = Instantiate(this, transform.position, transform.rotation);
                s.gameObject.GetComponent<Rigidbody2D>().AddForce(Target * (speed + i), ForceMode2D.Impulse);
            }
        }
    }

    public void UpdateValues()
    {
        Target = projectile.Target;
        Source = projectile.Source;
        ParentShot = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Hit:" + col.gameObject.name);
        if (col.gameObject != Source && col.gameObject.GetComponent<Scatter>() == null )
        {
            LivingThing livingThing = col.gameObject.GetComponent<LivingThing>();
            if (livingThing != null)
            {
                livingThing.Die(); // OOF
                Destroy(gameObject);
            }
        }
    }
}
