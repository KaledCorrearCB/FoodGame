using UnityEngine;

public class PlayerArmor : MonoBehaviour
{
    [Header("Estado de la armadura")]
    public bool HasArmor = false;

    /// <summary>
    /// Activa la armadura (por ejemplo, cuando el jugador la recoge).
    /// </summary>
    public void EquipArmor()
    {
        HasArmor = true;
        Debug.Log("🛡 Armadura equipada. El ácido ya no afecta al jugador.");
    }

    /// <summary>
    /// Remueve la armadura (si quieres que pueda perderla).
    /// </summary>
    public void RemoveArmor()
    {
        HasArmor = false;
        Debug.Log(" Armadura perdida. El ácido vuelve a ser mortal.");
    }
}