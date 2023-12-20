# IDAS2-BDAS2-semestralni_prace-Dvorak-Polivka
==========================================================================================
# Přihlašovací údaje do aplikace:
## Role: administrátor
username: admin
heslo: admin

## Role: doktor
username: doktor
heslo: doktor

## Role: sestra
username: sestra
heslo: sestra

## Role: asistent
username: asistent
heslo: asistent

## Role: host
přihlášení pomocí tlačítka "Pokračovat bez přihlášení"
==========================================================================================
# Přihlašovací údaje do reálných emailových schránek
(kvůli dvoufázovému ověření přihlašovacích údajů do aplikace.)

## Email pro roli admin
Email: spravce.upcehospital@gmail.com
Heslo: UPCEhospital123

## Email pro roli doktor
Email: doktor.upcehospital@gmail.com
Heslo: UPCEhospital123

## Email pro roli sestra
Email: sestra.upcehospital@gmail.com
Heslo: UPCEhospital123

## Email pro roli asistent
Email: asistent.upcehospital@seznam.cz
Heslo: UPCEhospital123

==========================================================================================
# Popis aplikace
Aplikace slouží jako interní nemocniční systém evidující data o pacientech do Oracle databáze.
Uživately jsou zaměstnanci, kteří můžou mít v aplikaci různou roli. Na základě této role se otevře příslušný pohled aplikace.
Pro otestování aplikace zadejte jedny z výše uvedených přihlašovacích údajů. 
V aplikaci funguje dvoufázové ověření přes email, proto je nutností se nejprve přihlásit na danou emailovou schránku.
Po stisku tlačítka přihlásit přijde na příslušný email ověřovací kód, jež je nutný zadat do aplikace.

Každý uživatel si může editovat základní údaje, fotografii a heslo.
Admin - Má možnost zjednodušeně přididávat nové uživatele (zaměstnance), má možnost se na jednotlivé uživatele emulovat a může editovat veškeré tabulky z db. Též vidí výpis logů a systém. katalogu.

Doktor - Může si vyhledat pacienta podle rodného čísla, upravit mu data(přidat hospitalizaci, nemoc, lék atd.). Do osobních anamnéz se zapisuje automaticky. Může si stáhnout do PDF podrobnou zprávu o pacientovi.
	Dále si může zobrazit seznam všech pacientů, všech svých podřízených, všech léků které předepsal, hospitalizací a může přijímat nového pacienta. Též si může zobrazit info a statistiky.
 
Sestra - V omezenějším zobrazení si může vyhledat konkrétního pacienta, ale nemůže např. předepisovat léky. Dále vidí dávkování léků pro aktuálně hospitalizované pacienty na jejím oddělení, dále si může zobrazit všechny hospitalizace a její podřízené a info.

Asistent - V podstatě jako sestra ale místo zobrazení léků může pacienty přijímat.

Host - Může se jen seznámit s doktory.

