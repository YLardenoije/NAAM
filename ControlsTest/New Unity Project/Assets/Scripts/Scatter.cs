using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scatter : MonoBehaviour
{
    public Vector2 Target;
    public GameObject Source;

    [SerializeField] private int AdditionalShots;
    [SerializeField] private float speed;
    [SerializeField] private Projectile projectile;
    // Start is called before the first frame update
    void Start()
    {
        projectile = GetComponent<Projectile>();
        projectile.ValuesGotSet.AddListener(UpdateValues);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Source == null)
        {
            Source = gameObject.GetComponentInParent<Scatter>().Source;
            Debug.Log("Source is: " + Source.name);
            Target = gameObject.GetComponentInParent<Scatter>().Target;
        }
    }

    public void UpdateValues()
    {
        Target = projectile.Target;
        Source = projectile.Source;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if( Source != null )
        {
            Debug.Log("Hit:" + col.gameObject.name);
            if (col.gameObject != Source && col.GetComponent<Scatter>() == null )
            {
                LivingThing livingThing = col.gameObject.GetComponent<LivingThing>();
                if (livingThing != null)
                {
                    livingThing.Die(); // OOF
                }
                Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log("Source was null.....");
        }
        
    }
}
