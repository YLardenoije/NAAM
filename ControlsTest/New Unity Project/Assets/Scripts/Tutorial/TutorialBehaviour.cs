using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBehaviour : MonoBehaviour
{
    [SerializeField] private Spawner TutorialStartPoint;
    [SerializeField] private Player PlayerPrefab;
    [SerializeField] private int TutorialProgress;

    private Player player;
    private List<Goal> Goals;  
    
    // Start is called before the first frame update
    void Start()
    {
        player = Instantiate(PlayerPrefab, TutorialStartPoint.transform.position, PlayerPrefab.transform.rotation);
        Goals = new List<Goal>();
        TutorialProgress = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch( TutorialProgress )
        {
            case 0:
                Goals.AddRange( FindObjectsOfType<Goal>() );
                break;
        }
    }
}