Ukončení aplikace proběhne při zavření okna.
==========================================================================================
# Požadavky
1. ✅min. 10 tabulek navrženého datového modelu i s číselníky; •
2. ✅min. 2 číselníky, v dokumentaci bude uvedeno, proč byly tabulky vybrány jako číselníky; • (role a oddělení)
3. ✅Každý umělý primární klíč bude mít vytvořenou sekvenci; •
4. ✅min. 3 pohledy – logicky využité, různého typu; pohledy je nutné využít pro výpis hodnot;
5. ✅min. 3 funkce různého typu s odpovídající složitostí, triviální a podobné funkce nebudou uznány, každá z funkcí musí mít odlišný výstup, tedy pracovat s různými operacemi;
6. ✅min. 4 uložené procedury různého typu s odpovídající složitostí, triviální a podobné procedury nebudou uznány, každá z procedur musí mít odlišný výstup, tedy pracovat s různými operacemi, procedura může data zpracovávat i dávkově; 
7. ✅min. 2 triggery různého typu opět odpovídající složitostí, triviální a podobné spouště nebudou uznány;
8. ✅Bude umožňovat uložit vybraný binární obsah do databáze a následně jej i z databáze získat (a pokud se bude jednat o obrázek, tak i v rámci aplikace zobrazit). Binární obsah bude možné skrz DA vložit, změnit či odstranit. Pro tento úkol vytvořte ve svém schématu speciální tabulku. Tabulku navrhněte tak, aby kromě samotného binární obsahu umožnila uložit doplňkové informace, jako např.: název souboru, typ souboru, přípona souboru, datum nahrání, datum modifikace, kdo provedl jakou operaci. Binárním obsahem může být kromě obrázku i jakýkoliv soubor např. PDF či DOCX apod. •
9. ✅Bude využívat minimálně 3 plnohodnotné formuláře (ošetření vstupních polí, apod.) pro vytváření nebo modifikaci dat v tabulkách, ostatní potřebné formuláře jsou samozřejmostí. 
10. ✅Datová vrstva aplikace bude v rámci vybraných PL/SQL bloků pracovat minimálně s jedním implicitním kurzorem a jedním explicitním kurzorem.
11. ✅DA mohou plnohodnotně využívat pouze registrovaní uživatelé, neregistrovaný uživatel má velmi omezený výpis obsahu.
12. ✅DA umožňuje vyhledávat a zobrazovat výsledky o všech přístupných datech v jednotlivých tabulkách dle svého oprávnění. V případě tabulky obsahující BLOB pak zobrazí název dokumentu/obrázku/jiného binárního souboru dle zvoleného tématu a návazné informace alespoň ze dvou tabulek.  •
13. ✅DA umožňuje vkládání či modifikaci dat skrz uložené procedury! 
14. ✅V DA nejsou viditelné ID a ani nelze vyhledávat a ani vyplňovat jakékoliv ID, aplikace je uživatelsky přívětivá.
15. ✅Aplikace využívá triggery k logování, zasílání zpráv mezi uživateli, apod.  •
16. ✅DA využívá plnohodnotné vlastní funkce, které jsou vhodně nasazeny.
17. ✅Grafické rozhraní DA bude funkční a bude umožňovat editovat jakýkoliv záznam, který je načtený z databáze.
18. ✅DA řádně pracuje s transakcemi a má ošetřenou práci tak, aby nedošlo k nepořádku v datech.
19. ✅DA využívá z datové vrstvy vlastní hierarchický dotaz, který je vhodně využit dle tématu semestrální práce.
20. ✅DA eviduje a spravuje údaje o všech uživatelích, kteří mají do aplikace přístup. •
21. ✅DA umožňuje pracovat i s číselníky.
22. ✅DA má implementované veškerá pravidla, omezení, apod., která byla popsána v projektu z předmětu BDAS1/IDAS1 a nebyla řešena na datové vrstvě.
23. ✅DA je navržena tak, aby uchovávala historii o vkládání či úpravách jednotlivých záznamů, toto je zobrazeno pouze uživatelům s rolí Administrátor.  •
24. ✅V DA existuje funkcionalita, které umožňuje nezobrazovat osobní údaje jiným uživatelům jako například rodné číslo, telefon, číslo účtu, apod. Toto neplatí pro uživatele v roli Administrátor, ty mají plný přístup všude.
25. ✅DA umožňuje přidávat, modifikovat a mazat záznamy ve všech tabulkách dle oprávnění uživatele.
26. ✅Aplikace bude mít menu nastaveno tak, že je možné z jedné karty přepnout na všechny ostatní, tak aby byla zaručena příjemná uživatelská správa.
27. ✅Všechny tabulky musí být naplněny řádnými daty, nikoliv zkušebními.
28. ✅Aplikace se skládá z hlavního okna, kde má možnost neregistrovaný uživatel procházet povolené položky menu. Hlavní okno aplikace také umožňuje přihlásit registrovaného uživatele.
29. ✅Administrátor může spravovat jakákoliv data a zároveň se může přepnout (emulovat) na jakéhokoliv jiného uživatele. •
30. ✅Uživatel si nemůže sám zvolit při registraci svoji roli, vždy obdrží roli s nejnižšími právy a poté jej může změnit administrátor.
31. ✅Databázová aplikace bude umožňovat výpis všech použitých databázových objektů  v semestrální práci (využijte systémový katalog).
32. ✅Všechny číselníky se v DA chovají jako číselníky, tzn. že bude využit např. combobox, apod. Data z tabulky označená jako číselník nebude uživatel ručně zapisovat.

==========================================================================================
