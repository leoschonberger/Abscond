using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public TextMeshPro healthText;

    public int startingHealth = 100;

    public GameObject parentGameObject;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        if (healthText != null)
        {
            healthText.text = "HP: "+startingHealth;
        }
    }

    public void TakeDamage(int damageToTake)
    {
        currentHealth -= damageToTake;
        if (currentHealth <= 0)
        {
            Die();
        }

        if (healthText != null)
        {
            Debug.Log("lkjhdsfa");
            healthText.text = "HP: " + currentHealth;
        }

    }

    public virtual void Die() //Make this a virtual method in case we want to do more later. Public in case we need one shot kill for something;
    {
        parentGameObject.SetActive(false);
    }
}
