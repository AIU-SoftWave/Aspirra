import "./globals.css";

export const metadata = { title: "Aspirra", description: "Sprint 1 student planner" };

export default function RootLayout({ children }: Readonly<{ children: React.ReactNode }>) {
  return <html lang="en"><body>{children}</body></html>;
}
