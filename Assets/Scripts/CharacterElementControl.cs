using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterElementControl : MonoBehaviour
{
    private EElements currentElement;

    public Material[] playerMaterials;
    public Material playerMaterial;

    public GameObject[] orbs = new GameObject[4];

    public AudioSource fireSound;
    public AudioSource waterSound;

    float playerHP;


    void Start()
    {
        ResetPlayer();
    }

    public void PlayWater()
    {
        GameObject.Find("Player").transform.Find("WaterGrabExplosion").GetComponent<ParticleSystem>().Play();
        waterSound.Play();
    }
    
    public void PlayFire()
    {
        GameObject.Find("Player").transform.Find("FireGrabExplosion").GetComponent<ParticleSystem>().Play();
        /*collider.transform.Find("FireGrabExplosion").GetComponent<ParticleSystem>().Play();*/
        fireSound.Play();
    }

    public void ChangeElement(GameObject collider)
    {
        if(currentElement == EElements.FIRE)
        {
            currentElement = EElements.WATER;
            playerMaterial = playerMaterials[1];
            Debug.Log("Element changed to WATER.");

            PlayWater();

            orbs[0].SetActive(false);
            orbs[1].SetActive(false);
            orbs[2].SetActive(true);
            orbs[3].SetActive(true);
        }
        else 
        {
            currentElement = EElements.FIRE;
            playerMaterial = playerMaterials[0];
            Debug.Log("Element changed to FIRE.");

            PlayFire();

            orbs[0].SetActive(true);
            orbs[1].SetActive(true);
            orbs[2].SetActive(false);
            orbs[3].SetActive(false);
        }
        /*if(currentElement == EElements.GOLDEN)
        {
            currentElement = EElements.FIRE;
            playerMaterial = playerMaterials[2];
            Debug.Log("Element changed to GOLD.");
        }*/
    }

    public void IncreasePlayerHP(float gainHP)
    {
        if(gainHP + playerHP <= 1)
        {
            playerHP += gainHP;
        }
    }

    public float DecreasePlayerHP(float loseHP)
    {
        playerHP -= loseHP;
        return playerHP;
    }

    public EElements FetchCurrentElement()
    {
        return currentElement;
    }

    public Material FetchPlayerMaterial()
    {
        return playerMaterial;
    }

    public void ResetPlayer()
    {
        currentElement = EElements.FIRE;
        playerHP = 0.5f;
    }
}
