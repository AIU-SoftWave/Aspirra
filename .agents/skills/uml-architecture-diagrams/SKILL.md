---
name: uml-architecture-diagrams
description: >-
  Guides the creation and migration of 4-level software architecture diagrams
  (Level 0 System Context, Level 1 Container, Level 2 Component, and Level 3
  Class diagrams) using native UML 2.x PlantUML notation (ports, provided
  lollipops, required sockets, and ball-and-socket assembly connectors). Use
  this skill whenever the user asks to create, update, or convert architecture,
  context, container, component, or SOLID class diagrams to UML notation in
  PlantUML.
---

# UML 2.x Architecture Diagrams (PlantUML)

Produce clean, standards-compliant UML 2.x architecture diagrams across the four levels of zoom (**Context -> Container -> Component -> Code**) using native PlantUML (`1.2023+`, tested on `1.2026.10`) instead of C4-PlantUML macros.

## Workflow

### 1. Verify PlantUML Version
Check which `plantuml` binary is available before rendering. Native `portin`/`portout` syntax requires modern PlantUML (`1.2023+`):
- Prefer `~/.local/bin/plantuml` (or a recent `plantuml.jar`) over older system packages such as `/usr/bin/plantuml` (`1.2020.02`, which lacks `portin`/`portout`).
- Run `plantuml -version` to confirm.

### 2. Choose the Zoom Level & Template
Start from the matching template in `./templates/`:
- **Level 0 — System Context:** [level0-system-context.puml](./templates/level0-system-context.puml) (`<<system>>` box, actors, provided boundary ports + lollipops, required external ports + ball-and-socket connectors to `<<external>>` systems).
- **Level 1 — Container:** [level1-container.puml](./templates/level1-container.puml) (`<<system>>` boundary enclosing `<<service>>` containers grouped by owner/domain, boundary ports, internal `-(0-` ball-and-socket assembly connectors, `<<external>>` systems, and `<<database>>`).
- **Level 2 — Component:** [level2-component.puml](./templates/level2-component.puml) (top actor + provided lollipop + `portin` on `<<container>>`, internal `<<component>>` boxes wired via `-(0-`, bottom `portout` squares connected via `-(0-` to external `<<service>>`, `<<external>>`, and `<<database>>`).
- **Level 3 — Code (Class Diagram):** [level3-class.puml](./templates/level3-class.puml) (`<<provided>>` interfaces, `<<component>>` classes, `<<required>>` interfaces, and owned domain entities applying SOLID).

Consult [uml-notation-cheatsheet.md](./references/uml-notation-cheatsheet.md) for exact syntax, `skinparam` theme, and layout pitfalls.

### 3. Enforce Core UML 2.x Rules
1. **Component style:** Always include `skinparam componentStyle uml2` so components render with the standard UML 2.x two-tab icon.
2. **Port invariant:** Every interface crossing a `<<system>>` or `<<container>>` boundary **must** attach to a port square (`portin " "` / `portout " "`). Never leave an unconnected port or a portless boundary interface.
3. **Lollipop vs. socket direction:**
   - **Provided interface (lollipop `O`):** attached to the provider.
   - **Required interface (socket `(`):** attached to the consumer.
   - **Assembly connector:** `consumer -(0- provider : IName` (socket on `consumer`, ball on `provider`).
4. **Black-box discipline (Level 2):** Do **not** draw interfaces (`IFoo`, `IFooRepository`) or domain model lists as fake `<<component>>` boxes. Model interfaces as lollipops/assembly connectors, and list owned tables/entities inside the `<<database>>` label (leaving full entity structure for Level 3 Class Diagrams).
5. **Preserve output filenames:** When updating existing diagrams embedded in Markdown/reports, preserve the exact `@startuml <name>` identifier (or omit `<name>` to match the `.puml` basename) so rendered PNGs overwrite the expected targets in place.

### 4. Apply Readability & Layout Patterns
- **Actor separation:** Always use double-dash `--(` / `)--` (never single-dash `-(`) between actors and provided interface lollipops so GraphViz places actors in a clean separate rank without overlapping interface labels.
- **High-fan-out Level 0 diagrams:** When a system exposes many provided interfaces (> 8), repeat the actor cleanly on **both left and right** grouped by functional area (3–4 interfaces per actor instance) with `-[hidden]right-` ordering links to eliminate crossing lines and keep a balanced print-friendly aspect ratio.
- **Dense Level 1 databases:** On large multi-service Level 1 diagrams, connect service groups to a single `portout` (`IPersistence`) -> `<<database>>` to avoid dozens of crossing lines; show per-service `portout` -> `<<database>> (<schema> schema)` on Level 2 Component diagrams and per-owner split Container diagrams.
- **Clean legends:** Never put raw Markdown backticks in PlantUML `legend` blocks (they render literally).

### 5. Render & Validate
1. Render all modified `.puml` files to PNG (`plantuml -tpng ...`) and verify exit code `0`.
2. Check generated PNG dimensions — ensure no diagram hits PlantUML's default `4096px` clipping limit.
3. Verify zero leftover `C4_` includes if migrating from C4-PlantUML.
4. **Visually inspect** rendered PNGs to verify:
   - UML2 component icons appear on components.
   - Ports sit cleanly on boundaries with no orphan ports.
   - Socket `(` always faces the requiring side and lollipop `O` faces the providing side.
   - No overlapping labels or clipped legends.
