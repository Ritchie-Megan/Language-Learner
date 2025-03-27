using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Question
{
   string partialQuestion;
   string fullQuestion;
   string correct;
   string incorrect1;
   string incorrect2;
   string incorrect3;
   string goodResponse1;
   string goodResponse2;
   string badResponse;


   public Question(string partial, string full, string c, string i1, string i2, string i3, string good1, string good2, string bad) {
      partialQuestion = partial;
      fullQuestion = full;
      correct = c;
      incorrect1 = i1;
      incorrect2 = i2;
      incorrect3 = i3;
      goodResponse1 = good1;
      goodResponse2 = good2;
      badResponse = bad;

   }
   public string getPartial() {
      return partialQuestion;
   }
   public string getFull() {
      return fullQuestion;
   }
   public string getAnswer() {
      return correct;
   }
   public string getI1() {
      return incorrect1;
   }
   public string getI2() {
      return incorrect2;
   }
   public string getI3() {
      return incorrect3;
   }
   public string getGood1() {
      return goodResponse1;
   }
   public string getGood2() {
      return goodResponse2;
   }
   public string getBad() {
      return badResponse;
   }
};