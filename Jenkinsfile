pipeline {
    agent any

    stages {
        stage('Tests') {
            agent {
                docker {
                    image 'mcr.microsoft.com/dotnet/sdk:8.0'
                    reuseNode true
                }
            }
            
            environment {
                DOTNET_CLI_HOME = "/tmp/dotnet_tools"  // Set the tools directory to a path Jenkins can access
            }

            steps {
                sh '''
                    echo "Running .NET tests"
                    dotnet test
                '''
            }
        }

        stage('Build') {
            agent {
                docker {
                    image 'mcr.microsoft.com/dotnet/sdk:8.0'
                    reuseNode true
                }
            }

            environment {
                DOTNET_CLI_HOME = "/tmp/dotnet_tools"
            }

            steps {
                sh '''
                    echo "Running .NET build"
                    dotnet build
                '''
            }
        }

        stage('Publish') {
            agent {
                docker {
                    image 'mcr.microsoft.com/dotnet/sdk:8.0'
                    reuseNode true
                }
            }
            
            environment {
                DOTNET_CLI_HOME = "/tmp/dotnet_tools"  // Set the tools directory to a path Jenkins can access
            }

            steps {
                sh '''
                    echo "Running .NET publish"
                    dotnet publish --configuration Release --output ./publish
                '''
            }
        }

        stage('Deploy application') {
            
            steps {
                echo "Deploying application ... "
            }
        }
    }
}