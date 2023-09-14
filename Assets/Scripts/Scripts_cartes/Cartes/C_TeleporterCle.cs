using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(menuName = "Cartes/TeleporteCle", fileName = "Cartes")]
public class C_TeleporterCle : Carte
{
    public override void activer(int id)
    {
        GameObject instantiation = Instantiate(activation, Vector3.zero, Quaternion.identity);
        activationTeleporterCle truc = instantiation.GetComponent<activationTeleporterCle>();
        truc.SetId(id);
        truc.SetCout(GetCoup);
    }
}
