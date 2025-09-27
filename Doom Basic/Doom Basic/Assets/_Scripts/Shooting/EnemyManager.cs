using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Se crea un singleton para acceder a la info de la clase
    public static EnemyManager instance;

    // Lista para almacenar los enemigos que se detecten en el rango del arma
    public List<Enemy> enemiesInRange = new List<Enemy>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    // Adiciona o elimina los enemigos de la lista
    public void AddEnemy(Enemy enemy)
    {
        enemiesInRange.Add(enemy);
        enemy.MaterialChange();
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemiesInRange.Remove(enemy);
        enemy.MaterialChange();
    }
}
