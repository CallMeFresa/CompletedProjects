using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public GameObject Door;
    public bool isPuzzleSolved;
    //public Door_left DoorAnimTrigger;
    public GameObject Slots_puzzle;
    public GameObject Pieces_Puzzle;
    public Button Button_trigger;
    public ButtomAppear scriptBoton;

    //public bool PiecesPutInPlace;

    //La puso Santiago
    //public SlotScript AbrirPuerta;

    public int pointsToUnblock;
    public int currentPoints;


    void Start()
    {
        isPuzzleSolved = Door.GetComponent<Animator>().enabled = false;
        pointsToUnblock = Pieces_Puzzle.transform.childCount;
       
    }
    private void Update()
    {
        Button_trigger.onClick.AddListener(TaskOnClick);

        if (currentPoints >= pointsToUnblock)
        {
            isPuzzleSolved = Door.GetComponent<Animator>().enabled = true;
            Slots_puzzle.SetActive(false);
            Pieces_Puzzle.SetActive(false);

            Destroy(Button_trigger);
            Destroy(scriptBoton);
        }

        #region 
        //if (AbrirPuerta == true)
        //{
        //    isPuzzleSolved = Door.GetComponent<Animator>().enabled = true;
        //    Slots_puzzle.SetActive(false);
        //    Pieces_Puzzle.SetActive(false);

        //    Destroy(Button_trigger);
        //    Destroy(scriptBoton);
        //}
        #endregion
    }
    void TaskOnClick()
    {
        Slots_puzzle.SetActive(true);
        Pieces_Puzzle.SetActive(true);
    }

    public void UpdatePoints()
    {
         currentPoints++;
    }
}
