using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeUi : MonoBehaviour
{
    List<GameObject> knifeList;
    public GameObject Ui;
    int count;

    void Awake()
    {
        //knifeList adında bir liste oluşturuyoruz
        knifeList = new List<GameObject>();
    }

    public void setKnifeUi(int totalKnife)
    {
        count = 0;
        //listeyi sıfırlıyoruz
        knifeList.Clear();
        //bıçak sayısı kadar biçak ikonu oluşturuyoruz
        for (int i = 0; i < totalKnife; i++)
        {
            //bıçakları oluşturup listeye kaydediyoruz
            GameObject knife = Instantiate(Ui, Vector3.zero, Quaternion.identity, transform);
            knifeList.Add(knife);
        }
    }
    public void deleteUi()
    {
        //bıçak fırlatıldığında ikonu siliyoruz
        Destroy(knifeList[count]);
        count = count + 1;
    }
}
