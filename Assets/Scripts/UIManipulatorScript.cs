using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManipulatorScript : MonoBehaviour
{
    public CharacterMovement movementScript;
    public Canvas mainCanvas;
    public Canvas endCanvas;
    void Start()
    {
        movementScript = GameObject.Find("Player").GetComponent<CharacterMovement>();
    }

    public void OnStartTap()
    {
        movementScript.StartNewGame();
    }

    public void OnSecretTap()
    {
        CharacterElementControl cec = movementScript.gameObject.GetComponent<CharacterElementControl>();
        //

    }

    bool changed = true;

    public void ChangeMainView()
    {
        if (changed)
        {
            movementScript.mainView = GameObject.Find("View2").transform;
            changed = false;
        }
        else
        {
            movementScript.mainView = GameObject.Find("View1").transform;
            changed = true;
        }        
    }

    public void OnRestartLevel()
    {
        mainCanvas.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        endCanvas.enabled = false;
    }
}
