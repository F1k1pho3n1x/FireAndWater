using UnityEngine;
using System.Collections.Generic;

public class AnimateOrbs : MonoBehaviour
{
    public float orbRotationSpeed = 30f;
    public float multiplierRotationSpeed = 100f;
    public float crystalRotationSpeed = 30f;

    Transform revolveOrbsWaist;
    Transform rotateCrystal;

    List<GameObject> revolvablePickups = new List<GameObject>();
    List<GameObject> revolvableMultipliers = new List<GameObject>();

    //GameObject Inve
    void Start()
    {
        rotateCrystal = GameObject.Find("CrystalCollected").transform;
        GameObject[] objects = GameObject.FindGameObjectsWithTag("InvertOrb");
        GameObject[] objects2 = GameObject.FindGameObjectsWithTag("MultiplierOrb");

        //Debug.Log(objects.Length);
        foreach (GameObject item in objects)
	    {
            if(item != null)
            {
                revolvablePickups.Add(item);
                //Debug.Log(item.name);
            }
        }
        foreach (GameObject item in objects2)
        {
            if (item != null)
            {
                revolvableMultipliers.Add(item);
            }
        }
        revolveOrbsWaist = GameObject.Find("CurrentElementOrbs").transform;
    }

    // Update is called once per frame
    void Update()
    {        
        foreach (GameObject item in revolvablePickups)
        {
            if (item != null)
                item.transform.Rotate(new Vector3(0, Time.deltaTime * orbRotationSpeed * 0.1f, 0));
        }
            foreach (GameObject item in revolvableMultipliers)
            {
                if(item != null)
                    item.transform.Rotate(new Vector3(0, Time.deltaTime * multiplierRotationSpeed * 0.1f, 0));
            }
        if (revolveOrbsWaist != null)
            revolveOrbsWaist.transform.Rotate(new Vector3(0, Time.deltaTime * orbRotationSpeed * 0.1f, 0));
        if(rotateCrystal != null)
            rotateCrystal.transform.Rotate(new Vector3(0, 0 , Time.deltaTime * crystalRotationSpeed * 0.1f));
    }
}
