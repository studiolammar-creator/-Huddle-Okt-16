# Mokka: Blazor + Tailwind

De volledige Mokka-app uit Figma (Huddle 16 Oktober, pagina "🎨 Iteration - V1.1.1", node `38:11`) als Blazor-app met Tailwind CSS v4 en het Mokka-thema uit `../theme/mokka.css`.

## Starten

Nodig: .NET 9 SDK en Node.js 20+.

```bash
cd blazor
npm install          # eenmalig: Tailwind CLI
dotnet run           # bouwt ook wwwroot/app.css, daarna http://localhost:5180
```

Tijdens het werken: `npm run css:watch` in een tweede terminal, zodat nieuwe Tailwind-klassen meteen in de CSS komen.

## Schermen

| Route | Figma-scherm | Wat |
|---|---|---|
| `/` | Mokka / Home | Merkpagina: hero, "Zo werkt het", bento, de zaak, langskomen. Login of avatar rechtsboven. |
| `/menu` | Mokka / Menu (+ gast) | Begroeting, zoeken, categorieën, productkaarten met snel toevoegen. |
| `/product/{slug}` | Mokka / Product | Maat, melk, aantal en "Toevoegen". |
| `/bestelling` | Mokka / Bestelling (+ gast) | Winkelmand, afhalen, opmerking, betalen (alleen ingelogd), overzicht. |
| `/bevestiging` | Mokka / Bevestiging | Bestelticket met nummer en status. |
| `/bestellingen` | Mokka / Bestellingen | Lopende en eerdere bestellingen, "Bestel opnieuw". |
| `/profiel` | Mokka / Profiel | Stempelkaart, vaste bestelling, instellingen, uitloggen. |
| `/login`, `/registreren` | Mokka / Login (varianten samengevoegd) | Tabs Inloggen/Registreren. `?terug=afrekenen` toont "Nog één stap." en stuurt na het inloggen terug naar de bestelling. |
| `/wachtwoord-vergeten`, `/wachtwoord-vergeten/verstuurd`, `/nieuw-wachtwoord` | Herstelflow | Eindigt met de melding "Wachtwoord gewijzigd". |

De gast- en ingelogde varianten uit Figma zijn telkens één pagina die op `AppState.IsLoggedIn` reageert.

## Flow

- Gasten kunnen alles bekijken en toevoegen. Pas bij afrekenen vraagt de app om in te loggen.
- `Bestellingen` en `Profiel` sturen gasten door naar `/login`.
- De melding "Toegevoegd aan je bestelling" (feedbackkleuren success) verschijnt bij elk toevoegen en verdwijnt na 5 seconden.

## Structuur

| Pad | Wat |
|---|---|
| `Services/AppState.cs` | Sessiestaat per browsertab: login, winkelmand (met 2 demoproducten), bestelnummer, stempels, historiek, melding. |
| `Components/StateComponent.cs` | Basisklasse: hertekent een pagina als `AppState` verandert. |
| `Models/Catalog.cs` | Producten, maten, melksoorten en prijsnotatie (nl-BE). |
| `Components/Pages` | De pagina's hierboven, auth-schermen in `Pages/Auth`. |
| `Components/Ui` | Herbruikbare stukken: `Icon`, `Tabbalk`, `ProductCard`, `RadioCard`, `Stepper`, `TextField`, `Toast`, `AuthShell`, `BackButton`, `AccountButton`. |
| `Styles/app.css` | Tailwind-ingang: thema-import plus knop-, veld- en kaartklassen (`btn`, `field`, `card`, `segmented` ...). |
| `wwwroot/img` | Foto's uit Figma (512 px productfoto's, hero en zaak). |
| `wwwroot/icons` | Lucide-iconen (uit Figma en `lucide-static`), getoond als CSS-mask in de tekstkleur. |

De app draait volledig interactief (Interactive Server, zonder prerender), zodat de staat bewaard blijft zolang de tab open is. Er is geen database: herladen zet alles terug.

## Van Figma naar code

- **Kleuren**: alleen de shadcn-tokens uit het thema (`bg-primary`, `text-muted-foreground`, `bg-success-subtle` ...). Geen losse hexwaarden.
- **Typografie**: `text-hero`, `text-display`, `text-title`, `text-body`, `text-body-strong`, `text-small`, `text-caption`.
- **Raster**: standaard Tailwind-spacing = het 4px-raster.
- **Radius**: `rounded-lg` = 12 px.

## Toegankelijkheid

- Knoppen, velden en tabbladen zijn 44 px hoog; de segmenten in een segmented control 38 px (boven het WCAG 2.2-minimum van 24 px).
- Formuliervelden hebben labels, `aria-invalid` en `aria-describedby` voor fout- en hulptekst.
- Radiokeuzes zijn echte radio's in een `fieldset` met `legend`.
- Focus is zichtbaar via `outline-ring`; animaties alleen bij `prefers-reduced-motion: no-preference`.

## Let op

De code is niet gecompileerd tijdens het genereren (geen .NET SDK in die omgeving). Draai `dotnet build` en meld eventuele fouten.
