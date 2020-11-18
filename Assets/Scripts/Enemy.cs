using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    // config params
    [SerializeField]
    Transform path;
    [SerializeField]
    private float movementSpeed;


    //cached
    List<Transform> wayPoints;


    //state
    Vector2 nextPos;
    int destIndex;

    void Awake()
    {
        InitializeWayPointsList();
    }

    private void InitializeWayPointsList()
    {
        wayPoints = new List<Transform>();
        for (int i = 0; i < path.childCount; i++)
        {
            wayPoints.Add(path.GetChild(i));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = wayPoints[0].position;
        nextPos = transform.position;
        destIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
    }

    private void Move()
    {

        if ((Vector2)transform.position == nextPos)
        {
            destIndex++;
            if (destIndex < wayPoints.Count)
            {
                nextPos = wayPoints[destIndex].position;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, nextPos, movementSpeed * Time.deltaTime);
    }

}
