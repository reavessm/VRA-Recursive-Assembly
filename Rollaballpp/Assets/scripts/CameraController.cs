using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject attached;       //Public variable to reference the attached game object


    private Vector3 offset;         //Offset to determine camera position relative to attached object.
    private float speed;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Start()
    {
        // Gets starting distance between camera and attached object position.
        offset = transform.position - attached.transform.position + new Vector3(0, -3);
    }

    void Update()
    {
        // Sets the camera distance from object every frame.
        transform.position = attached.transform.position + offset;

        // Allow the user to control the orientation of the camera with E and Q keys.
        if (Input.GetKey(KeyCode.E))
        {
            rotationX += 1 * 90 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            rotationX -= 1 * 90 * Time.deltaTime;
        }

        transform.localEulerAngles = new Vector3(0, rotationX, 0);
    }

}