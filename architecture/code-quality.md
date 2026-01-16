# Code Quality
 
Detect Code Quality and Code Security issues on the fly in Visual Studio

https://www.sonarlint.org/visualstudio/


**SonarQube**


| Herramienta       | Fortalezas                     | Debilidades                 |
|-------------------|--------------------------------|----------------------------|
| SonarQube         | Completo, maduro, multi-lenguaje | Configuración compleja     |
| ESLint/TSLint     | Rápido, específico JS/TS      | Solo JavaScript/TypeScript |
| Checkstyle        | Específico Java, extensible   | Solo Java                  |
| Codacy            | SaaS, fácil de usar           | Menos personalizable       |
| SonarCloud        | Cloud, sin infraestructura    | Pago por análisis          |


```
dotnet tool install --global dotnet-sonarscanner
``


# Revisiones

Cobertura de pruebas - code coverage

To get an understanding of code coverage, we have to talk about testing code. A test runs a piece of code and ensures that the system produces the expected behavior given some input. Some tests check to see if a database updates properly, others will make sure a UI component is available. But testing is so important because bugs are everywhere. Finding and fixing issues in code can take days of an engineer’s time. A bad deployment can cost a company thousands to millions of dollars before it can be fixed. Bugs can leave holes for malicious users to take advantage of vulnerable systems. As a user, you have undoubtedly run into a few software bugs in your own life. 

Code coverage is a testing technique that informs what code is tested and what is not tested. It is often represented as a percentage of the number of lines of code that are tested versus the entire codebase. 


Tools
Codecov 
- Source Code Coverage
- Multi Language, Multi CI/CD
- Status Checks
- GitHub Checks
https://about.codecov.io/