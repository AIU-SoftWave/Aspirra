"use client";

import { FormEvent, useState } from "react";

const api = process.env.NEXT_PUBLIC_API_URL ?? "http://localhost:5000";

type Goal = { id: string; title: string; targetDate: string; priority: string; status: string };
type Habit = { id: string; name: string; frequency: string; currentStreak: number };

async function request<T>(path: string, init?: RequestInit): Promise<T> {
  const response = await fetch(`${api}${path}`, {
    ...init,
    headers: { "Content-Type": "application/json", ...(init?.headers ?? {}) }
  });
  if (!response.ok) {
    const body = await response.json().catch(() => ({}));
    throw new Error(body.message ?? "Request failed");
  }
  return response.status === 204 ? (undefined as T) : response.json();
}

export default function Home() {
  const studentId = "demo-student";
  const [goal, setGoal] = useState({ title: "", targetDate: "", priority: "High" });
  const [habit, setHabit] = useState({ name: "", frequency: "daily" });
  const [goalText, setGoalText] = useState("");
  const [goals, setGoals] = useState<Goal[]>([]);
  const [habits, setHabits] = useState<Habit[]>([]);
  const [message, setMessage] = useState("");

  async function createGoal(event: FormEvent) {
    event.preventDefault();
    try {
      const created = await request<Goal>(`/api/students/${studentId}/goals`, {
        method: "POST", body: JSON.stringify({ ...goal, targetDate: goal.targetDate })
      });
      setGoals(current => [...current, created]);
      setGoal({ title: "", targetDate: "", priority: "High" });
      setMessage("Goal created");
    } catch (error) { setMessage((error as Error).message); }
  }

  async function createHabit(event: FormEvent) {
    event.preventDefault();
    try {
      const created = await request<Habit>(`/api/students/${studentId}/habits`, {
        method: "POST", body: JSON.stringify(habit)
      });
      setHabits(current => [...current, created]);
      setHabit({ name: "", frequency: "daily" });
      setMessage("Habit created");
    } catch (error) { setMessage((error as Error).message); }
  }

  async function generatePlan(event: FormEvent) {
    event.preventDefault();
    try {
      await request(`/api/students/${studentId}/goal-plans`, {
        method: "POST", body: JSON.stringify({ goalText })
      });
      setMessage("Plan generated for review");
    } catch (error) { setMessage((error as Error).message); }
  }

  return <main>
    <header><p className="eyebrow">ASPIRRA · SPRINT 1</p><h1>Turn your goals into progress.</h1><p className="lead">Manage your academic goals and build consistent study habits.</p></header>
    <p className="message" aria-live="polite">{message}</p>
    <section className="grid">
      <form className="card" onSubmit={createGoal}><h2>Academic goal</h2>
        <label>Title<input required value={goal.title} onChange={event => setGoal({ ...goal, title: event.target.value })} /></label>
        <label>Target date<input required type="date" value={goal.targetDate} onChange={event => setGoal({ ...goal, targetDate: event.target.value })} /></label>
        <label>Priority<select value={goal.priority} onChange={event => setGoal({ ...goal, priority: event.target.value })}><option>High</option><option>Medium</option><option>Low</option></select></label>
        <button type="submit">Create goal</button>
      </form>
      <form className="card" onSubmit={createHabit}><h2>Study habit</h2>
        <label>Name<input required value={habit.name} onChange={event => setHabit({ ...habit, name: event.target.value })} /></label>
        <label>Frequency<select value={habit.frequency} onChange={event => setHabit({ ...habit, frequency: event.target.value })}><option value="daily">Daily</option><option value="weekly">Weekly</option></select></label>
        <button type="submit">Create habit</button>
      </form>
      <form className="card wide" onSubmit={generatePlan}><h2>Plan a goal with AI</h2>
        <label>Describe your goal<textarea required value={goalText} onChange={event => setGoalText(event.target.value)} placeholder="e.g. Reach a 3.5 GPA this semester" /></label>
        <button type="submit">Generate plan</button>
      </form>
    </section>
    <section className="lists"><div><h2>Your goals</h2>{goals.length === 0 ? <p className="muted">No goals yet.</p> : goals.map(item => <article key={item.id}><strong>{item.title}</strong><span>{item.status} · due {item.targetDate}</span></article>)}</div>
      <div><h2>Your habits</h2>{habits.length === 0 ? <p className="muted">No habits yet.</p> : habits.map(item => <article key={item.id}><strong>{item.name}</strong><span>{item.frequency} · streak {item.currentStreak}</span></article>)}</div></section>
  </main>;
}
