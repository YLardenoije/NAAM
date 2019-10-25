using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBehaviour : MonoBehaviour
{
    [SerializeField] private Spawner TutorialStartPoint;
    [SerializeField] private Player PlayerPrefab;
    [SerializeField] private int TutorialProgress;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera Cam;

    private Player player;
    private List<Goal> Goals;  
    
    // Start is called before the first frame update
    void Start()
    {
        player = Instantiate(PlayerPrefab, TutorialStartPoint.transform.position, PlayerPrefab.transform.rotation);
        Goals = new List<Goal>();
        TutorialProgress = 0;
        Cam.Follow = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        switch( TutorialProgress ) //If a tut step needs special action to be taken, add it in this switch case.
        {
            case 0:
                Goals.AddRange( FindObjectsOfType<Goal>() );
                foreach( Goal g in Goals )
                {
                    g.GoalReached.AddListener(OnTutProgress);
                }
                TutorialProgress++;
                break;
            case 3:
                Cam.m_Lens.OrthographicSize = 7;
                break;
        }
    }

    void OnTutProgress()
    {
        TutorialProgress++;
    }
}
