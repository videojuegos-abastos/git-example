using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{

    [SerializeField]
    float radius;
    void Start()
    {
        SetRandomPositions();
    }

    public void SetRandomPositions()
    {
        foreach (Transform child in transform)
        {
            float posX = Random.Range(-radius, radius);
            float posZ = Random.Range(-radius, radius);
            
            Vector3 newPosition = new Vector3(posX, child.transform.position.y, posZ);
            child.position = newPosition;
        }
    }

    void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere(Vector3.zero, radius);
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(radius * 2, 0, radius * 2));
    }
}
