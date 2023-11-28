# IDAS2-BDAS2-semestralni_prace-Dvorak-Polivka
# Požadavky
1. ✅min. 10 tabulek navrženého datového modelu i s číselníky; •
2. min. 2 číselníky, v dokumentaci bude uvedeno, proč byly tabulky vybrány jako číselníky; •
3. ✅Každý umělý primární klíč bude mít vytvořenou sekvenci; •
4. min. 3 pohledy – logicky využité, různého typu; pohledy je nutné využít pro výpis hodnot;
5. min. 3 funkce různého typu s odpovídající složitostí, triviální a podobné funkce nebudou uznány, každá z funkcí musí mít odlišný výstup, tedy pracovat s různými operacemi;
6. min. 4 uložené procedury různého typu s odpovídající složitostí, triviální a podobné procedury nebudou uznány, každá z procedur musí mít odlišný výstup, tedy pracovat s různými operacemi, procedura může data zpracovávat i dávkově; 
7. min. 2 triggery různého typu opět odpovídající složitostí, triviální a podobné spouště nebudou uznány;
8. ✅Bude umožňovat uložit vybraný binární obsah do databáze a následně jej i z databáze získat (a pokud se bude jednat o obrázek, tak i v rámci aplikace zobrazit). Binární obsah bude možné skrz DA vložit, změnit či odstranit. Pro tento úkol vytvořte ve svém schématu speciální tabulku. Tabulku navrhněte tak, aby kromě samotného binární obsahu umožnila uložit doplňkové informace, jako např.: název souboru, typ souboru, přípona souboru, datum nahrání, datum modifikace, kdo provedl jakou operaci. Binárním obsahem může být kromě obrázku i jakýkoliv soubor např. PDF či DOCX apod. •
9. ✅Bude využívat minimálně 3 plnohodnotné formuláře (ošetření vstupních polí, apod.) pro vytváření nebo modifikaci dat v tabulkách, ostatní potřebné formuláře jsou samozřejmostí. 
10. Datová vrstva aplikace bude v rámci vybraných PL/SQL bloků pracovat minimálně s jedním implicitním kurzorem a jedním explicitním kurzorem.
11. DA mohou plnohodnotně využívat pouze registrovaní uživatelé, neregistrovaný uživatel má velmi omezený výpis obsahu.
12. ✅DA umožňuje vyhledávat a zobrazovat výsledky o všech přístupných datech v jednotlivých tabulkách dle svého oprávnění. V případě tabulky obsahující BLOB pak zobrazí název dokumentu/obrázku/jiného binárního souboru dle zvoleného tématu a návazné informace alespoň ze dvou tabulek.  •
13. ✅DA umožňuje vkládání či modifikaci dat skrz uložené procedury! 
14. ✅V DA nejsou viditelné ID a ani nelze vyhledávat a ani vyplňovat jakékoliv ID, aplikace je uživatelsky přívětivá.
15. ✅Aplikace využívá triggery k logování, zasílání zpráv mezi uživateli, apod.  •
16. ✅DA využívá plnohodnotné vlastní funkce, které jsou vhodně nasazeny.
17. ✅Grafické rozhraní DA bude funkční a bude umožňovat editovat jakýkoliv záznam, který je načtený z databáze.
18. ✅DA řádně pracuje s transakcemi a má ošetřenou práci tak, aby nedošlo k nepořádku v datech.
19. ✅DA využívá z datové vrstvy vlastní hierarchický dotaz, který je vhodně využit dle tématu semestrální práce.
20. ✅DA eviduje a spravuje údaje o všech uživatelích, kteří mají do aplikace přístup. •
21. DA umožňuje pracovat i s číselníky.
22. DA má implementované veškerá pravidla, omezení, apod., která byla popsána v projektu z předmětu BDAS1/IDAS1 a nebyla řešena na datové vrstvě.
23. ✅DA je navržena tak, aby uchovávala historii o vkládání či úpravách jednotlivých záznamů, toto je zobrazeno pouze uživatelům s rolí Administrátor.  •
24. V DA existuje funkcionalita, které umožňuje nezobrazovat osobní údaje jiným uživatelům jako například rodné číslo, telefon, číslo účtu, apod. Toto neplatí pro uživatele v roli Administrátor, ty mají plný přístup všude.
25. ✅DA umožňuje přidávat, modifikovat a mazat záznamy ve všech tabulkách dle oprávnění uživatele.
26. ✅Aplikace bude mít menu nastaveno tak, že je možné z jedné karty přepnout na všechny ostatní, tak aby byla zaručena příjemná uživatelská správa.
27. Všechny tabulky musí být naplněny řádnými daty, nikoliv zkušebními.
28. Aplikace se skládá z hlavního okna, kde má možnost neregistrovaný uživatel procházet povolené položky menu. Hlavní okno aplikace také umožňuje přihlásit registrovaného uživatele.
29. ✅Administrátor může spravovat jakákoliv data a zároveň se může přepnout (emulovat) na jakéhokoliv jiného uživatele. •
30. Uživatel si nemůže sám zvolit při registraci svoji roli, vždy obdrží roli s nejnižšími právy a poté jej může změnit administrátor.
31. ✅Databázová aplikace bude umožňovat výpis všech použitých databázových objektů  v semestrální práci (využijte systémový katalog).
32. Všechny číselníky se v DA chovají jako číselníky, tzn. že bude využit např. combobox, apod. Data z tabulky označená jako číselník nebude uživatel ručně zapisovat.

