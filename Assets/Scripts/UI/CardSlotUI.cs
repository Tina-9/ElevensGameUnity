using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Elevens.Core

public class UI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI cardText;

    [SerializeField] private Image background;

    private int slotIndex = -1;
    private ElevenUnityUI owner;

    public void SetCard(CardSlotUI card, int index, ElevenUnityUI uiOwner)
    {
        slotIndex = index;
        owner = uniOwner;

        gameObject.SetActive(true);
        cardText.text = card.ToString();
        button.interactable = true;

        button.onclick.RemoveAllListeners();
        button.onclick.AddListener(()=>owner.onCardClicked(slotIndex));

        SetSelected(false);
    } 

    public void SetSelected(bool selected)
    {
        if(background != null)
        {
            background.color = selected ? new color (if, 0.9f, 0.3f, 1f) : Color.while;

        }
    }

    public void Clear()
    {
        solotIndex = -1;
        owner = null;
        cardText.text = "";
        button.onclick.RemoveAllListeners();
        button.interactable = false;
        SetSelected(false);
        gameObject.SetActive(false);
    }
}
