using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBehaviour : MonoBehaviour
{
    public enum OffsetPreference { NONE = 0, LOW_OFFSET, HIGH_OFFSET };

    [SerializeField] float InaccuracyX;
    [SerializeField] float InaccuracyY;
    [SerializeField] OffsetPreference OffsetPref;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 Offset( Vector2 original )
    {
        List<Vector2> results = new List<Vector2>();
        Vector2 ret = new Vector2();
        
        switch( OffsetPref )
        {
            case OffsetPreference.NONE:
                ret = new Vector2(Random.Range( -InaccuracyX, InaccuracyX), Random.Range( -InaccuracyY, InaccuracyY));
                break;
            case OffsetPreference.LOW_OFFSET:
                for ( int i = 0; i < 3; i++ )
                {
                    results.Add(new Vector2(Random.Range(-InaccuracyX, InaccuracyX), Random.Range(-InaccuracyY, InaccuracyY)));
                }
                ret = results[0];
                for( int i = 1; i < 3; i++ )
                {
                    if( i > 0 )
                    {
                        if( results[i].magnitude < ret.magnitude )
                        {
                            ret = results[i];
                        }
                    }
                }
                break;
            case OffsetPreference.HIGH_OFFSET:
                for (int i = 0; i < 3; i++)
                {
                    results.Add(new Vector2(Random.Range(-InaccuracyX, InaccuracyX), Random.Range(-InaccuracyY, InaccuracyY)));
                }
                ret = results[0];
                for (int i = 1; i < 3; i++)
                {
                    if (i > 0)
                    {
                        if (results[i].magnitude > ret.magnitude)
                        {
                            ret = results[i];
                        }
                    }
                }
                break;
        }
        ret += original;
        return ret;
    }
}
