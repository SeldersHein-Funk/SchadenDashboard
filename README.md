# SchadenDashboard

Das "Schadensfälle Dashboard" für das Management läuft seit gestern extrem langsam (30+ Sekunden). Der Fachbereich beschwert sich, dass das Dashboard nicht mehr nutzbar ist. Die Datenbank hat ~2 Millionen Schadensfälle und wächst um 10.000/Tag.

## Rahmenbedinungen

- **Tabelle Schaden:** 2.000.000 Zeilen
- **Durchschnittliche SchadenPositionen pro Schaden:** 2-3
- **Dashboard wird aufgerufen mit:** `from = today - 30 days`, `to = today`
- **Aktueller Index:** Nur Primary Keys vorhanden
- **Memory:** Server hat 16 GB RAM
- **Beobachtung:** SQL Server zeigt 100% CPU während der Query
