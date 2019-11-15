using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private LivingThing livingThing;
    
    [SerializeField] private Projectile projectile;
    [SerializeField] GlobalData GlobalData;
    [SerializeField] private float AttackTimer;

    public bool CanSeePlayer;
    public float AttackChargeTimeInSeconds;
    public float IntensityValue = 30;
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
        if( player != null )
        {
            player.livingThing.OnDeathEvent.AddListener(PlayerDied);
        }
        GlobalData.AddEnemy(this);
    }

    // Update is called once per frame
    void Update()
    {
          //TODO: REFACTOR
        if (Rend.isVisible && !CanSeePlayer) //since we do this before CanSeePlayer is set 
                                                   //this will be the first frame the enemy is visible
        {
            GlobalData.AddIntesinty(IntensityValue);
        }
        if (!Rend.isVisible && CanSeePlayer)
        {
            GlobalData.SubtractIntesinty(IntensityValue);
        }
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
        GlobalData.SubtractIntesinty(IntensityValue);
        //gameObject.SetActive(false); FOR OBJ POOL
        Destroy(gameObject); 
    }

    private void AttackPlayer()
    {
        if( player != null )
        {
            Projectile p = Instantiate(projectile, transform.position, transform.rotation);
            p.SetValues(player.transform.position, gameObject);
        }
    }

    public void PlayerChanged()
    {
        player = GlobalData.GetPlayer();
        player.livingThing.OnDeathEvent.AddListener(PlayerDied);
    }

    public void OnDestroy()
    {
        GlobalData.RemoveEnemy(this);
    }

    public void PlayerDied()
    {
        player = null;
    }
}
