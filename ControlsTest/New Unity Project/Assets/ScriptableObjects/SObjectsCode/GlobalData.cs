using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GlobalData", menuName = "ScriptableObjects/GlobalData", order = 1)]
public class GlobalData : ScriptableObject
{

    public UnityEvent IntensityChanged = new UnityEvent();
    public UnityEvent PlayerChanged = new UnityEvent();
    public UnityEvent EnemyCountChanged = new UnityEvent();

    

    public enum ItemTypes { MovementItem = 0, CombatItem };
    public enum MovementItems { Grapple = 0, JetPack };
    public enum CombatItems { FireBall = 0 };
    public enum Handedness { Lefthanded = 0, Righthanded };

    public ItemTypes SelectedItemType;
    public MovementItems SelectedMovementItem;
    public CombatItems SelectedCombatItem;
    public Handedness SelectedHandedness;

    private Player player;
    private List<Enemy> Enemies = new List<Enemy>();

    public float Intensity { get;private set; }

    void Start()
    {
        Intensity = 0;
        SelectedItemType = ItemTypes.MovementItem;
        SelectedCombatItem = CombatItems.FireBall;
        SelectedMovementItem = MovementItems.Grapple;
        SelectedHandedness = Handedness.Lefthanded;
    }
    public void AddIntesinty(float f)
    {
        
        Intensity += f;
        IntensityChanged.Invoke();
    }
    public void SubtractIntesinty(float f)
    {
        Intensity -= f;
        IntensityChanged.Invoke();
    }
    public void setIntensity(float f)
    {
        Intensity = f;
        IntensityChanged.Invoke();
    }

    public void SetPlayer( Player player )
    {
        this.player = player;
        PlayerChanged.Invoke();
    }

    public void RemovePlayer()
    {
        player = null;
    }

    public Player GetPlayer()
    {
        return player;
    }

    public Player GetPlayerAndAddListener( Enemy enemy )
    {
        PlayerChanged.AddListener(enemy.PlayerChanged);
        return player;
    }

    public Player GetPlayerAndAddListener(GameEventHandler Geh)
    {
        PlayerChanged.AddListener(Geh.PlayerChanged);
        return player;
    }

    public void AddEnemy( Enemy enemy )
    {
        Enemies.Add(enemy);
        EnemyCountChanged.Invoke();
    }

    public void RemoveEnemy( Enemy enemy )
    {
        Enemies.Remove(enemy);
        EnemyCountChanged.Invoke();
    }

    public int LivingEnemiesCount()
    {
        return Enemies.Count;
    }
}
