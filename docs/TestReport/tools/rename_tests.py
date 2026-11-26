#!/usr/bin/env python3
"""Prefix every [Fact]/[Theory] test method with its TC ID (TC_<FR>_<sub>_<nn>_...).

Idempotent: methods already starting with TC_ are skipped.
The mapping rules (method-name prefix -> FR) live in RULES below.
Writes docs/TestReport/tools/tc-map.json used by generate_docs.py.
"""
import json, re, pathlib, collections

ROOT = pathlib.Path(__file__).resolve().parents[3]

# (project folder, regex on method name) -> FR id ; first match wins
RULES = {
    "StudentService.Tests": [
        (r"CreateProfile_(Missing|EndBefore)", "01.4"), (r"CreateProfile", "01.1"),
        (r"UpdateProfile", "01.2"), (r"ViewProfile|GetProfile|GetAvailability", "01.3"),
    ],
    "AcademicService.Tests": [
        (r"Health", "02.1"),
        (r"UpdateGoal_PastTargetDate", "02.3"), (r"UpdateGoal", "02.2"),
        (r"CompleteGoal", "02.4"), (r"ArchiveGoal|RestoreGoal|ArchiveAndRestore", "02.5"),
        (r"DeleteGoal", "02.6"), (r"CreateGoal|GetGoal", "02.1"),
    ],
    "CourseService.Tests": [
        (r"AddCourse_WithValid", "03.2"), (r"AddCourse|GetCourses", "03.1"),
        (r"ViewCourseProgress", "03.3"), (r"UpdateCourse", "03.4"), (r"RemoveCourse", "03.5"),
    ],
    "AssessmentService.Tests": [
        (r"AddAssignment|GetAssignmentsByCourse", "04.1"), (r"AddExam|GetAssessments", "04.2"),
        (r"SetAssignmentStatus", "04.3"), (r"SetExamReminder", "04.4"),
    ],
    "GoalPlannerService.Tests": [
        (r"GeneratePlan_Empty", "05.1"), (r"GeneratePlan", "05.2"),
        (r"UpdateDraft_ApprovedPlan", "05.5"), (r"UpdateDraft", "05.4"),
        (r"Approve", "05.6"), (r"Discard|GetTasks|GetDraft", "05.3"),
    ],
    "HabitService.Tests": [
        (r"CreateHabit", "13.1"),
        (r"CompleteHabit_(FirstTime|Consecutive|Missed|SameDay)", "13.3"),
        (r"GetStreaks", "13.3"), (r"CompleteHabit", "13.2"), (r"LinkHabit", "13.4"),
    ],
}

ATTR = re.compile(r"(\[(?:Fact|Theory)[^\]]*\]\s*(?:\[[^\]]*\]\s*)*)(public\s+(?:async\s+)?(?:Task|void)\s+)(\w+)(\s*\()")
mapping, counters = [], collections.Counter()

for proj, rules in RULES.items():
    for f in sorted((ROOT / "tests" / proj).glob("*.cs")):
        text = open(f, encoding="utf-8", newline="").read()

        def sub(m):
            name = m.group(3)
            mt = re.match(r"TC_(\d+)_(\d+)_(\d+)_(.*)", name)
            if mt:
                fr = f"{mt.group(1)}.{mt.group(2)}"; n = int(mt.group(3)); base = mt.group(4)
                counters[(proj, fr)] = max(counters[(proj, fr)], n)
                new = name
            else:
                fr = next((r for p, r in rules if re.match(p, name)), None)
                if fr is None:
                    raise SystemExit(f"No rule for {proj}:{name}")
                counters[(proj, fr)] += 1
                n = counters[(proj, fr)]
                major, minor = fr.split(".")
                new = f"TC_{major}_{minor}_{n:02d}_{name}"
                base = name
            mapping.append({"tc": f"TC-{fr}-{n:02d}", "fr": f"FR-{fr}", "project": proj,
                            "file": f.name, "method": new, "base": base})
            return m.group(1) + m.group(2) + new + m.group(4)

        new_text = ATTR.sub(sub, text)
        if new_text != text:
            open(f, "w", encoding="utf-8", newline="").write(new_text)

out = pathlib.Path(__file__).with_name("tc-map.json")
out.write_text(json.dumps(mapping, indent=1), encoding="utf-8")
print(len(mapping), "test methods mapped")
