using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;

    public int health { get => this.currentHealth; }

    public bool isDamaged { get => this.currentHealth != this.maxHealth; }

    public System.Action<Vector3> OnShootReceived;

    private int currentHealth;

    private float damageIndicator;

    private Renderer[] renderers;

    /// ================================
    void Awake()
    {
        this.currentHealth = maxHealth;

        this.renderers = GetComponentsInChildren<Renderer>();
    }

    /// ================================
    void SetDamageGlow()
    {
        foreach (var renderer in this.renderers)
        {
            renderer.material.SetColor("_EmissionColor", Color.Lerp(Color.black, Color.red, this.damageIndicator));
        }
    }

    /// ================================
    private void Update()
    {
        if (this.damageIndicator > 0)
        {
            this.SetDamageGlow();

            this.damageIndicator -= Time.deltaTime * 2;
        }
    }

    /// ================================
    public void Restore(int amount)
    {
        this.currentHealth += amount;
        this.currentHealth = Mathf.Clamp(this.currentHealth, 0, this.maxHealth);
    }

    /// ================================
    public void Damage(int amount, Vector3 direction)
    {
        if (this.OnShootReceived != null)
        {
            this.OnShootReceived(direction);
        }

        this.currentHealth -= amount;
        this.currentHealth = Mathf.Clamp(this.currentHealth, 0, this.maxHealth);

        this.damageIndicator = 1;

        if (this.currentHealth == 0)
        {
            Destroy(this.gameObject);
        }
    }
}