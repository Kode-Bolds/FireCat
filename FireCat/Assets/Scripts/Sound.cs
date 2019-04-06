using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{

    public static Sound soundInstance = null;
    public static Sound getSoundInstance
    {
        get { return soundInstance; }
    }

    private void Awake()
    {
        if (soundInstance != null && soundInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            soundInstance = this;
        }


        DontDestroyOnLoad(this.gameObject);
    }
}
