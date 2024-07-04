# Azure function in container
This is single endpoint and probably will not be used continuously so its endpoint will be deployed to HTTP triggered azure function in container. 

# Running locally
1. Install docker desktop latest version
2. Navigate to object root
3. Run 'docker build -t {tag} .'
4. Run 'docker run -it -p {port}:80 {tag}'
5. Try out using swagger http://localhost:{port}/api/swagger/ui or endpoint http://localhost:{port}/api/Fibonacci?number={number}
    
# CI/CD 
For CI/CD are used github actions. Pipeline code can be found workflow folder and infrastructure as a code in Iac folder. The main steps:
- Build and push container image to docker hub repository
- Deploy resources to azure cloud using arm templates
- Run azure function with latest image
  
There will three environments: staging, test and production. Each will have its own CI/CD pipelines and container image repository, secret stores. Staging pipeline will be auto triggered by the push to staging branch. Testing pipeline will triggered manually. Production pipeline will be trigger only by push to main branch

# Logging
For logging are used azure application insight. It is included in azure ARM deployment script in Iac folder.

# Scaling
Azure functions in container can be scaled up to 1000 instances as long as there's enough cores quota available. It is event driven and scale out automatically, even during periods of high load. Azure Functions infrastructure scales CPU and memory resources by adding more instances of the Functions host, based on the number of events that its functions are triggered usually it a concurrent request. Scaling could be configured with iac.
