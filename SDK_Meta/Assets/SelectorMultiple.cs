using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Interaction.Input;

public class SelectorMultiple : MonoBehaviour
{
    public string CurrentLeftGesture;
    public string CurrentRightGesture;
    public TextMeshPro NombreGesto;
    public List<GameObject> selectores;

    void Start()
    {
        foreach (GameObject selector in selectores)
        {
            if (selector.TryGetComponent<ISelector>(out var selector2))
            {
                Debug.Log("selector");
                if (selector.TryGetComponent<HandRef>(out var hand))
                {
                    if (hand.Hand.Handedness == Handedness.Left)
                    {
                        selector2.WhenSelected += () => OnDetectGesture(selector.name, true);
                        selector2.WhenUnselected += () => OnUndetectGesture(selector.name, true);
                    }
                    else
                    {
                        selector2.WhenSelected += () => OnDetectGesture(selector.name, false);
                        selector2.WhenUnselected += () => OnUndetectGesture(selector.name, false);
                    }
                }
            }
        }
    }

    private void OnUndetectGesture(string GestureName, bool islefthandness)
    {
        Debug.Log("GestoPerdido " + GestureName);
        NombreGesto.text = "GestoPerdido";
        if (islefthandness)
        {
            CurrentLeftGesture = "";
        }
        else
        {
            CurrentRightGesture = "";
        }
    }

    private void OnDetectGesture(string GestureName, bool islefthandness)
    {
        Debug.Log("GestoReconocido " + GestureName);
        NombreGesto.text = GestureName;
        if (islefthandness)
        {
            CurrentLeftGesture = GestureName;
        }
        else
        {
            CurrentRightGesture = GestureName;
        }
        if (CurrentRightGesture == "PinkyPose_R" && CurrentLeftGesture == "PaperPose_L")
        {
            Debug.Log("PinkyPaper");
            NombreGesto.text = "PinkyPaper";
        }
        if (CurrentRightGesture == "PaperPose_R" && CurrentLeftGesture == "PinkyPose_L")
        {
            Debug.Log("PaperPinky");
            NombreGesto.text = "PaperPinky";
        }
    }

    void Update()
    {

    }
}