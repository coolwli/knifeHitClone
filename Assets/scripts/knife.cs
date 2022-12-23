using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knife : MonoBehaviour
{
    //public float speed;
    //burada bıçak fırlatma hızı her zaman 32 olacağı için değişkene bağlı tutmadım fakat isteğe bağlı olarak speed tanımlanabiilir

    [Header("clips")]
    public AudioClip[] clips;

    [Header("particle systems")]
    public ParticleSystem woodParticle;
    public ParticleSystem pofudukParticle;
    public ParticleSystem knifeParticle;


    bool isHitted;

    Rigidbody2D rb;

    gameController gameController;
    touchManager touchManager;
    gameUiManager gameUiManager;

    GameObject gameControllerObject;
    GameObject touchField;

    AudioSource audioSource;



    void Start()
    {
        //kendi objesinde olan komponentleri tanımlıyoruz
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        //gameUiManager scriptini manager tagını arayak buldugumuz objenin içinden seçerek tanımlıyoruz
        gameControllerObject = GameObject.FindGameObjectWithTag("manager");
        gameUiManager = gameControllerObject.GetComponent<gameUiManager>();
        //gameController scriptini manager tagını arayak buldugumuz objenin içinden seçerek tanımlıyoruz
        gameController = gameControllerObject.GetComponent<gameController>();

        //touchManager scriptini touch tagını arayak buldugumuz objenin içinden seçerek tanımlıyoruz
        touchField = GameObject.FindGameObjectWithTag("touch");
        touchManager = touchField.GetComponent<touchManager>();



    }

    void Update()
    {
        //eger ekrana dokunulursa daha önce atış yapılmadıysa ve oyun durmadıysa bıçak fırlatılacak
        if (touchManager.isTouch && !isHitted && !gameUiManager.isStopped)
        {
            //bıçak fırlatma sesni bir kez oynatıyoruz
            audioSource.PlayOneShot(clips[0]);
            rb.velocity = Vector2.up * 32;
            //istouch degerini false yaparak yeniden erkana dokunuldugunda algılanılmasını saglıyoruz
            touchManager.isTouch = false;
        }



    }
    //bir objeye temas ederse bu fonksiyonu çalıştırıyoruz
    private void OnCollisionEnter2D(Collision2D other)
    {
        //eğer obje çarkssa  
        if (other.gameObject.CompareTag("circle") && !isHitted)
        {
            //saplanma sesini bir kez oynatıyoruz
            audioSource.PlayOneShot(clips[1]);
            //eğer obje pofuduksa pofuduk partikul efektini oynatıyoruz
            if (gameController.boss) pofudukParticle.Play();
            //eğer obje kütükse kutuk partikul efektini oynatıyoruz
            else woodParticle.Play();
            isHitted = true;
            //bıçağı çarkın elemanı haline getiriyoruz böylece çarkla beraber dönüyor
            transform.parent = other.transform;
            //bıçak sayısını bir azaltması ve yeni bıçak pluşturması için bu fonksiyonu kullanıyoruz
            gameController.hittedKnife();
            //rigidbody tipini statik yaparak bıçağı sabitliyoruz
            rb.bodyType = RigidbodyType2D.Static;
            //çarka saplanmış olarak gözükmesi için bi miktar içine itiyoruz
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.7f, 1);
            //ileride karışıklık olmaması için scripti kullanılmaz hale getiriyoruz
            gameObject.GetComponent<knife>().enabled = false;
        }




        if (other.gameObject.CompareTag("knife") && !isHitted)
        {
            //bıçağa çarpma sesini bir kez oynatıyoruz
            audioSource.PlayOneShot(clips[2]);
            //bıçak partikul efektini oynatıyoruz
            knifeParticle.Play();
            //colliderleri kapatarak düşerken başka bir cisme carpmasını engelliyoruz
            GetComponents<BoxCollider2D>()[0].enabled = false;
            GetComponents<BoxCollider2D>()[1].enabled = false;
            isHitted = true;
            //bu komutla ileri gitmesini iptal ediyoruz
            rb.velocity = Vector2.zero;
            //yer çekimi vererek düşmesini sağlıyoruz
            rb.gravityScale = 1;
            //dönme efekti veriyoruz
            rb.angularVelocity = 300f;
            //gerekli kaybettme fonksiyonları için ana kodda bu fonksiyonu çalıştırıyoruz
            gameController.gameOver();

        }

    }
}
