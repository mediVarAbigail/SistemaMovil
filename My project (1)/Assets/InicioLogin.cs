using System.Collections;
using System.Text.RegularExpressions; // Necesario para la validaci�n del email
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Necesario para el manejo del bot�n de visibilidad

public class InicioLogin : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdownRol;
    [SerializeField] private TMP_InputField inputNombreCompleto;
    [SerializeField] private TMP_InputField inputNombreUsuario;
    [SerializeField] private TMP_InputField inputEmail;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private Button btnMostrarOcultarPassword; // Bot�n para mostrar/ocultar contrase�a
    [SerializeField] private Button btnRegistrarse; // Bot�n de registro

    private string rolSeleccionado;
    //private bool mostrarPassword = false; // Estado para mostrar/ocultar la contrase�a

    private void Start()
    {
        int indexUsuario = PlayerPrefs.GetInt("UsuarioIndex");
        /*if (GameManager.Instance.usuarios[indexUsuario].rolSeleccion != null)
        {
            Instantiate(GameManager.Instance.usuarios[indexUsuario].rolSeleccion, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("El rol seleccionado es null para el usuario en el �ndice " + indexUsuario);
        }*/
        Instantiate(GameManager.Instance.usuarios[indexUsuario].rolSeleccion,transform.position, Quaternion.identity);

        // Configuraci�n inicial del bot�n para mostrar/ocultar contrase�a
        btnMostrarOcultarPassword.onClick.AddListener(MostrarOcultarContrase�a);
        inputPassword.contentType = TMP_InputField.ContentType.Password; // Inicialmente oculta la contrase�a

    
        btnRegistrarse.onClick.AddListener(ContinuarRegistro);
    }

    private void DropdownRolSeleccionado()
    {
        rolSeleccionado = dropdownRol.options[dropdownRol.value].text;
    }

    public void MostrarOcultarContrase�a()
    {
        // Verificar si inputPassword est� asignado
        if (inputPassword == null)
        {
            Debug.LogError("inputPassword no est� asignado.");
            return;
        }

        // Verificar si btnMostrarOcultarPassword est� asignado
        if (btnMostrarOcultarPassword == null)
        {
            Debug.LogError("btnMostrarOcultarPassword no est� asignado.");
            return;
        }

        // Verificar si el componente TextMeshProUGUI est� asignado
        TextMeshProUGUI buttonText = btnMostrarOcultarPassword.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText == null)
        {
            Debug.LogError("No se encontr� TextMeshProUGUI en el bot�n.");
            return;
        }

        // Verifica el tipo de contenido actual y lo alterna
        if (inputPassword.contentType == TMP_InputField.ContentType.Password)
        {
            inputPassword.contentType = TMP_InputField.ContentType.Standard; // Cambia a texto normal
            buttonText.text = "Ocultar Contrase�a";
        }
        else
        {
            inputPassword.contentType = TMP_InputField.ContentType.Password; // Cambia a contrase�a
            buttonText.text = "Mostrar Contrase�a";
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
            // Verifica si el nombre de usuario es �nico antes de registrar
            StartCoroutine(VerificarNombreUsuarioUnico(inputNombreUsuario.text));
        }
    }

    private bool ValidarRegistro()
    {
        // Validaci�n b�sica del registro
        if (string.IsNullOrEmpty(inputEmail.text) || string.IsNullOrEmpty(inputNombreCompleto.text) ||
            string.IsNullOrEmpty(inputNombreUsuario.text) || string.IsNullOrEmpty(inputPassword.text))
        {
            Debug.LogError("Nombre completo, nombre de usuario, correo electr�nico o contrase�a est�n vac�os.");
            return false;
        }

        if (!EsEmailValido(inputEmail.text))
        {
            Debug.LogError("El formato del correo electr�nico no es v�lido.");
            return false;
        }

        return true;
    }

    private bool EsEmailValido(string email)
    {
        // Expresi�n regular para validar el formato del correo electr�nico
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
                Debug.LogError("El nombre de usuario ya est� en uso.");
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
            if (rolSeleccionado == "Ni�o" || rolSeleccionado == "Terapeuta/Cuidador")
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
