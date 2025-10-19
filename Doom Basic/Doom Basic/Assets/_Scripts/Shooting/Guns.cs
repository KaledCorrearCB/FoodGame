
using UnityEngine;


[CreateAssetMenu (fileName = "New Gun", menuName = "Gun/New Gun")]
public class Guns : ScriptableObject
{
    //Informacion mostrable
    [Header("Informaci�n del arma")]
    [SerializeField] private string gunName;
    // Las variables que van a tener las armas
    [SerializeField] private float range;
    [SerializeField] private int verticalRange;
    [SerializeField] private int horizontalRange;
    [SerializeField] private float fireRate;
    [SerializeField] private int damage;
    [SerializeField] public AudioClip sound;

    [Header("Sistema de munici�n")]
    [SerializeField] private int magazineSize;       // Balas por cargador
    [SerializeField] private int maxAmmo;            // Munici�n m�xima total
    [SerializeField] private float reloadTime;       // Tiempo de recarga

    [SerializeField] private AmmoType ammoType;

    // Setters y Getters para acceder y cambiar los valores
    public string GunName => gunName;
    public float Range { get => range; set => range = value; }
    public int VerticalRange { get => verticalRange; set => verticalRange = value; }
    public int HorizontalRange { get => horizontalRange; set => horizontalRange = value; }
    public float FireRate { get => fireRate; set => fireRate = value; }
    public int Damage { get => damage; set => damage = value; }
    public AudioClip Sound { get => sound; set => sound = value; }

    // Nuevos getters para munici�n
    public int MagazineSize => magazineSize;
    public int MaxAmmo => maxAmmo;
    public float ReloadTime => reloadTime;

    public AmmoType AmmoType => ammoType;



}
