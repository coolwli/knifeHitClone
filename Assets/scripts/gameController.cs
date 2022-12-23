using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public int bicakSayisi;

    [Header("scripts")]
    public circle cark;
    public knifeUi knifeUi;

    [Header("clips")]
    public AudioClip[] clips;

    [Header("knife")]
    public GameObject bicak;

    [Space(20)]
    public bool boss;

    AudioSource audioSource;
    gameUiManager gameUiManager;

    //levelDesign adında yeni sınıf oluşturuyoruz
    levelDesign levelDesign = new levelDesign();

    private void Awake()
    {
        //oyun start vermeden bellekten level adlı degeri cekerek cark hızını ve bıcak sayısını belirliyoruz bu noktada zor olmaması için +5 verdim
        cark.speed = PlayerPrefs.GetInt("level") + 5;
        bicakSayisi = PlayerPrefs.GetInt("level") + 5;
    }
    private void Start()
    {
        //kendi objesinde olan komponentleri tanımlıyoruz
        audioSource = GetComponent<AudioSource>();
        gameUiManager = GetComponent<gameUiManager>();

        //levelDesign sınıfından yeni bölüm oluşturuyoruz ve çark ile bu scripti tanımlıyoruz
        levelDesign.loadChapter(cark, this);
        //bölüm textimizi buradan güncelliyoruz
        gameUiManager.chapterText.text = levelDesign.chapter.ToString();
        //yeni bıçak oluşturuyoruz ve bıçak sayısını tanımlayarak canvasta bıçak iconlarını oluşturuyoruz
        Instantiate(bicak, new Vector3(0, -6, 1), Quaternion.identity);
        knifeUi.setKnifeUi(bicakSayisi);



    }

    public void hittedKnife()
    {

        //bıçak sayısını azaltıyoruz
        bicakSayisi = bicakSayisi - 1;
        knifeUi.deleteUi();

        if (bicakSayisi > 0)
        {
            //eğer bıçak bitmediyse yeni bıçak yaratıyoruz
            Instantiate(bicak, new Vector3(0, -6, 1), Quaternion.identity);
        }
        else
        {
            audioSource.PlayOneShot(clips[0]);

            //eğer başarılı bir şekilde bıçak bittiyse level atlıyoruz
            levelDesign.loadChapter(cark, this);
            if (levelDesign.chapter == 5)
                boss = true;
            else
                boss = false;
            gameUiManager.chapterText.text = levelDesign.chapter.ToString();
            knifeUi.setKnifeUi(bicakSayisi);
            Instantiate(bicak, new Vector3(0, -6, 1), Quaternion.identity);


        }
    }
    //oyunu kaybettiğimizde bu fonksiyonu çalıştırıyoruz
    public void gameOver()
    {
        //çarkın yavaşlayarak durması fonksiyonunu burda çağırıyoruz
        cark.StartCoroutine("stopCark");
        //çark durduktan sonra işleme devam etmek için bu fonksiyonu kullanıyoruz
        StartCoroutine(gameOverAnim());


    }
    //oyunu kazandığımızda bu fonksiyonu çalıştırıyoruz
    public void winGame()
    {
        //çarkın yavaşlayarak durması fonksiyonunu burda çağırıyoruz
        cark.StartCoroutine("stopCark");
        //oyun kazanma klibini bir kez oynatıyoruz
        audioSource.PlayOneShot(clips[1]);
        //paneli açması için fonksiyonu çalıştırıyoruz
        gameUiManager.winGame();


    }
    //oyunu kaybettikten sonra bu fonksiyonu çalıştırıyoruz
    IEnumerator gameOverAnim()
    {
        //kütüğün durması için 1.5sn bekliyoruz
        yield return new WaitForSeconds(1.5f);
        //oyun kaybetme klibini bir kez oynatıyoruz
        audioSource.PlayOneShot(clips[2]);
        //paneli açması için fonksiyonu çalıştırıyoruz
        gameUiManager.looseGame();

    }

}
