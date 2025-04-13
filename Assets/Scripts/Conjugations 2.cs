using System.Collections.Generic;
using UnityEngine;

public class ConjugationManager : MonoBehaviour
{
    public static ConjugationManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private Dictionary<string, string[]> regularVerbEndings = new Dictionary<string, string[]>()
    {
        {"ar", new string[] {"o", "as", "a", "amos", "áis", "an"} },
        {"er", new string[] {"o", "es", "e", "emos", "éis", "en"} },
        {"ir", new string[] {"o", "es", "e", "imos", "ís", "en"} }
    };
    
    private Dictionary<string, Dictionary<string, string>> irregularVerbs = new Dictionary<string, Dictionary<string, string>>()
    {
        {"ser", new Dictionary<string, string> { {"yo", "soy"}, {"tú", "eres"}, {"él", "es"}, {"nosotros", "somos"}, {"vosotros", "sois"}, {"ellos", "son"} }},
        {"estar", new Dictionary<string, string> { {"yo", "estoy"}, {"tú", "estás"}, {"él", "está"}, {"nosotros", "estamos"}, {"vosotros", "estáis"}, {"ellos", "están"} }},
        {"tener", new Dictionary<string, string> { {"yo", "tengo"}, {"tú", "tienes"}, {"él", "tiene"}, {"nosotros", "tenemos"}, {"vosotros", "tenéis"}, {"ellos", "tienen"} }},
        {"haber", new Dictionary<string, string> { {"yo", "he"}, {"tú", "has"}, {"él", "ha"}, {"nosotros", "hemos"}, {"vosotros", "habéis"}, {"ellos", "han"} }},
        {"hacer", new Dictionary<string, string> { {"yo", "hago"}, {"tú", "haces"}, {"él", "hace"}, {"nosotros", "hacemos"}, {"vosotros", "hacéis"}, {"ellos", "hacen"} }},
        {"ir", new Dictionary<string, string> { {"yo", "voy"}, {"tú", "vas"}, {"él", "va"}, {"nosotros", "vamos"}, {"vosotros", "vais"}, {"ellos", "van"} }},
        {"poder", new Dictionary<string, string> { {"yo", "puedo"}, {"tú", "puedes"}, {"él", "puede"}, {"nosotros", "podemos"}, {"vosotros", "podéis"}, {"ellos", "pueden"} }},
        {"querer", new Dictionary<string, string> { {"yo", "quiero"}, {"tú", "quieres"}, {"él", "quiere"}, {"nosotros", "queremos"}, {"vosotros", "queréis"}, {"ellos", "quieren"} }},
        {"decir", new Dictionary<string, string> { {"yo", "digo"}, {"tú", "dices"}, {"él", "dice"}, {"nosotros", "decimos"}, {"vosotros", "decís"}, {"ellos", "dicen"} }},
        {"venir", new Dictionary<string, string> { {"yo", "vengo"}, {"tú", "vienes"}, {"él", "viene"}, {"nosotros", "venimos"}, {"vosotros", "venís"}, {"ellos", "vienen"} }},
        {"saber", new Dictionary<string, string> { {"yo", "sé"}, {"tú", "sabes"}, {"él", "sabe"}, {"nosotros", "sabemos"}, {"vosotros", "sabéis"}, {"ellos", "saben"} }}
    };
    
    private string[] subjectPronouns = { "yo", "tú", "él", "nosotros", "vosotros", "ellos" };

    public string ConjugateVerb(string verb, string subject)
    {
        subject = subject.ToLower();
        
        if (irregularVerbs.ContainsKey(verb))
        {
            if (irregularVerbs[verb].ContainsKey(subject))
            {
                return irregularVerbs[verb][subject];
            }
            else
            {
                return "Invalid subject";
            }
        }
        
        if (verb.Length < 2) return "Invalid verb";

        string root = verb.Substring(0, verb.Length - 2);
        string ending = verb.Substring(verb.Length - 2);

        if (!regularVerbEndings.ContainsKey(ending))
            return "Invalid verb type"; 

        int pronounIndex = System.Array.IndexOf(subjectPronouns, subject);
        if (pronounIndex == -1)
            return "Invalid subject";

        return root + regularVerbEndings[ending][pronounIndex];
    }
    
    public bool IsCorrectConjugation(string verb, string subject, string userInput)
    {
        string correctConjugation = ConjugateVerb(verb, subject);
        return userInput.Trim().ToLower() == correctConjugation.ToLower();
    }
}