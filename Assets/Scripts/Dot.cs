using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public int column;
    public int row;
    public int targetX;
    public int targetY;
    private GameObject otherDot;
    private Board board;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;
    public float swipeAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetY;
        column = targetX;
    }

    // Update is called once per frame
    void Update()
    {
        targetX = column;
        targetY = row;
        if(Mathf.Abs(targetX-transform.position.x)>.1){
            //move toward X
            tempPosition = new Vector2(targetX,transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else{
            // direct set position X
            tempPosition = new Vector2(targetX,transform.position.y);
            transform.position = tempPosition;
            board.allDots[column,row] = this.gameObject;
        }
         if(Mathf.Abs(targetY-transform.position.y)>.1){
            //move toward Y
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else{
            // direct set position Y
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
            board.allDots[column,row] = this.gameObject;
        }
    }

    private void OnMouseDown() {
        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(firstTouchPosition);   
    }
    private void OnMouseUp() {
        finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalcualteAngle();
    }

    void CalcualteAngle(){
        swipeAngle = Mathf.Atan2(finalTouchPosition.y-firstTouchPosition.y,finalTouchPosition.x-firstTouchPosition.x)*180/Mathf.PI;
        //Debug.Log(swipeAngle);
        MovePieces();
    }

    void MovePieces(){
        if(swipeAngle> -45 && swipeAngle <= 45 && column < board.width){
            //right swipe
            otherDot = board.allDots[column +1,row];
            otherDot.GetComponent<Dot>().column -=1;
            column +=1;
        }
        else if(swipeAngle> 45 && swipeAngle <= 135 && row < board.height){
            //up swipe
            otherDot = board.allDots[column ,row + 1];
            otherDot.GetComponent<Dot>().row -=1;
            row +=1;
        }
        else if(swipeAngle> 135 || swipeAngle <= -135 && column > 0){
            //left swipe
            otherDot = board.allDots[column-1,row];
            otherDot.GetComponent<Dot>().column +=1;
            column -=1;
        }
        else if(swipeAngle < -45 && swipeAngle >= -135 && row > 0){
            //down swipe
            otherDot = board.allDots[column,row-1];
            otherDot.GetComponent<Dot>().row +=1;
            row -=1;
        }
        
    }
}
