using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button Play;
    [SerializeField] private Button Quit;

    private void Awake() {
        Play.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.GameScene);
        });
        Quit.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
