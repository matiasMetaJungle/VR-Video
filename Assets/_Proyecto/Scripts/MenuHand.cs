using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHand : MonoBehaviour
{
    public GameObject MenuMano;
    bool isMenuActivo = false;


    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start)) 
        {
            isMenuActivo = !isMenuActivo;
            MenuMano.SetActive(isMenuActivo);
        }   
    }
}
