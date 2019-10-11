using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameEventHandler : MonoBehaviour
{
    [SerializeField] private GlobalData GlobalData;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = (Player)GlobalData.GetPlayerAndAddListener(this);
        if ( player != null )
        {
            player.livingThing.OnDeathEvent.AddListener(OnPlayerDeath);
            Debug.Log("Subbed to death event.");
        }
        GlobalData.EnemyCountChanged.AddListener(CheckForGameVictory);
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
        InGameMenuIndentifier[] items = canvas.GetComponentsInChildren<InGameMenuIndentifier>(true);
        foreach( InGameMenuIndentifier i in items )
        {
            if( i.gameObject.name == "GameOver" )
            {
                i.gameObject.SetActive(true);
            }
            else
            {
                i.gameObject.SetActive(false);
            }
        }
    }

    public void PlayerChanged()
    {
        player = GlobalData.GetPlayer();
        player.livingThing.OnDeathEvent.AddListener(OnPlayerDeath);
        Debug.Log("Subbed to death event.");
    }

    public void CheckForGameVictory()
    {
        if( GlobalData.LivingEnemiesCount() == 0 )
        {
            InGameMenuIndentifier[] items = canvas.GetComponentsInChildren<InGameMenuIndentifier>(true);
            foreach (InGameMenuIndentifier i in items)
            {
                if (i.gameObject.name == "Won")
                {
                    i.gameObject.SetActive(true);
                }
                else
                {
                    i.gameObject.SetActive(false);
                }
            }
        }
    }
}
