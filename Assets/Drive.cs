using UnityEngine;
using System.Collections;
using UnityEngine.UI;


// A very simplistic car driving on the x-z plane.

public class Drive : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public GameObject fuel;

    void Start()
    {

    }

    void calcDistance()
    {
        Vector3 tankPos = transform.up; // tank Position / Facing direction
        Vector3 fuelDir = fuel.transform.position - this.transform.position; // relatvie vector pointing to Fuel

        float distance = Mathf.Sqrt(Mathf.Pow(tankPos.x-fuelDir.x,2)+ Mathf.Pow(tankPos.y - fuelDir.y, 2));
        Debug.Log("Distance: " + distance);


    } 

    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, translation, 0);

        // Rotate around our y-axis
        transform.Rotate(0, 0, -rotation);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            calcDistance();
        }

    }
}