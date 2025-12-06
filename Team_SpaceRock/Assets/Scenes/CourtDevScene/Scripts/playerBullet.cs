using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 5f;
    public int damage;

    public GameObject player;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void Initialize(int dmg)
    {
        damage = dmg;
    }

    void OnTriggerEnter(Collider other)
    {
        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null)
        {

            dmg.takeDamage(damage);
            Destroy(gameObject);

        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Asteroid"))
        {

            Destroy(gameObject);

        }
    }
}
