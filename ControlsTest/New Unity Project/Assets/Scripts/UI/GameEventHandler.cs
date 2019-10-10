using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventHandler : MonoBehaviour
{
    [SerializeField] private GlobalData GlobalData;
    [SerializeField] private Canvas Canvas;

    private Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GlobalData.player;
        player.OnPlayerDeath.AddListener(OnPlayerDeath);
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
        GameObject[] GameObjects;
        GameObjects = Canvas.GetComponents<GameObject>();
        GameObjects[0].SetActive(false);
        GameObjects[1].SetActive(true);
    }
}
