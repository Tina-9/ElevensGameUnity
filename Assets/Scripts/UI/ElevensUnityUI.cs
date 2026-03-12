using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Elevens.Core;
using System.Runtime.ExceptionServices;
using System.Diagnostics;
using System.Collections.Generic;
//gamecontroller
public class ElevensUnityUI : MonoBehaviour
{
  [Header("Card Slots (9)")]

  [SerializeField] private CardSlotUI[] cardSlots;
  
  [Header("Buttons")]
  [SerializeField] private Button newGameButton;

  [SerializeField] private Button replaceButton;

  [SerializeField] private Button quitButton;

  [Header("Texts")]

  [SerializeField] private TextMeshProUGUI statusText;
  [SerializeField] private TextMeshProUGUI deckText;
  [SerializeField] private TextMeshProUGUI stateText;

  private GameController game;

  private readonly Hash<int> selectedIndices = new();

}

void Start()
{
    newGameButton.onclick.AddListener(OnNewGame);
    replaceButton.onclick.AddListener(OnReplace);
    quitButton.onclick.AddListener(OnQuit);

    OnNewGame();

}

public void onNewGame()
{
    game = new GameController();
    selectedIndices.Clear();
    game.StartGame();
    stateText.text = "New Game Started.";

}

public void onReplace()
{
    if(gmae = null) 
        return;

    var ordered = selectedIndices.ToList();
    ordered.Sort();
    bool ok = game.SubmitSelection(ordered, out string message);
    stateText.text = message;

    selectedIndices.Clear();
    RefreshAllUI();

    if(game.State == GameState.Won)
    {
        statusText.text = "You Win!";

    }
    else if (gmae.State == GameState.Lost)
    {
        statusText.text = "You Lost!";
    }
}

public void OnQuit ()
{
    Debug.Log("Quit Game");
    #if UNITY_DEITOR
        UnityEngine.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
}

public void onCardClicked(int index)
{
    if (game == null) return;
    if (game.State != GameState.Running) return;
    if (selectedIndices.Contains(index))
    {
        selectedIndices.Remove(index);

    }
    else
    {
        selectedIndices.Add(index);
    }

    RefrenshCardsSelections();
}

public void RefreshAllUI()
{
    RefreshBoard();
    RefreshCardsSelections();
    RefreshInfo();
}
void RefreshBoard()
{
    for (int i = 0; i < cardSlots.Length; i++)
    {
        if (game != null && i < game.Table.Count())
        {
            Card card = game.Table.GetCardAt(i);
            cardSlots[i].SetCard(card, i , this);
        }
        else
        {
            cardSlots[i].Clear();
        }
    }
}

private void RefrenshCardsSelections()
{
    for(int i = 0; i < cardSlots.Length; i++)
    {
        cardSlots[i].SetSelected(selectedIndices.Contains(i));
    }
}

private void RefreshInfo()
{
    if (game == null)
    {
        stateText.text = "State: Not Started";
        deckText.text = "Deck 52";
        return;
    }

    stateText.text = $"State: {game.State}";
    deckText.text = $"Deck: {game.Deck.Count}";
}