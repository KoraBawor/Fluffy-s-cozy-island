using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    [Header("Money")]
    public int money;

    [Header("UI")]
    public Text moneyText;

    private const string SAVE_KEY = "MONEY_SAVE";

    void Awake()
    {
        
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadMoney();
    }

    void Start()
    {
        FindMoneyText();
        UpdateUI();
    }

    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindMoneyText();
        UpdateUI();
    }

    
    public void AddMoneyFromCat(int amount, GameObject source)
    {
        if (source == null || !source.CompareTag("Cat")) return;

        money += amount;

        SaveMoney();
        UpdateUI();
    }

    public bool SpendMoney(int amount)
    {
        if (money < amount) return false;

        money -= amount;

        SaveMoney();
        UpdateUI();
        return true;
    }

    
    void FindMoneyText()
    {
        GameObject obj = GameObject.Find("MoneyText");

        if (obj != null)
            moneyText = obj.GetComponent<Text>();
    }

    void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = money + "$";
    }

    
    void SaveMoney()
    {
        PlayerPrefs.SetInt(SAVE_KEY, money);
        PlayerPrefs.Save();
    }

    void LoadMoney()
    {
        money = PlayerPrefs.GetInt(SAVE_KEY, 0);
    }
}