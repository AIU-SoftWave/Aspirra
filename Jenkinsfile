pipeline {
    agent any

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        DOTNET_NOLOGO = '1'
        DOTNET_ROOT = '/usr/share/dotnet'
    }

    options {
        timestamps()
        disableConcurrentBuilds()
        buildDiscarder(logRotator(numToKeepStr: '10'))
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                sh 'dotnet restore Aspirra.slnx'
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build Aspirra.slnx --no-restore --configuration Release'
            }
        }

        stage('Test') {
            steps {
                sh '''
                    rm -rf "${WORKSPACE}/artifacts/coverage" "${WORKSPACE}/artifacts/junit"
                    dotnet test Aspirra.slnx --no-build --configuration Release \\
                        --settings coverlet.runsettings \\
                        --collect:"XPlat Code Coverage" \\
                        --results-directory "${WORKSPACE}/artifacts/coverage" \\
                        --logger "console;verbosity=minimal" \\
                        --logger "junit;LogFilePath=${WORKSPACE}/artifacts/junit/{assembly}-{framework}.xml"
                '''
            }
            post {
                always {
                    junit allowEmptyResults: true, testResults: 'artifacts/junit/**/*.xml'
                }
            }
        }

        stage('SonarQube') {
            steps {
                script {
                    def hasToken = false
                    try {
                        withCredentials([string(credentialsId: 'sonar-token', variable: 'SONAR_TOKEN')]) {
                            hasToken = true
                            sh '''
                                dotnet sonarscanner begin \\
                                    /k:"AIU-SoftWave_Aspirra" \\
                                    /o:"aboosalh" \\
                                    /d:sonar.host.url="https://sonarcloud.io" \\
                                    /d:sonar.token="$SONAR_TOKEN" \\
                                    /d:sonar.cs.opencover.reportsPaths="${WORKSPACE}/artifacts/coverage/**/coverage.opencover.xml"
                                dotnet build Aspirra.slnx --no-restore --configuration Release
                                dotnet sonarscanner end /d:sonar.token="$SONAR_TOKEN"
                            '''
                        }
                    } catch (e) {
                        hasToken = false
                        echo "Skipping SonarQube: the 'sonar-token' credential is not configured (${e.message})."
                    }

                    if (!hasToken) {
                        echo 'Skipping SonarQube analysis because no token is configured.'
                    }
                }
            }
        }

        stage('Archive') {
            steps {
                archiveArtifacts allowEmptyArchive: true, artifacts: 'artifacts/coverage/**, artifacts/junit/**'
            }
        }
    }

    post {
        always {
            echo "Pipeline result: ${currentBuild.result ?: 'SUCCESS'}"
        }
    }
}
