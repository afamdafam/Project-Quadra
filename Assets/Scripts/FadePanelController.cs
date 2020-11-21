using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePanelController : MonoBehaviour
{
    public Animator paneAnim;
    public Animator gameInfoAnim;
    
    public void OK()
    {
        if(paneAnim != null && gameInfoAnim != null)
        {
            paneAnim.SetBool("Out", true);
            gameInfoAnim.SetBool("Out", true);
            StartCoroutine(GameStartCo());
        }
    }

    public void GameOver()
    {
        paneAnim.SetBool("Out", false);
        paneAnim.SetBool("Game Over", true);
    }

    IEnumerator GameStartCo()
    {
        yield return new WaitForSeconds(1f);
        Board board = FindObjectOfType<Board>();
        board.currentState = GameState.move;
    }
}
