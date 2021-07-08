using DefaultNamespace.IGameStates;
using UnityEngine;

public class Clouds : MonoBehaviour,IGamePause,IGameResume
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float changeDirectionSeconds;
    private float time;
    private bool pause;

    private void Start()
    {
        time = changeDirectionSeconds;
    }

    private void Update()
    {
        if (pause)
        {
            return;
        }
        if (Time.time >= changeDirectionSeconds)
        {
            moveSpeed = -moveSpeed;
            changeDirectionSeconds += time;
        }

        transform.Translate((moveSpeed * Time.deltaTime * Vector2.right));
    }

    public void PauseGame()
    {
        pause = true;
    }

    public void ResumeGame()
    {
        pause = false;
    }
}
