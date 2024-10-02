using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryStateUI : MonoBehaviour
{
    private const string AnimatorShow = "PopUp";

    [SerializeField] private Image BackGround;
    [SerializeField] private Image CheckIcon;
    [SerializeField] private Image XIcon;
    [SerializeField] private TextMeshProUGUI Text;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess; ;
        DeliveryManager.Instance.OnRecipeFail += DeliveryManager_OnRecipeFail;
        Hide();
    }

    private void DeliveryManager_OnRecipeFail(object sender, System.EventArgs e) {
        BackGround.color = Color.red;
        CheckIcon.gameObject.SetActive(false);
        XIcon.gameObject.SetActive(true);
        Text.text = "Delivery\nFailed";
        animator.SetTrigger(AnimatorShow);
        Show();
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e) {
        BackGround.color = Color.green;
        CheckIcon.gameObject.SetActive(true);
        XIcon.gameObject.SetActive(false);
        Text.text = "Delivery\nSuccess";
        animator.SetTrigger(AnimatorShow);
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
}
