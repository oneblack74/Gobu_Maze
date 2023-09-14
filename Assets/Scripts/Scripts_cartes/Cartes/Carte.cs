using UnityEngine;

public class Carte : ScriptableObject
{
    // Propriétés commune à la toute les cartes
    [SerializeField] private string nom;
    [SerializeField] private string description;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int coup;
    public GameObject activation;

    // Méthodes communes à toutes les cartes
    public virtual void activer(int id)
    {
        Debug.Log("La carte est activée !");
    }

    public Sprite GetSprite
    {
        get {return sprite;}
    }

    public string GetNom
    {
        get {return nom;}
    }

    public string GetDescription
    {
        get {return description;}
    }

    public int GetCoup
    {
        get {return coup;}
    }
}