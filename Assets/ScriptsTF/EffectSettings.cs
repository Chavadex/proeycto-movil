using UnityEngine;

[CreateAssetMenu(fileName = "ExplosionSettings", menuName = "ScriptableObjects/ExplosionSettings")]
public class EffectSettings : FlyweightSettings
{
    // --- ESTO ES LO QUE FALTABA ---
    public override Flyweight Create()
    {
        // 1. Instanciamos el prefab (variable que viene de la clase padre)
        GameObject go = Instantiate(prefab);

        // 2. Le ponemos el nombre original para mantener el orden
        go.name = prefab.name;

        // 3. Obtenemos el componente Flyweight (que en este caso será tu script 'ExplosionFlyweight')
        Flyweight flyweight = go.GetComponent<Flyweight>();

        // (Seguridad) Por si se te olvidó ponerle el script al prefab
        if (flyweight == null)
        {
            Debug.LogError($"¡OJO! El prefab '{prefab.name}' no tiene el script ExplosionFlyweight o Flyweight.");
        }

        return flyweight;
    }

    public override void OnGet(Flyweight fw)
    {
        fw.gameObject.SetActive(true);
        // Reiniciar particula si es necesario
        if (fw.TryGetComponent<ParticleSystem>(out var ps))
        {
            ps.Clear();
            ps.Play();
        }
    }

    public override void OnRealise(Flyweight fw)
    {
        fw.gameObject.SetActive(false);
    }
}