using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelDesign
{
    public int chapter;


    public void loadChapter(circle circle, gameController gameController)
    {
        //bölüm sayısını bir arttırıyoruz
        chapter = chapter + 1;
        //eğer 5.Bölümdeysek boss bölümüne geçıyoruz
        if (chapter == 5)
        {
            loadBoss(circle, gameController);
        }
        //eger boss bölümünü başarıyla gecersek leveli bitiriyoruz 
        else if (chapter == 6)
        {
            finishBoss(gameController);
        }
        //eğer bölüm sayısı 5ten küçükse... 
        else
        {
            //çarkı başlatıyoruz
            circle.newChapter(chapter);
            //eğer bölüm sayısı 3e ulaşırsa zorluk efektlerını dahil ediyoruz
            if (chapter == 3)
            {
                circle.StartCoroutine("hizEffect");
                circle.StartCoroutine("tersYonEffect");
            }
            //bıçak sayısını belli bir aralıkta olcak şekilde arttırıyoruz
            gameController.bicakSayisi = Random.Range(chapter + 5, chapter + 8);
            //çark hızını küçük  bir miktarda arttırıyoruz
            circle.speed = circle.speed + 5 / circle.speed;

        }
    }

    void loadBoss(circle circle, gameController gameController)
    {
        //çarkı boss'a göre değiştiriyoruz
        circle.loadBoss();
        //bıçak sayısını 12 yapıyoruz
        gameController.bicakSayisi = 12;
        //çark hızını 2.5 arttırıyoruz
        circle.speed = circle.speed + 2.5f;
    }
    public void finishBoss(gameController gameController)
    {
        //kazanma fonksiyonları için ana kodda bu fonksiyonu çalıştırıyoruz
        gameController.winGame();

    }
}
