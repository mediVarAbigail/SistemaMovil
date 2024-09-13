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
        // Simula un dato del usuario (por ejemplo, un nombre de usuario o correo electrónico) para la verificación.
        string userEmail = "usuario@example.com"; // Este dato debería ser recuperado de tu lógica de autenticación.

        // Crear la solicitud a la API
        UnityWebRequest request = UnityWebRequest.Get(checkUserUrl + "?email=" + UnityWebRequest.EscapeURL(userEmail)); // Enviaremos el correo del usuario como parámetro de consulta.
        yield return request.SendWebRequest();

        // Comprobación de la respuesta de la solicitud
        if (request.result == UnityWebRequest.Result.Success)
        {
            // Procesa la respuesta de la API
            string responseText = request.downloadHandler.text;

            // Verifica si el usuario está registrado
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
        // Aquí puedes analizar el JSON de la respuesta y determinar si el usuario está registrado.
        // Por ejemplo, podrías utilizar un analizador JSON como JsonUtility para analizar la respuesta.
        return responseText.Contains("\"registered\":true"); // Cambia esto según la estructura de la respuesta de tu API.
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
