﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    [Header("Board Variables")]
    public int column;
    public int row;
    public int previousColumn;
    public int previousRow;
    public int targetX;
    public int targetY;
    public bool isMatched = false;

    public FindMatches findMatches;
    private GameObject otherDot;
    private Board board;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;
    public float swipeAngle = 0;
    public float swipeResist = 1f;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        findMatches = FindObjectOfType<FindMatches>();
      /*  targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetY;
        column = targetX;
        previousRow = row;
        previousColumn = column;*/
    }

    // Update is called once per frame
    void Update()
    {
       // FindMatches();
        if (isMatched)
        {
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.color = new Color(1f, 1f, 1f, .2f);
        }
        targetX = column;
        targetY = row;
        if(Mathf.Abs(targetX-transform.position.x)>.1){
            //move toward X
            tempPosition = new Vector2(targetX,transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if(board.allDots[column, row] != this.gameObject)
            {
                board.allDots[column, row] = this.gameObject;
            }
            findMatches.FindAllMatches();
        }
        else{
            // direct set position X
            tempPosition = new Vector2(targetX,transform.position.y);
            transform.position = tempPosition;
        }
         if(Mathf.Abs(targetY-transform.position.y)>.1){
            //move toward Y
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if (board.allDots[column, row] != this.gameObject)
            {
                board.allDots[column, row] = this.gameObject;
            }
            findMatches.FindAllMatches();
        }
        else{
            // direct set position Y
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
        }
    }

    public IEnumerator CheckMoveCo()
    {
        yield return new WaitForSeconds(.5f);
        if(otherDot != null)
        {
            if (!isMatched && !otherDot.GetComponent<Dot>().isMatched)
            {
                otherDot.GetComponent<Dot>().row = row;
                otherDot.GetComponent<Dot>().column = column;
                row = previousRow;
                column = previousColumn;
                yield return new WaitForSeconds (.5f);
                board.currentState = GameState.move;
            }
            else
            {
                board.DestroyMatches();
            }
            otherDot = null;
        }
    }

    private void OnMouseDown() {
        if(board.currentState == GameState.move){
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
        //Debug.Log(firstTouchPosition);   
    }
    private void OnMouseUp() {
        if(board.currentState == GameState.move){
        finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        CalcualteAngle();
    }

    void CalcualteAngle(){
        if(Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y)> swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MovePieces();
            board.currentState = GameState.wait;
        }
        else{
            board.currentState = GameState.move;
        }
    }

    void MovePieces(){
        if(swipeAngle> -45 && swipeAngle <= 45 && column < board.width-1){
            //right swipe
            otherDot = board.allDots[column +1,row];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().column -=1;
            column +=1;
        }
        else if(swipeAngle> 45 && swipeAngle <= 135 && row < board.height-1){
            //up swipe
            otherDot = board.allDots[column ,row + 1];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().row -=1;
            row +=1;
        }
        else if(swipeAngle> 135 || swipeAngle <= -135 && column > 0){
            //left swipe
            otherDot = board.allDots[column-1,row];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().column +=1;
            column -=1;
        }
        else if(swipeAngle < -45 && swipeAngle >= -135 && row > 0){
            //down swipe
            otherDot = board.allDots[column,row-1];
            previousRow = row;
            previousColumn = column;
            otherDot.GetComponent<Dot>().row +=1;
            row -=1;
        }
        StartCoroutine(CheckMoveCo());
    }

    void FindMatches()
    {
        if(column > 0 && column < board.width - 1)
        {
            GameObject leftDot1 = board.allDots[column - 1, row];
            GameObject rightDot1 = board.allDots[column + 1, row];
            if(leftDot1 != null && rightDot1 != null)
            {
                if (leftDot1.tag == this.gameObject.tag && rightDot1.tag == this.gameObject.tag)
                {
                    leftDot1.GetComponent<Dot>().isMatched = true;
                    rightDot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;
                }
            }
        }
        if (row > 0 && row < board.height - 1)
        {
            GameObject upDot1 = board.allDots[column, row + 1];
            GameObject downDot1 = board.allDots[column, row - 1];
            if(upDot1 != null && downDot1 != null)
            {
                if (upDot1.tag == this.gameObject.tag && downDot1.tag == this.gameObject.tag)
                {
                    upDot1.GetComponent<Dot>().isMatched = true;
                    downDot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;
                }
            }
        }
    }
}
