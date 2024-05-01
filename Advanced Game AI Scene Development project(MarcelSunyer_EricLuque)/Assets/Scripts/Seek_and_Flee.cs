using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Seek_and_Flee : MonoBehaviour
{
    public GameObject destino;
    public float maxSpeed = 3;
    public float angle;
    private Vector3  direction;
    private Vector3 movement = Vector3.zero;
    public float offset_stop = 1;

    public float linear_acceleration = 100;
    public float angular_acceleration;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Seek
        Vector3 direction = destino.transform.position - gameObject.transform.position;
        direction.y = 0f;    // (x, z): position in the floor

        // Flee
        //Vector3 direction = gameObject.transform.position - destino.transform.position;

        //Movment = Direction normalized
        movement += direction.normalized * maxSpeed;
        //movement = direction.normalized * linear_acceleration;

        angle = Mathf.Rad2Deg * Mathf.Atan2(movement.x, movement.z);
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);  // up = y


        if (direction.magnitude >= offset_stop)
        { 
            gameObject.transform.rotation = rotation;
            gameObject.transform.position += movement;
        
        }
        else if (direction.magnitude >= offset_stop + 2)
        {
            gameObject.transform.rotation = rotation;
            gameObject.transform.position += movement - ((direction.magnitude) * movement);

        }
        else
        {
            gameObject.transform.rotation = rotation;
        }
    }
}
