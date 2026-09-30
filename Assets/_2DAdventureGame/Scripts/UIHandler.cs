using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance {  get; private set; }

    VisualElement m_HealthBar;
    VisualElement m_WinScreen;
    VisualElement m_LoseScreen;

    public float displayTime = 4.0f;
    private VisualElement m_NonPlayerCharacterDialogue;
    private float m_TimerDisplay;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_HealthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthValue(1.0f);

        m_NonPlayerCharacterDialogue = uiDocument.rootVisualElement.Q<VisualElement>("NPCDialogue");
        m_NonPlayerCharacterDialogue.style.display = DisplayStyle.None;
        m_TimerDisplay = -1.0f;

        m_WinScreen = uiDocument.rootVisualElement.Q<VisualElement>("WinScreenContainer");
        m_LoseScreen = uiDocument.rootVisualElement.Q<VisualElement>("LoseScreenContainer");
    }

    private void Update()
    {
        if (m_TimerDisplay > 0)
        {
            m_TimerDisplay -= Time.deltaTime;

            if (m_TimerDisplay <= 0)
            {
                m_NonPlayerCharacterDialogue.style.display = DisplayStyle.None;
            }
        }
    }

    public void SetHealthValue(float percentage)
    {
        m_HealthBar.style.width = Length.Percent(percentage * 100.0f);
    }

    public void ShowNPCDialogue()
    {
        if (m_NonPlayerCharacterDialogue.style.display == DisplayStyle.None)
        {
            m_NonPlayerCharacterDialogue.style.display = DisplayStyle.Flex;
            m_TimerDisplay = displayTime;
        }
    }

    public void DisplayWinScreen()
    {
        m_WinScreen.style.opacity = 1.0f;
    }

    public void DisplayLoseScreen()
    {
        m_LoseScreen.style.opacity = 1.0f;
    }
}
