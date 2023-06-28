node {
  stage('SCM') {
    git branch: 'main', url: 'https://github.com/v-smartlab/BasicASPTutorial.git';
    echo "Code Checked-out Successfully!!"
  }
  stage('SonarQube Analysis') {
    //def scannerHome = 'D:/SonarQube/sonar-scanner-msbuild-5.13.0.66756-net5.0'
    withSonarQubeEnv(SonarQube.Net) {
      /*
      bat 'dotnet D:/SonarQube/sonar-scanner-msbuild-5.13.0.66756-net5.0/SonarScanner.MSBuild.dll begin /k:\"BasicASPTutorial\"'
      bat 'dotnet build'
      bat 'dotnet ${scannerHome}\\SonarScanner.MSBuild.dll end'
      */
      bat 'dotnet D:/SonarQube/sonar-scanner-msbuild-5.13.0.66756-net5.0/SonarScanner.MSBuild.dll begin /k:\"BasicASPTutorial\"'
      bat 'dotnet build'
      bat 'dotnet D:/SonarQube/sonar-scanner-msbuild-5.13.0.66756-net5.0/SonarScanner.MSBuild.dll end'
    }
  }
}
