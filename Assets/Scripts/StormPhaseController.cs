using System.Collections;
using UnityEngine;

public class StormPhaseController : MonoBehaviour
{
    [Header("Cloud groups (parents)")]
    [SerializeField] private RectTransform cloudGroupLeft;
    [SerializeField] private RectTransform cloudGroupRight;

    [Header("Start positions (anchoredPosition)")]
    [SerializeField] private Vector2 leftStart = new Vector2(-1200f, 0f);
    [SerializeField] private Vector2 rightStart = new Vector2(1200f, 0f);

    [Header("Center positions (anchoredPosition)")]
    [SerializeField] private Vector2 leftCenter = new Vector2(-150f, 0f);
    [SerializeField] private Vector2 rightCenter = new Vector2(150f, 0f);

    [Header("Timing")]
    [SerializeField] private float moveDuration = 1.5f;
    [SerializeField] private float stormDuration = 6f;

    [Header("Rain + Mice")]
    [SerializeField] private MiceSpawner miceSpawner;

    private Coroutine routine;

    private void OnEnable()
    {
        if (GameManager.Instance == null || GameManager.Instance.currentGameState != GameState.Storm)
            return;
        if (cloudGroupLeft != null) cloudGroupLeft.anchoredPosition = leftStart;
        if (cloudGroupRight != null) cloudGroupRight.anchoredPosition = rightStart;

        if (miceSpawner != null) miceSpawner.enabled = false;

        PlayStorm();
    }

    public void PlayStorm()
    {
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(RunStorm());
    }

    private void OnDisable()
    {
        if (routine != null) StopCoroutine(routine);
        routine = null;
    }

    private IEnumerator RunStorm()
    {
        yield return MoveGroupsToCenter();

        if (miceSpawner != null) miceSpawner.enabled = true;

        yield return new WaitForSeconds(stormDuration);

        if (miceSpawner != null) miceSpawner.enabled = false;

        GameManager.Instance?.EndStormAndStartLastWave();
    }

    private IEnumerator MoveGroupsToCenter()
    {
        if (cloudGroupLeft == null || cloudGroupRight == null) yield break;

        Vector2 l0 = cloudGroupLeft.anchoredPosition;
        Vector2 r0 = cloudGroupRight.anchoredPosition;

        float t = 0f;
        while (t < moveDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Clamp01(t / moveDuration);
            float eased = Mathf.SmoothStep(0f, 1f, a);

            cloudGroupLeft.anchoredPosition = Vector2.Lerp(l0, leftCenter, eased);
            cloudGroupRight.anchoredPosition = Vector2.Lerp(r0, rightCenter, eased);

            yield return null;
        }

        cloudGroupLeft.anchoredPosition = leftCenter;
        cloudGroupRight.anchoredPosition = rightCenter;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
