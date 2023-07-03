using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ExpBarUI : MonoBehaviour
{
    [SerializeField]
    private float pastExp;
    [SerializeField]
    private float expAnimationSpeed;
    [SerializeField]
    private Image progressImage;
    [SerializeField]
    private Gradient colorGradient;

    private Coroutine animationCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        pastExp = PlayerStats.instance.GetCurrExp();
        progressImage.color = colorGradient.Evaluate(0);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateExpBar();
    }

    // Checks if there are changes to exp and update the UI accordingly
    private void UpdateExpBar() {
        // Check if there is a need to update exp bar
        if (PlayerStats.instance.GetCurrExp() != pastExp) {
            // Set new past exp so there won't be multiple attempts to update the fill amount
            pastExp = PlayerStats.instance.GetCurrExp();

            // Check if the fill amount is already the current percentage
            if (PlayerStats.instance.GetCurrExpPercentClamped() != progressImage.fillAmount) {
                if (animationCoroutine != null) {
                    StopCoroutine(animationCoroutine);
                }
                animationCoroutine = StartCoroutine(AnimateExpUpdate(PlayerStats.instance.GetCurrExpPercentClamped(), expAnimationSpeed, 0));
            }
        }
    }

    public void SetMax() {
        progressImage.fillAmount = 1;
        progressImage.color = colorGradient.Evaluate(0);
    }

    public void SetZero() {
        Debug.Log("AAA");
        progressImage.fillAmount = 0;
        progressImage.color = colorGradient.Evaluate(1);
        pastExp = PlayerStats.instance.GetCurrExp();
        animationCoroutine = StartCoroutine(AnimateExpUpdate(PlayerStats.instance.GetCurrExpPercentClamped(), expAnimationSpeed, 0));

    }

    // Animate the process of updating exp
    private IEnumerator AnimateExpUpdate(float _progress, float _speed, float _time) {
        float initialProgress = progressImage.fillAmount;

        while (_time < 1) {
            progressImage.fillAmount = Mathf.Lerp(initialProgress, _progress, _time);
            _time += Time.deltaTime * _speed;

            progressImage.color = colorGradient.Evaluate(1 - progressImage.fillAmount);

            yield return null;
        }

        progressImage.fillAmount = _progress;
        progressImage.color = colorGradient.Evaluate(1 - progressImage.fillAmount);
    }
}
