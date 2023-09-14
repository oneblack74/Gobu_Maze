using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CarteManager : MonoBehaviour
{
    public List<Carte> pioche = new List<Carte>();
    public List<Carte> main = new List<Carte>();
    public Button boutonPiocher;
    void Start()
    {
        // Associer la méthode piocher() à l'événement de clic du bouton
        boutonPiocher.onClick.AddListener(piocher);
        Melangerpioche();
    }

    public void Melangerpioche()
    {
        int count = pioche.Count;
        for(int i = 0; i < count - 1; i++)
        {
            int randomId = Random.Range(i, count);
            Carte tmp = pioche[i];
            pioche[i] = pioche[randomId];
            pioche[randomId] = tmp;
        }
    }

    public void piocher()
    {
        if(pioche.Count > 0 && main.Count < 5)
        {
            main.Add(pioche[0]);
            pioche.RemoveAt(0);
        }
    }

    public void Jouer(int index)
    {
        pioche.Add(main[index]);
        main.RemoveAt(index);
    }
}
