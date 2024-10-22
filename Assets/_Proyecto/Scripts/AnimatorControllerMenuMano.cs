using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControllerMenuMano : MonoBehaviour
{
    public Animator animator;
    public bool IdiomaAbierto;

    public void AbrirMenu()
    {
        animator.SetTrigger("AbrirMenu"); 
    }

    public void CerrarMenu() 
    {
        animator.SetTrigger("CerrarMenu");
    }

    public void AbrirMenuIdioma() 
    {
        if(IdiomaAbierto == false) 
        {
            animator.SetTrigger("CerrarMenu");
            animator.SetTrigger("AbrirMenuIdioma");
        }
        else 
        {
            animator.SetTrigger("CerrarMenuIdioma");
            animator.SetTrigger("AbrirMenu");
        }
        
    }

    public void idiomaAbierto() 
    {
        IdiomaAbierto = true;
    }

    public void idiomaNOAbierto()
    {
        IdiomaAbierto = false;
    }
}
