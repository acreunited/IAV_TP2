using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{

    GameObject food;
    public Material good;
    public Material bad;

    float pauseTime = 30f;
    float nextTime = 0f;
   

    // Start is called before the first frame update
    void Start()
    {
 
        Spawn(50);
    }

    // Update is called once per frame
    void Update()
    {
      /*  if (Time.time > nextTime) {
            nextTime = Time.time + pauseTime;
            Spawn(5);
            
       }*/
       
    }

    private void Spawn(int n) {
       
        //rb.velocity = new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
        for (int i = 0; i < n; i++) {
            food = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            food.name = "food";
            Rigidbody rb = food.AddComponent<Rigidbody>();
            rb.useGravity = false;
            food.GetComponent<SphereCollider>().isTrigger = true;
            food.transform.position = new Vector3(Random.Range(-2f, 2f), -0.5f, Random.Range(-2f, 2f));
            //food.transform.parent = transform;
            
          

          /*  if (Random.value > 0.5f) {
                food.tag = "good";
                food.GetComponent<MeshRenderer>().material = good;
            }
            else {
                food.tag = "bad";
                food.GetComponent<MeshRenderer>().material = bad;
            }*/

            
        }
    }
}
