using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastWave : MonoBehaviour
{
    public Vector2 Target;
    public GameObject Source;
    private Projectile projectile;
    [SerializeField] private float LifeTime;
    [SerializeField] private float LivedTime;
    [SerializeField] private float GrowthSpeed;
    // Start is called before the first frame update
    void Start()
    {
        projectile = GetComponent<Projectile>();
        projectile.ValuesGotSet.AddListener(UpdateValues);
    }

    // Update is called once per frame
    void Update()
    {
        LivedTime += Time.deltaTime;
        if( LifeTime < LivedTime )
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Hit:" + col.gameObject.name);
        if (col.gameObject != Source)
        {
            LivingThing livingThing = col.gameObject.GetComponent<LivingThing>();
            if (livingThing != null)
            {
                livingThing.Die(); // OOF
                Destroy(gameObject);    
            }
            FireBall fb = col.gameObject.GetComponent<FireBall>();
            if ( fb != null )
            {
                LivedTime -= LifeTime * (2*(GrowthSpeed - 1));
                gameObject.transform.localScale *= GrowthSpeed;
            }
        }
    }

    public void UpdateValues()
    {
        Target = projectile.Target;
        Source = projectile.Source;
    }
}
