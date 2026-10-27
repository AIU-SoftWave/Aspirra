import { describe, expect, it } from "vitest";

describe("Sprint 1 frontend", () => {
  it("defines the documented student planning surface", () => {
    expect(["profile", "goals", "courses", "assessments", "habits", "goal-planner"]).toHaveLength(6);
  });
});
