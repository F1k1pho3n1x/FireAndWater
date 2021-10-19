using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateTextPopUps : MonoBehaviour
{
    List<Transform> labels = new List<Transform>();
    public Transform label;

    public float popUpSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform item in labels)
        {
            if(item != null)
                item.position += new Vector3(0, Time.deltaTime * popUpSpeed * 0.01f, 0);
        }
    }

    public void PopUpCrystal(Transform crystalTransform)
    {
        Transform item = Instantiate(label);
        item.position = crystalTransform.position + new Vector3(2.5f, 0, 4f);
        Destroy(item.gameObject, 0.5f);
        labels.Add(item);
    }
}
