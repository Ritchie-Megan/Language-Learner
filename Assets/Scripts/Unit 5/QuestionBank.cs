using System.Collections.Generic;

public static class QuestionBank
{
    public static List<Question> Questions = new List<Question>() {
        //abierto/a
        new Question(new string[]{"Me gusta conocer gente diferente. ¿Eres una persona [_____] a nuevas ideas y culturas?",
            "Me gusta conocer gente diferente. ¿Eres una persona abierta a nuevas ideas y culturas?"},
            new string[]{"abierta", "tranquila", "amable", "honesta"},
            new string[]{"Sí, me encanta aprender de otras personas y explorar diferentes formas de pensar.",
            "Siempre trato de escuchar con respeto y entender otros puntos de vista.",
            "No me interesa cambiar mi forma de pensar. Ya sé lo que funciona para mí."},
            "open"),
        
        //alegre
        new Question(new string[]{"Te invitan a una fiesta sorpresa. ¿Eres una persona [_____] en ese tipo de eventos?",
            "Te invitan a una fiesta sorpresa. ¿Eres una persona alegre en ese tipo de eventos?"},
            new string[] {"alegre", "triste", "única", " pacífica"},
            new string[] {"¡Claro! Me encanta bailar y reír con todos.",
            "Sí, siempre sonrío y trato de que todos se diviertan.",
            "Prefiero no hablar con nadie en las fiestas."},
            "happy"),

        //amable
        new Question(new string[]{"Tengo una familia muy grande. ¿Qué tan [_____] eres con la gente nueva?",
            "Tengo una familia muy grande. ¿Qué tan amable eres con la gente nueva?"},
            new string[] {"amable", "egoísta", "cerrado/a", " imaginativo/a"},
            new string[] {"Mi familia también es grande, estoy acostumbrada.",
            "Siempre me encanta conocer gente nueva. ¡Cuanto más, mejor!",
            "No me gusta conocer gente nueva."},
            "friendly"),

        //bondadoso/a
        new Question(new string[]{"¿Cómo reaccionas cuando alguien necesita ayuda? ¿Eres [_____]?",
            "¿Cómo reaccionas cuando alguien necesita ayuda? ¿Eres bondadoso/a?"},
            new string[] {"bondadoso/a", "honesto/a", "fácil", "chistoso/a"},
            new string[] {"Ayudo sin que me lo pidan, me gusta hacer cosas buenas.",
            "Siempre trato de apoyar a los demás cuando puedo.",
            "No me importa mucho lo que le pase a otras personas."},
            "kind/good"),

        //bueno
        new Question(
            new string[]{"Mis padres siempre me dicen que es importante ser una persona [_____]. ¿Qué significa eso para ti?",
            "Mis padres siempre me dicen que es importante ser una persona buena. ¿Qué significa eso para ti?"},
            new string[]{"buena", "mala", "insensible", "aburrida"},
            new string[]{"Para mí, ser buena persona es tratar a los demás con respeto y ayudar cuando puedo.",
            "Significa ser honesto, responsable y pensar en el bienestar de otros.",
            "Ser buena persona no me importa. Cada quien cuida de sí mismo."},
            "good"),

        //capaz
        new Question(new string[]{"¿Cómo reaccionas ante un problema difícil? ¿Te consideras [_____]?",
            "¿Cómo reaccionas ante un problema difícil? ¿Te consideras capaz?"},
            new string[] {"capaz", "incapaz", "sensible", "nervioso/a"},
            new string[] {"Resuelvo problemas con calma y lógica.",
            "Sí, siempre busco una solución aunque sea difícil.",
            "No sé qué hacer cuando algo es complicado."},
            "capable"),

        //cariñoso/a
        new Question(new string[]{"¿Cómo muestras que eres una persona [_____] con tus seres queridos?",
            "¿Cómo muestras que eres una persona cariñosa con tus seres queridos?"},
            new string[] {"cariñosa", "insensible", "aburrido/a", "cerrado/a"},
            new string[] {"Siempre les doy abrazos y paso tiempo con ellos.",
            "Les escribo mensajes lindos y los escucho.",
            "No me gusta hablar de sentimientos."},
            "caring"),

        //chistoso/a
        new Question(new string[]{"¿Te gusta hacer reír a los demás? ¿Te consideras [_____]?",
            "¿Te gusta hacer reír a los demás? ¿Te consideras chistoso/a?"},
            new string[] {"chistoso/a", "egoísta", "imaginativo/a", "capaz"},
            new string[] {"Sí, siempre tengo un chiste listo.",
            "Me encanta contar historias graciosas.",
            "No me gusta contar chistes ni reírme mucho."},
            "funny"),

        //compasivo/a
        new Question(new string[]{"¿Qué haces cuando ves a alguien sufriendo? ¿Eres [_____]?",
            "¿Qué haces cuando ves a alguien sufriendo? ¿Eres compasivo/a?"},
            new string[] {"compasivo/a", "responsable", "mejor", "nervioso/a"},
            new string[] {"Trato de ayudar y escuchar a la persona.",
            "Sí, siempre me preocupo por los demás.",
            "No me importa mucho lo que sienten los otros."},
            "compassionate"),

        //comprensivo/a
        new Question(new string[]{"Tu amigo está pasando por un mal momento. ¿Eres [_____] con él?",
            "Tu amigo está pasando por un mal momento. ¿Eres comprensivo/a con él?"},
            new string[] {"comprensivo/a", "nuevo/a", "único/a", "listo/a"},
            new string[] {"Sí, lo escucho y le doy espacio si lo necesita.",
            "Trato de entender lo que siente y estar ahí.",
            "Le digo que lo supere, no me gusta hablar de eso."},
            "understanding"),

        //creativo/a
        new Question(new string[]{"Estás en una competencia de arte. ¿Qué tan [_____] eres?",
            "Estás en una competencia de arte. ¿Qué tan creativo/a eres?"},
            new string[] {"creativo/a", "triste", "pacífico/a", "simpático/a"},
            new string[] {"Me encanta inventar ideas nuevas.",
            "Siempre hago cosas originales y divertidas.",
            "No tengo ideas, solo copio a otros."},
            "creative"),

        //entusiasta
        new Question(new string[]{"¿Cómo actúas cuando comienzas una actividad nueva? ¿Eres [_____]?",
            "¿Cómo actúas cuando comienzas una actividad nueva? ¿Eres entusiasta?"},
            new string[] {"entusiasta", "aburrido/a", "triste", "cerrado/a"},
            new string[] {"¡Sí! Empiezo con mucha energía y alegría.",
            "Siempre estoy emocionado de aprender cosas nuevas.",
            "Normalmente no tengo ganas de empezar nada."},
            "enthusiastic"),

        //fácil
        new Question(new string[]{"¿Te parece [_____] aprender otro idioma?",
            "¿Te parece fácil aprender otro idioma?"},
            new string[] {"fácil", "insensible", "triste", "compasivo/a"},
            new string[] {"Sí, se me da bien y me divierte.",
            "Aprender idiomas me resulta natural.",
            "No puedo aprender nada, es muy confuso."},
            "easy"),

        //fiel
        new Question(
            new string[]{"Tus amigos pueden confiar en ti. ¿Eres una persona [_____] con ellos?",
            "Tus amigos pueden confiar en ti. ¿Eres una persona fiel con ellos?"},
            new string[]{"fiel", "egoísta", "cerrado", "malo"},
            new string[]{"Sí, siempre estoy con mis amigos cuando me necesitan.",
            "Nunca hablo mal de mis amigos y los ayudo en todo.",
            "No me gusta ayudar a mis amigos. Ellos pueden hacer todo solos."},
            "faithful"),

        //gracioso/a
        new Question(new string[]{"¿Qué haces cuando quieres hacer reír a alguien? ¿Eres [_____]?",
            "¿Qué haces cuando quieres hacer reír a alguien? ¿Eres gracioso/a?"},
            new string[] {"gracioso/a", "responsable", "triste", "entusiasta"},
            new string[] {"Hago caras divertidas y todos se ríen.",
            "Siempre tengo un buen chiste.",
            "No soy bueno con los chistes, lo evito."},
            "funny"),

        //honesto/a
        new Question(new string[]{"¿Dices la verdad aunque sea difícil? ¿Eres [_____]?",
            "¿Dices la verdad aunque sea difícil? ¿Eres honesto/a?"},
            new string[] {"honesto/a", "tranquilo/a", "paciente", "único/a"},
            new string[] {"Siempre digo la verdad.",
            "Sí, la honestidad es importante para mí.",
            "Prefiero mentir si me conviene."},
            "honest"),

        //imaginativo/a
        new Question(new string[]{"Cuando tienes tiempo libre, ¿te gusta hacer cosas creativas o pensar de forma [_____]?",
            "Cuando tienes tiempo libre, ¿te gusta hacer cosas creativas o pensar de forma imaginativa?"},
            new string[] {"imaginativo/a", "honesto/a", "malo/a", "paciente"},
            new string[] {"Sí, me gusta inventar ideas nuevas y hacer cosas diferentes.",
            "A veces dibujo, escribo o hago juegos con ideas locas. Es divertido.",
            "Prefiero hacer lo mismo cada día. No me gustan las cosas raras."},
            "imaginative"),

        //inteligente
        new Question(new string[]{"¿Te gusta aprender cosas nuevas? ¿Eres [_____]?",
            "¿Te gusta aprender cosas nuevas? ¿Eres inteligente?"},
            new string[] {"inteligente", "honesto/a", "fiel", "gracioso/a"},
            new string[] {"Sí, me encanta leer y resolver problemas.",
            "Me gusta entender cómo funcionan las cosas.",
            "No me interesa estudiar o aprender."},
            "intelligent"),

        //leal
        new Question(new string[]{"¿Qué tan [_____] eres con tus amigos cercanos?",
            "¿Qué tan leal eres con tus amigos cercanos?"},
            new string[] {"leal", "listo/a", "imaginativo/a", "incapaz"},
            new string[] {"Siempre estoy con ellos, pase lo que pase.",
            "Nunca los abandono en momentos difíciles.",
            "Si tengo algo mejor que hacer, no los veo."},
            "loyal"),

        //listo/a
        new Question(new string[]{"¿Piensas rápido cuando hay un problema? ¿Eres [_____]?",
            "¿Piensas rápido cuando hay un problema? ¿Eres listo/a?"},
            new string[] {"listo/a", "único/a", "bondadoso/a", "cerrado/a"},
            new string[] {"Sí, encuentro soluciones rápido.",
            "Siempre tengo una buena idea cuando algo va mal.",
            "No sé qué hacer en situaciones difíciles."},
            "bright, smart"),

        //mejor
        new Question(new string[]{"Quiero crecer como persona. ¿Qué haces para ser [_____] en tu vida?",
            "Quiero crecer como persona. ¿Qué haces para ser mejor en tu vida?"},
            new string[]{"mejor", "insensible", "abierto/a", "gracioso/a"},
            new string[]{"Trato de escuchar a los demás y aprender cosas nuevas todos los días.",
            "Me esfuerzo por ayudar, leer, y ser más consciente de mis acciones.",
            "No pienso en cambiar nada. Estoy bien como estoy."},
            "best"),

        //nuevo/a
        new Question(new string[]{"A muchas personas les gusta probar cosas diferentes. ¿Cuándo fue la última vez que hiciste algo [_____]?",
            "A muchas personas les gusta probar cosas diferentes. ¿Cuándo fue la última vez que hiciste algo nuevo?"},
            new string[] {"nuevo/a", "nervioso/a", "comprensivo/a", "alegre"},
            new string[] {"La semana pasada fui a un restaurante diferente y probé una comida que nunca había comido.",
            "Hace poco empecé un hobby distinto y me gusta mucho.",
            "No me gusta cambiar mi rutina. Siempre hago lo mismo."},
            "new"),

        //paciente
        new Question(new string[]{"¿Cómo reaccionas cuando tienes que esperar mucho tiempo? ¿Eres [_____]?",
            "¿Cómo reaccionas cuando tienes que esperar mucho tiempo? ¿Eres paciente?"},
            new string[] {"paciente", "amable", "insensible", "valiente"},
            new string[] {"Espero tranquilo/a sin problema.",
            "Sí, entiendo que algunas cosas toman tiempo.",
            "No puedo esperar, me enojo rápido."},
            "patient"),

        //pacífico/a
        new Question(new string[]{"¿Qué haces cuando hay un conflicto? ¿Eres [_____] o te molestas?",
            "¿Qué haces cuando hay un conflicto? ¿Eres pacífico/a o te molestas?"},
            new string[] {"pacífico/a", "entusiasta", "nuevo/a", "cerrado/a"},
            new string[] {"Prefiero hablar y resolverlo calmadamente.",
            "Trato de mantener la calma siempre.",
            "Grito y discuto mucho cuando algo va mal."},
            "peaceful"),

        //positivo/a
        new Question(new string[]{"¿Qué haces cuando alguien comete un error? ¿Eres [_____]?",
            "¿Qué haces cuando alguien comete un error? ¿Eres positivo/a?"},
            new string[] {"positivo/a", "talentoso/a", "incapaz", "cerrado/a"},
            new string[] {"Doy ánimos y trato de ver lo bueno.",
            "Sí, siempre intento encontrar lo positivo.",
            "Me enojo y solo veo lo malo."},
            "positive"),

        //responsable
        new Question(new string[]{"¿Eres una persona [_____] en la escuela o el trabajo?",
            "¿Eres una persona responsable en la escuela o el trabajo?"},
            new string[] {"responsable", "simpático/a", "cerrado/a", "egoísta"},
            new string[] {"Sí, siempre cumplo con mis tareas.",
            "Llego a tiempo y hago lo que me piden.",
            "Nunca hago lo que debo, siempre olvido las cosas."},
            "responsible"),

        //sensible
        new Question(new string[]{"¿Te afectan mucho las emociones de los demás? ¿Eres [_____]?",
            "¿Te afectan mucho las emociones de los demás? ¿Eres sensible?"},
            new string[] {"sensible", "insensible", "cerrado/a", "nervioso/a"},
            new string[] {"Sí, me preocupo mucho por cómo se sienten los demás.",
            "A veces lloro cuando veo a alguien triste.",
            "No entiendo por qué la gente se emociona tanto."},
            "sensitive"),

        //simpático/a
        new Question(new string[]{"¿Qué tan [_____] eres cuando conoces gente nueva?",
            "¿Qué tan simpático/a eres cuando conoces gente nueva?"},
            new string[] {"simpático/a", "incapaz", "único/a", "aburrido/a"},
            new string[] {"Siempre saludo con una sonrisa.",
            "Me gusta hacer sentir cómodos a los demás.",
            "No hablo y no me importa si les caigo mal."},
            "nice/friendly"),

        //talentoso/a
        new Question(new string[]{"¿Qué tipo de cosas haces bien? ¿Eres [_____] en algo?",
            "¿Qué tipo de cosas haces bien? ¿Eres talentoso/a en algo?"},
            new string[] {"talentoso/a", "valiente", "simpático/a", "leal"},
            new string[] {"Sí, soy bueno/a para tocar música.",
            "Tengo talento para pintar y escribir.",
            "No tengo ninguna habilidad especial."},
            "talented"),

        //tranquilo
        new Question(new string[]{"¿Cómo te comportas en situaciones difíciles? ¿Eres [_____] o nervioso/a?",
            "¿Cómo te comportas en situaciones difíciles? ¿Eres tranquilo/a o nervioso/a?"},
            new string[] {"tranquilo/a", "talentoso/a", "fiel", "nuevo/a"},
            new string[] {"Mantengo la calma y pienso con claridad.",
            "Sí, respiro profundo y no pierdo el control.",
            "Me pongo muy tenso/a y grito."},
            "calm/quiet"),

        //único/a
        new Question(new string[]{"¿Te gusta ser como los demás o prefieres ser [_____]?",
            "¿Te gusta ser como los demás o prefieres ser único/a?"},
            new string[] {"único/a", "amable", "alegre", "aburrido/a"},
            new string[] {"Me gusta ser diferente y especial.",
            "Sí, creo que cada persona debe ser original.",
            "Solo quiero parecerme a los demás."},
            "unique"),

        //valiente
        new Question(new string[]{"¿Qué haces cuando tienes miedo? ¿Eres [_____]?",
            "¿Qué haces cuando tienes miedo? ¿Eres valiente?"},
            new string[] {"valiente", "incapaz", "listo/a", "alegre"},
            new string[] {"Enfrento mis miedos sin dudar.",
            "Trato de ser fuerte aunque tenga miedo.",
            "Me escondo o huyo de todo."},
            "brave"),

        //aburrido/a
        new Question(new string[]{"No quiero salir con alguien [_____]. ¿Cómo te diviertes cuando estás con otras personas?",
            "No quiero salir con alguien aburrido. ¿Cómo te diviertes cuando estás con otras personas?"},
            new string[] {"aburrido/a", "creativo/a", "entusiasta", "único/a"},
            new string[] {"Me encanta contar historias, hacer reír a la gente y probar cosas nuevas con mis amigos.",
            "Siempre busco actividades divertidas y me gusta que todos se sientan bien.",
            "Normalmente no hago mucho. Me gusta estar en silencio y no salgo mucho con nadie."},
            "boring"),

        //cerrado/a
        new Question(new string[]{"Las personas [_____] tienen miedo de probar cosas nuevas. ¿Tú qué piensas sobre salir de tu zona de confort?",
        "Las personas cerradas tienen miedo de probar cosas nuevas. ¿Tú qué piensas sobre salir de tu zona de confort?"},
            new string[] {"cerradas", "malas", "leales", "simpáticas"},
            new string[] {"Me encanta probar cosas nuevas, viajar, conocer gente y aprender de otras culturas.",
            "Siempre estoy dispuesto a hacer algo nuevo y salir a lugares diferentes.",
            "No me gusta cambiar mis costumbres. Prefiero quedarme en lo que ya conozco."},
            "closed (minded)"),

        //egoísta
        new Question(new string[]{"¿Compartes tus cosas con los demás o eres [_____]?",
            "¿Compartes tus cosas con los demás o eres egoísta?"},
            new string[] {"egoísta", "imaginativo/a", "aburrido/a", "leal"},
            new string[] {"Creo que compartir es muy importante. Siempre trato de ayudar y dar a los demás lo que necesito.",
            "Me gusta pensar en los demás antes que en mí. Ayudarme también me hace feliz.",
            "Yo siempre pienso primero en lo que quiero. Si alguien más quiere algo, que lo consiga por su cuenta."},
            "selfish"),

        //incapaz
        new Question(new string[]{"Algunas personas dicen que no pueden hacer ciertas tareas. ¿Te consideras [_____] cuando tienes que hacer algo difícil?",
            "Algunas personas dicen que no pueden hacer ciertas tareas. ¿Te consideras incapaz cuando tienes que hacer algo difícil?"},
            new string[] {"incapaz", "abierto/a", "leal", "simpático/a"},
            new string[] {"Siempre trato de hacer mi mejor esfuerzo y no me doy por vencido fácilmente.",
            "Me siento seguro de que puedo hacer cualquier cosa si me esfuerzo lo suficiente.",
            "No soy bueno en eso. Siempre pido ayuda porque creo que no puedo hacerlo."},
            "incapable"),

        //insensible
        new Question(new string[]{"Las personas [_____] no se preocupan por los sentimientos de los demás. ¿Qué haces tú cuando alguien está triste?",
            "Las personas insensibles no se preocupan por los sentimientos de los demás. ¿Qué haces tú cuando alguien está triste?"},
            new string[] {"insensibles", "sensibles", "graciosas", "amables"},
            new string[] {"Siempre trato de escuchar y dar apoyo a los demás cuando están pasando por un mal momento.",
            "Me gusta ayudar a los demás y hacerlos sentir mejor cuando se sienten tristes.",
            "No entiendo por qué la gente se pone triste. No me importa mucho cómo se sienten."},
            "insensitive"),

        //malo/a
        new Question(new string[]{"No me gustaría pasar tiempo con alguien [_____] que siempre está causando problemas. ¿Qué piensas de tratar bien a los demás?",
            "No me gustaría pasar tiempo con alguien malo que siempre está causando problemas. ¿Qué piensas de tratar bien a los demás?"},
            new string[] {"malo", "bueno", "amable", "tranquilo"},
            new string[] {"Creo que es importante ser amable y respetuoso con todos. Tratar bien a los demás siempre trae cosas buenas.",
            "Siempre trato de ser cordial y respetuoso, incluso cuando no estamos de acuerdo.",
            "No me importa causar problemas. Si alguien se molesta, no es mi responsabilidad."},
            "bad"),

        //nervioso/a
        new Question(new string[]{"Las personas [_____] se sienten inseguras cuando tienen que hablar con alguien. ¿Cómo te sientes cuando tienes que conocer a alguien nuevo?",
            "Las personas nerviosas se sienten inseguras cuando tienen que hablar con alguien. ¿Cómo te sientes cuando tienes que conocer a alguien nuevo?"},
            new string[] {"nerviosas", "chistosas", "graciosas", "amables"},
            new string[] {"Me siento tranquilo y hablo sin problemas.",
            "No me molesta conocer gente nueva, me siento bien.",
            "Me siento muy nervioso y no sé qué decir."},
            "nervous"),

        //triste
        new Question(new string[]{"No me gusta salir con alguien [_____] que siempre parece estar apagado. ¿Cómo te sientes cuando las cosas no van bien?",
            "No me gusta salir con alguien que siempre parece estar apagado. ¿Cómo te sientes cuando las cosas no van bien?"},
            new string[] {"triste", "alegre", "listo/a", "honesto/a"},
            new string[] {"Siempre trato de ver el lado positivo de las cosas, incluso cuando hay dificultades.",
            "Me gusta pensar que todo mejorará y siempre encuentro algo bueno.",
            "Me siento triste y a veces no sé cómo superar las cosas."},
            "sad"),

        //acompañar
        new Question(new string[]{"¿Te gusta [_____] a tus amigos en eventos importantes?",
            "¿Te gusta acompañar a tus amigos en eventos importantes?"},
            new string[] {"acompañar", "acompaño", "crear", "crea"},
            new string[] {"Sí, me gusta acompañar a mis amigos porque me importa mucho estar con ellos.",
            "Siempre acompaño a mis amigos, especialmente en días especiales.",
            "No me interesa ir a eventos de otras personas, prefiero quedarme en casa."},
            "to accompany"),

        //admirar
        new Question(new string[]{"¿Puedes decirme una persona que tú [_____] mucho y por qué?",
            "¿Puedes decirme una persona que tú admiras mucho y por qué?"},
            new string[] {"admiras", "admiro", "vives", "vivimos"},
            new string[] {"Admiro a mi madre porque siempre trabaja duro y nunca se rinde.",
            "Admiro a mi profesor porque es muy paciente y sabe mucho.",
            "No admiro a nadie. Prefiero enfocarme solo en mí mismo."},
            "to admire"),

        //apoyar
        new Question(new string[]{"¿En qué formas puedes [_____] a alguien que tiene un mal día?",
            "¿En qué formas puedes apoyar a alguien que tiene un mal día?"},
            new string[] {"apoyar", "escribes", "escribir", "saber"},
            new string[] {"Puedo escuchar, hablar con ellos y recordarles que no están solos.",
            "Siempre trato de apoyar a mis amigos con palabras positivas.",
            "Si alguien tiene un mal día, no es mi problema. Que lo resuelva solo."},
            "to support"),

        //ayudar
        new Question(new string[]{"¿Cuándo fue la última vez que decidiste [_____] a alguien con una tarea difícil?",
            "¿Cuándo fue la última vez que decidiste ayudar a alguien con una tarea difícil?"},
            new string[] {"ayudar", "ayudé", "conoces", "conocer"},
            new string[] {"Ayer ayudé a mi hermano con su tarea de matemáticas.",
            "Ayudé a una señora mayor a cruzar la calle la semana pasada.",
            "Yo no ayudo con tareas difíciles. Cada quien debe aprender solo."},
            "to help"),

        //comer
        new Question(new string[]{"¿Qué prefieres [_____] durante una cita: pizza o algo más elegante?",
            "¿Qué prefieres comer durante una cita: pizza o algo más elegante?"},
            new string[] {"comer", "comemos", "permitir", "permiten"},
            new string[] {"Prefiero comer pasta o sushi, algo un poco especial.",
            "Me encanta comer pizza, pero también disfruto de comida elegante.",
            "No me gusta comer en citas. Es incómodo y prefiero no hacerlo."},
            "to eat"),

        //compartir
        new Question(new string[]{"¿Te gusta [_____] tus cosas con otras personas?",
            "¿Te gusta compartir tus cosas con otras personas?"},
            new string[] {"compartir", "apoyar", "insisten", "insistir"},
            new string[] {"Sí, me encanta compartir lo que tengo con mis amigos.",
            "Siempre comparto mi comida o útiles cuando alguien los necesita.",
            "No comparto nada. Si quieren algo, que lo consigan ellos."},
            "to share"),

        //comprender
        new Question(new string[]{"¿Puedes [_____] una situación difícil de otra persona fácilmente?",
            "¿Puedes comprender una situación difícil de otra persona fácilmente?"},
            new string[] {"comprender", "comprendo", "merece", "merecer"},
            new string[] {"Sí, trato de ponerme en su lugar y entender cómo se siente.",
            "Siempre intento comprender los problemas de mis amigos.",
            "No me importa lo que otros sientan. Sus problemas no son míos."},
            "to understand"),

        //conocer
        new Question(new string[]{"¿A quién te gustaría [_____] mejor este año?",
            "¿A quién te gustaría conocer mejor este año?"},
            new string[] {"conocer", "conozco", "saber", "sabemos"},
            new string[] {"Me gustaría conocer mejor a una amiga nueva de mi clase.",
            "Quiero conocer más a mis compañeros de trabajo.",
            "No quiero conocer a nadie. Estoy bien con mi grupo actual."},
            "to know"),

        //crear
        new Question(new string[]{"¿Qué te gusta [_____] cuando estás solo/a en casa?",
            "¿Qué te gusta crear cuando estás solo/a en casa?"},
            new string[] {"crear", "creamos", "vivo", "vivimos"},
            new string[] {"Me gusta crear dibujos o escribir historias.",
            "A veces creo canciones o proyectos de arte.",
            "No me gusta crear cosas. Solo veo televisión o duermo."},
            "to create"),

        //creer
        new Question(new string[]{"¿En qué cosas es importante [_____] para ti?",
            "¿En qué cosas es importante creer para ti?"},
            new string[] {"creer", "creo", "escoges", "escoger"},
            new string[] {"Creo en la amistad, el esfuerzo y la bondad.",
            "Es importante creer en uno mismo y en los demás.",
            "No creo en nada. Todo es una pérdida de tiempo."},
            "to believe"),

        //cumplir
        new Question(new string[]{"¿Qué metas quieres [_____] este año?",
            "¿Qué metas quieres cumplir este año?"},
            new string[] {"cumplir", "vivir", "permites", "permitir"},
            new string[] {"Quiero cumplir mis metas académicas y ser más organizado.",
            "Espero cumplir con mis planes de viajar y aprender más.",
            "No tengo metas. Solo dejo que las cosas pasen como sean."},
            "to complete, finish"),

        //escoger
        new Question(new string[]{"¿Cómo decides qué opción [_____] en una situación difícil?",
            "¿Cómo decides qué opción escoger en una situación difícil?"},
            new string[] {"escoger", "escojo", "conocen", "conocer"},
            new string[] {"Primero pienso en los pros y los contras antes de escoger.",
            "Escojo lo que creo que es mejor para todos.",
            "No me gusta escoger. Siempre dejo que otros decidan."},
            "to choose"),

        //escribir
        new Question(new string[]{"¿Te gusta [_____] cartas o mensajes a tus amigos?",
            "¿Te gusta escribir cartas o mensajes a tus amigos?"},
            new string[] {"escribir", "escribimos", "merecen", "merecer"},
            new string[] {"Sí, me encanta escribir cosas bonitas para mis amigos.",
            "Escribo cartas en cumpleaños o cuando quiero animarlos.",
            "No escribo nada. Me parece una pérdida de tiempo."},
            "to write"),

        //exigir
        new Question(new string[]{"¿Crees que está bien [_____] mucho de los demás?",
            "¿Crees que está bien exigir mucho de los demás?"},
            new string[] {"exigir", "exijo", "cumplimos", "cumplir"},
            new string[] {"A veces es necesario exigir cuando se necesita algo justo.",
            "Solo exijo lo necesario para que todos hagan su parte.",
            "Exijo que todos me sirvan, y si no lo hacen, me enojo."},
            "to demand"),

        //insistir (en)
        new Question(new string[]{"¿En qué situaciones sueles [_____] en algo?",
            "¿En qué situaciones sueles insistir en algo?"},
            new string[] {"insistir", "saber", "apoya", "comparte"},
            new string[] {"Insisto cuando sé que algo es lo correcto.",
            "Insisto en que las cosas se hagan con respeto.",
            "Insisto siempre en tener la razón, aunque esté equivocado."},
            "to insist on"),

        //merecer
        new Question(new string[]{"¿Qué tipo de persona [_____] tu respeto?",
            "¿Qué tipo de persona merece tu respeto?"},
            new string[] {"merece", "merezco", "resolvemos", "resolver"},
            new string[] {"Alguien honesto, responsable y que ayuda a los demás.",
            "Merecen respeto quienes luchan por lo que creen.",
            "Nadie merece mi respeto hasta que me lo prueben."},
            "to deserve"),

        //nominar
        new Question(new string[]{"¿A quién te gustaría [_____] para un premio especial?",
            "¿A quién te gustaría nominar para un premio especial?"},
            new string[] {"nominar", "nomino", "exiges", "conocer"},
            new string[] {"Me gustaría nominar a mi mejor amigo por ser siempre tan generoso.",
            "Nominaría a mi hermana porque siempre ayuda a todos.",
            "No nominaría a nadie. Nadie lo merece."},
            "to nominate"),

        //permitir
        new Question(new string[]{"¿Cuándo es importante [_____] a alguien hacer algo?",
            "¿Cuándo es importante permitir a alguien hacer algo?"},
            new string[] {"permitir", "crear", "exigir", "comparte"},
            new string[] {"Es importante permitir a otros expresarse libremente.",
            "Permitimos a nuestros amigos tomar decisiones importantes.",
            "No me gusta permitir nada. Prefiero tener el control."},
            "to allow"),

        //preservar
        new Question(new string[]{"¿Por qué es importante [_____] la naturaleza?",
            "¿Por qué es importante preservar la naturaleza?"},
            new string[] {"preservar", "preservamos", "apoyas", "escribir"},
            new string[] {"Para que las futuras generaciones puedan disfrutarla también.",
            "Preservamos la naturaleza para evitar la contaminación.",
            "No me importa preservar nada. No es mi problema."},
            "to preserve"),

        //recibir
        new Question(new string[]{"¿Qué es lo más bonito que has [_____] de otra persona?",
            "¿Qué es lo más bonito que has recibido de otra persona?"},
            new string[] {"recibido", "recibo", "creamos", "creer"},
            new string[] {"Una carta hecha a mano por mi mejor amigo.",
            "Una sonrisa sincera cuando la ayudé.",
            "No me interesa lo que recibo. Nunca es suficiente."},
            "to receive"),

        //resolver
        new Question(new string[]{"¿Cómo te gusta [_____] los problemas con tus amigos?",
            "¿Cómo te gusta resolver los problemas con tus amigos?"},
            new string[] {"resolver", "resuelvo", "exigir", "nominas"},
            new string[] {"Me gusta hablar calmadamente y escuchar a todos.",
            "Resuelvo los conflictos con respeto y honestidad.",
            "No resuelvo nada. Si hay problemas, dejo de hablarles."},
            "to resolve"),

        //saber
        new Question(new string[]{"¿Qué cosa nueva te gustaría [_____] este año?",
            "¿Qué cosa nueva te gustaría saber este año?"},
            new string[] {"saber", "sé", "conocer", "conozco"},
            new string[] {"Me gustaría saber más sobre arte y cultura.",
            "Quiero saber cómo tocar un instrumento musical.",
            "No quiero saber nada nuevo. Ya tengo suficiente."},
            "to know"),

        //vivir
        new Question(new string[]{"¿Dónde te gustaría [_____] en el futuro?",
            "¿Dónde te gustaría vivir en el futuro?"},
            new string[] {"vivir", "vivo", "permitimos", "permitir"},
            new string[] {"Me gustaría vivir en una ciudad tranquila con naturaleza.",
            "Quiero vivir cerca del mar algún día.",
            "No quiero vivir en ningún lugar. Todos me parecen malos."},
            "to live")

    };
   
};