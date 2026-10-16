# Huddle 16 oktober: Mokka

Mokka is een fictieve koffie-app: menu bekijken, koffie kiezen, bestellen en afhalen aan de toog.
Deze repo bevat voorlopig alleen het **thema**. De app zelf bouwen we tijdens de workshop op 16 oktober.

## Wat staat er al in

| Bestand | Inhoud |
|---|---|
| `theme/mokka.css` | shadcn/ui-tokens als CSS-variabelen, feedbackkleuren, radius, fonts en de Mokka-typeschaal, gekoppeld aan Tailwind v4 via `@theme inline` |
| `theme/mokka.tokens.json` | Dezelfde waarden in het W3C Design Tokens-formaat (voor Figma of Style Dictionary) |
| `blazor/` | Het homescreen als Blazor-app met Tailwind v4, gebouwd op dit thema (zie `blazor/README.md`) |

De tokennamen zijn dezelfde als in Figma (collectie **Mokka theme**) en in de shadcn-componenten:
`background`, `foreground`, `primary`, `secondary`, `muted`, `accent`, `destructive`, `border`, `input`, `ring`.
Bovenop de stijlgids komen vier feedbackkleuren: `success`, `warning`, `error` en `info`, elk met `-foreground`, `-subtle`, `-border` en `-text`.

## Gebruiken op de workshop

1. Maak een project en initialiseer shadcn/ui:
   ```bash
   npx create-next-app@latest mokka
   cd mokka
   npx shadcn@latest init
   ```
2. Kopieer `theme/mokka.css` naar het project en importeer het in `app/globals.css`, direct na Tailwind.
   Verwijder de `:root`-kleuren die `shadcn init` zelf aanmaakte, zodat ze die van Mokka niet overschrijven.
   ```css
   @import "tailwindcss";
   @import "./mokka.css";
   ```
3. Laad de fonts in `app/layout.tsx`:
   ```tsx
   import { Fraunces, Inter } from "next/font/google";

   const fraunces = Fraunces({ subsets: ["latin"], weight: ["600"], variable: "--font-fraunces" });
   const inter = Inter({ subsets: ["latin"], variable: "--font-inter" });
   ```
   Zet beide variabelen op `<html>` en vervang in `mokka.css` de fontnamen door `var(--font-fraunces)` en `var(--font-inter)`.
4. Voeg de componenten uit de stijlgids toe:
   ```bash
   npx shadcn@latest add button card tabs input badge toggle-group radio-group separator sonner switch
   ```

Daarna werken utilities zoals `bg-primary`, `text-muted-foreground`, `bg-success-subtle text-success-text` en `rounded-lg` (12 px), plus de tekstklassen `text-display`, `text-title`, `text-body`, `text-small` en `text-caption`.

## Afwijkingen van de stijlgids (PDF)

- **`input` is `#998677` in plaats van `#E5DCD1`.** De lichte rand haalde maar 1,27:1 tegen de achtergrond. De nieuwe waarde haalt 3,26:1 (WCAG 1.4.11). `border` blijft `#E5DCD1` voor decoratieve lijnen.
- **Feedbackkleuren zijn nieuw.** Alle combinaties halen minstens WCAG AA.
- **Twee extra tekststijlen:** `hero` (Fraunces 44/50) en `body-strong` (Inter SemiBold 16/24).
- **Geen dark mode.** De stijlgids definieert die niet; dat kunnen we op de workshop toevoegen.

## Toegankelijkheid om mee te nemen in de bouw

- Aanraakdoelen minimaal 44 × 44 px (knoppen en invoervelden zijn 44 px hoog in Figma).
- Zoekveld: `aria-label="Zoek je koffie"`.
- Toasts: `role="status"`, minstens 5 seconden zichtbaar.
- Tabbalk: `aria-current="page"` op het actieve tabblad.
- Productfoto's krijgen de productnaam als alt-tekst; sfeerfoto's `alt=""`.
