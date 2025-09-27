
using UnityEngine;


[CreateAssetMenu (fileName = "New Gun", menuName = "Gun/New Gun")]
public class Guns : ScriptableObject
{
    // Las variables que van a tener las armas
    [SerializeField] private float range;
    [SerializeField] private int verticalRange;
    [SerializeField] private int horizontalRange;
    [SerializeField] private float fireRate;
    [SerializeField] private int damage;
    [SerializeField] public AudioClip sound;

    // Setters y Getters para acceder y cambiar los valores
    public float Range { get => range; set => range = value; }
    public int VerticalRange { get => verticalRange; set => verticalRange = value; }
    public int HorizontalRange { get => horizontalRange; set => horizontalRange = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public int Damage { get => damage; set => damage = value; }
    public AudioClip Sound { get => sound; set => sound = value; }

}
