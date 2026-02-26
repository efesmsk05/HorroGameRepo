using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventControl : MonoBehaviour
{
    public static EventControl instance;    
    public UnityEvent UnityEvent;

    private void Awake()
    {
        instance = this;
    }
    public void InvokeEvent()
    {
        UnityEvent.Invoke();
    }   

    public void isim(string isim)
    {
        print(isim);

    }

}
