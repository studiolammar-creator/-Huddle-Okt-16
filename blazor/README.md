# Mokka: Blazor + Tailwind

Het homescreen uit Figma (Huddle 16 Oktober, pagina "Iteration - V1.1.0", node `3068:72`) als Blazor-app met Tailwind CSS v4 en het Mokka-thema uit `../theme/mokka.css`.

## Starten

Nodig: .NET 9 SDK en Node.js 20+.

```bash
cd blazor
npm install          # eenmalig: Tailwind CLI
npm run css:build    # bouwt wwwroot/app.css (gebeurt ook automatisch bij dotnet build)
dotnet run           # http://localhost:5180
```

Tijdens het werken: `npm run css:watch` in een tweede terminal, zodat nieuwe Tailwind-klassen meteen in de CSS komen.

## Structuur

| Pad | Wat |
|---|---|
| `Components/Pages/Home.razor` | Het homescreen. Voorbeelddata staat onderaan in `@code`. |
| `Components/Ui/Icon.razor` | Icoon uit `wwwroot/icons` als CSS-mask, neemt de tekstkleur over. |
| `Components/Ui/ProductTile.razor` | Tegel in de rij "Iets anders?" met toevoegknop. |
| `Components/Ui/StampProgress.razor` | Stempelkaart, 7 van 10. |
| `Components/Ui/Tabbalk.razor` | Vaste tabbalk onderaan (Home, Menu, Bestellingen, Profiel). |
| `Models/Product.cs` | Records voor producten, de vaste bestelling en de zaakstatus. |
| `Styles/app.css` | Tailwind-ingang: importeert `../theme/mokka.css`. |
| `wwwroot/img`, `wwwroot/icons` | Foto's (2x) en iconen, rechtstreeks uit Figma geëxporteerd. |

## Van Figma naar code

- **Kleuren**: alleen de shadcn-tokens uit het thema (`bg-primary`, `text-muted-foreground`, `bg-info-subtle` ...). Geen losse hexwaarden.
- **Typografie**: de Figma-stijlen Mokka/Display, Titel, Body, Klein en Caption zijn de klassen `text-display`, `text-title`, `text-body`, `text-small` en `text-caption`.
- **Raster**: de standaard Tailwind-spacing is het 4px-raster van het ontwerp (`gap-6` = 24 px, `pt-14` = 56 px).
- **Radius**: `rounded-lg` = 12 px, `rounded-xl` = 16 px (de kaart "Je vaste").

## Toegankelijkheid

- Alle knoppen en tabbladen zijn minstens 44 px hoog.
- De tabbalk is een `nav` met `aria-current="page"` op het actieve tabblad.
- De toevoegknoppen hebben een label ("Cold brew toevoegen aan je bestelling"), en een `aria-live`-melding bevestigt het toevoegen.
- De stempelvoortgang wordt voorgelezen als "7 van 10 stempels".
- Productfoto's hebben de productnaam als alt-tekst.

## Nog te doen tijdens de workshop

- De routes `menu`, `bestellingen` en `profiel` bestaan nog niet.
- "Bestel opnieuw" en de toevoegknoppen bewaren de bestelling nu alleen in het geheugen van de pagina.
- De afhaallocatie-knop opent nog geen keuzelijst.
