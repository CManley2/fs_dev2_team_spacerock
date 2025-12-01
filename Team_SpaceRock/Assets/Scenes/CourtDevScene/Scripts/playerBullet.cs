using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 5f;
    public GameObject player;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        IDamage dmg = other.GetComponent<IDamage>();

        if (dmg != null)
        {

            dmg.takeDamage(1);

            Destroy(gameObject);

        }
    }
}
