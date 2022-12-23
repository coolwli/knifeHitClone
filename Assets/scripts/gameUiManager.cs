using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameUiManager : MonoBehaviour
{
    public bool isStopped;

    [Header("texts")]
    public Text levelText;
    public Text chapterText;

    [Header("panels")]
    public GameObject pauseUi;
    public GameObject winUi;
    public GameObject looseUi;

    [Space(20)]
    public AudioClip uiClip;

    AudioSource audioSource;
    gameController gameController;


    void Start()
    {
        //kendi objesinde olan komponentleri tanımlıyoruz
        gameController = GetComponent<gameController>();
        audioSource = GetComponent<AudioSource>();

        //karışıklık olmaması için oyun başında tüm panelleri kapatıyoruz
        pauseUi.SetActive(false);
        winUi.SetActive(false);
        looseUi.SetActive(false);



    }
    void Update()
    {
        //sürekli olarak level textine bellekten level degerini çekerek yazdırıyoruz
        levelText.text = PlayerPrefs.GetInt("level").ToString();
    }
    //eger oyun durdurulursa bu fonksiyonu çalıstırıyoruz
    public void stopGame()
    {
        //buton clik sesini bir kez çalıyoruz
        audioSource.PlayOneShot(uiClip);
        isStopped = true;
        //pause panelini açıyoruz
        pauseUi.SetActive(true);

    }
    //eger oyun devam ettirilirse bu fonksiyonu çalıstırıyoruz
    public void resumeGame()
    {
        //buton clik sesini bir kez çalıyoruz
        audioSource.PlayOneShot(uiClip);
        isStopped = false;
        //pause panelini kapatıyoruz
        pauseUi.SetActive(false);
    }
    //eger oyun kaybedilirse bu fonksiyonu çalıstırıyoruz
    public void looseGame()
    {
        isStopped = true;
        //loose panelini açıyoruz
        looseUi.SetActive(true);
    }
    //eger oyun kazanılırsa bu fonksiyonu çalıstırıyoruz
    public void winGame()
    {
        isStopped = true;
        //loose panelini kapatıyoruz
        winUi.SetActive(true);
        //bellekteki level degişkenini 1 arttırıyoruz
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
    }
    //eger oyun yeniden başlatılmak istenirse bu fonksiyonu çalıstırıyoruz
    public void resetGame()
    {
        //sahneyi yeniden başlatıyoruz
        SceneManager.LoadScene(1);
    }
    //eger oyundan çıkmak istenirse bu fonksiyonu çalıstırıyoruz
    public void exitGame()
    {
        //menü sahnesini açıyoruz
        SceneManager.LoadScene(0);
    }
    //eger yeni level başlatılırsa bu fonksiyonu çalıstırıyoruz
    public void newGame()
    {
        //sahneyi yeniden başlatıyoruz
        SceneManager.LoadScene(1);
    }


}
