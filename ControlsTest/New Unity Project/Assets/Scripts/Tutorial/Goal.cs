using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Goal : MonoBehaviour
{
    public UnityEvent GoalReached = new UnityEvent();
    [SerializeField] private Spawner NextSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayerCollision( Player player )
    {
        player.transform.position = NextSpawn.transform.position;
        GoalReached.Invoke();
    }
}
