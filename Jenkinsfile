pipeline {
    agent any

    environment {
        SONAR_HOST_URL = 'http://sonarqube:9000'
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/Nayzow/lodger-backend'
            }
        }

        stage('Restore') {
            steps {
                sh 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build --no-restore'
            }
        }

        stage('Test') {
            steps {
                sh 'dotnet test --no-build --collect:"XPlat Code Coverage"'
            }
        }

        stage('SonarQube Analysis') {
            steps {
                withSonarQubeEnv('MySonar') {
                    withCredentials([string(credentialsId: 'LODGER_TOKEN', variable: 'SONAR_AUTH_TOKEN')]) {
                        sh '''
                            dotnet tool install dotnet-sonarscanner --tool-path .sonar --version 6.0.0 || true

                            ./.sonar/dotnet-sonarscanner begin /k:"MonProjet" \
                                /d:sonar.host.url=${SONAR_HOST_URL} \
                                /d:sonar.login=$SONAR_AUTH_TOKEN \
                                /d:sonar.cs.opencover.reportsPaths=src/Tests/TestResults/**/coverage.cobertura.xml \

                            dotnet build

                            ./.sonar/dotnet-sonarscanner end /d:sonar.login=$SONAR_AUTH_TOKEN
                        '''
                    }
                }
            }
        }
    }
}
