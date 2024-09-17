using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private PlayerScript player;
    private float footstepTimer;
    private float footstepmaxTime = 0.1f;
    
    private void Awake() {
        player = GetComponent<PlayerScript>();
    }

    private void Update() {
        footstepTimer -= Time.deltaTime;
        if(footstepTimer < 0f) {
            footstepTimer = footstepmaxTime;
            if (player.IsWalking()) {
                float volume = 1f;
                SoundManager.Instance.playSoundFootsteps(player.transform.position,volume);
            }
        }
    }
}
