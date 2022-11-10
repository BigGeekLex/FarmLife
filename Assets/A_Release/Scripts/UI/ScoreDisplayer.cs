using MSFD;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public class ScoreDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private UnityEvent scoreRefreshed;
    private IScoreProvidable scoreProvidable => ServiceLocator.Get<IScoreProvidable>();

    private void Start()
    {
        scoreProvidable.GetScoreObs().Subscribe((x) => RefreshScore(x)).AddTo(this);
    }

    private void RefreshScore(int score)
    {
        scoreText.text = "Scores: " + score.ToString();
        scoreRefreshed.Invoke();
    }
}