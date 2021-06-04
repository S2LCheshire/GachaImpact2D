using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour,IPointerClickHandler//,IPointerExitHandler
{
    [Header("Button")]
    public bool isSelected = false;
    [SerializeField] protected Color32 tabColor = new Color32(200, 214, 229, 255);
    [SerializeField] protected Color32 selectedTabColor = new Color32(131, 149, 167, 255);
    [Header("Text")]
    //[SerializeField] string text;
    [SerializeField] Color32 textColor = new Color32(0, 0, 0, 255);
    [SerializeField] Color32 selectedTextColor = new Color32(255, 255, 255, 255);
    protected Image tabImage;
    TextMeshProUGUI tmpro;
    protected TabController tabController;
    [Header("Dialogue")]
    [SerializeField] string dialogue = "";
    protected bool isStarted = false;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        tabImage = GetComponent<Image>();
        tmpro = GetComponentInChildren<TextMeshProUGUI>(); // NEED TMPRO
        tabController = GetComponentInParent<TabController>();
    }

    protected virtual void Start()
    {
        isStarted = true;
    }

    public virtual void UnselectTab()
    {
        isSelected = false;
        tabImage.color = tabColor; 
        if (tmpro != null)tmpro.color = textColor;
    }
    public virtual void SelectTab()
    {
        isSelected = true;
        tabImage.color = selectedTabColor;
        if (tmpro != null) tmpro.color = selectedTextColor;
        DialogueBox.current.TypeSentence(dialogue);
    }


    public virtual void OnPointerClick(PointerEventData eventData)
    {

        if (!tabController.isKeyActive) StartCoroutine(tabController.ActivateKey());
        tabController.SetActiveTabIndex(this);
    }


}
