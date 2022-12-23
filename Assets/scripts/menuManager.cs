using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    public GameObject[] panels;
    AudioSource audioSource;
    public AudioClip uiClip;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        setPanelActive(0);
        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level", 1);
        }

    }

    // Update is called once per frame
    public void setPanelActive(int id)
    {
        audioSource.PlayOneShot(uiClip);
        for (int i = 0; i < panels.Length; i++)
        {
            Debug.Log("q");
            if (i == id)
                panels[i].SetActive(true);
            else
                panels[i].SetActive(false);
        }
    }
    public void startGame()
    {
        audioSource.PlayOneShot(uiClip);
        SceneManager.LoadScene(1);
    }
    public void exitGame()
    {
        audioSource.PlayOneShot(uiClip);
        Application.Quit();
    }

}
