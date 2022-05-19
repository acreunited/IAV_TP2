using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class EatFood : Agent {

    public MeshRenderer renderFloor;

    public override void OnEpisodeBegin() {
        float x;
        float z;
        
        if (transform.position.x < -18.5f) {
            x = -18.5f;
        }
        else if (transform.position.x > 18.5f) {
            x = 18.5f;
        }
        else {
            x = transform.position.x;
        }

        if (transform.position.z < -18.5f) {
            z = -18.5f;
        }
        else if (transform.position.z > 18.5f) {
            z = 18.5f;
        }
        else {
            z = transform.position.z;
        }

        transform.position = new Vector3(x, -2.5f, z);
        //transform.position = new Vector3(Random.Range(-18.5f, 18.5f), -2.5f, Random.Range(-18.5f, 18.5f));
        //targetTransform.position = new Vector3(Random.Range(-3.f, 3.5f), 0, Random.Range(0.5f, 3.5f));
      
    }

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(transform.position.x);
        sensor.AddObservation(transform.position.z);
    }
    public override void OnActionReceived(ActionBuffers actions) {

        float moveSpeed = 3f;
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        
        transform.position += new Vector3(moveX, -2.5f, moveZ) * Time.deltaTime * moveSpeed;
    }

    private void OnTriggerEnter(Collider other) {
      
        if (other.gameObject.CompareTag("good")) {
            SetReward(+2f);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Wall")) {
            SetReward(-1f);
        }
        else if (other.gameObject.CompareTag("bad")) {
            SetReward(-2f);
            Destroy(other.gameObject);
        }
        EndEpisode();
    }
    public override void Heuristic(in ActionBuffers actionsOut) {
        float force = 0.1f;
        ActionSegment<float> ca = actionsOut.ContinuousActions;
        ca[0] = Input.GetAxis("Horizontal")*force;
        ca[1] = Input.GetAxis("Vertical")*force;
    }


}
