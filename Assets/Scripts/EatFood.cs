using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class EatFood : Agent {

    public MeshRenderer renderFloor;

    public Text pointsText;

    private int points = 0;

    public GameObject foodSpwaner;
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
       
        for(int i = 0; i<foodSpwaner.transform.childCount; i++)
        {
            if (foodSpwaner.transform.GetChild(i).CompareTag("good"))
            {
              
                Vector3 dirToFood = (foodSpwaner.transform.GetChild(i).position - transform.position).normalized;
                sensor.AddObservation(dirToFood.x);
                sensor.AddObservation(dirToFood.z);
            }
            else
            {
                Vector3 dirToFood = (foodSpwaner.transform.GetChild(i).position - transform.position).normalized;
                sensor.AddObservation(0);
                sensor.AddObservation(0);
            }
        }
       // sensor.AddObservation(transform.position.x);
        //sensor.AddObservation(transform.position.z);
    }
    public override void OnActionReceived(ActionBuffers actions) {

        float moveSpeed = 5f;
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        
        transform.position += new Vector3(moveX, -2.5f, moveZ) * Time.deltaTime * moveSpeed;
    }

    private void OnTriggerEnter(Collider other) {
      
        if (other.gameObject.CompareTag("good")) {
            SetReward(+1f);
            points += 2;
            pointsText.text = "" + points;
           // Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Wall")) {
            SetReward(-1f);
            Debug.Log("wall");
            EndEpisode();

        }
        else if (other.gameObject.CompareTag("bad")) {
            SetReward(-1f);
            points--;
            pointsText.text = "" + points;
            EndEpisode();
            //   Destroy(other.gameObject);
        }
       
    }
    public override void Heuristic(in ActionBuffers actionsOut) {
        float force = 0.1f;
        ActionSegment<float> ca = actionsOut.ContinuousActions;
        ca[0] = Input.GetAxis("Horizontal")*force;
        ca[1] = Input.GetAxis("Vertical")*force;
    }


}
