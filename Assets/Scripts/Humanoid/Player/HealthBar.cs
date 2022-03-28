using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Image healthBar;
    Player player;
    public void Start()
    {
        healthBar = GetComponent<Image>();
        player = FindObjectOfType<Player>();

    }
    private void Update()
    {
        healthBar.fillAmount = player.health / player.maxHealth;
    }
}
