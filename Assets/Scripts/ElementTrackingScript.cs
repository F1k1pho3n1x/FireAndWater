using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum ScoringTracker
{
    INCREASING,
    DECREASING,
    INACTIVE
}

public class ElementTrackingScript : MonoBehaviour
{
    private Color redColor = new Color(1, 0.2235294f, 0.1176471f, 1);
    private Color blueColor = new Color(0.1137255f, 0.4588236f, 0.882353f, 1);

    public GameObject player;
    CharacterElementControl controlScript;

    public Slider slider;
    float targetProgress = 0;
    public float fillSpeed = 0.9f;

    ScoringTracker tracker;

    // Start is called before the first frame update
    private void Awake()
    {
        controlScript = player.GetComponent<CharacterElementControl>();
        tracker = ScoringTracker.INACTIVE;
        GameObject.Find("ElementFillColor").GetComponent<Image>().color = redColor;
    }
    void Start()
    {
        //IncrementValue(0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        switch (tracker)
        {
            case ScoringTracker.INCREASING:
                if (slider.value < targetProgress)
                {
                    slider.value += fillSpeed * Time.deltaTime;
                }
                else
                {
                    tracker = ScoringTracker.INACTIVE;
                }
                break;
            case ScoringTracker.DECREASING:
                if (slider.value > targetProgress)
                {
                    slider.value -= fillSpeed * Time.deltaTime;
                }
                else
                {
                    tracker = ScoringTracker.INACTIVE;
                }
                break;
            default:

                break;
        }
        if (slider.value < targetProgress)
            slider.value += fillSpeed * Time.deltaTime;

        if (Input.GetKeyDown("space"))
        {
            DecrementValue(0.5f);
        }
    }

    public void IncrementValue(float value)
    {
        tracker = ScoringTracker.INCREASING;
        targetProgress = slider.value + value;
    }

    public void DecrementValue(float value)
    {
        tracker = ScoringTracker.DECREASING;
        targetProgress = slider.value - value;
    }

    public void SwitchSliderElement()
    {
        if(controlScript.FetchCurrentElement() == EElements.FIRE)
        {

            GameObject.Find("ElementFillColor").GetComponent<Image>().color = redColor;
        }
        else
        {
            GameObject.Find("ElementFillColor").GetComponent<Image>().color = blueColor;
        }
    }
}
