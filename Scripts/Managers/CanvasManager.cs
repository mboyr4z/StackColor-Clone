using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoSingleton<CanvasManager>
{

    [SerializeField] private Menu[] menus;


    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                menus[i].Open();
            }
            else if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }

    public void CloseMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                menus[i].Close();
                break;
            }      
        }
    }

    public void OpenMenu(Menu menu)
    {
        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }

    public void quitGame()
    {
        Application.Quit();
    }


  /*  private void Start()
    {
        Actions.act_playerTouchedToObstackle += () => { losePanel.SetActive(true);  };  +
        Actions.act_runStarted += () => { inGamePanel.SetActive(true); };               +       
        Actions.act_enteredFinisArea += () => { finishPanel.SetActive(true); };         +
        Actions.act_enteredBonusArea += (float force) => { finishPanel.SetActive(false); }; +
        Actions.act_finishGame += (float b) => { winPanel.SetActive(true); inGamePanel.SetActive(false);  };    

        Actions.act_nextLevel += () => { winPanel.SetActive(false); waitPanel.SetActive(true);  };
        Actions.act_noBrickLeft += () => { losePanel.SetActive(true); inGamePanel.SetActive(false); };
    }*/
}
