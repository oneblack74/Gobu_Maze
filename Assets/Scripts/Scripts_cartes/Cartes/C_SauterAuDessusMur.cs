using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(menuName = "Cartes/SauterAuDessusMur", fileName = "Cartes")]
public class C_SauterAuDessusMur : Carte
{
    public override void activer(int id)
    {
        GameObject instantiation = Instantiate(activation, Vector3.zero, Quaternion.identity);
        activationSauterAuDessusMur truc = instantiation.GetComponent<activationSauterAuDessusMur>();
        // truc.SetId(id);
        // truc.SetCout(GetCoup);
    }
}
