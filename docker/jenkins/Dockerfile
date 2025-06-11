FROM jenkins/jenkins:2.504.2-jdk21

USER root

# Installer .NET SDK 8 (adaptable à ta version)
RUN apt-get update && apt-get install -y wget apt-transport-https software-properties-common gnupg curl unzip \
 && wget https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
 && dpkg -i packages-microsoft-prod.deb \
 && apt-get update && apt-get install -y dotnet-sdk-9.0

# Installer Docker CLI (pour communiquer avec docker:dind)
RUN apt-get install -y lsb-release \
 && curl -fsSLo /usr/share/keyrings/docker-archive-keyring.asc https://download.docker.com/linux/debian/gpg \
 && echo "deb [arch=$(dpkg --print-architecture) signed-by=/usr/share/keyrings/docker-archive-keyring.asc] https://download.docker.com/linux/debian $(lsb_release -cs) stable" > /etc/apt/sources.list.d/docker.list \
 && apt-get update && apt-get install -y docker-ce-cli

# Installer SonarScanner CLI
RUN curl -sSLo /tmp/sonar-scanner.zip https://binaries.sonarsource.com/Distribution/sonar-scanner-cli/sonar-scanner-cli-5.0.1.3006-linux.zip \
 && unzip /tmp/sonar-scanner.zip -d /opt \
 && mv /opt/sonar-scanner-* /opt/sonar-scanner \
 && ln -s /opt/sonar-scanner/bin/sonar-scanner /usr/local/bin/sonar-scanner

USER jenkins
