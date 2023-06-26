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
      bat "${scannerHome}\\SonarQube.Scanner.MSBuild.exe begin /k:myKey"
      bat 'MSBuild.exe /t:Rebuild'
      bat "${scannerHome}\\SonarQube.Scanner.MSBuild.exe end"
    }
  }
}
