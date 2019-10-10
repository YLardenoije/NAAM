using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameEventHandler : MonoBehaviour
{
    [SerializeField] private GlobalData GlobalData;
    [SerializeField] private Canvas Canvas;
    [SerializeField] private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = (Player)GlobalData.GetPlayerAndAddListener(this);
        if ( player != null )
        {
            player.OnDeathEvent.AddListener(OnPlayerDeath);
            Debug.Log("Subbed to death event.");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerWin()
    {

    }

    public void OnPlayerDeath()
    {
        Debug.Log("I came here Wgooo");
        //now somehow turn on the right scene thingy.
    }

    public void PlayerChanged()
    {
        player = GlobalData.GetPlayer();
        player.OnDeathEvent.AddListener(OnPlayerDeath);
        Debug.Log("Subbed to death event.");
    }
}
