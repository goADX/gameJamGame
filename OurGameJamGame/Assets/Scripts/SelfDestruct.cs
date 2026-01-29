using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [Header("This Script is generall for any object that you want to dystroy after a certain time after the summon\nits good to use for visual effect granades and stuff")]
    public float TimeToDestruct = 1f;
    [Header("If you want to summon another object after the destruction put it here")]
    public GameObject SummonedAfterDestruct;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, TimeToDestruct);
    }
    private void OnDestroy()
    {
        if(SummonedAfterDestruct != null)
        {
            GameObject newSummoned = Instantiate(SummonedAfterDestruct, transform.position, Quaternion.identity);
            newSummoned.transform.position = transform.position;
        }
    }

    
}
