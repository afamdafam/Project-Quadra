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
        }
    }
}