==========================================================================================
# Rozřazené požadavky
Relační model
- 3. nastavit sekvence/ auto-inkrement pro každý pk •

# developer
- 4. 3 pohledy 
- 5. 3 funkce
- 6. procedury
- 7. trigger
- 10. implicitní a explicitní kurzoz - možná pro vracení hesel z db 
- 13. procedury - pro veškeré vládání nebo modifikace
- 15. Aplikace využívá triggery k logování, zasílání zpráv mezi uživateli, apod
- 16. DA využívá plnohodnotné vlastní funkce, které jsou vhodně nasazeny.
- 19. DA využívá z datové vrstvy vlastní hierarchický dotaz, který je vhodně využit dle tématu semestrální práce.
- 27. Všechny tabulky musí být naplněny řádnými daty, nikoliv zkušebními.
 
- procedury pro login atd
- používat balíčky (bonus body)

# c#
- 9. 3 formuláře
- 11. neregistrovaný uživatel má omezené možnosti
- 12. DA umožňuje vyhledávat a zobrazovat výsledky o všech přístupných datech v jednotlivých tabulkách dle svého oprávnění. V případě tabulky obsahující BLOB pak zobrazí název dokumentu/obrázku/jiného binárního souboru dle zvoleného tématu a návazné informace alespoň ze dvou tabulek.
- 13. procedury - pro veškeré vládání nebo modifikace
- 15. Aplikace využívá triggery k logování, zasílání zpráv mezi uživateli, apod
- 16. DA využívá plnohodnotné vlastní funkce, které jsou vhodně nasazeny.
- 19. DA využívá z datové vrstvy vlastní hierarchický dotaz, který je vhodně využit dle tématu semestrální práce.
- 20. DA eviduje a spravuje údaje o všech uživatelích, kteří mají do aplikace přístup.
- 21. DA umožňuje pracovat i s číselníky.
- 22. DA má implementované veškerá pravidla, omezení, apod., která byla popsána v projektu z předmětu BDAS1/IDAS1 a nebyla řešena na datové vrstvě.
	PROCEDURÁLNÍ PRAVIDLA
	•	PACIENT
		o	Pokud byl u pacienta prováděným výkonem změna pohlaví, je nutné změnit položku pohlaví.
	•	UZITY_LEK
		o	Dávkování je uvedeno ve formě čísla „kolikrát za den“, nikoliv ve formátu „ráno – poledne – večer“
		o	Aby atribut dávkování dával smysl, nesmí být jeho hodnota rovno nule.
	•	PROVEDENY_VYKON
		o	Hrazeno pojišťovnou je ve formě datového typu boolean, kdy hodnota true znamená hrazeno pojišťovnou a hodnota false naopak.
	•	ZDRAVOTNI_POJISTOVNA
		o	Zkratka – jedná se o třímístný číselný kód, pod kterým pojišťovna vystupuje a ne zkratku odvozenou od názvu pojišťovny
	•	ADRESA
		o	Atribut stát v adrese musí být vyplněn ve formě dvoumístného alfabetického kódu státu.
- 23. DA je navržena tak, aby uchovávala historii o vkládání či úpravách jednotlivých záznamů, toto je zobrazeno pouze uživatelům s rolí Administrátor.
- 24. V DA existuje funkcionalita, které umožňuje nezobrazovat osobní údaje jiným uživatelům jako například rodné číslo, telefon, číslo účtu, apod. Toto neplatí pro uživatele v roli Administrátor, ty mají plný přístup všude.
- 25. DA umožňuje přidávat, modifikovat a mazat záznamy ve všech tabulkách dle oprávnění uživatele.
- 26. Aplikace bude mít menu nastaveno tak, že je možné z jedné karty přepnout na všechny ostatní, tak aby byla zaručena příjemná uživatelská správa.
- 28. Aplikace se skládá z hlavního okna, kde má možnost neregistrovaný uživatel procházet povolené položky menu. Hlavní okno aplikace také umožňuje přihlásit registrovaného uživatele.
- 29. Administrátor může spravovat jakákoliv data a zároveň se může přepnout (emulovat) na jakéhokoliv jiného uživatele.
- 30. Uživatel si nemůže sám zvolit při registraci svoji roli, vždy obdrží roli s nejnižšími právy a poté jej může změnit administrátor.
- 31. Databázová aplikace bude umožňovat výpis všech použitých databázových objektů  v semestrální práci (využijte systémový katalog).
- 32. Všechny číselníky se v DA chovají jako číselníky, tzn. že bude využit např. combobox, apod. Data z tabulky označená jako číselník nebude uživatel ručně zapisovat.


View doktora:
Pacient
- OSOBNÍ ÚDAJE po vyhledání RČ zobrazit osobní údaje (vše z entity pacient, 
	kontaktní údaje, adresa, zdravotní pojišťovna, zdravotní karta(kurak, alergik) (RU)
- ANAMNÉZA (R)
- ZÁKROK - provedené výkony (CRU)
- AKTUÁLNÍ NEMOCI - budou zde vypsané nemoci, co na to používá za léky(dávkování) a kdo mu to
		    předepsal, lze na nemoc napsat lék (CRUD)
- HOSPITALIZACE - prubeh hospitalizace a na jakem oddeleni

Moje oddělení
- hierarchický dotaz na podřízené plus jejich osobní údaje

Předepsané léky
- jen výpis jaké léky komu předepsal (úprava dávky)

Přijmout pacienta
- zadám RČ, když neexistuje -> vytvoření pacienta
	  - když existuje, tak mu vytvořím průběh hospitalizace a přiřadím ho na oddělení

