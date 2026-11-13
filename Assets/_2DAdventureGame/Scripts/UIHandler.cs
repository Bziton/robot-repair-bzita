using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance { get; private set; }

    public float displayTime = 4.0f;
    private VisualElement m_NonPlayerDialogue;
    private float m_TimerDisplay;
    private VisualElement m_Healthbar;
    private VisualElement m_WinScreen;
    private VisualElement m_LoseScreen;

    private Label m_FixedCounter;
    private int m_TotalEnemies;
    private int m_FixedEnemies;

    private void Awake() { instance = this; }

    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        VisualElement root = uiDocument.rootVisualElement;
        m_Healthbar = root.Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f);
        m_NonPlayerDialogue = root.Q<VisualElement>("NPCDialogue");
        m_NonPlayerDialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;
        m_LoseScreen = root.Q<VisualElement>("LoseScreenContainer");
        m_WinScreen = root.Q<VisualElement>("WinScreenContainer");
        m_FixedCounter = root.Q<Label>("FixedCounter");
        UpdateCounter();
    }

    public void SetHealthValue(float percentage)
    {
        m_Healthbar.style.width = Length.Percent(100 * percentage);
    }

    private void Update()
    {
        if (m_TimerDisplay > 0)
        {
            m_TimerDisplay -= Time.deltaTime;
            if (m_TimerDisplay < 0)
                m_NonPlayerDialogue.style.display = DisplayStyle.None;
        }
    }

    public void DisplayDialogue()
    {
        m_NonPlayerDialogue.style.display = DisplayStyle.Flex;
        m_TimerDisplay = displayTime;
    }

    public void DisplayWinScreen() { m_WinScreen.style.opacity = 1.0f; }
    public void DisplayLoseScreen() { m_LoseScreen.style.opacity = 1.0f; }

    public void SetTotalEnemies(int total) { m_TotalEnemies = total; UpdateCounter(); }
    public void EnemyFixed() { m_FixedEnemies++; UpdateCounter(); }

    private void UpdateCounter()
    {
        if (m_FixedCounter == null) return;
        m_FixedCounter.text = "Полагоджено " + m_FixedEnemies + " / " + m_TotalEnemies;
    }
}