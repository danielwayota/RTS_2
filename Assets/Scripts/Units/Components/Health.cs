using UnityEngine;

public class Health : MonoBehaviour
{
	public int maxHealth;

	public int health {
		get { return this.currentHealth; }
		set {
			this.currentHealth = value;

			this.currentHealth = Mathf.Clamp(this.currentHealth, 0, this.maxHealth);

			if (this.currentHealth == 0) {
				Destroy(this.gameObject);
			}
		}
	}

	private int currentHealth;

	// ================================
	void Awake()
	{
		this.currentHealth = maxHealth;	
	}
}