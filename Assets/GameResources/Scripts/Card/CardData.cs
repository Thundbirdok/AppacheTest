using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card")]
public class CardData : ScriptableObject
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

    public CardDataStruct GetDataStruct()
    {
        CardDataStruct data = new CardDataStruct();

        data.Model = model;
        data.Header = header;
        data.Description = description;
        data.Id = id;

        return data;
    }
}
