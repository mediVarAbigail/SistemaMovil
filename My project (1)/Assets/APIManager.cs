using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{/*
    private string apiUrl = "http://localhost:3000/api/usuarios"; // Cambia esta URL por la de tu API REST

    // Método para registrar un nuevo usuario
    public void RegisterUser(string username, string email, string password)
    {
        StartCoroutine(PostRequest(username, email, password));
    }

    private IEnumerator PostRequest(string username, string email, string password)
    {
        // Crea un objeto JSON con los datos del usuario
        Dictionary<string, string> userData = new Dictionary<string, string>
        {
            { "username", username },
            { "email", email },
            { "password", password }
        };

        string jsonData = JsonUtility.ToJson(userData);

        // Configura la solicitud HTTP POST
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Espera la respuesta de la API
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Usuario registrado con éxito");
        }
        else
        {
            Debug.Log("Error al registrar el usuario: " + request.error);
        }
    }
    */
}
