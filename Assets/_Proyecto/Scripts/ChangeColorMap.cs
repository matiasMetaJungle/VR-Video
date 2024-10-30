using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorMap : MonoBehaviour
{
    public Material SelectedColor;
    public Material UnselectedColor;
    public GameObject[] zonas;
    public int Numero;

    public void CambiarColor(int numero)
    {
        // Recorre todas las zonas
        for (int i = 0; i < zonas.Length; i++)
        {
            // Si el índice coincide con 'numero', se aplica el material seleccionado
            if (i == numero)
            {
                zonas[i].GetComponent<Renderer>().material = SelectedColor;
            }
            else
            {
                // Si no coincide, se aplica el material no seleccionado
                zonas[i].GetComponent<Renderer>().material = UnselectedColor;
            }
        }
    }
}
