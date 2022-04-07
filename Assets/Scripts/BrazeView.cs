
using Appboy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class BrazeView :MonoBehaviour
    {
        public TMP_InputField userIdInput;
        public Button changeUserButton;
        public Button displayContentCardButton;
        public Button requestContentCardsRefreshButton;
        
        private void Awake()
        {
            AppboyBinding.EnableSDK();

            changeUserButton.onClick.AddListener(ChangeUser);
            displayContentCardButton.onClick.AddListener(DisplayContentCards);
            requestContentCardsRefreshButton.onClick.AddListener(RequestContentCardsRefresh);
            userIdInput.text = "849121";
            AppboyBinding.ConfigureListener(BrazeUnityMessageType.CONTENT_CARDS_UPDATED,"Braze_Observer", "ContendCardCallback");
        }

        private void ChangeUser()
        {
            Debug.Log("BrazeView::ChangeUser "+ userIdInput.text);
            AppboyBinding.ChangeUser(userIdInput.text);
        }

        private void DisplayContentCards()
        {
            Debug.Log("BrazeView::DisplayContentCards ");
            AppboyBinding.DisplayContentCards();
        }
        
        private void RequestContentCardsRefresh()
        {
            Debug.Log("BrazeView::RequestContentCardsRefresh ");
            AppboyBinding.RequestContentCardsRefresh();
        }
    }
}