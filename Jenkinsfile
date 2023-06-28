node {
  stage('SCM') {
    git branch: 'main', url: 'https://github.com/v-smartlab/BasicASPTutorial.git'
  }
  stage('SonarQube Analysis') {
    def scannerHome = tool 'SonarScanner for MSBuild'
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
