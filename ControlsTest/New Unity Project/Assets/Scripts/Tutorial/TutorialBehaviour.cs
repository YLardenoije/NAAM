using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialBehaviour : MonoBehaviour
{
    [SerializeField] private Spawner TutorialStartPoint;
    [SerializeField] private Player PlayerPrefab;
    [SerializeField] private int TutorialProgress;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera Cam;
    [SerializeField] private GameObject Canvas;

    private Player player;
    private PlayerController pc;
    private List<Goal> Goals;
    private Projectile GrapplePrefab, FireballPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        player = Instantiate(PlayerPrefab, TutorialStartPoint.transform.position, PlayerPrefab.transform.rotation);
        pc = player.GetComponent<PlayerController>();
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
                GrapplePrefab = pc.GrapplePrefab;
                FireballPrefab = pc.FireBallPrefab;
                pc.GrapplePrefab = null; //remove the grapple from the player, so that he cant use it for now.
                pc.FireBallPrefab = null; //remove the fireball as well.
                pc.LeaveUI = Canvas;
                break;
            case 1:
                Cam.m_Lens.OrthographicSize = 4;
                break;
            case 2:
                Cam.m_Lens.OrthographicSize = 4;
                pc.GrapplePrefab = GrapplePrefab; //give back the grapple, since the player needs from part 2 and further.
                break;
            case 3:
                Cam.m_Lens.OrthographicSize = 7;
                break;
            case 4:
                Cam.m_Lens.OrthographicSize = 4;
                pc.FireBallPrefab = FireballPrefab; //give back the fireball as well.
                break;
            case 5:
                Cam.m_Lens.OrthographicSize = 4;
                break;
            case 6:
                BackToMainMenu();
                break;
        }
    }

    void OnTutProgress()
    {
        TutorialProgress++;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
