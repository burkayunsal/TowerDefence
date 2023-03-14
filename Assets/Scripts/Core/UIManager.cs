using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
   [Header("Panels")]
    [SerializeField] Panels pnl;
    [Header("Buttons")]
    [SerializeField] public Buttons btn;

    private CanvasGroup activePanel = null;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        btn.play.gameObject.SetActive(true);
        FadeInAndOutPanels(pnl.mainMenu);
    }

    public void StartGame()
    {
        GameManager.OnStartGame();
    }

    public void OnGameStarted()
    {
        FadeInAndOutPanels(pnl.gameIn);
    }

    public void OnFail()
    {
        FadeInAndOutPanels(pnl.fail);
    }

    private Tween activeOutTween = null, activeInTween = null;

    void FadeInAndOutPanels(CanvasGroup _in)
    {
        CanvasGroup _out = activePanel;
        activePanel = _in;

        if(_out != null)
        {
            if(activeOutTween != null)
                activeOutTween.Kill(false);
            _out.interactable = false;
            _out.blocksRaycasts = false;

            activeOutTween = _out.DOFade(0f, 0.5f).OnComplete(() =>
            {
                activeOutTween =_in.DOFade(1f, 0.5f).OnComplete(() =>
                {
                    _in.interactable = true;
                    _in.blocksRaycasts = true;
                    activeOutTween = null;
                });
            });
        }
        else
        {
            if(activeInTween != null)
                activeInTween.Kill(false);
            
            activeInTween = _in.DOFade(1f, 0.5f).OnComplete(() =>
            {
                activeInTween = null;
                _in.interactable = true;
                _in.blocksRaycasts = true;
            });
        }
    }


    public void GoHome()
    {
        GameManager.ReloadScene();
    }
    
    [System.Serializable]
    public class Panels
    {
        public CanvasGroup mainMenu, gameIn, fail;
    }

    [System.Serializable]
    public class Buttons
    {
        public Button play;
    }
}

