using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sky : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time*speed);
    }
}

