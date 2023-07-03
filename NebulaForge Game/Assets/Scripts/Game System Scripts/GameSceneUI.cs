using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneUI : MonoBehaviour
{
    [SerializeField]
    private float pastEXP;
    [SerializeField]
    private float EXPAnimationSpeed;
    [SerializeField]
    private Image progressImage;

    private Coroutine animationCoroutine;

    // Start is called before the first frame update
    // void Start()
    // {
    //     pastEXP = PlayerStats.instance.GetCurrEXP();
    //     progressImage.color = colorGradient.Evaluate(0);
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     UpdateHealthBar();
    // }

    // // Checks if there are changes to health and update the UI accordingly
    // private void UpdateHealthBar() {
    //     // Check if there is a need to update health bar
    //     if (PlayerStats.instance.GetCurrHealth() != pastHealth) {
    //         // Set new past health so there won't be multiple attempts to update the fill amount
    //         pastHealth = PlayerStats.instance.GetCurrHealth();

    //         // Check if the fill amount is already the current percentage
    //         if (PlayerStats.instance.GetCurrHealthPercentClamped() != progressImage.fillAmount) {
    //             if (animationCoroutine != null) {
    //                 StopCoroutine(animationCoroutine);
    //             }
    //             animationCoroutine = StartCoroutine(AnimateHealthUpdate(PlayerStats.instance.GetCurrHealthPercentClamped(), healthAnimationSpeed, 0));
    //         }
    //     }
    // }

    // // Animate the process of updating health
    // private IEnumerator AnimateHealthUpdate(float _progress, float _speed, float _time) {
    //     float initialProgress = progressImage.fillAmount;

    //     while (_time < 1) {
    //         progressImage.fillAmount = Mathf.Lerp(initialProgress, _progress, _time);
    //         _time += Time.deltaTime * _speed;

    //         progressImage.color = colorGradient.Evaluate(1 - progressImage.fillAmount);

    //         yield return null;
    //     }

    //     progressImage.fillAmount = _progress;
    //     progressImage.color = colorGradient.Evaluate(1 - progressImage.fillAmount);
    // }
}
