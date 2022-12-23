using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class circle : MonoBehaviour
{
    public float speed;

    [Header("sprites")]
    public Sprite pofuduk;
    public Sprite wood;

    [Header("clips")]
    public AudioClip[] clips;


    AudioSource audioSource;
    bool stopped, tersYon;
    SpriteRenderer spriteR;
    CircleCollider2D collider;

    void Awake()
    {
        //kendi objesinde olan komponentleri tanımlıyoruz
        //Awake kısmında tanımlıyoruz çunku oyun start verdiginde bunların hepsinden once tanımlanması gerekli
        audioSource = GetComponent<AudioSource>();
        spriteR = GetComponent<SpriteRenderer>();
        collider = GetComponent<CircleCollider2D>();


    }

    void Update()
    {
        //çark bölüm 2den sonra ters ya da düz dönme efektine sahip olacaktır ve buna göre pozitif ya da negatif yonde dönecektır
        if (tersYon)
        {
            transform.Rotate(Vector3.forward * -speed * 10 * Time.deltaTime);

        }
        else
        {
            transform.Rotate(Vector3.forward * speed * 10 * Time.deltaTime);

        }
    }

    public void newChapter(int level)
    {
        //bölüm geçme klibini bir kez oynatıyoruz
        audioSource.PlayOneShot(clips[0]);
        //carkın görünümünü kütük yapıyoruz
        spriteR.sprite = wood;
        //çarkın colliderini kütüğe gore yapıyoruz
        collider.radius = 1.9f;
        //kütüğün üzerindeki bıçakları siliyoruz
        GameObject[] knifes = GameObject.FindGameObjectsWithTag("knife");
        for (int i = 0; i < knifes.Length; i++)
        {
            Destroy(knifes[i]);
        }

    }
    public IEnumerator tersYonEffect()
    {

        while (true)
        {
            //sürekli random sürelerle ters ya da düz dönderiyoruz
            yield return new WaitForSeconds(Random.Range(2f, 6f));
            if (tersYon && !stopped)
            {
                tersYon = false;
            }
            if (!tersYon && !stopped)

            {
                tersYon = true;
            }
        }
    }
    public IEnumerator hizEffect()
    {
        while (true)
        {
            //sürekli random sürelerle hızını azaltıp arttırıyoruz
            yield return new WaitForSeconds(Random.Range(2f, 6f));
            if (!stopped)
                speed = speed - 6;
            yield return new WaitForSeconds(Random.Range(2f, 6f));
            if (!stopped)
                speed = speed + 6;

        }
    }
    public IEnumerator stopCark()
    {
        //oyun bittiğinde çarkı durdurma efektini oynatıyoruz
        stopped = true;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            //hızını sürekli azaltıyoruz ve 1in altına düşünce durduruyoruz
            if (speed < 1)
                speed = 0;
            else
                speed = speed - 1f;
        }
    }
    //boss bölümü geldiğinde bu fonksiyonu çağırıyoruz
    public void loadBoss()
    {
        //boss uyarısı klibini bir kez oynatıyoruz
        audioSource.PlayOneShot(clips[1]);
        //carkın görünümünü pofuduk yapıyoruz
        spriteR.sprite = pofuduk;
        //çarkın colliderini pofuduga gore yapıyoruz
        collider.radius = 2.4f;
        //kütüğün üzerindeki bıçakları siliyoruz
        GameObject[] knifes = GameObject.FindGameObjectsWithTag("knife");
        for (int i = 0; i < knifes.Length; i++)
        {
            Destroy(knifes[i]);
        }

    }
}
