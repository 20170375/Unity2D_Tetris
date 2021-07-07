using UnityEngine;
using TMPro;

public class ScoreTextViewer : MonoBehaviour
{
    [SerializeField]
    private GameController  gameController;
    [SerializeField]
    private TextMeshProUGUI textScore;

    private void Update()
    {
        textScore.text = "Score " + gameController.Score;
    }
}
