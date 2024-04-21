using System;
using recipes;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    public IRecipe recipe;
    public TextMeshProUGUI orderNameText;
    public Canvas itemNameCanvas;
    [FormerlySerializedAs("ItemSprite")]
    public SpriteRenderer itemSprite;

    private float timeLeft;
    private OrderManager orderManager;

    public int remainingPoints
    {
        get
        {
            return (int) (timeLeft / recipe.timeLimit * recipe.points);
        }
    }

    private void Start()
    {
        timeLeft = recipe.timeLimit;
        orderManager = FindObjectOfType<OrderManager>();
        ShowItemName(false);
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            orderManager.OrderExpired(this);
        }
    }

    public void SetRecipe(IRecipe recipe)
    {
        this.recipe = recipe;
        orderNameText.SetText(recipe.recipeName);
        itemSprite.sprite = recipe.GetIcon();
        timeLeft = recipe.timeLimit;
    }

    public void OnMouseOver()
    {
        ShowItemName(true);
    }

    public void OnMouseExit()
    {
        ShowItemName(false);
    }

    public void ShowItemName(bool show)
    {
        itemNameCanvas.enabled = show;
    }
}
