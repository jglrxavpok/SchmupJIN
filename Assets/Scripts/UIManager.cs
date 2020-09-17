using System;
using Player;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private GameObject GameOverPrefab;
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Slider energySlider;

    private static UIManager instance;

    public static UIManager Instance => instance;

    private PlayerAvatar playerAvatar;
    private bool gameOver;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void OnPlayerDeath() {
        playerAvatar.OnDeathEvent -= OnPlayerDeath;
        ShowGameOverScreen();
    }

    private void ShowGameOverScreen() {
        gameOver = true;
        Instantiate(GameOverPrefab, transform);
    }

    private void Update() {
        if (gameOver) return;
        // lazy-init to ensure player has been instantiated, because Start() order between UIManager and GameManager is not known
        if (playerAvatar == null) {
            GameObject player = GameObject.FindWithTag("Player");
            playerAvatar = player.GetComponent<PlayerAvatar>();
            playerAvatar.OnDeathEvent += OnPlayerDeath;
        }

        Assert.IsNotNull(playerAvatar);

        UpdateHealth(playerAvatar.CurrentHealth, playerAvatar.MaxHealth);
        UpdateEnergy(playerAvatar.CurrentEnergy, playerAvatar.MaxEnergy);
    }

    private void UpdateHealth(float newValue, float maxHealth) {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = newValue;
    }

    private void UpdateEnergy(float newValue, float maxEnergy) {
        energySlider.maxValue = maxEnergy;
        energySlider.value = newValue;
    }
}