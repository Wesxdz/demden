using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reincarnate : MonoBehaviour
{
    public void Respawn()
    {
        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);
    }
}
