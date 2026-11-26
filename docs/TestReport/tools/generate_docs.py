#!/usr/bin/env python3
"""Generate docs/TestReport/*.md from the FR spec, tc-map.json, TRX results and coverage.

Usage: generate_docs.py <dir-with-trx-files> <coverage-root-dir>
Re-run after every test run; it overwrites the generated files.
"""
import json, re, sys, pathlib, collections, datetime
import xml.etree.ElementTree as ET

ROOT = pathlib.Path(__file__).resolve().parents[3]
OUT = ROOT / "docs" / "TestReport"
REPO = "https://github.com/AIU-SoftWave/Aspirra"
BRANCH = "develop"

MEMBERS = [  # key, name, github, FR groups
    ("ahmed", "Ahmed Mohamed Ahmed", "AbooSalh", [1, 2, 3, 4]),
    ("yomna", "Yomna Mohamed Fekry", "YomnaElmenawy", [5, 6, 7, 8]),
    ("arwa", "Arwa Mohey Hamdy", "ArwaMohey", [9, 10, 11, 12]),
    ("mostafa", "Mostafa Khamis Abozead", "Mostafa-abozead", [13, 14, 15, 16]),
    ("muhammed", "Muhammed Hany Hamdy", "muhammedhanii", [17, 18, 19, 20]),
]
SERVICE = {1: "StudentService", 2: "AcademicService", 3: "CourseService", 4: "AssessmentService",
           5: "GoalPlannerService", 6: "StudyPlannerService", 7: "SchedulingService",
           8: "AdaptationService", 9: "CareerService", 10: "SkillService", 11: "PortfolioService",
           12: "JobPreparationService", 13: "HabitService", 14: "AnalyticsService",
           15: "PredictionService", 16: "AICoachService", 17: "DashboardService",
           18: "NotificationService", 19: "StudyGroupService", 20: "AdministrationService"}

# ---- parse FR spec ---------------------------------------------------------
frs = collections.OrderedDict()   # group -> {"title":..., "rows":[(id,text)]}
cur = None
for line in (ROOT / "docs/proposal/04-B4-functional-requirements.md").read_text(encoding="utf-8").splitlines():
    m = re.match(r"#### FR-(\d+) — (.*)", line)
    if m:
        cur = int(m.group(1)); frs[cur] = {"title": m.group(2).strip(), "rows": []}
        continue
    m = re.match(r"\| (FR-(\d+)\.(\d+)) \| (.*?) \| [^|]+ \| [^|]+ \|\s*$", line)
    if m and cur:
        frs[cur]["rows"].append((m.group(1), m.group(4).strip()))

tcmap = json.loads((pathlib.Path(__file__).with_name("tc-map.json")).read_text())
by_fr = collections.defaultdict(list)
for t in tcmap:
    by_fr[t["fr"]].append(t)

# ---- results ---------------------------------------------------------------
results = {}
trx_files = sorted(pathlib.Path(sys.argv[1]).glob("*.trx")) if len(sys.argv) > 1 else []
if True:
    ns = {"t": "http://microsoft.com/schemas/VisualStudio/TeamTest/2010"}
    for r in (x for f in trx_files for x in ET.parse(f).getroot().iterfind(".//t:UnitTestResult", ns)):
        m = re.search(r"\.(TC_\d+_\d+_\d+)_", r.get("testName", ""))
        if m:
            results[m.group(1).replace("_", "-", 1).replace("_", ".", 1).replace("_", "-", 1)] = r.get("outcome")
# normalise keys: TC_01_1_01 -> TC-01.1-01
def key(tc): return tc  # tc ids in tcmap are already TC-01.1-01
# (the key transform above yields TC-01.1-01)

def status(tc):
    o = results.get(tc)
    return {"Passed": "Passed", "Failed": "Failed"}.get(o, "Not run" if o is None else o)

