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

    void calcAngle()
    {
        Vector3 tankUp = transform.up; // tank Position / Facing direction
        Vector3 fuelDir = fuel.transform.position - this.transform.position; // relatvie vector pointing to Fue
        float calcAngleRadians = Mathf.Acos(  ((tankUp.x*fuelDir.x)+(tankUp.y*fuelDir.y))
                                                 / (tankUp.magnitude*fuelDir.magnitude)  );
        Debug.Log("calcAngleRadians: " + calcAngleRadians);
        Debug.Log("Convert calcAngleRadians to regular angle" + (calcAngleRadians * Mathf.Rad2Deg));
        Debug.DrawRay(this.transform.position, tankUp * 3, Color.yellow, 3);
        Debug.DrawRay(this.transform.position, fuelDir, Color.green, 3);

        // Final cross check using built in unity functions
        Debug.Log("Unity angle" + Vector3.Angle(tankUp, fuelDir));


        // Un-signed angle - sometimes in right direction but sometime right amount but wrong direction
        this.transform.Rotate(0,0,calcAngleRadians * Mathf.Rad2Deg);

       
    }


        void calcDistance()
    {
        Vector3 tankPos = transform.up; // tank Position / Facing direction
        Vector3 fuelDir = fuel.transform.position - this.transform.position; // relatvie vector pointing to Fuel

        float distance = Mathf.Sqrt(Mathf.Pow(tankPos.x-fuelDir.x,2)+ Mathf.Pow(tankPos.y - fuelDir.y, 2));
        float unityDistance = Vector3.Distance(tankPos, fuelDir); // regular unity built in method (which should agree with Pythagoras's theorem)
        float delta = distance - unityDistance; // lets see if our calculation to match up 

        Debug.Log("Distance: " + distance);
        Debug.Log("unityDistance: " + unityDistance);
        Debug.Log("delta: " + delta);
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
            //calcDistance();
            calcAngle();
        }

    }
}