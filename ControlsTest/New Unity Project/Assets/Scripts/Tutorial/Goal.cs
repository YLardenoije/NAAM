using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    public UnityEvent GoalReached = new UnityEvent();
    [SerializeField] private bool allowProgress;
    [SerializeField] private Spawner NextSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( allowProgress )
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log(this.name + "Was hit. Next spawn: " + NextSpawn.name);
                Destroy(player.GetComponent<PlayerController>().CurrentGrapple);
                player.transform.position = NextSpawn.transform.position;
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GoalReached.Invoke();
            }
        }
    }

    public void AllowProgress( bool allow )
    {
        allowProgress = true;
    }
}
