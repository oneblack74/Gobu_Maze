using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f; // Vitesse de déplacement de la caméra
    public float panBorderThickness = 10f; // Épaisseur de la bordure de déplacement

    public float minX = -10f; // Valeur minimale de la coordonnée X
    public float maxX = 10f; // Valeur maximale de la coordonnée X
    public float minY = -10f; // Valeur minimale de la coordonnée Y
    public float maxY = 10f; // Valeur maximale de la coordonnée Y


    private Camera cam; // Référence à la caméra
    private Vector3 lastMousePosition; // Dernière position de la souris


    private void Start()
    {
        cam = GetComponent<Camera>();
        lastMousePosition = Input.mousePosition;

        
    }

    public void PlacerCamera(GameObject fantome)
    {
        Vector3 nouvellePosition = new Vector3(fantome.transform.position.x, fantome.transform.position.y, transform.position.z);
        transform.position = nouvellePosition;
    }

    private void Update()
    {
        if (!VariableGlobale.pause)
        {
            // Déplacement de la caméra
            PanCamera();

            // Mise à jour de la dernière position de la souris
            lastMousePosition = Input.mousePosition;
        }
        
    }

    private void PanCamera()
    {
        // Obtient le déplacement de la souris depuis la dernière frame
        Vector3 mouseDelta = Input.mousePosition - lastMousePosition;

        // Calcule les déplacements horizontaux et verticaux de la caméra
        float horizontal = 0f;
        float vertical = 0f;

        // Vérifie si le curseur de la souris est près des bords de l'écran
        if (Input.mousePosition.x < panBorderThickness)
            horizontal = -1f;
        else if (Input.mousePosition.x > Screen.width - panBorderThickness)
            horizontal = 1f;

        if (Input.mousePosition.y < panBorderThickness)
            vertical = -1f;
        else if (Input.mousePosition.y > Screen.height - panBorderThickness)
            vertical = 1f;

        // Calcule le déplacement de la caméra en fonction des mouvements horizontaux et verticaux
        Vector3 pan = new Vector3(horizontal, vertical, 0f) * panSpeed * Time.deltaTime;

        // Calcule la position cible de la caméra en ajoutant le déplacement au position actuelle
        Vector3 targetPosition = transform.position + pan;

        // Restreint la position cible aux limites spécifiées
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        // Calcule le déplacement réel en soustrayant la position cible de la position actuelle
        Vector3 actualPan = targetPosition - transform.position;

        // Applique le déplacement réel à la position de la caméra
        transform.Translate(actualPan, Space.World);
    }


}
