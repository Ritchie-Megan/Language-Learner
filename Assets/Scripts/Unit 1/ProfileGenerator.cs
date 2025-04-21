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
            "aburrid*", "cansad*", "content*", "emocionad*", "enojad*", "nervios*",
            "ocupad*", "relajad*"
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

    List<string> hairTypes = new List<string>
        {
            "corto", "largo"
        };
    
    //public GameObject name;
    //public GameObject age;
    //public GameObject nationality;
    //public GameObject personality;
    //public GameObject eyeColor;
    //public GameObject hairType;
    //public GameObject status;

    public TMP_Text name;
    public TMP_Text age;
    public TMP_Text nationality;
    public TMP_Text personality;
    public TMP_Text eyeColor;
    public TMP_Text hairType;
    public TMP_Text status;

    private List<TMP_Text> entries = new List<TMP_Text>();
    bool male;


    
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

        entries.Add(name);
        entries.Add(age);
        entries.Add(nationality);
        entries.Add(personality);
        entries.Add(eyeColor);
        entries.Add(hairType);
        entries.Add(status);
        
        UpdateProfile();
    }

    void UpdateProfile()
    {
        male = Random.value < 0.5f;
        string firstName = male ? RandomEntry(maleNames) : RandomEntry(femaleNames);

        name.text = firstName + " " + RandomEntry(lastNames);
        age.text = RandomEntry(ages);
        nationality.text = "Soy de " + RandomEntry(countries);
        personality.text = "Yo soy " + GenderWord(RandomEntry(personalityTraits));
        eyeColor.text = "Mis ojos son " + RandomEntry(eyeColors);
        hairType.text = "Mi cabello es " + RandomEntry(hairTypes);

        if (Random.value < 0.66f)
            status.text = "Ahora estoy " + GenderWord(RandomEntry(estoyStatuses));
        else
            status.text = "Ahora tengo " + GenderWord(RandomEntry(tengoStatuses));
        
         StartCoroutine(RefreshLayout());
    }

    IEnumerator RefreshLayout()
    {
        yield return new WaitForEndOfFrame();

        foreach (TMP_Text entry in entries)
            entry.enabled = false;

        foreach (TMP_Text entry in entries)
            entry.enabled = true;
    }

    string RandomEntry(List<string> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    string GenderWord(string word)
    {
        if (word == "trabajador*")
            return male ? "trabajador" : word.Replace('*', 'a');
        else
            return male ? word.Replace('*', 'o') : word.Replace('*', 'a');
    }
}
