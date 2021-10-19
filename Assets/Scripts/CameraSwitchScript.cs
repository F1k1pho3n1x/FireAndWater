using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchScript : MonoBehaviour
{
    public Transform[] cameraPositions;
    public float transitionSpeed;
    Transform currentCameraPosition;

    public Transform inGamePosition;

    public bool isMovable { get; set; }

    public void SwitchMainPosition()
    {
        if(inGamePosition == cameraPositions[0])
        {
            inGamePosition = cameraPositions[1];
        }
        else
        {
            inGamePosition = cameraPositions[0];
        }
    }

    public void SwitchPostGamePosition()
    {
        if(currentCameraPosition == cameraPositions[2])
        {
            currentCameraPosition = inGamePosition;
        }
        else
        {
            currentCameraPosition = cameraPositions[2];
        }
    }

    private void Start()
    {
        isMovable = true;
        inGamePosition = cameraPositions[0];
        currentCameraPosition = cameraPositions[2];
        /*cameraPositions = new Transform[3];

        GameObject gameObject = new GameObject();

        gameObject.transform.position = new Vector3(17.8441124f, 16.6197567f, 0.58523196f);
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(27.0221825f, 270.271973f, 0.000861603126f));
        gameObject.transform.localScale = new Vector3(1.75000024f, 1.75000024f, 1.74999976f);
        cameraPositions[0] = gameObject.transform;
        currentCameraPosition = cameraPositions[0];

        gameObject.transform.position = new Vector3(34.7095909f, 45.5480576f, -0.299512923f);
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(44.7265816f, 271.135162f, 0.00432851398f));
        gameObject.transform.localScale = new Vector3(1.75000024f, 1.75000024f, 1.74999976f);
        cameraPositions[1] = gameObject.transform;

        gameObject.transform.position = new Vector3(8.45634079f, 4.9821496f, 8.12032413f);
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(8.97405434f, 228.330872f, -0.00117271242f));
        gameObject.transform.localScale = new Vector3(1.75000024f, 1.75000024f, 1.74999976f);
        cameraPositions[2] = gameObject.transform;*/
    }

    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            SwitchMainPosition();
        }
        if (Input.GetKeyDown("x"))
        {
            SwitchPostGamePosition();
        }
    }

    public void SwitchCameraToMain()
    {
        currentCameraPosition = inGamePosition;
    }
    public void SwitchCameraPositions()
    {
        if(currentCameraPosition == cameraPositions[0])
        {
            currentCameraPosition = cameraPositions[1];
        }
        else
        {
            currentCameraPosition = cameraPositions[0];
        }
    }

    private void LateUpdate()
    {
        /*
         * if (isMovable)
        {
            transform.position = Vector3.Lerp(transform.position, currentCameraPosition.position, Time.deltaTime * transitionSpeed);

            Vector3 currentAngle = new Vector3(
                Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentCameraPosition.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentCameraPosition.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentCameraPosition.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed)
            );

            transform.eulerAngles = currentAngle;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, inGamePosition.position, Time.deltaTime * transitionSpeed);

            Vector3 currentAngle = new Vector3(
                Mathf.LerpAngle(transform.rotation.eulerAngles.x, inGamePosition.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle(transform.rotation.eulerAngles.y, inGamePosition.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
                Mathf.LerpAngle(transform.rotation.eulerAngles.z, inGamePosition.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed)
            );

            transform.eulerAngles = currentAngle;
        }
        */
    }

}
