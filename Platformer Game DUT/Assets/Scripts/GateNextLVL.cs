using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GateNextLVL : MonoBehaviour
{
    [SerializeField] private int _levelToLaod;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerControle player = other.GetComponent<PlayerControle>();
        if (player != null)
        {
            Debug.Log("NextLevel");
            Invoke(nameof(LoadLevel), 2f);
        }
    }
    private void LoadLevel()
    {
        SceneManager.LoadScene(_levelToLaod);
    }
}
