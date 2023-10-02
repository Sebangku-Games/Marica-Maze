using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Pipe> connectedPipes = new List<Pipe>();

    // Panggil metode ini saat pipa terhubung.
    public void PipeConnected(Pipe pipe)
    {
        if (!connectedPipes.Contains(pipe))
        {
            connectedPipes.Add(pipe);
            CheckWinCondition();
        }
    }

    // Panggil metode ini saat pipa terputus.
    public void PipeDisconnected(Pipe pipe)
    {
        if (connectedPipes.Contains(pipe))
        {
            connectedPipes.Remove(pipe);
        }
    }

    // Metode untuk memeriksa apakah pemain memenangkan permainan.
    private void CheckWinCondition()
    {
        // Tambahkan logika di sini untuk memeriksa apakah pemain menang.
        // Contohnya, jika semua pipa terhubung, maka pemain menang.
        bool allPipesConnected = connectedPipes.Count == 2;

        if (allPipesConnected)
        {
            // Tambahkan tindakan yang perlu diambil saat pemain menang.
            Debug.Log("Pemain menang!");
        }
    }
}
