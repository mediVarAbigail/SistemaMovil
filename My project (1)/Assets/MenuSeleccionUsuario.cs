using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MenuSeleccionUsuario : MonoBehaviour
{
    private int index;

    [SerializeField] private Image imagen;
    [SerializeField] private TextMeshProUGUI nombre;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        index = PlayerPrefs.GetInt("UsuarioIndex");

        if (index > gameManager.usuarios.Count - 1)
        {
            index = 0;
        }

        CambiarPantalla();
    }

    private void CambiarPantalla()
    {
        PlayerPrefs.SetInt("UsuarioIndex", index);
        imagen.sprite = gameManager.usuarios[index].imagen;
        nombre.text = gameManager.usuarios[index].nombre;
    }

    public void SiguientesUsuario()
    {
        if (index == gameManager.usuarios.Count - 1)
        {
            index = 0;
        }
        else
        {
            index += 1;
        }
        CambiarPantalla();
    }

    public void AnteriorUsuario()
    {
        if (index == 0)
        {
            index = gameManager.usuarios.Count - 1;
        }
        else
        {
            index -= 1;
        }
        CambiarPantalla();
    }
    public void IniciarRegistro()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        /*// Guarda el rol seleccionado en PlayerPrefs para ser usado en la siguiente escena
        PlayerPrefs.SetString("RolSeleccionado", gameManager.usuarios[index].nombre);

        // Cambia a la siguiente escena donde se ingresarán los datos del usuario
        SceneManager.LoadScene("PanelLogin"); // Asegúrate de que el nombre de la escena esté correcto
        */
    }
}
