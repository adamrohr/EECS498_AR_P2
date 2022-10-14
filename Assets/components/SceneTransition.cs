using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    private static bool isExplore = true;

    public void SwitchModes() {
        if(!isExplore) {
            SceneManager.LoadScene("exploration_scene", LoadSceneMode.Single);
        } else {
            SceneManager.LoadScene("interaction_scene", LoadSceneMode.Single);
        }

        isExplore = !isExplore;
    }
}
