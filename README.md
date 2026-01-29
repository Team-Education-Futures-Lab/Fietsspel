# README.md

## Overdracht document

### Design
De bedoeling van level 2 is dat de speler begint op het wandelgedeelte waar verkeersborden moeten spawnen net zoals de onderdelen in level 1 (dit is nog niet geimplementeert) wanneer alle onderdelen verzameld zijn komt er een segment waar de speler een fiets pakt uit een stalling (met de eerder gewonnen fietsleutel na level 1) en overwisselt naar het fietspad gedeelte en daar moet reageren op de verkeersborden.

**Wat moet er nog gebeuren aan level 2:**
* Water in de vijver in het eerste segment
* De popups (het canvas met 2 kleine opzetjes van een popup) moeten nog verder uitgewerkt worden, de bedoeling van de popups is dat je die in beeld krijgt bij het verzamelen van een verkeersbord en in totaal 7x te zien krijgt gedurende het level.
* Invulling laatste 3 segmenten
* De laatste 3 segmenten hebben in het wandel gedeelte nog geen obstacles de indeling voor dat level moet nog verzonnen worden (misshien iets met een modderpoel ontwijken varieert de gameplay)

---

### Technical
**Github Link:** https://github.com/Team-Education-Futures-Lab/Fietsspel

De game wordt gemaakt in Unity 6 versie 6000.2.3f1. Er wordt voor de beweging gebruikt wordt gemaakt van het Input system. Voor level 2 moeten de overgang van lopen naar fietsen gemaakt worden. Ook hadden wij bedacht dat er misschien nog een hond of iets achter je aan rent die moet ook nog gemaakt worden in zowel level 1 als 2. Ook moet er nog na de borden 7x zien een fiets + fietsenrek langs de weg gezet moeten worden. Er was namelijk het idee om in level 2 ook echt de fiets te pakken in de game. Dat kan als de speler alle delen van de fiets in level 1 heeft opgepakt. Dan krijgt de speler namelijk een sleutel waar ze dan in level 2 weer de fiets mee kunnen pakken door de sleutel te gebruiken. Dit zodat het niet lijkt alsof ze de fiets stelen.

---

### Meeting notes

#### 09-09-2025
Vandaag hadden wij de eerste stakeholder meeting. Vandaag was het voorstellen aan iedereen ook de andere stakeholders daarna zijn we met Marja apart gaan zitten om onze game te bespreken en te kijken wat de bedoeling is. Wij hadden van te voren wat vragen opgeschreven voor de dingen die nog niet helemaal duidelijk waren voor ons.

**De vragen waren:**
* **Wat is de bedoeling van de game?**
De bedoeling van het spel is dat de leerlingen van het schakelcollege kunnen leren wat de regels zijn om te fietsen in Nederland. De inspiratie is Subway Surfer. Maar dan in verschillende levels. Ook willen we in het begin van de game een soort van slow-mo hebben om de controls uit te leggen en dan als de “tutorial” is afgelopen gaat er rustig een versnelling in komen.
In level 1 is het de bedoeling dat je de onderdelen van een fiets bij elkaar verzameld. Als je een onderdeel van een fiets hebt dan krijg je het woord in beeld van dat onderdeel en wordt het woord gesproken.
In level 2 is het de bedoeling om de verkeersborden en tekens op de weg te leren.
In level 3 is het de bedoeling om eenvoudige verkeersituaties te behandelen. Of je doorfietst of dat je stopt met fietsen.

* **Moeten we doorwerken aan de huidige game of mogen wij opnieuw beginnen?**
Wij mogen opnieuw beginnen met het maken van de game. Wij hebben daar ook voor gekozen omdat de game die er was vooral uit een tutorial is gemaakt en met een AI zoals chatGPT. De game was ook gemaakt in een oude versie van Unity. Wij hebben er wel voor gekozen om het uiterlijk van de game bijna helemaal over te nemen omdat daar niks mis mee was.

* **Moeten we in deze versie van Unity werken of mogen we een nieuwere versie gebruiken?**
Dat mochten wij zelf kiezen. Doordat wij hebben gekozen om de game opnieuw te maken hebben wij gekozen om een nieuwe versie van Unity te gebruiken. Wij gaan Unity versie 6000.2.3f1 gebruiken. Dit ook zodat we het input systeem kunnen gebruiken voor de bewegingen van het character.

* **Wat is de doelgroep?**
De doelgroep zijn de studenten van het schakelcollege op het Yonders. Dat zijn leerlingen van de leeftijd 14 tot en met 18 jaar.

