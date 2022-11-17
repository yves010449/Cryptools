using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureStats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        this.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Death");

        Destroy(gameObject);
    }
}
