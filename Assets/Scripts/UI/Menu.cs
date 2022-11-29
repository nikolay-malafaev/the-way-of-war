using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   [SerializeField] private GameObject settings;
   public void Play (int index)
   {
      SceneManager.LoadScene(index);
   }

   public void Exit()
   {
      Application.Quit();
   }

   public void ActiveAnti(GameObject ent)
   {
      ent.SetActive(!ent.activeSelf);
   }
}
