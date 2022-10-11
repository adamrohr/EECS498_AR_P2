using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private bool isExplore = true;

    public void SwitchModes() {
        if(isExplore) {
            SceneManager.LoadScene("exploration_scene");
        } else {
            SceneManager.LoadScene("interaction_scene");
        }

        isExplore = !isExplore;
    }
}