def words(s):
    s = re.sub(r"(?<=[a-z0-9])(?=[A-Z])", " ", s)
    s = s.lower()
    return s[0].upper() + s[1:] if s else s

def describe(base):
    p = base.split("_")
    desc = words(p[0])
    if len(p) == 1:
        return desc, "Valid input", "Operation succeeds"
    if len(p) == 2:
        return desc, "Valid input", words(p[1])
    return desc, words("_".join(p[1:-1]).replace("_", " ")), words(p[-1])

def tcid_to_method(t):
    return t["method"]

def src_link(t):
    return f"{REPO}/blob/{BRANCH}/tests/{t['project']}/{t['file']}"

def esc(s): return s.replace("|", "\\|")

# ---- 01 requirements -------------------------------------------------------
def pass_criteria(fr, rows):
    tcs = by_fr.get(fr, [])
    if tcs:
        ids = ", ".join(t["tc"] for t in tcs)
        return f"All mapped test cases pass ({ids}); valid input succeeds and invalid input is rejected with the documented error."
    return "Behaviour is verified by at least one passing test: valid input succeeds, invalid input is rejected with a clear error. (Planned: service not implemented yet.)"

lines = ["# 1. Testing Requirements", "",
         "Requirements (FRs) under test per service and what must be true for each one to pass. "
         "Source: `docs/proposal/04-B4-functional-requirements.md`. Coverage column shows the number of test cases mapped to the FR.", ""]
for key_, name, gh, groups in MEMBERS:
    lines += [f"## {name} (`{gh}`)", ""]
    for g in groups:
        svc = SERVICE[g]; impl = (ROOT / f"src/Services/{svc}/{svc}.csproj").exists()
        lines += [f"### {svc} — FR-{g:02d} {frs[g]['title']}",
                  f"Implementation: {'implemented, automated tests exist' if impl else '**not implemented yet** — test cases are planned drafts'}", "",
                  "| FR | Requirement | Must be true to pass | Test cases |", "|---|---|---|---|"]
        for fid, text in frs[g]["rows"]:
            n = len(by_fr.get(fid, []))
            lines.append(f"| {fid} | {esc(text)} | {esc(pass_criteria(fid, frs[g]['rows']))} | {n if n else 'planned'} |")
        lines.append("")
(OUT / "01-testing-requirements.md").write_text("\n".join(lines), encoding="utf-8")

# ---- 02 test cases per member ---------------------------------------------
for idx, (key_, name, gh, groups) in enumerate(MEMBERS, 1):
    L = [f"# 2. Test Cases — {name}", "",
         f"Owner: `{gh}`. Status/Actual Result are filled from the latest test run (`04-testing-results.md`).", ""]
    for g in groups:
        svc = SERVICE[g]
        L += [f"## {svc}", "",
              "| ID | Requirement | Description | Input | Expected Result | Actual Result | Status |",
              "|---|---|---|---|---|---|---|"]
        for fid, text in frs[g]["rows"]:
            tcs = by_fr.get(fid, [])
            if tcs:
                for t in tcs:
                    d, i, e = describe(t["base"])
                    st = status(t["tc"])
                    act = "As expected" if st == "Passed" else ("Deviates from expected" if st == "Failed" else "—")
                    L.append(f"| {t['tc']} | {fid} | {esc(d)} | {esc(i)} | {esc(e)} | {act} | {st} |")
            else:
                n = fid.split("-")[1]; a, b = n.split(".")
                L.append(f"| TC-{a}.{b}-01 | {fid} | Verify: {esc(text[:90])} | Valid request | Requirement satisfied | — | Planned |")
        L.append("")
    (OUT / f"02-test-cases-{idx:02d}-{key_}.md").write_text("\n".join(L), encoding="utf-8")

