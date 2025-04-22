using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ProfileGenerator : MonoBehaviour
{
    List<string> maleNames = new List<string>
        {
            "Santiago", "Matías", "Sebastián", "Mateo", "Nicolás", "Alejandro",
            "Samuel", "Diego", "Daniel", "Benjamín", "Leonardo", "Tomás", "Joaquín",
            "Gabriel", "Emiliano", "Martín", "Lucas", "Agustín", "David", "Iker",
            "José", "Maximiliano", "Adrián", "Emmanuel", "Felipe", "Juan Pablo",
            "Andrés", "Gerónimo", "Ángel", "Rodrigo", "Bruno", "Alexander", "Thiago",
            "Pablo", "Ian", "Isaac", "Miguel Ángel", "Fernando", "Javier", "Emilio",
            "Sebastián", "Alonso", "Aarón", "Rafael", "Esteban", "Juan", "Axel", 
            "Francisco", "Bautista", "Carlos", "Dylan", "Julián", "Manuel", "Facundo",
            "Gael", "Valentino", "Damián", "Santino", "Vicente", "Máximo",
            "Christopher", "Jorge", "Luciano", "Dante", "Alan", "Cristóbal", "Jesús",
            "Lorenzo", "Alex", "Patricio", "Pedro", "Manuel", "Matthew", "Antonio",
            "Iván", "José", "Hugo", "Josué", "Lautaro", "Diego Alejandro", "Miguel",
            "Franco", "Kevin", "Luis", "Simón", "Elías", "Caleb", "Eduardo", "Ricardo", "Juan David", "Marcos",
            "Salvador", "Jacobo", "Ignacio", "Camilo", "Mauricio", "Gonzalo"
        };
    List<string> femaleNames = new List<string>
        {
            "Sofía", "Isabella", "Valentina", "Emma", "Martina", "Lucía", "Victoria",
            "Luciana", "Valeria", "Camila", "Julieta", "Ximena", "Sara", "Daniela",
            "Emilia", "Xiomara", "Mía", "Catalina", "Julia", "Elena", "Olivia",
            "Regina", "Paula", "Natalia", "Mariana", "Samantha", "María", "Antonella",
            "Gabriela", "Emily", "Zoe", "Alma", "Alejandra", "Andrea", "Juliana",
            "Alba", "Aaliyah", "Jahaira", "Carla", "Laura", "Ángela", "Clara",
            "Teresa", "Laura", "Fernanda", "Camila", "Inés", "Silvia", "Regina",
            "Carmen", "Teresa", "Valeria", "Marisol", "Guadalupe", "Adriana",
            "Beatriz", "Patricia", "Carmen", "Isabel", "Mariana", "Teresa", "María",
            "Susana", "Clara", "Mónica", "Viviana", "Lidia", "Dolores", "Stefanie",
            "Violeta", "Veronica", "Jocelyn", "Gloria", "Angélica", "Rosalía",
            "Silvia", "Aida", "Raquel", "Leticia"

        };
    List<string> lastNames = new List<string>
        {
            "Aguilar", "Alonso", "Álvarez", "Betancourt", "Blanco", "Burgos",
            "Castillo", "Castro", "Chávez", "Colón", "Contreras", "Cortez", "Cruz",
            "De la Cruz", "De León", "Delgado", "Díaz", "Domínguez", "Estrada",
            "Fernández", "Flores", "Fuentes", "García", "Garza", "Gil", "Gómez",
            "González", "Guerrero", "Gutiérrez", "Guzmán", "Hernández", "Herrera",
            "Iglesias", "Jiménez", "Juárez", "López", "Luna", "Marín", "Marroquín",
            "Martín", "Martínez", "Medina", "Méndez", "Mendoza", "Molina", "Morales",
            "Moreno", "Muñoz", "Navarro", "Narvaez", "Núñez", "Ortega", "Ortiz",
            "Pérez", "Peña", "Ramírez", "Ramos", "Reyes", "Rivera", "Rodríguez",
            "Rojas", "Romero", "Rosario", "Rubio", "Ruiz", "Salazar", "Sánchez",
            "Santana", "Santiago", "Santos", "Sanz", "Serrano", "Soto", "Suárez",
            "Torres", "Vargas", "Vázquez"
        };
    List<string> ages = new List<string>
        {
            "trece", "catorce", "quince", "dieciséis", "diecisiete", "dieciocho",
            "diecinueve", "veinte", "veintiuno", "veintidós", "veintitrés",
            "veinticuatro", "veinticinco", "veintiséis", "veintisiete", "veintiocho",
            "veintinueve", "treinta", "treinta y uno", "treinta y dos", "treinta y tres",
            "treinta y cuatro", "treinta y cinco", "treinta y seis","treinta y siete",
            "treinta y ocho", "treinta y nueve", "cuarenta", "cuarenta y uno",
            "cuarenta y dos", "cuarenta y tres", "cuarenta y cuatro", "cuarenta y cinco"
        };
    List<string> personalityTraits = new List<string>
        {
            "generos*", "sedentari*", "tímid*", "activ*", "atlétic*", "aventurer*",
            "cómic*", "estudios*", "inteligente", "trabajador*"
        };
    List<string> estoyStatuses = new List<string>
        {
            "aburrid*", "cansad*", "content*", "enojad*"
        };
    List<string> tengoStatuses = new List<string>
        {
            "miedo", "sueño", "vergüenza"
        };
    List<string> countries = new List<string>
        {
            "Mexico", "Colombia", "España", "Argentina", "Perú", "Venezuela", "Chile",
            "Ecuador", "Guatemala", "Cuba", "Bolivia", "la República Dominicana",
            "Honduras", "Paraguay", "El Salvador", "Nicaragua", "Costa Rica", "Panamá",
            "Uruguay", "Puerto Rico", "Estados Unidos"
        };

    List<string> eyeColors = new List<string>
        {
            "azules", "marrones", "verdes", "negros", "grises"
        };

    List<string> maleHairTypes = new List<string>
        {
            "corto, liso", "corto, rizado"
        };

    List<string> femaleHairTypes = new List<string>
        {
            "largo, liso", "largo, rizado"
        };

    List<string> hairColors = new List<string>
        {
            "castaño", "negro", "rubio", "pelirroja"
        };
    
    //public GameObject name;
    //public GameObject age;
    //public GameObject nationality;
    //public GameObject personality;
    //public GameObject eyeColor;
    //public GameObject hairType;
    //public GameObject status;

    public TMP_Text nameField;
    public TMP_Text userNameField;
    public TMP_Text ageField;
    public TMP_Text nationalityField;
    public TMP_Text personalityField;
    public TMP_Text eyeColorField;
    public TMP_Text hairTypeField;
    public TMP_Text statusField;
    private bool male;

    [HideInInspector]
    public string ageRange;
    [HideInInspector]
    public string personality;
    [HideInInspector]
    public string eyeColor;
    [HideInInspector]
    public string hairType;
     [HideInInspector]
    public string hairColor;
    [HideInInspector]
    public string status;
    [HideInInspector]
    public string gender;

    private List<TMP_Text> entries = new List<TMP_Text>();

    private Dictionary<string, string> ageRangeDict;
    


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //nameText = name.GetComponentInChildren<TMP_Text>();
        //ageText = age.GetComponentInChildren<TMP_Text>();
        //nationalityText = nationality.GetComponentInChildren<TMP_Text>();
        //personalityText = personality.GetComponentInChildren<TMP_Text>();
        //eyeText = eyeColor.GetComponentInChildren<TMP_Text>();
        //hairText = hairType.GetComponentInChildren<TMP_Text>();
        //statusText = status.GetComponentInChildren<TMP_Text>();
        
        entries.Add(nameField);
        entries.Add(userNameField);
        entries.Add(ageField);
        entries.Add(nationalityField);
        entries.Add(personalityField);
        entries.Add(eyeColorField);
        entries.Add(hairTypeField);
        entries.Add(statusField);
        
        UpdateProfile();
    }

    void UpdateProfile()
    {
        male = Random.value < 0.5f;
        
        string age = RandomEntry(ages);

        //ageRange = ageRangeDict[age];
        personality = GenderWords(RandomEntry(personalityTraits));
        eyeColor = RandomEntry(eyeColors);
        hairType = male ? RandomEntry(maleHairTypes) : RandomEntry(femaleHairTypes);
        hairColor = RandomEntry(hairColors);
        gender = male ? "male" : "female";

        if (Random.value < 0.66f)
        {
            status =  GenderWords(RandomEntry(estoyStatuses));
            statusField.text = "Ahora estoy " + status;
        }
        else
        {
            status = GenderWords(RandomEntry(tengoStatuses));
            statusField.text = "Ahora tengo " + status;
        }

        

        string firstName = male ? RandomEntry(maleNames) : RandomEntry(femaleNames);
        string lastName = RandomEntry(lastNames);

        nameField.text = firstName + " " + lastName;
        ageField.text = RandomEntry(ages);
        nationalityField.text = "Soy de " + RandomEntry(countries);
        personalityField.text = "Yo soy " + personality;
        eyeColorField.text = "Mis ojos son " + eyeColor;
        hairTypeField.text = "Mi cabello es " + hairType + "\ny " + hairColor;

        if (Random.value < 0.3f)
        {
            userNameField.text = firstName.ToLower()[0] + lastName.ToLower() + Random.Range(0,100);
        }
        else if (Random.value < 0.9f)
        {
            userNameField.text = firstName.ToLower() + lastName.ToLower()[0] + Random.Range(0,100);
        }
        else if (Random.value < 0.9f)
        {
            userNameField.text = lastName.ToLower() + firstName.ToLower()[0]+ Random.Range(0,100);
        }
        else
        {
            userNameField.text = firstName.ToLower() + lastName.ToLower() + Random.Range(0,100);
        }
        
        foreach (TMP_Text entry in entries)
        {
            GameObject parentBox = entry.transform.parent.gameObject;
            LayoutRebuilder.ForceRebuildLayoutImmediate(parentBox.GetComponent<RectTransform>());
        }
    }

    string RandomEntry(List<string> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    string GenderWords(string phrase)
    {
        string[] words = phrase.Split(' ');
        string genderedPhrase = "";

        foreach (string word in words)
        {
            if (word == "trabajador*")
                genderedPhrase += male ? "trabajador" : word.Replace('*', 'a');
            else
                genderedPhrase += male ? word.Replace('*', 'o') : word.Replace('*', 'a');
        }

        return genderedPhrase;
    }
}
