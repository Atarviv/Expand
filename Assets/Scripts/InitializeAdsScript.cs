using UnityEngine;
using UnityEngine.Advertisements;

public class InitializeAdsScript : MonoBehaviour
    {
    string gameId = "4145969";
    bool testMode = false;
    void Start()
        {
        Advertisement.Initialize(gameId, testMode);
        }
    }