# ---- 03 testing code -------------------------------------------------------
L = ["# 3. Testing Code", "",
     f"Test folder: {REPO}/tree/{BRANCH}/tests", "",
     "Framework: xUnit with Moq (the .NET equivalents of JUnit and Mockito). "
     "Every test method name carries its test case ID: `TC-02.1-01` becomes `TC_02_1_01_<Scenario>`.", "",
     "| Service | Test project |", "|---|---|"]
for proj in sorted({t["project"] for t in tcmap}):
    L.append(f"| {proj.replace('.Tests','')} | [{proj}]({REPO}/tree/{BRANCH}/tests/{proj}) |")
L += ["", "## Test case → method map", "", "| TC ID | FR | Method | Source |", "|-----|-----|----------------------------------------|---------------|"]
for t in sorted(tcmap, key=lambda x: (x["project"], x["tc"])):
    L.append(f"| {t['tc']} | {t['fr']} | `{t['method']}` | [{t['file']}]({src_link(t)}) |")
(OUT / "03-testing-code.md").write_text("\n".join(L) + "\n", encoding="utf-8")

# ---- 04 results ------------------------------------------------------------
per_proj = collections.defaultdict(lambda: collections.Counter())
for t in tcmap:
    per_proj[t["project"]][status(t["tc"])] += 1
cov = {}
if len(sys.argv) > 2:
    for f in pathlib.Path(sys.argv[2]).rglob("coverage.opencover.xml"):
        root = ET.parse(f).getroot()
        for mod in root.iterfind(".//Module"):
            name = mod.findtext("ModuleName")
            hit = {}
            for sp in mod.iterfind(".//SequencePoint"):
                k = (sp.get("sl"), sp.get("fileid") or "")
                hit[k] = hit.get(k, False) or int(sp.get("vc", "0")) > 0
            # module-level: aggregate by lines
            tot, vis = len(hit), sum(hit.values())
            o = cov.get(name, (0, 0)); cov[name] = (max(o[0], tot), max(o[1], vis)) if o[0] <= tot else o
L = ["# 4. Testing Results", "",
     f"Generated: {datetime.date.today()} on branch `develop`.", "",
     "## Pass / fail summary", "", "| Test project | Passed | Failed | Not run | Total |", "|---|---|---|---|---|"]
tp = tf = tn = 0
for proj, c in sorted(per_proj.items()):
    p, f_, n = c["Passed"], c["Failed"], sum(v for k, v in c.items() if k not in ("Passed", "Failed"))
    tp += p; tf += f_; tn += n
    L.append(f"| {proj} | {p} | {f_} | {n} | {p+f_+n} |")
L.append(f"| **Total** | **{tp}** | **{tf}** | **{tn}** | **{tp+tf+tn}** |")
L += ["", "## Coverage (line coverage per service, from coverlet/opencover)", "", "| Service | Lines covered | Lines total | Coverage |", "|---|---|---|---|"]
for name, (tot, vis) in sorted(cov.items()):
    if name.endswith("Service") and tot:
        L.append(f"| {name} | {vis} | {tot} | {100*vis/tot:.1f}% |")
L += ["", "Quality-gate coverage (authoritative) is reported by SonarCloud: "
      "https://sonarcloud.io/project/overview?id=AIU-SoftWave_Aspirra", "",
      "## Pipeline screenshot", "",
      "![GitHub Actions run](images/pipeline-green.png)", "",
      "> Add `images/pipeline-green.png` — a screenshot of the green `CI - Build, Test and Push to GHCR` run on GitHub Actions after the PR is opened.", "",
      "## Reproduce", "", "```bash",
      'dotnet test Aspirra.slnx -c Release --settings coverlet.runsettings --collect:"XPlat Code Coverage" --results-directory artifacts/coverage',
      "```", "",
      "Services without source code yet (14 of 20) have no executable tests; their test cases are marked **Planned**."]
(OUT / "04-testing-results.md").write_text("\n".join(L) + "\n", encoding="utf-8")
print("generated; passed", tp, "failed", tf, "other", tn)
