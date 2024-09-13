using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class InicioAnimaciones : MonoBehaviour
{
    [SerializeField] private GameObject logo;
    [SerializeField] private GameObject inicioGrupo;
    [SerializeField] private GameObject panelRolSeleccion;
    [SerializeField] private GameObject panelLogin;
    [SerializeField] private GameObject panelRegistro; // Panel de registro principal
    [SerializeField] private GameObject panelRegistroNino; // Subpanel de registro para Niño
    [SerializeField] private GameObject panelRegistroTerapeuta; // Subpanel de registro para Terapeuta/Cuidador
    [SerializeField] private CanvasGroup canvasGroup;

    private string checkUserUrl = "http://localhost:3000/api/usuarios/check"; // Asegúrate de que este endpoint exista en tu API

    private void Start()
    {
        LeanTween.moveY(logo.GetComponent<RectTransform>(), Screen.height / 1.5f, 1.5f).setDelay(2.5f)
            .setEase(LeanTweenType.easeInBounce).setOnComplete(() =>
            {
                StartCoroutine(VerificarRegistro()); // Llama a la corutina después de la animación
            });
    }

    private void BajarAlpha()
    {
        // Obtener el componente CanvasGroup
        CanvasGroup canvasGroup = inicioGrupo.GetComponent<CanvasGroup>();

        if (canvasGroup != null)
        {
            // Ajustar la opacidad usando LeanTween
            LeanTween.alphaCanvas(canvasGroup, 0f, 1f).setDelay(0.5f);
            canvasGroup.blocksRaycasts = false; // Bloquear interacciones después de la animación
        }
        else
        {
            Debug.LogError("No se encontró el componente CanvasGroup en inicioGrupo.");
        }
    }

    private IEnumerator VerificarRegistro()
    {
        string userEmail = PlayerPrefs.GetString("EmailUsuario", ""); // Suponiendo que almacenas el email del usuario

        if (string.IsNullOrEmpty(userEmail))
        {
            MostrarPanel(panelRolSeleccion);
            BajarAlpha(); // Llama a BajarAlpha después de mostrar el panel de selección de rol
            yield break;
        }

        // Crear la solicitud a la API para verificar el registro
        UnityWebRequest request = UnityWebRequest.Get(checkUserUrl + "?email=" + UnityWebRequest.EscapeURL(userEmail));
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // Procesa la respuesta de la API
            string responseText = request.downloadHandler.text;

            if (ProcessResponse(responseText, out User user))
            {
                if (user.rol == "Nino")
                {
                    MostrarPanel(panelRegistro);
                    MostrarSubpanel(panelRegistroNino);
                    SceneManager.LoadScene("InicioGame"); // Cambia "NombreDeLaEscenaDeNino" al nombre real de la escena
                }
                else if (user.rol == "Terapeuta/Cuidador")
                {
                    MostrarPanel(panelRegistro);
                    MostrarSubpanel(panelRegistroTerapeuta);
                    // Aquí puedes cargar la escena correspondiente para Terapeuta/Cuidador si es necesario
                }
                else
                {
                    MostrarPanel(panelRolSeleccion); // Usuario registrado y no necesita registro adicional
                }
            }
            else
            {
                MostrarPanel(panelRolSeleccion); // Usuario no registrado
            }
        }
        else
        {
            Debug.LogError("Error en la solicitud: " + request.error);
            MostrarPanel(panelRolSeleccion); // En caso de error, muestra el panel de selección de rol
        }

        BajarAlpha(); // Llama a BajarAlpha después de verificar el registro del usuario
    }

    private bool ProcessResponse(string responseText, out User user)
    {
        // Deserializa la respuesta JSON
        UserResponse response = JsonUtility.FromJson<UserResponse>(responseText);

        // Asigna el usuario a la variable de salida
        user = response.user;

        // Verifica si el campo 'registered' es verdadero
        return response.registered;
    }

    // Clases para deserializar el JSON
    [System.Serializable]
    public class UserResponse
    {
        public bool registered;
        public User user;
    }

    [System.Serializable]
    public class User
    {
        public int id;
        public string username;
        public string password;
        public string nombre;
        public string email;
        public string rol;
        public string fecha_nacimiento;
        public string diagnostico;
        public string padre_id;
        public string especialidad;
        public int estado;
        public string fechaCreacion;
        public string fechaActualizacion;
        public string idUsuario;
    }

    private void MostrarPanel(GameObject panel)
    {
        // Oculta todos los paneles primero
        inicioGrupo.SetActive(false);
        panelRolSeleccion.SetActive(false);
        panelLogin.SetActive(false);
        panelRegistro.SetActive(false); // Oculta el panel de registro principal

        // Luego, muestra el panel deseado
        panel.SetActive(true);
    }

    private void MostrarSubpanel(GameObject subpanel)
    {
        // Asegúrate de que los subpaneles dentro del panel de registro estén ocultos
        panelRegistroNino.SetActive(false);
        panelRegistroTerapeuta.SetActive(false);

        // Luego, muestra el subpanel deseado
        subpanel.SetActive(true);
    }
}
