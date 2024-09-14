using System.Collections;
using System.Text.RegularExpressions; // Necesario para la validación del email
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Necesario para el manejo del botón de visibilidad

public class InicioLogin : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdownRol;
    [SerializeField] private TMP_InputField inputNombreCompleto;
    [SerializeField] private TMP_InputField inputNombreUsuario;
    [SerializeField] private TMP_InputField inputEmail;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private Button btnMostrarOcultarPassword; // Botón para mostrar/ocultar contraseña
    [SerializeField] private Button btnRegistrarse; // Botón de registro

    private string rolSeleccionado;
    //private bool mostrarPassword = false; // Estado para mostrar/ocultar la contraseña

    private void Start()
    {
        int indexUsuario = PlayerPrefs.GetInt("UsuarioIndex");
        /*if (GameManager.Instance.usuarios[indexUsuario].rolSeleccion != null)
        {
            Instantiate(GameManager.Instance.usuarios[indexUsuario].rolSeleccion, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("El rol seleccionado es null para el usuario en el índice " + indexUsuario);
        }*/
        Instantiate(GameManager.Instance.usuarios[indexUsuario].rolSeleccion,transform.position, Quaternion.identity);

        // Configuración inicial del botón para mostrar/ocultar contraseña
        btnMostrarOcultarPassword.onClick.AddListener(MostrarOcultarContraseña);
        inputPassword.contentType = TMP_InputField.ContentType.Password; // Inicialmente oculta la contraseña

    
        btnRegistrarse.onClick.AddListener(ContinuarRegistro);
    }

    private void DropdownRolSeleccionado()
    {
        rolSeleccionado = dropdownRol.options[dropdownRol.value].text;
    }

    public void MostrarOcultarContraseña()
    {
        // Verificar si inputPassword está asignado
        if (inputPassword == null)
        {
            Debug.LogError("inputPassword no está asignado.");
            return;
        }

        // Verificar si btnMostrarOcultarPassword está asignado
        if (btnMostrarOcultarPassword == null)
        {
            Debug.LogError("btnMostrarOcultarPassword no está asignado.");
            return;
        }

        // Verificar si el componente TextMeshProUGUI está asignado
        TextMeshProUGUI buttonText = btnMostrarOcultarPassword.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText == null)
        {
            Debug.LogError("No se encontró TextMeshProUGUI en el botón.");
            return;
        }

        // Verifica el tipo de contenido actual y lo alterna
        if (inputPassword.contentType == TMP_InputField.ContentType.Password)
        {
            inputPassword.contentType = TMP_InputField.ContentType.Standard; // Cambia a texto normal
            buttonText.text = "Ocultar Contraseña";
        }
        else
        {
            inputPassword.contentType = TMP_InputField.ContentType.Password; // Cambia a contraseña
            buttonText.text = "Mostrar Contraseña";
        }

        // Actualizar el InputField para reflejar los cambios
        inputPassword.gameObject.SetActive(false);
        inputPassword.gameObject.SetActive(true);
        inputPassword.ForceLabelUpdate();
    }



    public void ContinuarRegistro()
    {
        if (ValidarRegistro())
        {
            // Verifica si el nombre de usuario es único antes de registrar
            StartCoroutine(VerificarNombreUsuarioUnico(inputNombreUsuario.text));
        }
    }

    private bool ValidarRegistro()
    {
        // Validación básica del registro
        if (string.IsNullOrEmpty(inputEmail.text) || string.IsNullOrEmpty(inputNombreCompleto.text) ||
            string.IsNullOrEmpty(inputNombreUsuario.text) || string.IsNullOrEmpty(inputPassword.text))
        {
            Debug.LogError("Nombre completo, nombre de usuario, correo electrónico o contraseña están vacíos.");
            return false;
        }

        if (!EsEmailValido(inputEmail.text))
        {
            Debug.LogError("El formato del correo electrónico no es válido.");
            return false;
        }

        return true;
    }

    private bool EsEmailValido(string email)
    {
        // Expresión regular para validar el formato del correo electrónico
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }

    private IEnumerator VerificarNombreUsuarioUnico(string nombreUsuario)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost:3000/api/usuarios/verificar/" + nombreUsuario);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error al verificar nombre de usuario: " + www.error);
        }
        else
        {
            if (www.downloadHandler.text == "false") // Si el nombre de usuario no existe
            {
                StartCoroutine(RegistrarUsuario());
            }
            else
            {
                Debug.LogError("El nombre de usuario ya está en uso.");
            }
        }
    }

    private IEnumerator RegistrarUsuario()
    {
        WWWForm form = new WWWForm();
        form.AddField("nombreCompleto", inputNombreCompleto.text);
        form.AddField("nombreUsuario", inputNombreUsuario.text);
        form.AddField("email", inputEmail.text);
        form.AddField("password", inputPassword.text);
        form.AddField("rol", rolSeleccionado);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/api/usuarios/register", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error al registrar: " + www.error);
        }
        else
        {
            Debug.Log("Registro exitoso!");
            if (rolSeleccionado == "Niño" || rolSeleccionado == "Terapeuta/Cuidador")
            {
                SceneManager.LoadScene("PanelDatosAdicionales");
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
