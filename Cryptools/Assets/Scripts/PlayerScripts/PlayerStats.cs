using Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public HealthBar healthBar;

    [SerializeField]
    public GameObject gameoverUI;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;   
        healthBar.SetMaxHealth(currentHealth);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        TakeDamage(20);
    //    }
    //}
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
            Die();
        }

        healthBar.SetHealth(currentHealth);
    }

    public void AddHealth(int health)
    {
        currentHealth += health;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.SetHealth(currentHealth);
    }

    void Die()
    {
        gameoverUI.SetActive(true);
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<InventoryController>().enabled = false;
    }



}
