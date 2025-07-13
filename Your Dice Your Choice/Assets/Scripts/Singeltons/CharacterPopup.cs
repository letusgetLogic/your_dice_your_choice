using TMPro;
using UnityEngine;

public class CharacterPopup : MonoBehaviour
{
    public static CharacterPopup Instance { get; private set; }

    [SerializeField] private RectTransform   _canvasRectTransform;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _maxHp;
    [SerializeField] private TextMeshProUGUI _currentHp;
    [SerializeField] private TextMeshProUGUI _ap;
    [SerializeField] private TextMeshProUGUI _dp;
    [SerializeField] private Vector2         _distance;

    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        Instance = this;
    }

    /// <summary>
    /// Transfers the values to the info panel.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="maxHp"></param>
    /// <param name="currentHp"></param>
    /// <param name="ap"></param>
    /// <param name="dp"></param>
    public void SetData(string name, Color color, float maxHp, float currentHp, float ap, float dp)
    {
        _name.text = name;
        _name.color = color;
        _maxHp.text = maxHp.ToString();
        _currentHp.text = currentHp.ToString();
        _ap.text = ap.ToString();
        _dp.text = dp.ToString();
    }

    /// <summary>
    /// Sets the values default.
    /// </summary>
    public void SetDefault()
    {
        _name.text = "";
        _name.color = Color.white;
        _maxHp.text = "";
        _currentHp.text = "";
        _ap.text = "";
        _dp.text = "";
    }

    /// <summary>
    /// Sets the position of the info panel.
    /// </summary>
    public void SetPosition(GameObject characterObject)
    {
        var characterPos = characterObject.transform.position;

        var pos = _canvasRectTransform.InverseTransformPoint(characterPos);
       
        gameObject.GetComponent<RectTransform>().localPosition = pos + Distance(pos);
    }

    /// <summary>
    /// Return the distance to the character.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private Vector3 Distance(Vector3 pos)
    {
        Vector3 distance = new();

        distance.x = _distance.x * Direction(pos).x;
        distance.y = _distance.y * Direction(pos).y;

        return distance;
    }
    
    /// <summary>
    /// Return the direction of the distance.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private Vector3 Direction(Vector3 pos)
    {
        Vector3 dir = new();

        switch (pos.x)
        {
            case <= 0: dir.x = 1; break;
            case > 0: dir.x = -1; break;
        }

        switch (pos.y)
        {
            case <= 0: dir.y = 1; break;
            case > 0: dir.y = -1; break;
        }

        return dir;
    }
}
