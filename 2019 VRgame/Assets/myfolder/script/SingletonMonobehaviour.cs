using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMonobehaviour
{
    private static SingletonMonobehaviour instance;
    
    public static SingletonMonobehaviour Instance
    {
        get
        {
            if (instance == null) instance = new SingletonMonobehaviour();
            return instance;
        }
    }
}
