# Camels

## Backend fejlesztés - Teve Nyilvántartó (Camel Registry) CRUD API implementáció

A feladat egy ASP.NET Core Minimal API alapú szolgáltatás elkészítése, amely alkalmas
tevék (Camels) adatainak kezelésére. A fejlesztés során SQLite adatbázist és Entity
Framework Core-t kell használni. Az elkészült API-hoz átlátható dokumentációt és
tesztelő felületet kell biztosítani.

### Adatmodell (Camel entitás)

A pontos tulajdonságok definiálása a fejlesztő feladata, de az alábbi mezők megléte elvárt:
- Id: Egyedi azonosító.
- Name: A teve neve (kötelező mező).
- Color: A teve színe.
- HumpCount: Púpok száma (validáció: csak 1 vagy 2 lehet).
- LastFed: Utolsó etetés ideje.

### Funkcionális követelmények (Végpontok)

Az API-nak az alábbi 5 végpontot kell tartalmaznia:

1. **Új teve létrehozása** (POST)
    - Validálja a bemeneti adatokat (pl. púpok száma).
    - Siker esetén a válasz: 201 Created státuszkód és a létrehozott objektum (a generált ID-val együtt).
2. **Tevék listázása** (GET)
    - Visszaadja az összes rendszerben lévő tevét egy listában.
3. **Egy adott teve lekérdezése** (GET /{id})
    - ID alapján visszaadja a kért teve adatait.
    - Ha az entitás nem létezik: 404 Not Found.
4. **Teve adatainak módosítása** (PUT vagy PATCH /{id})
    - Meglévő teve adatainak frissítése.
    - Ha az entitás nem létezik: 404 Not Found.
5. **Teve törlése** (DELETE /{id})
    - ID alapján törli az adatbázisból a tevét.

### Technikai követelmények

- **Framework:** .NET 8 (vagy újabb).
- **API Architektúra:** Kizárólag Minimal API (Controller-ek használata nélkül).
- **Adatbázis:** SQLite.
    - Az alkalmazásnak kezelnie kell az adatbázis fájl létrejöttét (pl. EF CoreMigrations vagy EnsureCreated használatával).
- **ORM:** Entity Framework Core.

- **Dokumentáció és UI:**
    - OpenAPI (Swagger) specifikáció: Az API végpontjainak szabványos leírása generálódjon le automatikusan (pl. Swashbuckle vagy beépített Microsoft.AspNetCore.OpenApi segítségével).
    - Swagger UI: Legyen elérhető egy interaktív webes felület (pl. /swagger útvonalon), ahol a végpontok könnyen kipróbálhatók böngészőből.

- **Tesztelés: xUnit** keretrendszer használata unit tesztekhez.

- **Külső könyvtárak:**
    - Framework jellegű, mindent átfogó könyvtárak (pl. ABP Framework) használata tilos.
    - Kisebb, célzott library-k (pl. FluentValidation, AutoMapper stb.) használata megengedett, a fejlesztő döntése szerint.


## Frontend fejlesztés – Mini Teve Nyilvántartó (Angular)

Készíts egy egyszerű Angular alkalmazást, amely egy meglévő REST API-n keresztül tevék
adatainak listázását és szerkesztését valósítja meg.

### Adatmodell (Camel)
Az alábbi mezők megléte kötelező:
- id: egyedi azonosító
- name: string, kötelező
- humpCount: number, csak 1 vagy 2 lehet

### Funkcionális követelmények

1. Lista nézet
    - A rendszerben lévő tevék megjelenítése listában.
    - A lista Bootstrap alapú táblázatként jelenjen meg.
    - Minden sorban legyen lehetőség a szerkesztésre.

2. Űrlap
    - Ugyanaz az űrlap szolgáljon új teve létrehozására és meglévő teve szerkesztésére.
    - Az űrlap Bootstrap stílusú form elemeket használjon.
    - Validációk:
        - name kötelező, minimum 2 karakter
        - humpCount kötelező, értéke csak 1 vagy 2 lehet
    - Hibás mezők vizuálisan legyenek kiemelve.
    - Mentés:
        - új teve esetén POST
        - szerkesztés esetén PUT
    - Mentés után a lista frissüljön.

### API végpontok

- GET /api/camels
- POST /api/camels
- PUT /api/camels/{id}

### Technikai követelmények

- Angular 17 vagy újabb
- TypeScript
- Reactive Forms
- Angular HttpClient használata
- API base URL environment.ts fájlban
- Bootstrap integrálása (npm vagy CDN)
- Egyedi CSS használata megengedett az alap stílusok finomhangolására
- Hibás API válasz esetén jelenjen meg egy egyszerű, Bootstrap stílusú hibaüzenet

### Tesztelés
- Legalább egy unit teszt a form validációira


## Megvalósítás

A backend oldali megvalósításhoz .NET 8-at használtam, Entity Framework-el és AutoMapper-el a kifelé való kommunikáció megkönnyítéséért. Alapértelmezett útvonalon (https://localhost:7178/swagger/index.html) megtalálható indítás után az API swagger dokumentációja, és a CRUD műveletekhez használt használt útvonalak.

- (GET) /camels
    - Az összes teve egyed listázása
- (GET) /camels/{id}
    - Adott azonosítójú teve lekérése
- (POST) /camels
    - Új teve egyed létrehozása
- (PUT) /camels/{id}
    - Adott azonosítójú teve módosítása
- (DELETE) /camels/{id}
    - Adott azonosítójú teve törlése

Az adatbázis code first alapon futáskor készül el. A Camels.WebAPI projektben megtalálható appsettings.json fájlba lehet állítani a generálódó .db fájl létrehozásának és elérhetőségének helyét. Új adatbázissal való első futtatáskor a program alapértelmezetten seedert futtat két "dummy" teve objektum generálásáért a könnyű tesztelésért. Az appsettings.json seed elemének false-ra való állításval ezt kikapcsolhatjuk.


A frontend oldalhoz Angular 21-et használtam. A projekthez két enviroment fájl tartozik: 
1. A specifikációban megtalálható /api/camels elérhetőséget használja (enviroment.ts)
    - Futtatás: ng serve
2. A fentebb említett ASP.net WebAPI kapcsolatát használja (enviroment.aspnet.ts)
    - Futtatás: ng serve --configuration=aspnet

Futtatás előtt az **npm install** paranccsal telepíteni kell az alkalmazást.

A lehetséges összekapcsolás érdekében a frontend oldalon elérhetőek a WebAPI-ban extrának számító mezők is.