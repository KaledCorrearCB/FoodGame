using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerArmor armor = other.GetComponent<PlayerArmor>();
            if (armor != null)
            {
                armor.EquipArmor();
                Destroy(gameObject); // elimina el pickup
            }
        }
    }
}
