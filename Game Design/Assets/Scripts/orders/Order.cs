using System.Collections.Generic;
using items;
using recipes;
using TMPro;
using UnityEngine;

public class Order : MonoBehaviour
{
    private IRecipe _recipe;

    public List<Sprite> progressSprites;
    public SpriteRenderer progressSpriteRenderer;
    private int _currentSpriteIndex = 0;
    public double x = 0;
    public double queuepod = 0;

    public TextMeshProUGUI orderNameText;
    public Canvas itemNameCanvas;
    public SpriteRenderer itemSprite;
    public int queuePosition;
    public int queueStopX;
    public float speed = 0.5f;
    public ItemType deliveryItem
    {
        get => _recipe.deliveryItem;
    }

    private Transform _transform;
    private float _timeLeft;
    private OrderManager _orderManager;
    private bool _isExpired = false;

    public int remainingPoints
    {
        get => (int)(_timeLeft / _recipe.timeLimit * _recipe.points);
    }

    private void Start()
    {
        _transform = transform;
        _timeLeft = _recipe.timeLimit;
        _orderManager = FindObjectOfType<OrderManager>();
        itemNameCanvas.worldCamera = Camera.main;
        ShowItemName(false);
    }

    private void Update()
    {
        if (_isExpired)
        {
            return;
        }
        var deltaTime = Time.deltaTime;
        _timeLeft -= deltaTime;

        var currentSpriteIndex = (int)((1 - _timeLeft / _recipe.timeLimit) * (progressSprites.Count - 1));
        if (currentSpriteIndex != _currentSpriteIndex)
        {
            _currentSpriteIndex = currentSpriteIndex;
            progressSpriteRenderer.sprite = progressSprites[currentSpriteIndex];
        }

        if (_timeLeft <= 0)
        {
            _isExpired = true;
            _orderManager.OrderExpired(this);
        }

        if (transform.localPosition.x > queuePosition + queueStopX)
        {
            _transform.position += Vector3.left * (speed * deltaTime);
        }
    }

    public void SetRecipe(IRecipe recipe)
    {
        _recipe = recipe;
        orderNameText.SetText(_recipe.recipeName);
        itemSprite.sprite = _recipe.GetIcon();
        _timeLeft = _recipe.timeLimit;
    }

    public void OnMouseOver()
    {
        ShowItemName(true);
    }

    public void OnMouseExit()
    {
        ShowItemName(false);
    }

    private void ShowItemName(bool show)
    {
        itemNameCanvas.enabled = show;
    }           
}
