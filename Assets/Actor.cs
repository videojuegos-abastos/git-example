using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Actor : Agent
{

    [SerializeField]
    Transform target;
    [SerializeField]
    float speed;

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector2 targetPos = new Vector2(target.position.x, target.position.z);
        Vector2 myPos = new Vector2(transform.position.x, transform.position.z);


        sensor.AddObservation(targetPos);
        sensor.AddObservation(myPos);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        Vector3 direction = new Vector3(moveX, 0, moveZ);

        transform.position += direction * speed * Time.deltaTime;
    }



}
