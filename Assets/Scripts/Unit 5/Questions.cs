using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Question
{
   string[] questions_; //index 0: question with blank, index 1: question without blank
   string[] solutions_; //index 0: correct solution; indices 1-3: incorrect solutions
   string[] responses_; //indices 0-1: compatible responses; index 2: incompatible response
   string translation_;


   public Question(string[] questions, string[] solutions, string[] responses, string translation) {
      questions_ = questions;
      solutions_ = solutions;
      responses_ = responses;
      translation_ = translation;
   }
   public string getPartial() {
      return questions_[0];
   }
   public string getFull() {
      return questions_[1];
   }
   public string getAnswer() {
      return solutions_[0];
   }
   public string getI1() {
      return solutions_[1];
   }
   public string getI2() {
      return solutions_[2];
   }
   public string getI3() {
      return solutions_[3];
   }
   public string getGood1() {
      return responses_[0];
   }
   public string getGood2() {
      return responses_[1];
   }
   public string getBad() {
      return responses_[2];
   }
   public string getTranslation() {
      return translation_;
   }
};