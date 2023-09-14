using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class IA : MonoBehaviour
{
    private List<int> matrice;
    [SerializeField] private LabyrintheGenerateur laby;
    [SerializeField] private int vitesseDeplacement;
    [SerializeField] private Sprite[] tabSprite;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject ecranLose;
    private int id = 0;

    void Start()
    {
        matrice = new List<int>();
        spriteRenderer.sprite = tabSprite[0];
        VariableGlobale.cleTrouve = false;
        VariableGlobale.dicoPiege = new Dictionary<Vector2Int, GameObject>();
        GameObject.Find("Cle").SetActive(true);
    }

    void Update()
    {
        
    }

    public bool move()
    {
        VariableGlobale.tourFantome = true;

        int xA;
        int yA;

        if (VariableGlobale.cleTrouve)
        {
            xA = VariableGlobale.coordTrappe.x;
            yA = VariableGlobale.coordTrappe.y;
        }
        else
        {
            xA = VariableGlobale.coordCle.x;
            yA = VariableGlobale.coordCle.y;
        }
        


        // parcourir le labyrinthe
        parcourirMatrice(xA, yA);
        
        // trouver le bon chemin
        Vector2Int nouvelleCase = CheminPlusCourt();

        // deplacer sur la prochaine case
        if (nouvelleCase != new Vector2Int(-1, -1))
        {
            if(VariableGlobale.coordFantome.x < nouvelleCase.x && VariableGlobale.coordFantome.y == nouvelleCase.y)
            {
                id = 0;
            }
            else if(VariableGlobale.coordFantome.x > nouvelleCase.x && VariableGlobale.coordFantome.y == nouvelleCase.y)
            {
                id = 1;
            }
            else if(VariableGlobale.coordFantome.x == nouvelleCase.x && VariableGlobale.coordFantome.y < nouvelleCase.y)
            {
                id = 2;
            }
            else if(VariableGlobale.coordFantome.x == nouvelleCase.x && VariableGlobale.coordFantome.y > nouvelleCase.y)
            {
                id = 3;
            }

            spriteRenderer.sprite = tabSprite[id];

            StartCoroutine(Deplacer(nouvelleCase));
            VariableGlobale.coordFantomeDestination = nouvelleCase;
            VariableGlobale.coordFantome = nouvelleCase;

            // vérifier si on est sur la cle
            if (!VariableGlobale.cleTrouve && VariableGlobale.coordFantome == VariableGlobale.coordCle)
            {
                VariableGlobale.cleTrouve = true;
                GameObject.Find("Cle").SetActive(false);
            }
            // vérifier si on est sur la trappe et qu'on a la cle
            else if (VariableGlobale.cleTrouve && VariableGlobale.coordFantome == VariableGlobale.coordTrappe)
            {
                VariableGlobale.end = true;
            }
            // vérifier si on marche pas sur un piège
            
            if (!VariableGlobale.end)
            {
                string tagObjet = ObtenirTagDuPiege();

                if (!tagObjet.Equals("null"))
                {
                    if (tagObjet.Equals("Mine"))
                    {
                        VariableGlobale.nbTourMine = 2;
                        Destroy(VariableGlobale.dicoPiege[VariableGlobale.coordFantome].gameObject);
                    }
                }
            }
            
            
            
            return true;
        }
        else
        {
            ecranLose.SetActive(true);
            return false;
        }
        

        // vérifie les objects
    }

    public string ObtenirTagDuPiege()
    {
        if (VariableGlobale.dicoPiege.Count > 0)
        {
            foreach (KeyValuePair<Vector2Int, GameObject> paire in VariableGlobale.dicoPiege)
        {
            if (paire.Key == VariableGlobale.coordFantome)
            {
                return paire.Value.tag;
            }
        }
        }
        return "null";
    }

    private void parcourirMatrice(int xA, int yA)
    {
        matrice.Clear();

        int hauteur = laby.GetHauteur();
        int largeur = laby.GetLargeur();

        for (int i = 0; i < hauteur; i++)
        {
            for (int j = 0; j < largeur; j++)
            {
                matrice.Add(-1);
            }
        }

        int indexD = xA*hauteur + yA;
        matrice[indexD] = 0;

        Queue<int> file = new Queue<int>();
        file.Enqueue(indexD);

        int[] lig = new int[] { -1, 0, 1, 0 };
        int[] col = new int[] { 0, 1, 0, -1 };

        while (file.Count > 0)
        {
            int indexActuel = file.Dequeue();
            int xActuel = indexActuel / largeur;
            int yActuel = indexActuel % largeur;

            for (int dir = 0; dir < 4; dir++)
            {
                int voisinX = xActuel + lig[dir];
                int voisinY = yActuel + col[dir];

                if (voisinX >= 0 && voisinX < hauteur && voisinY >= 0 && voisinY < largeur)
                {
                    int voisinIndex = voisinX * largeur + voisinY;

                    if (matrice[voisinIndex] == -1 && laby.GetCellule(voisinX, voisinY).GetCelluleType() == E_Cellule.Sol)
                    {
                        matrice[voisinIndex] = matrice[indexActuel] + 1;
                        file.Enqueue(voisinIndex);
                    }
                }
            }
        }


    }

    private void parcourirMatricePourVerifChemin(int xA, int yA, List<Cellule> nouvelleMap)
    {
        matrice.Clear();

        int hauteur = laby.GetHauteur();
        int largeur = laby.GetLargeur();

        for (int i = 0; i < hauteur; i++)
        {
            for (int j = 0; j < largeur; j++)
            {
                matrice.Add(-1);
            }
        }

        int indexD = xA*hauteur + yA;
        matrice[indexD] = 0;

        Queue<int> file = new Queue<int>();
        file.Enqueue(indexD);

        int[] lig = new int[] { -1, 0, 1, 0 };
        int[] col = new int[] { 0, 1, 0, -1 };

        while (file.Count > 0)
        {
            int indexActuel = file.Dequeue();
            int xActuel = indexActuel / largeur;
            int yActuel = indexActuel % largeur;

            for (int dir = 0; dir < 4; dir++)
            {
                int voisinX = xActuel + lig[dir];
                int voisinY = yActuel + col[dir];

                if (voisinX >= 0 && voisinX < hauteur && voisinY >= 0 && voisinY < largeur)
                {
                    int voisinIndex = voisinX * largeur + voisinY;

                    if (matrice[voisinIndex] == -1 && nouvelleMap[voisinX * largeur + voisinY].GetCelluleType() == E_Cellule.Sol)
                    {
                        matrice[voisinIndex] = matrice[indexActuel] + 1;
                        file.Enqueue(voisinIndex);
                    }
                }
            }
        }


    }

    private Vector2Int CheminPlusCourt()
    {
        int hauteur = laby.GetHauteur();
        int largeur = laby.GetLargeur();
        int numeroDepart = matrice[VariableGlobale.coordFantome.x * largeur + VariableGlobale.coordFantome.y];

        int[] lig = new int[] { -1, 0, 1, 0 };
        int[] col = new int[] { 0, 1, 0, -1 };

        for (int direction = 0; direction < 4; direction++)
        {
            int voisinX = VariableGlobale.coordFantome.x + lig[direction];
            int voisinY = VariableGlobale.coordFantome.y + col[direction];

            if (voisinX >= 0 && voisinX < hauteur && voisinY >= 0 && voisinY < largeur)
            {
                int numeroTmp = matrice[voisinX * largeur + voisinY];


                if (numeroTmp != -1 && numeroTmp < numeroDepart)
                {
                    return new Vector2Int(voisinX, voisinY);
                }
            }
        }

        return new Vector2Int(-1, -1);
    }

    private IEnumerator Deplacer(Vector2Int destination)
    {
        Vector3 positionDepart = transform.position;
        Vector3 positionDestination = new Vector3(destination.x + 0.5f, destination.y + 0.5f, transform.position.z);
        float distance = Vector3.Distance(positionDepart, positionDestination);

        float t = 0f;
        while (t < 1f)
        {
            t += vitesseDeplacement * Time.deltaTime / distance;
            transform.position = Vector3.Lerp(positionDepart, positionDestination, t);
            yield return null;
        }

        VariableGlobale.tourFantome = false;
    }

    public int TourMax()
    {
        int xA = VariableGlobale.coordTrappe.x;
        int yA = VariableGlobale.coordTrappe.y;
        parcourirMatrice(xA, yA);

        int hauteur = laby.GetHauteur();
        int largeur = laby.GetLargeur();
        int numeroDepart = matrice[VariableGlobale.coordFantome.x * largeur + VariableGlobale.coordFantome.y];

        return numeroDepart*3;
    }

    public bool CheminPossible(Vector2Int coordMur)
    {   
        int xA;
        int yA;

        if (VariableGlobale.cleTrouve)
        {
            xA = VariableGlobale.coordTrappe.x;
            yA = VariableGlobale.coordTrappe.y;
        }
        else
        {
            xA = VariableGlobale.coordCle.x;
            yA = VariableGlobale.coordCle.y;
        }

        List<Cellule> nouvelleMap = CopierMatrice(coordMur);
        parcourirMatricePourVerifChemin(xA, yA, nouvelleMap);
        Vector2Int nouvelleCase = CheminPlusCourt();

        if (nouvelleCase == new Vector2Int(-1, -1))
        {
            return false;
        }
        return true;

    }

    private List<Cellule> CopierMatrice(Vector2Int coordMur)
    {
        List<Cellule> nouvelleMatrice = new List<Cellule>();

        for (int i = 0; i < laby.GetHauteur() - 1; i++)
        {
            for (int j = 0; j < laby.GetLargeur() - 1; j++)
            {
                nouvelleMatrice.Add(laby.GetCellule(i, j));
            }
        }

        Vector3 coord = new Vector3(coordMur.x * laby.GetTailleCellulePx(), coordMur.y * laby.GetTailleCellulePx(), 0);
        nouvelleMatrice[coordMur.x * laby.GetLargeur() + coordMur.y] = Instantiate(laby.GetCellulePrefab(), coord, Quaternion.identity, transform);

        return nouvelleMatrice;
    }



}
