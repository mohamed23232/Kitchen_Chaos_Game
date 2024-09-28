using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private const string VOLUME_KEY = "SoundEffectsVolume";
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AllSounds allSounds;

    private float volume = 1f;

    private void Awake() {
        Instance = this;

        volume = PlayerPrefs.GetFloat(VOLUME_KEY, 1f);
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeSuccess += Instance_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFail += Instance_OnRecipeFail;
        CuttingCounter.OnAnyCuttingCounter += CuttingCounter_OnAnyCuttingCounter;
        PlayerScript.Instance.OnPickUp += Instance_OnPickUp;
        BaseCounter.OnDrop += BaseCounter_OnDrop;
        TrashCounter.OnTrash += TrashCounter_OnTrash; ;
    }

    private void TrashCounter_OnTrash(object sender, System.EventArgs e) {
        TrashCounter trashCounter = (TrashCounter)sender;
        playSound(allSounds.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnDrop(object sender, System.EventArgs e) {
        BaseCounter baseCounter = (BaseCounter)sender;
        playSound(allSounds.drop_object, baseCounter.transform.position);
    }

    private void Instance_OnPickUp(object sender, System.EventArgs e) {
        playSound(allSounds.pickUp_object, PlayerScript.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCuttingCounter(object sender, System.EventArgs e) {
        CuttingCounter cuttingCounter = (CuttingCounter)sender;
        playSound(allSounds.chop, cuttingCounter.transform.position);
    }

    private void Instance_OnRecipeFail(object sender, System.EventArgs e) {
        playSound(allSounds.delivery_fail, DeliveryCounter.Instance.transform.position);
    }

    private void Instance_OnRecipeSuccess(object sender, System.EventArgs e) {
        playSound(allSounds.delivery_success, DeliveryCounter.Instance.transform.position);
    }

    private void playSound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f) {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volumeMultiplier * volume);
    }
    public void playSoundFootsteps(Vector3 position, float volumeMultiplier = 1f) {
        AudioSource.PlayClipAtPoint(allSounds.footSteps[Random.Range(0, allSounds.footSteps.Length)], position, volumeMultiplier * volume);
    }

    public void ChangeVolume() {
        volume += 0.1f;
        if (volume > 1f) {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(VOLUME_KEY, volume);
        PlayerPrefs.Save();
    }
    public float GetVolume() {
        return volume;
    }
}
