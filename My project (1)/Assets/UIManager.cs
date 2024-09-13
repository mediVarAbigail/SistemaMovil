using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UIManager : MonoBehaviour
{/*
    public GameObject inicioPanel;
    public GameObject loginPanel;
    public GameObject rolSeleccionPanel;
    public GameObject registroPanel;

    private string checkUserUrl = "";
    void Start()
    {
        StartCoroutine(CheckUserStatusCoroutine());
    }

    IEnumerator CheckUserStatusCoroutine()
    {
        // Simula un dato del usuario (por ejemplo, un nombre de usuario o correo electr�nico) para la verificaci�n.
        string userEmail = "usuario@example.com"; // Este dato deber�a ser recuperado de tu l�gica de autenticaci�n.

        // Crear la solicitud a la API
        UnityWebRequest request = UnityWebRequest.Get(checkUserUrl + "?email=" + UnityWebRequest.EscapeURL(userEmail)); // Enviaremos el correo del usuario como par�metro de consulta.
        yield return request.SendWebRequest();

        // Comprobaci�n de la respuesta de la solicitud
        if (request.result == UnityWebRequest.Result.Success)
        {
            // Procesa la respuesta de la API
            string responseText = request.downloadHandler.text;

            // Verifica si el usuario est� registrado
            if (ProcessResponse(responseText))
            {
                ShowInicioPanel();
            }
            else
            {
                ShowRolSeleccionPanel();
            }
        }
        else
        {
            Debug.LogError("Error en la solicitud: " + request.error);
        }
    }

    bool ProcessResponse(string responseText)
    {
        // Aqu� puedes analizar el JSON de la respuesta y determinar si el usuario est� registrado.
        // Por ejemplo, podr�as utilizar un analizador JSON como JsonUtility para analizar la respuesta.
        return responseText.Contains("\"registered\":true"); // Cambia esto seg�n la estructura de la respuesta de tu API.
    }

    // Funciones para mostrar/ocultar los paneles
    public void ShowInicioPanel()
    {
        inicioPanel.SetActive(true);
        loginPanel.SetActive(false);
        rolSeleccionPanel.SetActive(false);
        registroPanel.SetActive(false);
    }

    public void ShowRolSeleccionPanel()
    {
        inicioPanel.SetActive(false);
        loginPanel.SetActive(false);
        rolSeleccionPanel.SetActive(true);
        registroPanel.SetActive(false);
    }*/

}
