pipeline {
    agent any

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        DOTNET_NOLOGO = 'true'
        PATH = "/root/.dotnet/tools:${env.PATH}"
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore') {
            steps {
                sh 'dotnet restore'
            }
        }

        stage('SonarQube Analysis') {
            steps {
                withSonarQubeEnv('SonarQube') {
                    sh '''
                    dotnet tool install --global dotnet-sonarscanner || true

                    dotnet-sonarscanner begin \
                      /k:devopsplatform \
                      /d:sonar.host.url=http://sonarqube:9000 \
                      /d:sonar.login=$SONAR_TOKEN

                    dotnet build --no-restore

                    dotnet-sonarscanner end \
                      /d:sonar.login=$SONAR_TOKEN
                    '''
                }
            }
        }

        stage('Quality Gate') {
            steps {
                timeout(time: 1, unit: 'MINUTES') {
                    waitForQualityGate abortPipeline: true
                }
            }
        }
    }
}
