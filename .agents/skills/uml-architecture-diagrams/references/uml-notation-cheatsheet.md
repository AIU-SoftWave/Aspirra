# UML 2.x PlantUML Notation Cheatsheet

Reference for PlantUML `1.2023+` (verified on `1.2026.10` with GraphViz `dot`).

## 1. Standard Light-Theme `skinparam` Block

```plantuml
skinparam componentStyle uml2
skinparam shadowing false
skinparam defaultFontName Segoe UI
skinparam backgroundColor #FFFFFF
skinparam ArrowColor #334155
skinparam nodesep 30
skinparam ranksep 50
skinparam port {
  BackgroundColor #FFFFFF
  BorderColor #1E3A8A
}
skinparam actor {
  BackgroundColor #DBEAFE
  BorderColor #1E3A8A
}
skinparam package {
  BackgroundColor #F8FAFC
  BorderColor #94A3B8
  FontColor #1E293B
}
skinparam component {
  BackgroundColor #EFF6FF
  BorderColor #1D4ED8
  BackgroundColor<<service>> #FFFFFF
  BorderColor<<service>> #2563EB
  BackgroundColor<<dependency>> #F8FAFC
  BorderColor<<dependency>> #64748B
  BackgroundColor<<external>> #F1F5F9
  BorderColor<<external>> #475569
}
skinparam interface {
  BackgroundColor #FFFFFF
  BorderColor #1E3A8A
}
skinparam database {
  BackgroundColor #F1F5F9
  BorderColor #334155
}
```

## 2. Exact Connector & Port Syntax

| Pattern | PlantUML Syntax | Visual Meaning |
| --- | --- | --- |
| Boundary port (entry/left/top) | `portin " " as p_in` | White/navy square on entry edge of `<<system>>` or `<<container>>` |
| Boundary port (exit/right/bottom) | `portout " " as p_out` | White/navy square on exit edge of `<<system>>` or `<<container>>` |
| Left/Top actor -> provided interface | `actor --( iface` + `iface -- p_in` | Socket `(` on actor wrapping lollipop `O` attached to boundary port |
| Right actor -> provided interface | `p_out -- iface` + `iface )-- actor` | Lollipop `O` on right boundary port wrapped by socket `)` on right-side actor |
| Ball-and-socket assembly connector | `consumer -(0- provider : IName` | Socket `(` on `consumer` (requiring), lollipop `O` on `provider` (providing) |
| Boundary required -> external/service/DB | `p_out -(0- extTarget : IName` | Socket `(` on boundary port wrapping lollipop `O` on external provider |
| Internal delegation (port -> component) | `p_in -- innerComp` / `innerComp -- p_out` | Solid line wiring boundary port to internal component |
| Ordering actors cleanly (left-to-right mode) | `actor1 -[hidden]right- actor2` | Keeps actors stacked top-to-bottom in `left to right direction` |

## 3. Common Pitfalls & Fixes

1. **Legacy PlantUML (`/usr/bin/plantuml` `1.2020.02`):** Does **not** support `portin`/`portout`. Always use `~/.local/bin/plantuml` (`1.2026+`) or a modern `plantuml.jar`.
2. **Legacy `skinparam PortBackgroundColor`:** Ignored in PlantUML `1.2026+`; use `skinparam port { BackgroundColor ...; BorderColor ... }` instead.
3. **Single-dash `actor -( iface`:** Places the actor on the same rank as the lollipop, causing curved edges to cross interface labels. Always use double-dash `actor --( iface` or `iface )-- actor`.
4. **Orphan `interface` declarations:** When using inline `A -(0- B : IName`, do **not** also declare `interface "IName"` separately — PlantUML will draw a floating disconnected circle.
5. **Markdown backticks in `legend`:** PlantUML legends render backticks literally; use plain text (e.g., `-(0-`, `portin / portout`, `<<service>>`).
6. **Output PNG filename:** `@startuml foo-bar` outputs `foo-bar.png` regardless of the `.puml` filename. Omit the name (`@startuml`) to match the `.puml` basename exactly.
7. **Large diagram clipping:** PlantUML defaults to `PLANTUML_LIMIT_SIZE=4096`. Keep diagrams under `4000px` in both dimensions (split large 20+ service Container diagrams into an overview + per-owner/domain views).

## 4. Verification Checklist

- [ ] `plantuml -tpng` exits with code `0` and zero syntax warnings.
- [ ] Every boundary interface is attached to a `portin` or `portout` square; zero orphan ports.
- [ ] Every `-(0-` connector has the socket `(` on the requiring side and lollipop `O` on the providing side.
- [ ] No `C4_Context.puml`, `C4_Container.puml`, or `C4_Component.puml` includes remain.
- [ ] All PNG dimensions are `< 4096px` and visually inspected for label clarity.
