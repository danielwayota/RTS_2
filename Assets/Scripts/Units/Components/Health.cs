using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;

    public int health
    {
        get { return this.currentHealth; }
        set
        {
            // Damage
            if (value < this.currentHealth)
            {
                this.damageIndicator = 1;
            }

            this.currentHealth = value;
            this.currentHealth = Mathf.Clamp(this.currentHealth, 0, this.maxHealth);

            if (this.currentHealth == 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public bool isDamaged
    {
        get { return this.currentHealth != this.maxHealth; }
    }

    private int currentHealth;

    private float damageIndicator;

    private Renderer[] renderers;

    // ================================
    void Awake()
    {
        this.currentHealth = maxHealth;

        this.renderers = GetComponentsInChildren<Renderer>();
    }

    void SetDamageGlow()
    {
        foreach (var renderer in this.renderers)
        {
            renderer.material.SetColor("_EmissionColor", Color.Lerp(Color.black, Color.red, this.damageIndicator));
        }
    }

    private void Update()
    {
        if (this.damageIndicator > 0)
        {
            this.SetDamageGlow();

            this.damageIndicator -= Time.deltaTime * 2;
        }
    }
}