using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
            player.OnPlayerDeath.AddListener(OnPlayerDeath);
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
        GameObject[] GameObjects;
        GameObjects = Canvas.GetComponents<GameObject>();
        GameObjects[0].SetActive(false);
        GameObjects[1].SetActive(true);
    }

    public void PlayerChanged()
    {
        player = GlobalData.GetPlayer();
        player.OnPlayerDeath.AddListener(OnPlayerDeath);
    }
}