#### 16-09-2025
Vandaag hadden we de tweede stakeholder meeting. Wij hadden weer wat vragen voor Marja over de game en wat er wel en niet in moet. Wij wisten namelijk niet zo goed hoe level 2 er uit zou moeten zien. Het idee van level 2 is dat je de borden en verkeerstekens gaat leren. Wij zaten te denken dat je misschien in de juiste baan rent/fietst als antwoord op de vraag. Dat waren allemaal goede ideeën. Het belangrijkste van level 2 is dat je de borden en tekens ziet, hoort en leest wat de betekening is van de borden en tekens.

#### 07-10-2025
Vandaag hadden we weer een meeting. Wij hadden geen vragen dus hebben onze game laten zien wat we tot nu toe hebben. Het was ook nog een soort van brainstorm sessie om te kijken wat we nog meer kunnen toevoegen in de game. Een van de dingen waarvan we dachten dat het misschien wel leuk zou zijn als je een reden zou hebben om in level 1 te rennen. Dus bijvoorbeeld dat er een hond achter je aan zit of iets in die richting. Ook vertelde Marja dat het belangrijk is dat in level 2 de borden en tekens minimaal 7 keer voorkomen zodat je hersenen het goed kunnen opslaan/onthouden en je het dus ook echt leert. Ook zaten we te denken om als je in level 2 een vraag fout hebt dat je dan niet meteen opnieuw hoeft te beginnen maar 3 vragen fout mag maken voordat je echt af bent, dus dat je eigenlijk een soort van levens hebt.

#### 21-10-2025
Voor level 2 en 3 hadden wij een ander idee dan in eerste instantie de bedoeling was. Wij willen namelijk in level 2 echt alleen maar de informatie geven en dan in level 3 het als een soort van “toets” de vragen te beantwoorden. Wij wilde dit in level 2 doen met een overzicht aan de zijkant van het scherm te plaatsen met alle tekens en verkeersborden met een soort teller met hoe vaak je het bord of teken hebt gehad. Dit kan ook in het menu als je hem op pauze zet. Je krijgt dan een knop in beeld als je alles minimaal 7 keer hebt gehad om naar het volgende level te gaan. In level 3 dan de informatie toepassen dit door vragen te stellen. Als je dan een vraag fout hebt dan krijg je extra informatie over dat bordt of teken en komt de vraag vaker voor. Als je dan X aantal vragen fout hebt achter elkaar dan moet je terug naar level 2 om de borden en tekens meer te oefenen.

Ook zaten wij te denken dat het misschien een idee is om level 2 nog rennend te houden zodat de leerlingen niet denken dat het oké is om te gaan fietsen zonder dat je de regels een beetje begrijpt en weet wat de bedoeling is als je gaat fietsen. Wij zijn daar nog met Marja in gesprek over gegaan en hebben nu bedacht om eerst een X aantal borden en tekens voorbij te laten komen en dus te leren en dan langs een fietsenstalling te lopen en een fiets te pakken en dan verder te fietsen. Ook heb je bedacht hoe je in level 2 een fiets kan pakken zonder dat het lijkt alsof je de fietst steelt. Aan het einde van level 1 als je de fiets helemaal bij elkaar hebt verzamelt krijg je een fietssleutel als beloning. In level 2 gebruik je die sleutel dan om je fiets van slot te halen en te gaan fietsen.

#### 30-10-2025
Vandaag zijn wij naar het schakelcollege gegaan. Wij hebben daar een rondleiding gekregen en hebben bij twee verschillende niveau’s meegekeken bij een les. Ook hebben we het gehad over de game en wat we er wel en niet in toevoegen. Ook hebben we erg leuke/handige bordjes aan het plafon zien hangen waar op staat waar een lokaal is plus een kleine tekening van wat je doet in dat lokaal. Dit willen wij eigenlijk toevoegen aan de game maar op het moment weten we nog niet zo goed hoe we dit kunnen toevoegen. Wij hebben daar veel informatie over het niveau van de groepen gekregen en kunnen hier zeker wat mee.

#### 11-11-2025
Prioriteiten liggen ook bij school en kijken wat mogelijk gaat zijn en wat geen prioriteit heeft. Volgende week een werkende animatie die goed loopt. Docenten en leerlingen handleiding gaan hun maken. Onze planning voor de aankomende week is dat de animatie werkend wordt en dat level 2 het wandelpad met de verkeersborden er gaat komen. Het homescreen aanpassen naar een scherm met 4 knoppen voor ook uitleg over hoe het spel werken en uitleg over de verkeersborden en regels.

#### 20-11-2025
Vandaag hadden we een meeting omdat we die van dinsdag verplaatst hadden. Dit was een online meeting. Wij hebben het gehad over de duur van de game omdat we niet zo goed wisten hoelang we die moesten maken. Wij hebben nu afgesproken dat die tussen de 15 en 20 minuten gaat duren. Ook hebben we afgesproken dat de we op 9 december een usb-stick met daarop een build van de game zetten zodat Marja die kan laten zien bij een meeting.
