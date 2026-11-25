# Jenkins (PM2-12)

Local Jenkins controller with the .NET 10 SDK, JDK 21, Git, and the SonarScanner for .NET pre-installed. It runs the `Jenkinsfile` in this repository.

> **Build verified (2026-11-25):** `docker build -t aspirra-jenkins devops/jenkins` completes
> successfully. Result: image `aspirra-jenkins:latest` (1.63 GB), .NET SDK `10.0.404`,
> `dotnet-sonarscanner` `12.1.0`. The SDK is downloaded with `curl` (retry flags) and the CLI
> runs with invariant globalization (`DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1`) because the
> Debian base ships no ICU.

## Start

```bash
docker compose -f devops/jenkins/docker-compose.yml up -d --build
```

The controller is then available at http://localhost:8080.

> The setup wizard is disabled by `JAVA_OPTS`; create the initial admin user via
> the `jenkins` container, or start without that option if you prefer the wizard.

## Credentials

| ID | Kind | Purpose |
| --- | --- | --- |
| `sonar-token` | Secret text | SonarCloud token for `dotnet-sonarscanner` |

Add the credential from **Manage Jenkins → Credentials → System → Global credentials**.
If `sonar-token` is missing, the pipeline prints a skip message and completes without Sonar.

## Pipeline job

1. **New Item** → **Pipeline** → name `aspirra-pm2`.
2. Under **Pipeline**, choose **Pipeline script from SCM**.
3. Repository URL: `https://github.com/AIU-SoftWave/Aspirra.git`.
4. Branch: `*/develop`.
5. Script path: `Jenkinsfile`.
6. Save and **Build Now**.

## Stages

1. Checkout (`develop`).
2. `dotnet restore Aspirra.slnx`.
3. `dotnet build Aspirra.slnx -c Release --no-restore`.
4. `dotnet test` with OpenCover coverage and JUnit XML output.
5. SonarScanner begin/build/end when `sonar-token` exists.
6. Archive coverage and JUnit XML artifacts.

## Notes

- The agent image installs the .NET 10 SDK via the official `dotnet-install.sh` script.
- `dotnet-sonarscanner` is installed as a global .NET tool inside the image.
- Test results are published with the JUnit plugin; coverage XML is archived for the report.
