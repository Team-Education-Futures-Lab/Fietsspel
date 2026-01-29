# Fietsspel - Education Futures Lab

Welkom bij de repository van het **Fietsspel**. Dit spel is ontwikkeld voor leerlingen van het Schakelcollege (Yonders, leeftijd 14-18 jaar) om hen op een interactieve manier de Nederlandse verkeersregels aan te leren. De gameplay is geïnspireerd op *Subway Surfers*.

## 📌 Projectinformatie
* **Github:** [Team-Education-Futures-Lab/Fietsspel](https://github.com/Team-Education-Futures-Lab/Fietsspel)
* **Engine:** Unity 6 (Versie: `6000.2.3f1`)
* **Input System:** Unity New Input System
* **Doelgroep:** Schakelcollege studenten (14-18 jaar)

---

## 🎮 Game Design & Levels

Het spel is opgedeeld in drie kernfasen, waarbij de focus ligt op herhaling en visuele ondersteuning (zien, horen, lezen).

### Level 1: Onderdelen verzamelen
De speler verzamelt fietsonderdelen. Bij elk onderdeel verschijnt de naam in beeld en wordt deze uitgesproken. Als beloning ontvangt de speler een **fietssleutel**.

### Level 2: Verkeersborden en Tekens (Lopend & Fietsend)
In dit level leert de speler de betekenis van verkeersborden.
* **Segment 1 (Wandelend):** Borden spawnen langs het pad. De speler moet informatie verzamelen (doel: elk bord 7x zien voor optimale leeropbrengst).
* **Transitie:** De speler loopt langs een fietsenstalling en gebruikt de sleutel uit Level 1 om de fiets te pakken.
* **Segment 2 (Fietsend):** De speler vervolgt de weg op het fietspad en reageert op verkeerstekens.

### Level 3: Toepassing (De "Toets")
Interactieve verkeerssituaties waarbij de speler moet beslissen: stoppen of doorfietsen?
* Bij een fout antwoord volgt extra uitleg en komt de vraag vaker terug.
* Te veel fouten betekent terug naar Level 2 voor extra oefening.

---

## 🛠 Status & To-Do (Level 2)

Er is momenteel een stevige basis, maar de volgende onderdelen moeten nog worden geïmplementeerd of uitgewerkt:

### Design & Gameplay
- [ ] **Visueel:** Water toevoegen aan de vijver in het eerste segment.
- [ ] **Popups:** De UI-canvas voor verkeersinformatie (7x per level) verder uitwerken.
- [ ] **Obstakels:** Modderpoelen of andere obstakels toevoegen aan de laatste 3 wandel-segmenten.
- [ ] **Segmenten:** De invulling van de laatste 3 segmenten bepalen.

### Technisch
- [ ] **Transitie:** Mechanisme maken voor de overgang van lopen naar fietsen.
- [ ] **AI/Events:** Een achtervolgende hond (motivatie om te rennen) implementeren in Level 1 & 2.
- [ ] **Interactie:** Fietsenrek-interactie met de sleutel uit Level 1.
- [ ] **UI:** Homescreen uitbreiden naar 4 knoppen (Start, Uitleg game, Verkeersregels, Credits).

---

## 📅 Logboek & Meetings (Highlights)

* **09-09-2025:** Eerste stakeholder meeting. Besluit genomen om opnieuw te bouwen in Unity 6 voor betere code-structuur, met behoud van de bestaande art-style.
* **07-10-2025:** Brainstorm over 'levens' in de game. Speler mag 3 fouten maken voordat het "Game Over" is. Introductie van het "7x herhalen" principe.
* **21-10-2025:** Concept voor Level 3 aangescherpt (informatie toepassen vs. informatie zenden in Level 2).
* **30-10-2025:** Bezoek aan het Schakelcollege. Inspiratie opgedaan van visuele bordjes aan het plafond voor lokaal-aanduidingen.
* **20-11-2025:** Speelduur vastgesteld op 15-20 minuten. Deadline voor eerste build op USB: 9 december.

---

## 🚀 Aan de slag
1. Kloon de repository.
2. Open het project in **Unity 6 (6000.2.3f1)**.
3. Zorg dat je de juiste modules voor de target build hebt geïnstalleerd.