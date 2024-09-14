using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NuevoRol", menuName = "Usuario")]
public class SeleccionRol : ScriptableObject
{           //personajes
    public GameObject rolSeleccion;
                    //personajeJugable
    public Sprite imagen;

    public string nombre;
}
