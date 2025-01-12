using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private HealthBarScript healthBarScript;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBarScript)
        {

            healthBarScript.SetMaxValue(maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (healthBarScript)
        {
            healthBarScript.SetSlider(currentHealth);
        }
        Debug.Log($"{gameObject.name} took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died!");
        Destroy(gameObject); // Puedes reemplazar esto con lógica de muerte personalizada.
    }
}
