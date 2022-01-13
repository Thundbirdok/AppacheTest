using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Заполняет данные из структуры
/// </summary>
public class CardController : MonoBehaviour
{
    [SerializeField]
    private Text header = default;

    [SerializeField]
    private Text description = default;

    [SerializeField]
    private GameObject lockObject = default;

    private CardData data = default;
    public CardData Data
    {
        get
        {
            return data;
        }

        set
        {
            if (value == false)
            {
                return;
            }

            data = value;

            Init();
        }
    }

    private bool isUnlocked = false;
    public bool IsUnlocked
    {
        get
        {
            return isUnlocked;
        }

        set
        {
            isUnlocked = value;

            PlayerPrefs.SetInt(data.Id + IS_UNLOCKED_PREFS_KEY, isUnlocked ? 1 : 0);

            lockObject.SetActive(isUnlocked);
        }
    }

    private const string IS_UNLOCKED_PREFS_KEY = "IsUnlocked";

    private void Init()
    {
        if (data == false)
        {
            return;
        }

        header.text = data.Header;
        description.text = data.Description;

        //Set model

        IsUnlocked = PlayerPrefs.GetInt(data.Id + IS_UNLOCKED_PREFS_KEY, 0) > 0;
    }
}
