# AI Logbook

## 1. Změna z grid-based na tick-based pohyb

- **Použitý prompt:** Současný grid-based systém nahraď tick-based systémem, implementuj herní smyčku a plynulý pohyb.
- **Popis:** Provedl jsem refaktorizaci systému pohybu hráče. Původní grid-based systém (kde se hráč posouval o celé bloky na každé stisknutí klávesy) byl nahrazen tick-based systémem pro plynulý pohyb.
- **Provedené změny:**
  - **`GameForm.cs`:** 
    - Byla implementována herní smyčka (`GameLoop`) s využitím `System.Windows.Forms.Timer` běžící přibližně na 60 FPS (interval 16 ms).
    - Implementován výpočet `deltaTime` pro zajištění plynulosti a nezávislosti rychlosti pohybu na snímkovací frekvenci (framerate).
    - Přidáno sledování stavu kláves (události `KeyDown` a `KeyUp`) a plynulý pohyb po osách X a Y s normalizací při diagonálním pohybu.
    - Zapnuto `DoubleBuffered = true` pro zamezení blikání obrazovky při překreslování (`Invalidate()`).
    - Odstraněno vykreslování mřížky na pozadí.
  - **`Player.cs`:** 
    - Pozice hráče (`X`, `Y`) se nyní mění plynule (souřadnice představují přímo pixely namísto indexů mřížky).
    - Přidána vlastnost `Speed` (rychlost pohybu v pixelech za sekundu).
- **Ověření:** Po implementaci jsem kód zkontroloval a v některých částech logiky v `GameForm.cs` lehce upravil. Testoval jsem funkčnost a plynulost se závěrem, že vše funguje bez zpoždění.