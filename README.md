# 📅 Osobní Plánovač (Personal Planner)

Moderní desktopová aplikace pro správu času, denních úkolů a událostí. Aplikace nabízí přehledný měsíční kalendář, detailní denní rozvrh a chytré funkce pro efektivní plánování.

<img width="1141" height="899" alt="image123" src="https://github.com/user-attachments/assets/9338d062-dfd7-4ad7-b4d0-9d01c9881065" />



## 🚀 Hlavní Funkce

### 1. 🗓️ Měsíční Kalendář
- Přehledné zobrazení dnů v měsíci.
- Indikace dnů, které obsahují naplánované aktivity (podbarvení).
- Jednoduchá navigace mezi měsíci.

### 2. 📝 Denní Plánovač
- Detailní časová osa dne (00:00 - 23:00).
- Možnost přidávat úkoly do konkrétních časů.
- **Vlastní časy:** Možnost vložit událost i mimo celé hodiny (např. 14:20).
<img width="422" height="839" alt="image" src="https://github.com/user-attachments/assets/6a19122c-d9b3-4841-8e7a-fd283704c92e" />

### 3. 🏷️ Kategorie a Barvy
- Každá událost má svou kategorii (Práce, Škola, Zábava, Sport, Ostatní).
- **Vlastní kategorie:** Uživatel si může vytvořit libovolnou vlastní kategorii přímo při zadávání.
- Vizuální odlišení v seznamu aktivit.

<img width="328" height="266" alt="image" src="https://github.com/user-attachments/assets/8c2ee549-cc93-491d-a00b-076660960509" />
### 4. ⚡ Chytré Zadávání Času (Smart Input)
- Při kliknutí na čas (např. 12:00) se automaticky předvyplní hodina (`12:`).
- Stačí dopsat pouze minuty a stisknout Enter.
- Integrovaná ochrana proti překlepům a nevalidním časům (např. nelze zadat 08:99).
- Nutnost vyplnit popis aktivity (prevence prázdných úkolů).

### 5. 🔍 Vyhledávání a Filtrování
- Samostatné okno pro přehled všech aktivit.
- Filtrování událostí podle kategorií.
- Okamžitý přehled o tom, kolik úkolů v dané kategorii máte.
<img width="328" height="266" alt="image" src="https://github.com/user-attachments/assets/8c2ee549-cc93-491d-a00b-076660960509" />

### 6. 🔔 Chytré Notifikace
- Aplikace běží na pozadí a hlídá váš čas.
- **Upozornění 1 hodinu předem:** Pokud máte naplánovanou akci, aplikace vás včas upozorní bublinou v systémové liště Windows.
<img width="380" height="166" alt="image" src="https://github.com/user-attachments/assets/8d1bd560-d592-4de0-a7b3-0fad75d10eec" />

## 🛠️ Použité Technologie

- **Jazyk:** C# (.NET 8.0)
- **Framework:** Windows Forms (WinForms)
- **Data:** JSON (System.Text.Json)
- **Architektura:** Oddělení datové vrstvy (`EventStorage`, `EventModel`) od grafického rozhraní (`Forms`).

---

## 🎮 Jak používat aplikaci

1. **Spuštění:** Otevřete aplikaci a uvidíte aktuální měsíc.
2. **Přidání události:**
   - Klikněte na konkrétní den.
   - V denním plánu klikněte na řádek s časem (např. 08:00).
   - Doplňte minuty, vyberte kategorii a napište popis.
3. **Úprava/Smazání:**
   - Pro úpravu klikněte na tlačítko `✎`.
   - Pro smazání klikněte na tlačítko `✖`.
4. **Hledání:**
   - V hlavním okně klikněte na tlačítko **"🔍 Přehled aktivit"**.
   - V horní liště vyberte kategorii pro filtrování.

---

## 📦 Instalace

1. Naklonujte tento repozitář.
2. Otevřete soubor `Osobni.Planovac1.sln` ve Visual Studio 2022/2026.
3. Spusťte projekt (F5).

---

## 👨‍💻 Autor
Anh Duc Bui
Tento projekt byl vytvořen jako součást výuky programování v C# a jako semestrální projekt.
