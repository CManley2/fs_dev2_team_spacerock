using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour, IDamage
{
    [SerializeField] int health;
    [SerializeField] float breakForce;

    SphereCollider  mainAsteroidCollider;

    public void takeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            BreakApart();
            GameManager.instance.UpdateScore(1);
            SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.explosionSound);
        }
    }

    void BreakApart()
    {
        foreach (Rigidbody rb in gameObject.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = false;
            Vector3 force = (rb.transform.position - transform.position).normalized * breakForce;
            rb.AddForce(force);
        }
        StartCoroutine(DestructionDelay());
        mainAsteroidCollider.enabled = false;
    }

    IEnumerator DestructionDelay()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        mainAsteroidCollider = GetComponent<SphereCollider>();
        foreach (Rigidbody rb in gameObject.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }
    }
}
