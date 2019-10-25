using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeacefulFireball : MonoBehaviour
{
    public Vector2 Target;
    public GameObject Source;
    private Projectile projectile;

    [SerializeField] private Spawner RespawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        projectile = GetComponent<Projectile>();
        projectile.ValuesGotSet.AddListener(UpdateValues);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Hit:" + col.gameObject.name);
        if (col.gameObject != Source)
        {
            Player player = col.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.transform.position = RespawnPoint.transform.position;
            }
            Destroy(gameObject);
        }
    }

    public void UpdateValues()
    {
        Target = projectile.Target;
        Source = projectile.Source;
    }
}
