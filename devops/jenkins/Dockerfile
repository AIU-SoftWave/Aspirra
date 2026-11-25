FROM jenkins/jenkins:lts-jdk21

USER root

RUN apt-get update \
    && apt-get install -y --no-install-recommends wget ca-certificates curl \
    && rm -rf /var/lib/apt/lists/*

ENV DOTNET_ROOT=/usr/share/dotnet
ENV DOTNET_VERSION=10.0.404
ENV PATH="${PATH}:${DOTNET_ROOT}:/root/.dotnet/tools"

# Install the .NET 10 SDK (pinned patch; bump DOTNET_VERSION when 10.0.x updates).
RUN curl -fsSL --proto '=https' --tlsv1.2 --retry 5 --retry-all-errors --retry-delay 5 --connect-timeout 30 \
        "https://builds.dotnet.microsoft.com/dotnet/Sdk/${DOTNET_VERSION}/dotnet-sdk-${DOTNET_VERSION}-linux-x64.tar.gz" \
        -o /tmp/dotnet-sdk.tar.gz \
    && mkdir -p "${DOTNET_ROOT}" \
    && tar -xzf /tmp/dotnet-sdk.tar.gz -C "${DOTNET_ROOT}" \
    && ln -s "${DOTNET_ROOT}/dotnet" /usr/local/bin/dotnet \
    && rm /tmp/dotnet-sdk.tar.gz

# The Debian base does not ship ICU; run the CLI with invariant globalization so it
# does not FailFast on missing libicu (fine for a build agent).
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1

# Install the SonarScanner for .NET global tool.
RUN dotnet tool install --global dotnet-sonarscanner

USER jenkins
