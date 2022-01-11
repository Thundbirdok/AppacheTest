using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card")]
public class Card : ScriptableObject
{
    [SerializeField]
    private GameObject model = default;
    public GameObject Model => model;

    [SerializeField]
    private string header = "";
    public string Header => header;

    [SerializeField]
    private string description = "";
    public string Description => description;

    [SerializeField]
    private string id = "";
    public string Id => id;

    public CardData GetDataStruct()
    {
        CardData data = new CardData();

        data.Model = model;
        data.Header = header;
        data.Description = description;
        data.Id = id;

        return data;
    }
}

public struct CardData
{
    public GameObject Model;
    public string Header;
    public string Description;
    public string Id;
}
