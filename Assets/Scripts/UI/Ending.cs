using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class Ending : MonoBehaviour
{
    [Header("Video Components")]
    [SerializeField] private VideoPlayer player;
    [Header("Level Components")]
    [SerializeField] private PlayLevel level;

    private void Start()
    {
        StartCoroutine(End());
    }

    private IEnumerator End()
    {
        yield return new WaitForSeconds(2f);
        while (player.isPlaying)
        {
            yield return null;
        }
        level.LoadScene();
    }
}
