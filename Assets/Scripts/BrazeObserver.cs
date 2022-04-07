    using System;
    using Appboy;
    using Appboy.Models.Cards;
    using Appboy.Utilities;
    using UnityEngine;

    internal class BrazeObserver : MonoBehaviour
    {
        protected  void Awake()
        {
            Debug.Log("BrazeContentCardView::Awake "); 
        }
       
        public void ContendCardCallback(string message) {

            Debug.Log("BrazeContentCardView::ContendCardCallback 1 "+message); 

            
            // Example of logging a Content Card displayed event
            AppboyBinding.LogContentCardsDisplayed();
            try {
                JSONClass json = (JSONClass)JSON.Parse(message);
                Debug.Log("BrazeContentCardView::ContendCardCallback 2 "+json); 

                // Content Card data is contained in the `mContentCards` field of the top level object.
                if (json["mContentCards"] != null) {
                    
                    JSONArray jsonArray = (JSONArray)JSON.Parse(json["mContentCards"].ToString());
                    Debug.Log(String.Format("Parsed content cards array with {0} cards", jsonArray.Count));

                    // Iterate over the card array to parse individual cards.
                    for (int i = 0; i < jsonArray.Count; i++) {
                        JSONClass cardJson = jsonArray[i].AsObject;
                        try {
                            ContentCard card = new ContentCard(cardJson);
                            Debug.Log(String.Format("Created card object for card: {0}", card));

                            // Example of logging Content Card analytics on the ContentCard object 
                            card.LogImpression();
                            card.LogClick();
                            
                            
                        } catch {
                            Debug.Log(String.Format("Unable to create and log analytics for card {0}", cardJson));
                        }
                    }
                }
            } catch {
                throw new ArgumentException("Could not parse content card JSON message.");
            }
        }
    }