using Player;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class FiringModeUI : MonoBehaviour {
    [SerializeField]
    private Image[] modeIconBackground;
    [SerializeField] 
    private Color notSelectedColor;
    private GunControl control;

    private bool init;
    
    private void Start() {
        GameManager.Instance.OnPlayerSpawn += (player) => {
            control = player.GetComponent<GunControl>();
            Assert.AreEqual(modeIconBackground.Length, control.GunCount);
            init = true;
        };
    }

    private void Update() {
        if (!init) return;
        foreach (var background in modeIconBackground) {
            background.color = notSelectedColor;
        }
        modeIconBackground[control.Selected].color = Color.white;
    }
}