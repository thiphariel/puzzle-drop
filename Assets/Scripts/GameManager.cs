using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Draggable[] draggables;
    public GameObject win;
    public GameObject menu;

    private List<Vector2> m_positions = new List<Vector2> {
        new Vector2(-4, -2.5f),
        new Vector2(0, -2.5f),
        new Vector2(4, -2.5f)
    };

    private Vector2 GetRandomPosition()
    {
        Vector2 position = m_positions[Random.Range(0, m_positions.Count)];
        m_positions.Remove(position);
        return position;
    }

    private float counter = 3f;

    // Start is called before the first frame update
    void Start()
    {
        win.SetActive(false);

        for (int i = 0; i < draggables.Length; i++)
        {
            draggables[i].transform.position = GetRandomPosition();
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool hasWin = true;

        foreach (Draggable draggable in draggables)
        {
            if (!draggable.locked)
            {
                hasWin = false;
                continue;
            }
        }

        if (hasWin)
        {
            win.SetActive(true);

            if (counter >= 0f)
            {
                counter -= Time.deltaTime;

                if (counter <= 0f)
                {
                    Reset();
                }
            }
        }
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    private void Reset()
    {
        win.SetActive(false);
        counter = 3f;

        m_positions = new List<Vector2> {
            new Vector2(-4, -2.5f),
            new Vector2(0, -2.5f),
            new Vector2(4, -2.5f)
        };

        foreach (Draggable draggable in draggables)
        {
            draggable.locked = false;
            Vector2 position = GetRandomPosition();
            draggable.initialPosition = position;
            draggable.transform.position = position;
        }
    }
}
