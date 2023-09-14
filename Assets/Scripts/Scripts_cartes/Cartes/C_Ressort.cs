using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(menuName = "Cartes/Ressort", fileName = "Cartes")]
public class C_Ressort : Carte
{
    public override void activer(int id)
    {
        GameObject instantiation = Instantiate(activation, Vector3.zero, Quaternion.identity);
        activationRessort truc = instantiation.GetComponent<activationRessort>();
        // truc.SetId(id);
        // truc.SetCout(GetCoup);
    }
}
