using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class EatFood : Agent {

    public MeshRenderer renderFloor;

    public override void OnEpisodeBegin() {
        transform.position = new Vector3(Random.Range(-17.5f, 27.5f), -2.5f, Random.Range(-22.5f, 22.5f));
        //targetTransform.position = new Vector3(Random.Range(-3.f, 3.5f), 0, Random.Range(0.5f, 3.5f));

    }

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(transform.position.x);
        sensor.AddObservation(transform.position.z);
        //sensor.AddObservation(targetTransform.position);
    }
    public override void OnActionReceived(ActionBuffers actions) {

        float moveSpeed = 3f;
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        //Debug.Log("MOVE X: "+moveX);
        //Debug.Log("MOVE Z: " + moveZ);
        transform.position += new Vector3(moveX, -2.5f, moveZ) * Time.deltaTime * moveSpeed;

        //Debug.Log("POS:\nX:" + transform.position.x + "\nY: " + transform.position.y + "\nZ: " + transform.position.z);
        

    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("good")) {
            SetReward(+1f);
           // renderFloor.material = winMaterial;
        }
        else if (other.gameObject.CompareTag("Wall")) {
            SetReward(-1f);
            // renderFloor.material = loseMaterial;
        }
        else if (other.gameObject.CompareTag("bad")) {
            SetReward(-2f);
            // renderFloor.material = loseMaterial;
        }
        EndEpisode();
    }
    /*
    public override void Heuristic(in ActionBuffers actionsOut) {

        ActionSegment<float> ca = actionsOut.ContinuousActions;
        ca[0] = Input.GetAxis("Horizontal");
        ca[1] = Input.GetAxis("Vertical");
    }*/


}
