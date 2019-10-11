using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private LivingThing livingThing;
    
    [SerializeField] private FireBall fireBall;
    [SerializeField] GlobalData GlobalData;
    [SerializeField] private float AttackTimer;

    public bool CanSeePlayer;
    public float AttackChargeTimeInSeconds;

    private Renderer Rend;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        livingThing = gameObject.GetComponent<LivingThing>();
        livingThing.OnDeathEvent.AddListener( OnDeath );
        Rend = gameObject.GetComponent<Renderer>();
        AttackTimer = 0;
        player = GlobalData.GetPlayerAndAddListener(this);
        GlobalData.AddEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer = Rend.isVisible;

        if( CanSeePlayer )
        {
            AttackTimer += Time.deltaTime;
            if( AttackTimer >= AttackChargeTimeInSeconds )
            {
                AttackPlayer();
                AttackTimer = 0;
            }
        }
        else
        {
            AttackTimer = 0;
        }
    }

    public void OnDeath()
    {
        //gameObject.SetActive(false); FOR OBJ POOL
        Destroy(gameObject); 
    }

    private void AttackPlayer()
    {
        if( player != null )
        {
            FireBall FB = Instantiate(fireBall, transform.position, transform.rotation);
            FB.Target = player.transform.position;
            FB.Source = gameObject;
        }
    }

    public void PlayerChanged()
    {
        player = GlobalData.GetPlayer();
    }

    public void OnDestroy()
    {
        GlobalData.RemoveEnemy(this);
    }
}
