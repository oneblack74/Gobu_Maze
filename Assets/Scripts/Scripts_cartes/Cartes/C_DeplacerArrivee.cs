using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(menuName = "Cartes/DeplacerArrivee", fileName = "Cartes")]
public class C_DeplacerArrivee : Carte
{
    public override void activer(int id)
    {
        GameObject instantiation = Instantiate(activation, Vector3.zero, Quaternion.identity);
        activationDeplacerArrivee truc = instantiation.GetComponent<activationDeplacerArrivee>();
        truc.SetId(id);
        truc.SetCout(GetCoup);
    }
}
