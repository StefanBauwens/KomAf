using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoCanvas : MonoBehaviour {

    private static InfoCanvas instanceRef;

    void Awake()
    {
        if (instanceRef == null)
        {

            instanceRef = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
