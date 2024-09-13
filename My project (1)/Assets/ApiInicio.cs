using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ApiInicio : MonoBehaviour
{/*
    private string apiUrl = ""; // URL base de la API, cambia si es necesario

    // Función para iniciar sesión y obtener el token JWT
    public IEnumerator Login(string username, string password)
    {
        string loginUrl = $"{apiUrl}/usuarios/login";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post(loginUrl, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                // Maneja la respuesta
                string responseText = www.downloadHandler.text;
                Debug.Log("Login successful! Response: " + responseText);

                // Ejemplo: Extraer el token JWT del JSON de respuesta
                // Asegúrate de que tu respuesta es un JSON y parsea correctamente
                // string token = ExtractToken(responseText);
            }
        }
    }

    // Función para obtener datos protegidos usando el token JWT
    public IEnumerator GetProtectedData(string token)
    {
        string protectedUrl = $"{apiUrl}/usuarios/protected";
        UnityWebRequest request = UnityWebRequest.Get(protectedUrl);
        request.SetRequestHeader("Authorization", "Bearer " + token);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            // Maneja la respuesta
            string responseText = request.downloadHandler.text;
            Debug.Log("Protected data received: " + responseText);
        }
    }

    // Ejemplo de función para extraer el token del JSON
    private string ExtractToken(string jsonResponse)
    {
        // Aquí deberías implementar el código para extraer el token del JSON
        // Este es solo un ejemplo simple de cómo podrías hacerlo si la respuesta es un JSON simple
        // Usa una biblioteca JSON como JsonUtility o Newtonsoft.Json para parsear la respuesta
        // Ejemplo simple:
        var json = JsonUtility.FromJson<LoginResponse>(jsonResponse);
        return json.token;
    }
}

// Clase para deserializar la respuesta del inicio de sesión (ajustar según la respuesta real de la API)
[System.Serializable]
public class LoginResponse
{
    public string token;*/
}
