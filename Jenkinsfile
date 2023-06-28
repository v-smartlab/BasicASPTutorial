node {
  stage('SCM') {
    git branch: 'main', url: 'https://github.com/v-smartlab/BasicASPTutorial.git'
  }
  stage('SonarQube Analysis') {
    def scannerHome = 'D:/SonarQube/sonar-scanner-msbuild-5.13.0.66756-net5.0'
    withSonarQubeEnv(SonarQube.Net) {
      /*
      bat "dotnet ${scannerHome}\\SonarScanner.MSBuild.dll begin /k:\"BasicASPTutorial\""
      bat "dotnet build"
      bat "dotnet ${scannerHome}\\SonarScanner.MSBuild.dll end"
      */
      bat "dotnet ${scannerHome}\\SonarScanner.MSBuild.dll begin /k:\"BasicASPTutorial\""
      bat 'dotnet build'
      bat "${scannerHome}\\SonarScanner.MSBuild.dll end"
    }
  }
}
