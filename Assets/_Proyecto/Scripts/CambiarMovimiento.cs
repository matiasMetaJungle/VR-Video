using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarMovimiento : MonoBehaviour
{

    [Header ("TODOS LOS OBJETOS PARA APAGAR DEL TELEPORT")]
    public List<GameObject> Teleport;

    public CharacterController Controller;
    public OVRPlayerController PlayerController;


    public void LocomotionMovement()
    {
        Controller.enabled = true;
        PlayerController.enabled = true;

        foreach (GameObject obj in Teleport) 
        {
            if (obj != null) 
            {
                obj.SetActive(false);
            }
        }
    }

    public void TeleportMovement()
    {
        Controller.enabled = false;
        PlayerController.enabled = false;

        foreach (GameObject obj in Teleport)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }
}
