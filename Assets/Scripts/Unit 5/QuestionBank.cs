using System.Collections.Generic;

public static class QuestionBank
{
    public static List<Question> Questions = new List<Question>() {
        //abierto/a
        new Question(new string[]{"Me gusta conocer gente diferente. ¿Eres una persona [_____] a nuevas ideas y culturas?",
            "Me gusta conocer gente diferente. ¿Eres una persona abierta a nuevas ideas y culturas?"},
            new string[]{"abierta", "cerrada", "nerviosa", "egoísta"},
            new string[]{"Sí, me encanta aprender de otras personas y explorar diferentes formas de pensar.",
            "Siempre trato de escuchar con respeto y entender otros puntos de vista.",
            "No me interesa cambiar mi forma de pensar. Ya sé lo que funciona para mí."},
            "open"),
        
        //alegre
        new Question(new string[]{"Te invitan a una fiesta sorpresa. ¿Eres una persona [_____] en ese tipo de eventos?",
            "Te invitan a una fiesta sorpresa. ¿Eres una persona alegre en ese tipo de eventos?"},
            new string[] {"alegre", "triste", "cerrado/a", "nervioso/a"},
            new string[] {"¡Claro! Me encanta bailar y reír con todos.",
            "Sí, siempre sonrío y trato de que todos se diviertan.",
            "Prefiero no hablar con nadie en las fiestas."},
            "happy"),

        //amable
        new Question(new string[]{"Tengo una familia muy grande. ¿Qué tan [_____] eres con la gente nueva?",
            "Tengo una familia muy grande. ¿Qué tan amable eres con la gente nueva?"},
            new string[] {"amable", "egoísta", "cerrado/a", "nervioso/a"},
            new string[] {"Mi familia también es grande, estoy acostumbrada.",
            "Siempre me encanta conocer gente nueva. ¡Cuanto más, mejor!",
            "No me gusta conocer gente nueva."},
            "friendly"),

        //bondadoso/a
        new Question(new string[]{"¿Cómo reaccionas cuando alguien necesita ayuda? ¿Eres [_____]?",
            "¿Cómo reaccionas cuando alguien necesita ayuda? ¿Eres bondadoso/a?"},
            new string[] {"bondadoso/a", "egoísta", "aburrido/a", "cerrado/a"},
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
            new string[] {"cariñoso/a", "insensible", "aburrido/a", "cerrado/a"},
            new string[] {"Siempre les doy abrazos y paso tiempo con ellos.",
            "Les escribo mensajes lindos y los escucho.",
            "No me gusta hablar de sentimientos."},
            "caring"),

        //chistoso/a
        new Question(new string[]{"¿Te gusta hacer reír a los demás? ¿Te consideras [_____]?",
            "¿Te gusta hacer reír a los demás? ¿Te consideras chistoso/a?"},
            new string[] {"chistoso/a", "aburrido/a", "listo/a", "triste"},
            new string[] {"Sí, siempre tengo un chiste listo.",
            "Me encanta contar historias graciosas.",
            "No me gusta contar chistes ni reírme mucho."},
            "funny"),

        //compasivo/a
        new Question(new string[]{"¿Qué haces cuando ves a alguien sufriendo? ¿Eres [_____]?",
            "¿Qué haces cuando ves a alguien sufriendo? ¿Eres compasivo/a?"},
            new string[] {"compasivo/a", "insensible", "egoísta", "aburrido/a"},
            new string[] {"Trato de ayudar y escuchar a la persona.",
            "Sí, siempre me preocupo por los demás.",
            "No me importa mucho lo que sienten los otros."},
            "compassionate"),

        //comprensivo/a
        new Question(new string[]{"Tu amigo está pasando por un mal momento. ¿Eres [_____] con él?",
            "Tu amigo está pasando por un mal momento. ¿Eres comprensivo/a con él?"},
            new string[] {"comprensivo/a", "nuevo/a", "cerrado/a", "aburrido/a"},
            new string[] {"Sí, lo escucho y le doy espacio si lo necesita.",
            "Trato de entender lo que siente y estar ahí.",
            "Le digo que lo supere, no me gusta hablar de eso."},
            "understanding"),

        //creativo/a
        new Question(new string[]{"Estás en una competencia de arte. ¿Qué tan [_____] eres?",
            "Estás en una competencia de arte. ¿Qué tan creativo/a eres?"},
            new string[] {"creativo/a", "aburrido/a", "cerrado/a", "nervioso/a"},
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

        //fácil (EDIT)
        new Question(new string[]{"¿Te parece [_____] aprender otro idioma?",
            "¿Te parece fácil aprender otro idioma?"},
            new string[] {"fácil", "difícil", "triste", "complicado/a"},
            new string[] {"Sí, se me da bien y me divierte.",
            "Aprender idiomas me resulta natural.",
            "No puedo aprender nada, es muy confuso."},
            "easy"),

        //fiel
        new Question(new string[]{"¿Cómo defines a una pareja ideal? ¿Debe ser [_____]?",
            "¿Cómo defines a una pareja ideal? ¿Debe ser fiel?"},
            new string[] {"fiel", "mentiroso/a", "aburrido/a", "cerrado/a"},
            new string[] {"Sí, la lealtad es lo más importante.",
            "Una relación necesita confianza.",
            "No importa si miente a veces."},
            "faithful"),

        //gracioso/a
        new Question(new string[]{"¿Qué haces cuando quieres hacer reír a alguien? ¿Eres [_____]?",
            "¿Qué haces cuando quieres hacer reír a alguien? ¿Eres gracioso/a?"},
            new string[] {"gracioso/a", "aburrido/a", "triste", "cerrado/a"},
            new string[] {"Hago caras divertidas y todos se ríen.",
            "Siempre tengo un buen chiste.",
            "No soy bueno con los chistes, lo evito."},
            "funny"),

        //honesto/a
        new Question(new string[]{"¿Dices la verdad aunque sea difícil? ¿Eres [_____]?",
            "¿Dices la verdad aunque sea difícil? ¿Eres honesto/a?"},
            new string[] {"honesto/a", "mentiroso/a", "cerrado/a", "nervioso/a"},
            new string[] {"Siempre digo la verdad.",
            "Sí, la honestidad es importante para mí.",
            "Prefiero mentir si me conviene."},
            "honest"),

        //imaginativo/a
        new Question(new string[]{"¿Qué haces cuando estás solo/a? ¿Eres [_____] en tu tiempo libre?",
            "¿Qué haces cuando estás solo/a? ¿Eres imaginativo/a en tu tiempo libre?"},
            new string[] {"imaginativo/a", "aburrido/a", "nervioso/a", "cerrado/a"},
            new string[] {"Me invento juegos o escribo cuentos.",
            "Sí, uso mi imaginación para divertirme.",
            "No hago nada, solo me aburro."},
            "imaginative"),

        //inteligente
        new Question(new string[]{"¿Te gusta aprender cosas nuevas? ¿Eres [_____]?",
            "¿Te gusta aprender cosas nuevas? ¿Eres inteligente?"},
            new string[] {"inteligente", "perezoso/a", "aburrido/a", "cerrado/a"},
            new string[] {"Sí, me encanta leer y resolver problemas.",
            "Me gusta entender cómo funcionan las cosas.",
            "No me interesa estudiar o aprender."},
            "intelligent"),

        //leal
        new Question(new string[]{"¿Qué tan [_____] eres con tus amigos cercanos?",
            "¿Qué tan leal eres con tus amigos cercanos?"},
            new string[] {"leal", "mentiroso/a", "cerrado/a", "egoísta"},
            new string[] {"Siempre estoy con ellos, pase lo que pase.",
            "Nunca los abandono en momentos difíciles.",
            "Si tengo algo mejor que hacer, no los veo."},
            "loyal"),

        //listo/a
        new Question(new string[]{"¿Piensas rápido cuando hay un problema? ¿Eres [_____]?",
            "¿Piensas rápido cuando hay un problema? ¿Eres listo/a?"},
            new string[] {"listo/a", "confundido/a", "nervioso/a", "aburrido/a"},
            new string[] {"Sí, encuentro soluciones rápido.",
            "Siempre tengo una buena idea cuando algo va mal.",
            "No sé qué hacer en situaciones difíciles."},
            "bright, smart"),

        //mejor (EDIT)
        new Question(new string[]{"Quiero crecer como persona. ¿Qué haces para ser [_____] en tu vida?",
            "Quiero crecer como persona. ¿Qué haces para ser mejor en tu vida?"},
            new string[]{"mejor", "egoísta", "triste", "cerrado"},
            new string[]{"Trato de escuchar a los demás y aprender cosas nuevas todos los días.",
            "Me esfuerzo por ayudar, leer, y ser más consciente de mis acciones.",
            "No pienso en cambiar nada. Estoy bien como estoy."},
            "best"),

        //nuevo/a
        new Question(new string[]{"¿Cómo te sientes al ser el/la [_____] en un grupo?",
            "¿Cómo te sientes al ser el/la nuevo/a en un grupo?"},
            new string[] {"nuevo/a", "cerrado/a", "viejo/a", "aburrido/a"},
            new string[] {"Es emocionante conocer a todos.",
            "Me gusta descubrir cosas nuevas.",
            "No hablo con nadie y me quedo solo/a."},
            "new"),

        //paciente
        new Question(new string[]{"¿Cómo reaccionas cuando tienes que esperar mucho tiempo? ¿Eres [_____]?",
            "¿Cómo reaccionas cuando tienes que esperar mucho tiempo? ¿Eres paciente?"},
            new string[] {"paciente", "nervioso/a", "cerrado/a", "aburrido/a"},
            new string[] {"Espero tranquilo/a sin problema.",
            "Sí, entiendo que algunas cosas toman tiempo.",
            "No puedo esperar, me enojo rápido."},
            "patient"),

        //pacífico/a
        new Question(new string[]{"¿Qué haces cuando hay un conflicto? ¿Eres [_____] o te molestas?",
            "¿Qué haces cuando hay un conflicto? ¿Eres pacífico/a o te molestas?"},
            new string[] {"pacífico/a", "violento/a", "nervioso/a", "cerrado/a"},
            new string[] {"Prefiero hablar y resolverlo calmadamente.",
            "Trato de mantener la calma siempre.",
            "Grito y discuto mucho cuando algo va mal."},
            "peaceful"),

        //positivo/a
        new Question(new string[]{"¿Qué haces cuando alguien comete un error? ¿Eres [_____]?",
            "¿Qué haces cuando alguien comete un error? ¿Eres positivo/a?"},
            new string[] {"positivo/a", "crítico/a", "triste", "cerrado/a"},
            new string[] {"Doy ánimos y trato de ver lo bueno.",
            "Sí, siempre intento encontrar lo positivo.",
            "Me enojo y solo veo lo malo."},
            "positive"),

        //responsable
        new Question(new string[]{"¿Eres una persona [_____] en la escuela o el trabajo?",
            "¿Eres una persona responsable en la escuela o el trabajo?"},
            new string[] {"responsable", "perezoso/a", "cerrado/a", "egoísta"},
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
            new string[] {"simpático/a", "grosero/a", "cerrado/a", "aburrido/a"},
            new string[] {"Siempre saludo con una sonrisa.",
            "Me gusta hacer sentir cómodos a los demás.",
            "No hablo y no me importa si les caigo mal."},
            "nice/friendly"),

        //talentoso/a
        new Question(new string[]{"¿Qué tipo de cosas haces bien? ¿Eres [_____] en algo?",
            "¿Qué tipo de cosas haces bien? ¿Eres talentoso/a en algo?"},
            new string[] {"talentoso/a", "aburrido/a", "cerrado/a", "incapaz"},
            new string[] {"Sí, soy bueno/a para tocar música.",
            "Tengo talento para pintar y escribir.",
            "No tengo ninguna habilidad especial."},
            "talented"),

        //tranquilo
        new Question(new string[]{"¿Cómo te comportas en situaciones difíciles? ¿Eres [_____] o te alteras?",
            "¿Cómo te comportas en situaciones difíciles? ¿Eres tranquilo/a o te alteras?"},
            new string[] {"tranquilo/a", "nervioso/a", "emocionado/a", "aburrido/a"},
            new string[] {"Mantengo la calma y pienso con claridad.",
            "Sí, respiro profundo y no pierdo el control.",
            "Me pongo muy tenso/a y grito."},
            "calm/quiet"),

        //único/a
        new Question(new string[]{"¿Te gusta ser como los demás o prefieres ser [_____]?",
            "¿Te gusta ser como los demás o prefieres ser único/a?"},
            new string[] {"único/a", "común", "cerrado/a", "aburrido/a"},
            new string[] {"Me gusta ser diferente y especial.",
            "Sí, creo que cada persona debe ser original.",
            "Solo quiero parecerme a los demás."},
            "unique"),

        //valiente
        new Question(new string[]{"¿Qué haces cuando tienes miedo? ¿Eres [_____]?",
            "¿Qué haces cuando tienes miedo? ¿Eres valiente?"},
            new string[] {"valiente", "nervioso/a", "débil", "cerrado/a"},
            new string[] {"Enfrento mis miedos sin dudar.",
            "Trato de ser fuerte aunque tenga miedo.",
            "Me escondo o huyo de todo."},
            "brave"),

        //abburido/a (EDIT)
        new Question(new string[]{"¿Qué haces cuando no tienes nada que hacer? ¿Te sientes [_____]?",
            "¿Qué haces cuando no tienes nada que hacer? ¿Te sientes aburrido/a?"},
            new string[] {"aburrido/a", "feliz", "entusiasta", "activo/a"},
            new string[] {"Sí, no sé qué hacer con mi tiempo libre.",
            "A veces me aburro si no tengo un plan.",
            "Siempre encuentro algo que hacer y me divierto."},
            "boring"),

        //cerrado/a (EDIT)
        new Question(new string[]{"¿Te gusta hablar de tus emociones? ¿Eres [_____] o no?",
            "¿Te gusta hablar de tus emociones? ¿Eres cerrado/a o no?"},
            new string[] {"cerrado/a", "abierto/a", "positivo/a", "sensible"},
            new string[] {"No me gusta compartir lo que siento.",
            "Prefiero guardarme todo para mí.",
            "Hablo libremente sobre cómo me siento."},
            "closed (minded)"),

        //egoísta (EDIT)
        new Question(new string[]{"¿Compartes tus cosas con los demás o eres [_____]?",
            "¿Compartes tus cosas con los demás o eres egoísta?"},
            new string[] {"egoísta", "amable", "generoso/a", "leal"},
            new string[] {"No me gusta compartir nada.",
            "Solo pienso en mí mismo/a.",
            "Me encanta dar cosas a los demás."},
            "selfish"),

        //incapaz (EDIT)
        new Question(new string[]{"¿Piensas que puedes hacer todo bien? ¿O te sientes [_____] a veces?",
            "¿Piensas que puedes hacer todo bien? ¿O te sientes incapaz a veces?"},
            new string[] {"incapaz", "capaz", "valiente", "fuerte"},
            new string[] {"A veces siento que no puedo lograr nada.",
            "Me bloqueo cuando tengo que hacer cosas difíciles.",
            "Confío en mis habilidades y lo intento."},
            "incapable"),

        //insensible (EDIT)
        new Question(new string[]{"¿Qué haces cuando alguien está llorando? ¿Eres [_____] o no te afecta?",
            "¿Qué haces cuando alguien está llorando? ¿Eres insensible o no te afecta?"},
            new string[] {"insensible", "sensible", "empático/a", "amable"},
            new string[] {"No me importa si la gente llora.",
            "No entiendo por qué se sienten así.",
            "Siempre trato de ayudar y consolar."},
            "insensitive"),

        //malo/a (EDIT)
        new Question(new string[]{"¿Cómo reaccionas cuando alguien es malo contigo? ¿Eres también [_____]?",
            "¿Cómo reaccionas cuando alguien es malo contigo? ¿Eres también malo/a?"},
            new string[] {"malo/a", "bueno/a", "amable", "tranquilo/a"},
            new string[] {"Sí, si me tratan mal, yo también respondo mal.",
            "A veces soy cruel si me provocan.",
            "Prefiero mantener la calma y perdonar."},
            "bad"),

        //nervioso/a (EDIT)
        new Question(new string[]{"¿Cómo te sientes antes de una presentación importante? ¿Estás [_____]?",
            "¿Cómo te sientes antes de una presentación importante? ¿Estás nervioso/a?"},
            new string[] {"nervioso/a", "tranquilo/a", "seguro/a", "valiente"},
            new string[] {"Sí, me tiemblan las manos y no puedo dormir.",
            "Me siento con muchas dudas y preocupaciones.",
            "Estoy tranquilo/a y preparado/a."},
            "nervous"),

        //triste (EDIT)
        new Question(new string[]{"¿Cómo te sientes cuando las cosas no salen bien? ¿Te pones [_____]?",
            "¿Cómo te sientes cuando las cosas no salen bien? ¿Te pones triste?"},
            new string[] {"triste", "feliz", "contento/a", "positivo/a"},
            new string[] {"Sí, a veces lloro cuando algo no funciona.",
            "Me pongo muy melancólico/a.",
            "Sigo adelante sin preocuparme mucho."},
            "sad"),

        //acompañar
        new Question(new string[]{"¿Te gusta [_____] a tus amigos en eventos importantes?", "¿Te gusta acompañar a tus amigos en eventos importantes?"},
            new string[] {"acompañar", "acompaño", "recibes", "crea"},
            new string[] {"Sí, me gusta acompañar a mis amigos porque me importa mucho estar con ellos.",
            "Siempre acompaño a mis amigos, especialmente en días especiales.",
            "No me interesa ir a eventos de otras personas, prefiero quedarme en casa."},
            "to accompany"),

        //admirar
        new Question(new string[]{"¿Puedes decirme una persona que tú [_____] mucho y por qué?", "¿Puedes decirme una persona que tú admiras mucho y por qué?"},
            new string[] {"admiras", "admiro", "comen", "vivimos"},
            new string[] {"Admiro a mi madre porque siempre trabaja duro y nunca se rinde.",
            "Admiro a mi profesor porque es muy paciente y sabe mucho.",
            "No admiro a nadie. Prefiero enfocarme solo en mí mismo."},
            "to admire"),

        //apoyar
        new Question(new string[]{"¿En qué formas puedes [_____] a alguien que tiene un mal día?", "¿En qué formas puedes apoyar a alguien que tiene un mal día?"},
            new string[] {"apoyar", "apoyamos", "escribes", "sabemos"},
            new string[] {"Puedo escuchar, hablar con ellos y recordarles que no están solos.",
            "Siempre trato de apoyar a mis amigos con palabras positivas.",
            "Si alguien tiene un mal día, no es mi problema. Que lo resuelva solo."},
            "to support"),

        //ayudar
        new Question(new string[]{"¿Cuándo fue la última vez que decidiste [_____] a alguien con una tarea difícil?", "¿Cuándo fue la última vez que decidiste ayudar a alguien con una tarea difícil?"},
            new string[] {"ayudar", "ayudé", "conoces", "crees"},
            new string[] {"Ayer ayudé a mi hermano con su tarea de matemáticas.",
            "Ayudé a una señora mayor a cruzar la calle la semana pasada.",
            "Yo no ayudo con tareas difíciles. Cada quien debe aprender solo."},
            "to help"),

        //comer
        new Question(new string[]{"¿Qué prefieres [_____] durante una cita: pizza o algo más elegante?", "¿Qué prefieres comer durante una cita: pizza o algo más elegante?"},
            new string[] {"comer", "comemos", "permito", "resuelves"},
            new string[] {"Prefiero comer pasta o sushi, algo un poco especial.",
            "Me encanta comer pizza, pero también disfruto de comida elegante.",
            "No me gusta comer en citas. Es incómodo y prefiero no hacerlo."},
            "to eat"),

        //compartir
        new Question(new string[]{"¿Te gusta [_____] tus cosas con otras personas?", "¿Te gusta compartir tus cosas con otras personas?"},
            new string[] {"compartir", "comparto", "cumple", "insisten"},
            new string[] {"Sí, me encanta compartir lo que tengo con mis amigos.",
            "Siempre comparto mi comida o útiles cuando alguien los necesita.",
            "No comparto nada. Si quieren algo, que lo consigan ellos."},
            "to share"),

        //comprender
        new Question(new string[]{"¿Puedes [_____] una situación difícil de otra persona fácilmente?", "¿Puedes comprender una situación difícil de otra persona fácilmente?"},
            new string[] {"comprender", "comprendo", "merece", "nominas"},
            new string[] {"Sí, trato de ponerme en su lugar y entender cómo se siente.",
            "Siempre intento comprender los problemas de mis amigos.",
            "No me importa lo que otros sientan. Sus problemas no son míos."},
            "to understand"),

        //conocer
        new Question(new string[]{"¿A quién te gustaría [_____] mejor este año?", "¿A quién te gustaría conocer mejor este año?"},
            new string[] {"conocer", "conozco", "preservas", "escribimos"},
            new string[] {"Me gustaría conocer mejor a una amiga nueva de mi clase.",
            "Quiero conocer más a mis compañeros de trabajo.",
            "No quiero conocer a nadie. Estoy bien con mi grupo actual."},
            "to know"),

        //crear
        new Question(new string[]{"¿Qué te gusta [_____] cuando estás solo/a en casa?", "¿Qué te gusta crear cuando estás solo/a en casa?"},
            new string[] {"crear", "creamos", "vivo", "cumplen"},
            new string[] {"Me gusta crear dibujos o escribir historias.",
            "A veces creo canciones o proyectos de arte.",
            "No me gusta crear cosas. Solo veo televisión o duermo."},
            "to create"),

        //creer
        new Question(new string[]{"¿En qué cosas es importante [_____] para ti?", "¿En qué cosas es importante creer para ti?"},
            new string[] {"creer", "creo", "escoges", "recibe"},
            new string[] {"Creo en la amistad, el esfuerzo y la bondad.",
            "Es importante creer en uno mismo y en los demás.",
            "No creo en nada. Todo es una pérdida de tiempo."},
            "to believe"),

        //cumplir
        new Question(new string[]{"¿Qué metas quieres [_____] este año?", "¿Qué metas quieres cumplir este año?"},
            new string[] {"cumplir", "cumplo", "vivís", "permiten"},
            new string[] {"Quiero cumplir mis metas académicas y ser más organizado.",
            "Espero cumplir con mis planes de viajar y aprender más.",
            "No tengo metas. Solo dejo que las cosas pasen como sean."},
            "to complete, finish"),

        //escoger
        new Question(new string[]{"¿Cómo decides qué opción [_____] en una situación difícil?", "¿Cómo decides qué opción escoger en una situación difícil?"},
            new string[] {"escoger", "escojo", "conocen", "preservamos"},
            new string[] {"Primero pienso en los pros y los contras antes de escoger.",
            "Escojo lo que creo que es mejor para todos.",
            "No me gusta escoger. Siempre dejo que otros decidan."},
            "to choose"),

        //escribir
        new Question(new string[]{"¿Te gusta [_____] cartas o mensajes a tus amigos?", "¿Te gusta escribir cartas o mensajes a tus amigos?"},
            new string[] {"escribir", "escribimos", "merecen", "resuelve"},
            new string[] {"Sí, me encanta escribir cosas bonitas para mis amigos.",
            "Escribo cartas en cumpleaños o cuando quiero animarlos.",
            "No escribo nada. Me parece una pérdida de tiempo."},
            "to write"),

        //exigir
        new Question(new string[]{"¿Crees que está bien [_____] mucho de los demás?", "¿Crees que está bien exigir mucho de los demás?"},
            new string[] {"exigir", "exijo", "cumplimos", "vivís"},
            new string[] {"A veces es necesario exigir cuando se necesita algo justo.",
            "Solo exijo lo necesario para que todos hagan su parte.",
            "Exijo que todos me sirvan, y si no lo hacen, me enojo."},
            "to demand"),

        //insistir (en)
        new Question(new string[]{"¿En qué situaciones sueles [_____] en algo?", "¿En qué situaciones sueles insistir en algo?"},
            new string[] {"insistir", "insisto", "apoya", "comparte"},
            new string[] {"Insisto cuando sé que algo es lo correcto.",
            "Insisto en que las cosas se hagan con respeto.",
            "Insisto siempre en tener la razón, aunque esté equivocado."},
            "to insist on"),

        //merecer
        new Question(new string[]{"¿Qué tipo de persona [_____] tu respeto?", "¿Qué tipo de persona merece tu respeto?"},
            new string[] {"merece", "merezco", "resolvemos", "sabemos"},
            new string[] {"Alguien honesto, responsable y que ayuda a los demás.",
            "Merecen respeto quienes luchan por lo que creen.",
            "Nadie merece mi respeto hasta que me lo prueben."},
            "to deserve"),

        //nominar
        new Question(new string[]{"¿A quién te gustaría [_____] para un premio especial?", "¿A quién te gustaría nominar para un premio especial?"},
            new string[] {"nominar", "nomino", "exiges", "cumplís"},
            new string[] {"Me gustaría nominar a mi mejor amigo por ser siempre tan generoso.",
            "Nominaría a mi hermana porque siempre ayuda a todos.",
            "No nominaría a nadie. Nadie lo merece."},
            "to nominate"),

        //permitir
        new Question(new string[]{"¿Cuándo es importante [_____] a alguien hacer algo?", "¿Cuándo es importante permitir a alguien hacer algo?"},
            new string[] {"permitir", "permitimos", "vivís", "comparte"},
            new string[] {"Es importante permitir a otros expresarse libremente.",
            "Permitimos a nuestros amigos tomar decisiones importantes.",
            "No me gusta permitir nada. Prefiero tener el control."},
            "to allow"),

        //preservar
        new Question(new string[]{"¿Por qué es importante [_____] la naturaleza?", "¿Por qué es importante preservar la naturaleza?"},
            new string[] {"preservar", "preservamos", "apoyas", "escriben"},
            new string[] {"Para que las futuras generaciones puedan disfrutarla también.",
            "Preservamos la naturaleza para evitar la contaminación.",
            "No me importa preservar nada. No es mi problema."},
            "to preserve"),

        //recibir
        new Question(new string[]{"¿Qué es lo más bonito que has [_____] de otra persona?", "¿Qué es lo más bonito que has recibido de otra persona?"},
            new string[] {"recibido", "recibo", "creamos", "permite"},
            new string[] {"Una carta hecha a mano por mi mejor amigo.",
            "Una sonrisa sincera cuando la ayudé.",
            "No me interesa lo que recibo. Nunca es suficiente."},
            "to receive"),

        //resolver
        new Question(new string[]{"¿Cómo te gusta [_____] los problemas con tus amigos?", "¿Cómo te gusta resolver los problemas con tus amigos?"},
            new string[] {"resolver", "resuelvo", "exigen", "nominas"},
            new string[] {"Me gusta hablar calmadamente y escuchar a todos.",
            "Resuelvo los conflictos con respeto y honestidad.",
            "No resuelvo nada. Si hay problemas, dejo de hablarles."},
            "to resolve"),

        //saber
        new Question(new string[]{"¿Qué cosa nueva te gustaría [_____] este año?", "¿Qué cosa nueva te gustaría saber este año?"},
            new string[] {"saber", "sé", "comparten", "cumplís"},
            new string[] {"Me gustaría saber más sobre arte y cultura.",
            "Quiero saber cómo tocar un instrumento musical.",
            "No quiero saber nada nuevo. Ya tengo suficiente."},
            "to know"),

        //vivir
        new Question(new string[]{"¿Dónde te gustaría [_____] en el futuro?", "¿Dónde te gustaría vivir en el futuro?"},
            new string[] {"vivir", "vivo", "permitimos", "escoge"},
            new string[] {"Me gustaría vivir en una ciudad tranquila con naturaleza.",
            "Quiero vivir cerca del mar algún día.",
            "No quiero vivir en ningún lugar. Todos me parecen malos."},
            "to live")

    };
   
};