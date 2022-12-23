using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchManager : MonoBehaviour
{
    //isTouch adında bir bool tanımlayarak ekrana dokundugumuzda butona komut vererek bunu true yapıyoruz
    public bool isTouch;
    public void touch()
    {
        isTouch = true;
    }
}
