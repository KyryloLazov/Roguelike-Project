using System;
using System.Collections.Generic;
using Gameflow.Domain;
using ItemSystem.Domain;
using ItemSystem.Infrastructure.Config;
using UnityEngine;
using Zenject;

namespace Gameflow.Presentation
{
    public class RewardUI : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Transform _cardParent;
        [SerializeField] private RewardCardUI _cardPrefab;

        private RewardService _rewardService;
        private InventoryController _inventory;
        private List<RewardCardUI> _rewardCards = new();

        public event Action OnRewardSelected;

        [Inject]
        public void Construct(RewardService rewardService, InventoryController inventory)
        {
            _rewardService = rewardService;
            _inventory = inventory;
        }

        public void Show(int itemCount = 3)
        {
            ClearCards();
            
            var rewards = _rewardService.GetRandomRewards(itemCount);

            for (int i = 0; i < itemCount; i++)
            {
                if (i < rewards.Count)
                {
                    RewardCardUI card = Instantiate(_cardPrefab, _cardParent);
                    card.Setup(rewards[i], OnCardClicked);
                    _rewardCards.Add(card);
                }
            }

            _panel.SetActive(true);
        }
        
        private void ClearCards()
        {
            foreach (var card in _rewardCards)
                Destroy(card.gameObject);

            _rewardCards.Clear();
        }

        public void Hide()
        {
            _panel.SetActive(false);
        }

        private void OnCardClicked(ItemConfig item)
        {
            _inventory.AddItem(item);
            OnRewardSelected?.Invoke();
        }
    }
}