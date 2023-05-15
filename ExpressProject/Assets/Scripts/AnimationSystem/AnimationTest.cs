using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationTest : MonoBehaviour
{
    public Button btnFishing;
    public Button btnReel;
    public Button btnFinish;

    public AnimationActions animationActions;

    private void Start()
    {
        this.btnFishing.onClick.AddListener(() =>
        {
            animationActions.TakeAction("FishingCast");
        }
        );

        this.btnReel.onClick.AddListener(() =>
        {
            animationActions.TakeAction("FishingReel");
        });

        this.btnFinish.onClick.AddListener(() =>
        {
            animationActions.TakeAction("FishingFinish");
        });

    }
}
