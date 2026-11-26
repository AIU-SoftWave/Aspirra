import pathlib, datetime
d = pathlib.Path(__file__).resolve().parents[1]
parts = ["01-testing-requirements.md"] + sorted(p.name for p in d.glob("02-test-cases-*.md")) + ["03-testing-code.md", "04-testing-results.md"]
out = ["# Aspirra — Test Report", "",
 "**Course:** CSE454 — Advanced Software Engineering, Alamein International University  ",
 "**Repository:** https://github.com/AIU-SoftWave/Aspirra  ",
 "**Tools:** xUnit, Moq, GitHub Actions  ",
 f"**Date:** {datetime.date.today()}", "",
 "**Team:** Ahmed Mohamed Ahmed (Leader), Yomna Mohamed Fekry, Arwa Mohey Hamdy, Mostafa Khamis Abozead, Muhammed Hany Hamdy", "",
 "## Contents", "1. Testing Requirements", "2. Test Cases (one section per member)", "3. Testing Code", "4. Testing Results", ""]
for p in parts:
    out += ['\\newpage', "", (d / p).read_text(encoding="utf-8"), ""]
(d / "report.md").write_text("\n".join(out), encoding="utf-8")

import re, subprocess, os
txt = (d / "report.md").read_text(encoding="utf-8")
for a, c in (("—", "--"), ("→", "->"), ("·", "|"), ("…", "...")):
    txt = txt.replace(a, c)
def tt(m):
    body = m.group(1)
    if not body.startswith("TC_"):
        return m.group(0)
    return "`\\texttt{" + body.replace("_", "\\_\\allowbreak{}") + "}`{=latex}"
txt = re.sub(r"`([^`]+)`", tt, txt)
tmp = d / "report.pdf.tmp.md"
tmp.write_text(txt, encoding="utf-8")
subprocess.run(["pandoc", str(tmp), "-o", str(d / "report.pdf"), "--pdf-engine=pdflatex",
                "-V", "geometry:margin=1.5cm", "-V", "geometry:landscape", "-V", "colorlinks=true",
                "--toc", "--toc-depth=2"], check=True, cwd=d)
tmp.unlink()
