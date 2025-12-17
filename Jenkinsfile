pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:9.0'
            args '-u root'
        }
    }

    environment {
        SONAR_TOKEN = credentials('sonar-token')
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        DOTNET_NOLOGO = 'true'
    }

    stages {
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
            export PATH="$PATH:/root/.dotnet/tools"

            dotnet-sonarscanner begin \
              /k:devopsplatform \
              /d:sonar.host.url=http://sonarqube:9000 \
              /d:sonar.login=$SONAR_TOKEN

            dotnet build

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
