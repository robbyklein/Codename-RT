using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cysharp.Threading.Tasks;
using System;

public class OverlayManager : MonoBehaviour {
    [SerializeField] private UIDocument uiDocument;
    [SerializeField] private int transitionMs = 500;

    private VisualElement uiRootElement;
    private VisualElement uiOverlayElement;
    private string activeClass = "black-overlay--active";

    private void OnEnable() {
        SetUIElements();
        SetTransitionDuration();
    }

    private void SetUIElements() {
        uiRootElement = uiDocument.rootVisualElement;

        if (uiRootElement == null) {
            throw new System.NullReferenceException("The root Visual Element of the UIDocument is null. Make sure the UIDocument is properly configured.");
        }

        // .Q selects the first element by name
        uiOverlayElement = uiRootElement.Q("black-overlay");

        if (uiOverlayElement == null) {
            throw new System.NullReferenceException("The black-overlay element is not found in the UI hierarchy. Make sure it exists and is properly named.");
        }
    }

    private void SetTransitionDuration() {
        uiOverlayElement.style.transitionDuration = new List<TimeValue>() {
            new TimeValue(transitionMs, TimeUnit.Millisecond)
        };
    }

    public async UniTask FadeOut() {
        uiOverlayElement.RemoveFromClassList(activeClass);
        await UniTask.Delay(TimeSpan.FromMilliseconds(transitionMs), ignoreTimeScale: false);
    }

    public async UniTask FadeIn() {
        uiOverlayElement.AddToClassList(activeClass);
        await UniTask.Delay(TimeSpan.FromMilliseconds(transitionMs), ignoreTimeScale: false);
    }
}


