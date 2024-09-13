using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NuevoRol", menuName = "Usuario")]
public class SeleccionRol : ScriptableObject
{
    public GameObject rolSeleccion;

    public Sprite imagen;

    public string nombre;
}
